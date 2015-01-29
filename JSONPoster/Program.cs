using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace JSONPoster
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create new options for the command line args processor from the Options Class
            Options options = new Options();

            CommandLine.Parser parser = new Parser();

            //Try to parse the args into the options variable
            if (parser.ParseArguments(args, options))
            {
                Dictionary<string, string> JSONData = new Dictionary<string, string>();
                JSONData.Add(options.ContentName, options.Content);

                if (options.Content2 != null && options.ContentName2 != null)
                {
                    JSONData.Add(options.ContentName2, options.Content2);
                }

                // consume Options type properties
                if (options.Verbose)
                {
                    Console.WriteLine("Sending...");

                    //Post the JSON data to the URL
                    JSONWebResponse response = PostJSON.ToUrl(options.url, JSONData);

                    if (response.StatusCode == "200")
                    {
                        Console.WriteLine("Success!");
                    }
                    else
                    {
                        Console.WriteLine("Error " + response);
                    }
                }
                else
                {
                    JSONWebResponse response = PostJSON.ToUrl(options.url, JSONData);
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
