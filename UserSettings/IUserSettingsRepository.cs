using System;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NDbUnit.Utility
{
    public interface IUserSettingsRepository
    {
        string GetSetting(string key);
        UserSettingsRepository.UserSettingOperationResult RemoveSetting(string key);
        void SaveSetting(string key, string value);
    }
}
