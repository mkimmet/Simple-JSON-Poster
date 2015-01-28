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
    public class PostToURL
    {
        /// <summary>This function converts a dictionary to JSON (Needs tweaking depending on what JSON you are expecting)</summary>
        /// <param name="dictionary"> Dictionary to Encode in JSON</param>
        /// <returns>JSON encoded Dictionary</returns>
        private static string DictionaryToJSON(Dictionary<String, String> dictionary)
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
        ///  URL Encodes a dictionary, This doesn't quite work the way I want.  It needs some help.
        /// </summary>
        /// <param name="dictionary"> Dictionary to Encode</param>
        /// <returns>URL encoded Dictionary</returns>

        private static Dictionary<string, string> CleanDictionary(Dictionary<string, string> dictionary)
        {
            Dictionary<string, string> cleandictionary = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> item in dictionary)
            {
                cleandictionary.Add(item.Key.Replace(@"""", @"\""").Replace(@"\", @"\\"), item.Value.Replace(@"""", @"\""").Replace(@"\", @"\\"));
            }
            return cleandictionary;
        }

        /// <summary>This function will post fields to a website and return the results back to you.</summary>
        /// <param name="url">url where to post the data</param>
        /// <param name="paramList">Dictionary<string,string> of key value to send to url</param>
        /// <param name="sendAsJSON">If set to true sends as JSON, otherwise sends as posted form fields</param>
        /// <param name="responseType">Defaults to "Code" which will return the HTTP response code
        /// any other value will return the content that is returned by the call </param> 
        public static string HttpPost(string url, Dictionary<string,string> paramList, bool sendAsJSON = false, string responseType = "Code")
        {
            HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            req.Method = "POST";

            //If you want to send as JSON
            if (sendAsJSON)
            {
                req.ContentType = "application/json";

                string jsondata = DictionaryToJSON(paramList);

                // Encode the parameters as content data:
                byte[] JSONContent = UTF8Encoding.UTF8.GetBytes(jsondata);
                req.ContentLength = JSONContent.Length;

                // Send the request:
                using (Stream post = req.GetRequestStream())
                {
                    post.Write(JSONContent, 0, JSONContent.Length);
                }

                // Pick up the response:
                string result = "Error";

                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(resp.GetResponseStream());
                    if (responseType == "Code")
                    {
                        result = resp.StatusCode.ToString();
                    }
                    else
                    {
                        result = reader.ReadToEnd();
                    }
                }

                return result;
            }

                //IF you want to send as POST
            else
            {
                req.ContentType = "application/x-www-form-urlencoded";
                // Build a string with all the params, properly encoded.
                // We assume that the arrays paramName and paramVal are
                // of equal length:
                StringBuilder paramListClean = new StringBuilder();

                foreach (KeyValuePair<string, string> item in paramList)
                {
                    paramListClean.Append(HttpUtility.UrlEncode(item.Key));
                    paramListClean.Append("=");
                    paramListClean.Append(HttpUtility.UrlEncode(item.Value));
                    paramListClean.Append("&");
                }

                // Encode the parameters as form data:
                byte[] formData = UTF8Encoding.UTF8.GetBytes(paramListClean.ToString());
                req.ContentLength = formData.Length;

                // Send the request:
                using (Stream post = req.GetRequestStream())
                {
                    post.Write(formData, 0, formData.Length);
                }

                // Pick up the response:
                string result = "Error";

                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(resp.GetResponseStream());
                    if (responseType == "Code")
                    {
                        result = resp.StatusCode.ToString();
                    }
                    else
                    {
                        result = reader.ReadToEnd();
                    }
                }

                return result;
            }
          
            
        }
    }
}
