using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using NDbUnit.Utility;
using System.IO;
using System.Diagnostics;

namespace UserSettingsTest
{
    [TestFixture]
    public abstract class UserSettingsTestBase
    {
        protected const string TEST_APPLICATION_NAME = "The Application Name";

        protected const string TEST_COMPANY_NAME = "The Test Company";

        private const string TEST_KEY = "TheKey";

        private const string TEST_VALUE = "The Value";

        protected UserSettingsRepository _userSettings;

        protected UserSettingsRepository.Config _userSettingsConfigType;

        [SetUp]
        public abstract void _Setup();

        [FixtureSetUp]
        public void _TestFixtureSetUp()
        {
            DeleteAllUsersFileAndFolders(TEST_COMPANY_NAME);
            DeleteUserFileAndFolders(TEST_COMPANY_NAME);
        }

        [Test]
        public void Can_Remove_Value()
        {
            _userSettings.SaveSetting(TEST_KEY, TEST_VALUE);

            _userSettings.RemoveSetting(TEST_KEY);

            Assert.IsNull(_userSettings.GetSetting(TEST_KEY));

        }

        [Test]
        public void Can_Return_Initial_Value()
        {
            _userSettings.SaveSetting(TEST_KEY, TEST_VALUE);

            Assert.AreEqual(TEST_VALUE, _userSettings.GetSetting(TEST_KEY));
        }

        [Test]
        public void Can_Return_NULL_If_Value_Not_Found()
        {
            _userSettings.SaveSetting(TEST_KEY, TEST_VALUE);

            Assert.IsNull(_userSettings.GetSetting("Not The Key"));
        }

        [Test]
        public void Can_Run_All_Tests_With_Default_Company_Name()
        {
            const string DEFAULT_COMPANY_NAME = "NDbUnit";

            DeleteAllUsersFileAndFolders(DEFAULT_COMPANY_NAME);
            DeleteUserFileAndFolders(DEFAULT_COMPANY_NAME);

            _userSettings = new UserSettingsRepository(_userSettingsConfigType, TEST_APPLICATION_NAME);

            Can_Remove_Value();
            Can_Return_Initial_Value();
            Can_Return_NULL_If_Value_Not_Found();
        }

        private void DeleteAllUsersFileAndFolders(string companyName)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), companyName);

            if (Directory.Exists(path))
            {
                //Directory.Delete(path, true);
                Debug.WriteLine(string.Format("Deleting Path {0}", path));
            }

        }

        private void DeleteUserFileAndFolders(string companyName)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), companyName);

            if (Directory.Exists(path))
            {
                //Directory.Delete(path, true);
                Debug.WriteLine(string.Format("Deleting Path {0}", path));
            }
        }

    }
}
