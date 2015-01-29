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
    class Program
    {
        static void Main(string[] args)
        {

            //Create new options for the command line args processor from the Options Class
            Options options = new Options();
            CommandLine.Parser parser = new Parser();
            
            //Try to convert the args to JSON and post it
            CommandLinePostJSON.PostOptions(parser, options, args);

        }
    }
}
