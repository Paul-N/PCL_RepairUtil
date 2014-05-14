using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace PCL_RepairUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length < 1)
            {
                Console.WriteLine("You have to provide path to PCL installation");
                return;
            }

            var pathToPcl = args[0] + "\\v4.0\\Profile\\";

            if (!Directory.Exists(pathToPcl))
            {
                Console.WriteLine("Wrong path to PCL installation");
                return;
            }

            foreach (var profile in Directory.GetDirectories(pathToPcl))
            {
                var suppFrmw = profile + "\\SupportedFrameworks\\";

                if (Directory.Exists(suppFrmw))
                {
                    foreach (var xmlFile in Directory.GetFiles(suppFrmw).Where(f => f.EndsWith(".xml")))
                    {
                        var xml = XDocument.Load(xmlFile);

                        var attr = xml.Elements().ElementAt(0).Attributes().SingleOrDefault(a => a.Name == "MaximumVisualStudioVersion");

                        if (attr != null)
                        {
                            attr.Value = "12.0";
                            xml.Save(xmlFile);
                        }
                    }
                }
            }
        }
    }
}
