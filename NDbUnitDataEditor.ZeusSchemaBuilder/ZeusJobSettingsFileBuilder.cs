using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDbUnitDataEditor.Abstractions;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace NDbUnitDataEditor.ZeusSchemaBuilder
{
    public class ZeusJobSettingsFileBuilder : IZeusJobSettingsFileBuilder
    {
        private const string OUTPUTFILENAME = "GeneratedDataSet.xsd";

        private string _outputFileNameFullPath;

        private string _outputFolderPath;

        IBuilderSettings _settings;

        /// <summary>
        /// Initializes a new instance of the ZeusJobSettingsFileBuilder class.
        /// </summary>
        /// <param name="settings"></param>
        public ZeusJobSettingsFileBuilder(IBuilderSettings settings)
        {
            _settings = settings;
        }

        public void BuildJobSettingsFile(string outputFilePathname)
        {
            _outputFileNameFullPath = Path.GetFullPath(outputFilePathname);
            _outputFolderPath = Path.GetDirectoryName(_outputFileNameFullPath);
            var doc = new XmlDocument();
            var writer = XmlWriter.Create(outputFilePathname);
            //writer.Formatting = Formatting.Indented;
            writer.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
            writer.WriteStartElement("obj");
            writer.WriteAttributeString("name", string.Empty);
            writer.WriteAttributeString("uid", "28754f2a-da2d-45ec-87f0-2d461478f2b8");
            writer.WriteAttributeString("path", _settings.TemplateFileFullPath);
            writer.Close();

            doc.Load(outputFilePathname);

            XmlNode root = doc.DocumentElement;
            XmlElement items = doc.CreateElement("items");
            root.AppendChild(items);

            BuildAllItems(doc, items);

            doc.Save(outputFilePathname);
        }

        private void BuildAllItems(XmlDocument doc, XmlElement items)
        {
            BuildItem(doc, items, "__dbConnectionString", "System.String", _settings.ConnectionString);
            BuildItem(doc, items, "__dbDriver", "System.String", _settings.DatabaseDriver);
            BuildItem(doc, items, "__dbLanguageMappingFileName", "System.String", _settings.LanguageMappingFileFullPath);
            BuildItem(doc, items, "__dbTarget", "System.String", _settings.DatabaseTargetType);
            BuildItem(doc, items, "__dbTargetMappingFileName", "System.String", _settings.DatabaseTargetMappingFileFullPath);
            BuildItem(doc, items, "__defaultTemplatePath", "System.String", Path.GetFullPath("Generator"));
            BuildItem(doc, items, "__language", "System.String", "C#");
            BuildItem(doc, items, "__domainOverride", "System.Boolean", "True");
            BuildItem(doc, items, "__userMetaDataFileName", "System.String", _settings.UserMetaDataFileFullPath);
            BuildItem(doc, items, "__version", "System.String", "1.3.0.0");
            BuildItem(doc, items, "cmbDatabase", "System.String", _settings.DatabaseName);
            BuildItem(doc, items, "dbConnectionString", "System.Boolean", "True");
            BuildItem(doc, items, "dbDriver", "System.String", _settings.DatabaseDriver);
            BuildItem(doc, items, "lstTables", "System.Collections.ArrayList", FormatTablesAsDelimitedString(_settings.TablesToProcess));
            BuildItem(doc, items, "txtDatasetName", "System.String", _settings.DataSetName);
            BuildItem(doc, items, "txtPath", "System.String", Path.Combine(_outputFolderPath, OUTPUTFILENAME));

        }

        private void BuildItem(XmlDocument document, XmlNode parent, string name, string type, string value)
        {
            XmlElement item = document.CreateElement("item");

            item.SetAttribute("name", name);
            item.SetAttribute("type", type);
            item.SetAttribute("val", value);

            parent.AppendChild(item);
        }

        private string FormatTablesAsDelimitedString(IEnumerable<string> tablesToProcess)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string table in tablesToProcess)
            {
                sb.Append(string.Format("System.String|H|{0};", table));
            }

            return sb.ToString();
        }

    }
}