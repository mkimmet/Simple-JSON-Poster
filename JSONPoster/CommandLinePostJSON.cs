using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;
using System.Net;

namespace JSONPoster
{
    public sealed class CommandLinePostJSON : PostJSON
    {
        /// <summary>
        /// This function takes a parser and options from the CommandLion Parser Nuget and the
        /// args sent by the user and tries to post the inputs as JSON to a website using
        /// the PostJSON class
        /// </summary>
        public static void PostOptions(Parser parser, Options options, string[] args)
        {
            //Try to parse the args into the options variable
            if (parser.ParseArguments(args, options))
            {
                Dictionary<string, string> JSONData = new Dictionary<string, string>();

                //Check to see if the Type is 2Elements of ElementArray
                // *************** TYPE: 2Elements ***************
                if (options.InputType == "2Elements")
                {
                    JSONData.Add(options.ContentName, options.Content);

                    if (options.Content2 != null && options.ContentName2 != null)
                    {
                        JSONData.Add(options.ContentName2, options.Content2);
                    }
                }
                // *************** TYPE: Element Array ***************
                else //If the input is an ElementArray Do This 
                {
                    string[] NameList = options.NameList.Split('|');
                    string[] ValueList = options.ValueList.Split('|');

                    if (NameList.Count() != ValueList.Count() || NameList.Count() == 0 || ValueList.Count() == 0)
                    {
                        //If they didn't enter valid args show the description
                        Console.WriteLine(options.GetUsage());
                        return;
                    }

                    for (int i = 0; i < NameList.Count(); i++)
                    {
                        JSONData.Add(NameList[i], ValueList[i]);
                    }
                }

                //*************** Send Data *******************
                // consume Options type properties
                if (options.Verbose)
                {
                    Console.WriteLine("Sending...");
                    Console.WriteLine("");

                    //Post the JSON data to the URL
                    JSONWebResponse response = PostJSON.DictionaryToUrl(options.url, JSONData);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("Sent Successfully!");
                        Console.WriteLine("");
                        if (response.Content != null)
                        {
                            Console.WriteLine("Data Returned:");
                            Console.WriteLine(response.Content);
                            Console.WriteLine("");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error Code: " + response.StatusCode.ToString());
                        Console.WriteLine("");

                        if (response.Content != null)
                        {
                            Console.WriteLine("Data Returned:");
                            Console.WriteLine(response.Content);
                            Console.WriteLine("");
                        }

                        if (response.ErrorDetails != null && response.ErrorDetails.Message != null)
                        {
                            Console.WriteLine("Details:");
                            Console.WriteLine(response.ErrorDetails.Message);
                            Console.WriteLine("");
                        }
                    }
                }
                else
                {
                    JSONWebResponse response = PostJSON.DictionaryToUrl(options.url, JSONData);
                }
            }
            else
            {
                //If they didn't enter valid args show the description
                Console.WriteLine(options.GetUsage());
            }
        }
    }
}
