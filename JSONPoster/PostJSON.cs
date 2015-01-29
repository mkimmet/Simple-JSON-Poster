using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.IO;

namespace JSONPoster
{
    public class PostJSON
    {
        /// <summary>This function converts a dictionary to JSON (Needs tweaking depending on what JSON you are expecting)</summary>
        /// <param name="dictionary"> Dictionary to Encode in JSON</param>
        /// <returns>JSON encoded Dictionary</returns>
        protected static string DictionaryToJSON(Dictionary<String, String> dictionary)
        {
            List<string> data = new List<string>();
            int result;

            //check to see if the data is a number or true/false, if it is then don't put quotes around it
            //depending on your JSON formatting needs you might need to change this up
            foreach (var item in dictionary)
            {
                if (int.TryParse(item.Value, out result) || item.Value.ToLower() == "true" || item.Value.ToLower() == "false")
                {
                    data.Add(string.Format(@"""{0}"":{1}", item.Key, item.Value));
                }
                else
                {
                    data.Add(string.Format(@"""{0}"":""{1}""", item.Key, item.Value));
                }
            }

            return "{" + string.Join(",", data) + "}";
        }

        /// <summary>
        ///  This function escapes characters not suitable for JSON, although it's not working right at this point.
        /// </summary>
        /// <param name="dictionary"> Dictionary to escape</param>
        /// <returns>Escaped Dictionary</returns>

        protected static Dictionary<string, string> CleanDictionary(Dictionary<string, string> dictionary)
        {
            Dictionary<string, string> cleandictionary = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> item in dictionary)
            {
                cleandictionary.Add(item.Key.Replace(@"""", @"\""").Replace(@"\", @"\\"), item.Value.Replace(@"""", @"\""").Replace(@"\", @"\\"));
            }
            return cleandictionary;
        }

        /// <summary>This function converts a dictionary to a JSON string and posts it as content to a website
        /// and then returns a JSONWebResponse object to the user with the content, statuscode, and any Exception.</summary>
        /// <param name="url">url where to post the data</param>
        /// <param name="dictionay">Dictionary<string,string> of key value to send to url</param>
        /// <param name="sendAsJSON">If set to true sends as JSON, otherwise sends as posted form fields</param>
        /// <returns>A JSONWebResponse object</returns>
        public static JSONWebResponse DictionaryToUrl(string url, Dictionary<string, string> dictionay)
        {
            try
            {
                //Set up the web request
                HttpWebRequest request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/json";

                //Convert the Dictionary to JSON
                string jsondata = DictionaryToJSON(dictionay);

                // Encode the parameters as content data, and set the content length
                byte[] JSONContent = UTF8Encoding.UTF8.GetBytes(jsondata);
                request.ContentLength = JSONContent.Length;

                // Send the request:
                using (Stream post = request.GetRequestStream())
                {
                    post.Write(JSONContent, 0, JSONContent.Length);
                }

                // Pick up the response
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    
                    JSONWebResponse webresponse = new JSONWebResponse()
                    {
                        StatusCode = response.StatusCode,
                        Content = reader.ReadToEnd(),
                        ErrorDetails = null
                    };

                    return webresponse;
                }
            }
            catch (Exception ex)
            {
                JSONWebResponse response = new JSONWebResponse()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = "Error Retrieving Data",
                    ErrorDetails = ex
                };

                return response;
            }
        }
    }
}
