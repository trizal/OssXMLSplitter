using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OSSXMLSplitter
{
    class Options
    {
        [Option('r', "read", Required = true,
            HelpText = "XML File from OSS to be processesd")]
        public string InputFile { get; set; }

        [Option('v', "vendor", Required = false,
            HelpText = "specifies the origin of OSS vendor, supported value: Ericsson, Huawei")]
        public string vendorId { get; set; }


        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }


    
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                if (File.Exists(options.InputFile))
                {
                    // TODO: check if the file is zipped


                    System.IO.StreamReader xmlStream = new System.IO.StreamReader(options.InputFile);
                    XMLStreamReader reader = new XMLStreamReader(options.InputFile);
                    reader.readXML(xmlStream);
                }
            }

            
            
        }
    }
}
