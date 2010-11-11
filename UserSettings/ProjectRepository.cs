using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace NDbUnit.Utility
{


    public class ProjectRepository : IProjectRepository
    {
        public void SaveProject(NdbUnitEditorProject settings, string settingsFileName)
        {

            XmlSerializer mySerializer = new XmlSerializer(settings.GetType());
            // To write to a file, create a StreamWriter object.
            StreamWriter myWriter = new StreamWriter(settingsFileName);
            mySerializer.Serialize(myWriter, settings);
            myWriter.Close();
        }

        public NdbUnitEditorProject LoadProject(string settingsFilePath)
        {
			FileStream myFileStream=null;
			try
			{
				XmlSerializer mySerializer = new XmlSerializer(typeof(NdbUnitEditorProject));
				myFileStream = new FileStream(settingsFilePath, FileMode.Open);
				// Call the Deserialize method and cast to the object type.
				NdbUnitEditorProject settings = (NdbUnitEditorProject)mySerializer.Deserialize(myFileStream);
				return settings;
			}
			finally
			{
				if(myFileStream!=null)
					myFileStream.Close();
			}
            
        }
    }
}
