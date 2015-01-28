using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace JSONPoster
{
    //This is a class for the CommandLine Package to use to take commands from the command line
    class Options
    {
          [Option('u', "URL", Required = true, HelpText = "URL to post your content to")]
          public string url { get; set; }

          [Option('n', "ContentName", Required = true, HelpText = "Content Name field for the json")]
          public string ContentName { get; set; }

          [Option('c', "Content", Required = true, HelpText = "Content to send to the url")]
          public string Content { get; set; }

          [Option('a', "ContentName2", DefaultValue = "", HelpText = "A second Content Name field for the json")]
          public string ContentName2 { get; set; }

          [Option('b', "Content2", DefaultValue = "", HelpText = "A second Content to send to the url")]
          public string Content2 { get; set; }

          [Option('m', "Method", DefaultValue = "Post", HelpText = "Not Implemented - Will eventually let you Post or Get your data")]
          public string MethodType { get; set; }

          [Option('h', "Headers", DefaultValue = "None", HelpText = "Not Implemented - Will allow you to change the headers")]
          public string Header { get; set; }

          [Option('v', null, HelpText = "Print details during execution.")]
          public bool Verbose { get; set; }

          [HelpOption]
          public string GetUsage()
          {
            // this without using CommandLine.Text
              var help = new HelpText
              {
                  Heading = new HeadingInfo("JSONPoster", "v0.1"),
                  Copyright = new CopyrightInfo("University of Notre Dame - Mark Kimmet", 2015),
                  AdditionalNewLineAfterOption = false,
                  AddDashesToOption = true
              };
              help.AddPreOptionsLine("JSONPoster let's you post data to a URL in JSON format");
              help.AddPreOptionsLine(@"Example posting to Slack: JSONPoster.exe -u ""https://hooks.slack.com/services/B0SFEB3/SDFJOMVS34/sdfsdfsd3fsd"" -n ""username"" -c ""MyPostBot"" -a ""text"" -b ""This is a post from the post bot""");
              help.AddOptions(this);
              return help;
          }
    }
}
