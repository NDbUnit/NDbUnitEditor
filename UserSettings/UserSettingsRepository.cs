using System;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NDbUnit.Utility
{
    /// <summary>
    /// Encapsulates interaction with config files to save and load application values.
    /// </summary>
    public class UserSettingsRepository : IUserSettingsRepository
    {
        /// <summary>
        /// Controls the type of config file to interact with
        /// </summary>
        public enum Config
        {
            /// <summary>
            /// the main app.config file
            /// </summary>
            ApplicationFile = 0,
            /// <summary>
            /// all users use the same config file
            /// </summary>
            SharedFile = 1,
            /// <summary>
            /// each user has their own config file
            /// </summary>
            PrivateFile = 2
        }

        /// <summary>
        /// Status Values returned from operations
        /// </summary>
        public enum UserSettingOperationResult
        {
            /// <summary>
            /// Operation completed successfully
            /// </summary>
            Success = 1,
            /// <summary>
            /// Operation did not not complete successfully
            /// </summary>
            Failure = 2
        }

        private string _applicationName;

        private string _companyName = "NDbUnit";

        private string _configFileName;

        private Config _configFileType;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSettings"/> class.
        /// </summary>
        /// <param name="configFileType">Type of the config file.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="applicationName">Name of the application.</param>
        public UserSettingsRepository(Config configFileType, string companyName, string applicationName)
        {
            _configFileType = configFileType;

            _companyName = companyName;
            _applicationName = applicationName;

            //setup the filename and location
            InitializeConfigFile();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSettings"/> class.
        /// </summary>
        /// <param name="configFileType">Type of the config file.</param>
        /// <param name="applicationName">Name of the application.</param>
        public UserSettingsRepository(Config configFileType, string applicationName)
        {
            _configFileType = configFileType;

            _applicationName = applicationName;

            //setup the filename and location
            InitializeConfigFile();
        }

        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value found in the config file for the requested key; null if the key is not found.</returns>
        public string GetSetting(string key)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(_configFileName);

            XmlNode Node = xd.DocumentElement.SelectSingleNode(String.Format("/configuration/appSettings/add[@key=\"{0}\"]", key));

            if ((Node != null))
            {
                return Node.Attributes.GetNamedItem("value").Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Removes the setting.
        /// </summary>
        /// <param name="key">The key of the setting to remove.</param>
        /// <returns>Success or Failure Result</returns>
        public UserSettingOperationResult RemoveSetting(string key)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(_configFileName);

            XmlNode Node = xd.DocumentElement.SelectSingleNode(String.Format("/configuration/appSettings/add[@key=\"{0}\"]", key));

            if ((Node != null))
            {
                XmlNode ParentNode = Node.ParentNode;
                ParentNode.RemoveChild(Node);

                xd.Save(_configFileName);
            }

            return UserSettingOperationResult.Success;
        }

        /// <summary>
        /// Saves the setting.
        /// </summary>
        /// <param name="key">The key of the setting to save.</param>
        /// <param name="value">The value of the key to save.</param>
        public void SaveSetting(string key, string value)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(_configFileName);

            XmlElement Node = (XmlElement)xd.DocumentElement.SelectSingleNode(String.Format("/configuration/appSettings/add[@key=\"{0}\"]", key));

            if ((Node != null))
            {
                //key found, set the value
                Node.Attributes.GetNamedItem("value").Value = value;
            }
            else
            {
                //key not found, create it
                Node = xd.CreateElement("add");
                Node.SetAttribute("key", key);
                Node.SetAttribute("value", value);

                //look for the appsettings node
                XmlNode Root = xd.DocumentElement.SelectSingleNode("/configuration/appSettings");

                //add the new child node (this key)
                if ((Root != null))
                {
                    Root.AppendChild(Node);
                }
                else
                {
                    try
                    {
                        //appsettings node didn't exist, add it before adding the new child
                        Root = xd.DocumentElement.SelectSingleNode("/configuration");
                        Root.AppendChild(xd.CreateElement("appSettings"));
                        Root = xd.DocumentElement.SelectSingleNode("/configuration/appSettings");
                        Root.AppendChild(Node);
                    }
                    catch (Exception ex)
                    {
                        //failed adding node, throw an error
                        throw new Exception("Could not set value", ex);
                    }
                }
            }

            xd.Save(_configFileName);
        }

        /// <summary>
        /// Initializes the config file.
        /// </summary>
        private void InitializeConfigFile()
        {
            StringBuilder sb = new StringBuilder();

            //build the path\filename depending on the location of the config file
            switch (_configFileType)
            {
                case Config.PrivateFile:
                    //each user has their own personal settings
                    //use "\documents and settings\username\application data" for the config file directory
                    sb.Append(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                    break;
                case Config.SharedFile:
                    //all users share the same settings
                    //use "\documents and settings\All Users\application data" for the config file directory
                    sb.Append(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));

                    break;
                case Config.ApplicationFile:
                    sb.Append(String.Format("{0}.config", Application.ExecutablePath));
                    break;
            }

            //add the Company name
            if (!(_configFileType == Config.ApplicationFile))
            {
                sb.Append("\\");
                sb.Append(_companyName);

                //create the directory if it isn't there
                if (!Directory.Exists(sb.ToString()))
                {
                    Directory.CreateDirectory(sb.ToString());
                }

                //add the Application name
                sb.Append("\\");
                sb.Append(_applicationName);

                //create the directory if it isn't there
                if (!Directory.Exists(sb.ToString()))
                {
                    Directory.CreateDirectory(sb.ToString());
                }

                //finish building the file name
                sb.Append("\\");
                sb.Append(_applicationName);
                sb.Append(".config");
            }

            _configFileName = sb.ToString(); //completed config filename

            //if the file doesn't exist, create a blank xml file
            if (!File.Exists(_configFileName))
            {
                using (StreamWriter fn = new StreamWriter(File.Open(_configFileName, FileMode.Create)))
                {
                    fn.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    fn.WriteLine("<configuration>");
                    fn.WriteLine("  <appSettings>");
                    fn.WriteLine("    <!--   User application and configured property settings go here.-->");
                    fn.WriteLine("    <!--   Example: <add key=\"settingName\" value=\"settingValue\"/> -->");
                    fn.WriteLine("  </appSettings>");
                    fn.WriteLine("</configuration>");

                    fn.Close();
                }
            }
        }

    }
}
