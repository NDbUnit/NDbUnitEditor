using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace NDbUnit.Utility
{
    public class NdbUnitEditorSettingsManager : INdbUnitEditorSettingsManager
    {
        public void SaveSettings(NdbUnitEditorSettings settings, string settingsFileName)
        {

            XmlSerializer mySerializer = new XmlSerializer(settings.GetType());
            // To write to a file, create a StreamWriter object.
            StreamWriter myWriter = new StreamWriter(settingsFileName);
            mySerializer.Serialize(myWriter, settings);
            myWriter.Close();
        }

        public NdbUnitEditorSettings LoadSettings(string settingsFilePath)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(NdbUnitEditorSettings));
            FileStream myFileStream = new FileStream(settingsFilePath, FileMode.Open);
            // Call the Deserialize method and cast to the object type.
            NdbUnitEditorSettings settings = (NdbUnitEditorSettings)mySerializer.Deserialize(myFileStream);
            myFileStream.Close();
            return settings;
        }
    }
}
