using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OSSXMLSplitter
{
    class XMLStreamReader
    {
        private String xmlFileName;
        private String xmlFilePath;
        private System.IO.StreamWriter cellFile; 

        
        public XMLStreamReader(String xmlFile)
        {
            xmlFileName = Path.GetFileName(xmlFile);
            xmlFilePath = Path.GetDirectoryName(xmlFile);

            Console.WriteLine("Processing OSS XML File: " + xmlFileName);
            Console.WriteLine("Working folder would be: " + xmlFilePath);
    
        }

        public void readXML(System.IO.StreamReader input)  
        {
            XmlTextReader reader = new XmlTextReader(input);
            Boolean isUtranCellNode = false;
            String nodeName = null;


            while (reader.Read())
            {
                

                switch (reader.NodeType)
                { 
                    case XmlNodeType.Element:
                        if (reader.LocalName == "UtranCell") 
                        {
                            isUtranCellNode = true;
                            Console.WriteLine("Creating a file for cellID: " + reader.GetAttribute("id"));
                            cellFile = new StreamWriter(xmlFilePath + "\\" + reader.GetAttribute("id") + ".txt");
                        }

                        if (isUtranCellNode)
                        {
                            nodeName = reader.LocalName;
                            handleElement(reader);
                        }

                        break;

                    case XmlNodeType.Text:
                        if (isUtranCellNode)
                        {
                            
                            handleText(reader, nodeName);
                        }

                        
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.LocalName == "UtranCell")
                        {
                            isUtranCellNode = false;
                            cellFile.Flush();
                            cellFile.Close();
                            handleEndElement();
                        }
                        
                        

                        break;
                }
            }
        }

        private void handleElement(XmlTextReader node)
        {
            
            
        }

        private void handleText(XmlTextReader node, String nodeName)
        {
            cellFile.WriteLine(nodeName + ": " + node.Value );
        }

        private void handleEndElement()
        {

        }

    }
}
