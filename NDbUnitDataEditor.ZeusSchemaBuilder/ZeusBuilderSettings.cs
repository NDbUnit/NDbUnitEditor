using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDbUnitDataEditor.Abstractions;
using System.IO;

namespace NDbUnitDataEditor.ZeusSchemaBuilder
{

    public class ZeusBuilderSettings : IBuilderSettings
    {
        /// <summary>
        /// Initializes a new instance of the BuilderSettings class.
        /// </summary>
        public ZeusBuilderSettings(string connectionString, string databaseName, string databaseTargetType, string dataSetName, IEnumerable<string> tablesToProcess)
        {
            ValidateConnectionString(connectionString);
            ValidateDatabaseName(databaseName);
            ValidateDataSetName(dataSetName);
            ValidateDatabaseTargetType(databaseTargetType);
            ValidateTablesToProcess(tablesToProcess);

            ConnectionString = connectionString;
            DatabaseName = databaseName;
            DatabaseTargetType = databaseTargetType;
            DataSetName = dataSetName;
            TablesToProcess = tablesToProcess;

        }

        public string ConnectionString { get; private set; }

        public string DatabaseDriver
        {
            get { return "SQL"; }
        }

        public string DatabaseName { get; private set; }

        public string DatabaseTargetMappingFilename
        {
            get { return Path.GetFullPath(@"Generator\DBTargets.xml"); }
        }

        public string DatabaseTargetType { get; private set; }

        public string DataSetName { get; private set; }

        public string LanguageMappingFilename
        {
            get { return Path.GetFullPath(@"Generator\Languages.xml"); }
        }

        public IEnumerable<string> TablesToProcess { get; private set; }

        public string TemplateFilePathname
        {
            get { return Path.GetFullPath(@"Generator\AnnotationsXSD.zeus"); }
        }

        public string UserMetaDataFileName
        {
            get { return Path.GetFullPath(@"Generator\UserMetaData.xml"); }
        }

        private void ValidateConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connectionString cannot be null or empty!");
        }

        private void ValidateDatabaseName(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
                throw new ArgumentException("databaseName cannot be null or empty!");
        }

        private void ValidateDatabaseTargetType(string databaseTargetType)
        {
            if (string.IsNullOrEmpty(databaseTargetType))
                throw new ArgumentException("databaseTargetType cannot be null or empty!");
        }

        private void ValidateDataSetName(string dataSetName)
        {
            if (string.IsNullOrEmpty(dataSetName))
                throw new ArgumentException("dataSetName cannot be null or empty!");
        }

        private void ValidateTablesToProcess(IEnumerable<string> tablesToProcess)
        {
            if (tablesToProcess == null || tablesToProcess.Count() == 0)
                throw new ArgumentException("tablesToProcess cannot be null or empty!");
        }

    }
}
