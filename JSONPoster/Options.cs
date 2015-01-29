using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace JSONPoster
{
    //This is a class for the CommandLine Parser Package to use to take commands from the command line
    class Options
    {
          [Option('u', "URL", Required = true, HelpText = "URL to post your content to")]
          public string url { get; set; }

          [Option('t', "Type", DefaultValue = "2Elements", HelpText = "Type of input, either 2Elements or ElementArray. Defaults to 2Elements which allows you post 2 JSON Elements, ElementArray allows you to post mutliple Elements using two comma delimited lists.  See examples for more information.")]
          public string InputType { get; set; }

          [Option('x', "NameList", DefaultValue = "", HelpText = @"Pipe ""|"" delimited list of the names of JSON items in the same order as the ValueList and with the same number of items, ie ""FirstName|LastName|Age""")]
          public string NameList { get; set; }

          [Option('y', "ValueList", DefaultValue = "", HelpText = @"Pipe ""|"" delimited list of the values of JSON items in the same order as the NameList and have the same number of items, ie ""Marcus|Brodey|65""")]
          public string ValueList { get; set; }

          [Option('n', "ContentName", DefaultValue = "", HelpText = "Content Name field for the json")]
          public string ContentName { get; set; }

          [Option('c', "ContentValue", DefaultValue = "", HelpText = "Content value to send to the url")]
          public string Content { get; set; }

          [Option('a', "ContentName2", DefaultValue = "", HelpText = "A second Content Name field for the json")]
          public string ContentName2 { get; set; }

          [Option('b', "ContentValue2", DefaultValue = "", HelpText = "A second content value to send to the url")]
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
                  AdditionalNewLineAfterOption = false,
                  AddDashesToOption = true
              };
              help.AddPreOptionsLine("JSONPoster let's you post data to a URL in JSON format");
              help.AddPostOptionsLine("");
              help.AddPostOptionsLine("Examples:");
              help.AddPostOptionsLine(@"Posting to Slack using Type 2Elements:");
              help.AddPostOptionsLine(@"JSONPoster.exe -u ""https://hooks.slack.com/services/B0SFEB3/SDFJOMVS34/sdfsdfsd3fsd"" -n ""username"" -c ""MyPostBot"" -a ""text"" -b ""This is a post from the post bot""");
              help.AddPostOptionsLine("");
              help.AddPostOptionsLine(@"Example posting to a website using Type ElementArray:");
              help.AddPostOptionsLine(@"JSONPoster.exe -u ""https://mywebsite100.com/SomePostAPI"" -t ElementArray -x ""FirstName|LastName|Age"" -y ""Henry|Jones|60""");
              help.AddOptions(this);
              return help;
          }
    }
}
