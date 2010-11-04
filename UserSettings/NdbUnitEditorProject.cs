using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace NDbUnit.Utility
{
    [Serializable]
    public class NdbUnitEditorProject
    {
        public string SchemaFilePath { get; set; }
        public string XMLDataFilePath { get; set; }
        public string DatabaseConnectionString { get; set; }
        public string DatabaseClientType { get; set; }
    }
}
