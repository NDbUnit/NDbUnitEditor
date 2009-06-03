using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDbUnit.Utility;

namespace UserSettingsTest
{
    class When_Using_User_Specific_Config : UserSettingsTestBase
    {
        public override void _Setup()
        {
            _userSettingsConfigType = UserSettings.Config.PrivateFile;
            _userSettings = new UserSettings(_userSettingsConfigType, TEST_COMPANY_NAME, TEST_APPLICATION_NAME);
        }
    }
}
