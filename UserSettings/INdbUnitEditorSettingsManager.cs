using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace NDbUnit.Utility
{
    public interface INdbUnitEditorSettingsManager
    {
        void SaveSettings(NdbUnitEditorSettings settings, string settingsFileName);
        NdbUnitEditorSettings LoadSettings(string settingsFilePath);
    }
}
