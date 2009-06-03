using System;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NDbUnit.Utility
{
    public interface IUserSettings
    {
        string GetSetting(string key);
        UserSettings.UserSettingOperationResult RemoveSetting(string key);
        void SaveSetting(string key, string value);
    }
}
