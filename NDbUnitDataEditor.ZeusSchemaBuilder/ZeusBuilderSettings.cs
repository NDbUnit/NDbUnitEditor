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
            DataSetName = dataSetName;
            TablesToProcess = tablesToProcess;

        }

        public string ConnectionString { get; private set; }

        public string DatabaseDriver
        {
            get { return "SQL"; }
        }

        public string DatabaseName { get; private set; }

        public string DatabaseTargetMappingFileFullPath
        {
            get { return Path.GetFullPath(@"Generator\DBTargets.xml"); }
        }

        public string DatabaseTargetType
        {
            get { return "SqlClient"; }
        }

        public string DataSetName { get; private set; }

        public string LanguageMappingFileFullPath
        {
            get { return Path.GetFullPath(@"Generator\Languages.xml"); }
        }

        public IEnumerable<string> TablesToProcess { get; private set; }

        public string TemplateFileFullPath
        {
            get { return Path.GetFullPath(@"Generator\AnnotationsXSD.zeus"); }
        }

        public string UserMetaDataFileFullPath
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
