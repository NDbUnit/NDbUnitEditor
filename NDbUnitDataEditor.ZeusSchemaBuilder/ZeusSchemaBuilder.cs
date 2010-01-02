using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDbUnitDataEditor.Abstractions;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace NDbUnitDataEditor.ZeusSchemaBuilder
{
    public class ZeusSchemaBuilder : ISchemaBuilder
    {
        private const string OUTPUT_DATASET_FILENAME = "GeneratedDataset.xsd";

        private const int PROCESS_TIMEOUT = 20000;

        private Folder _outputFolder;

        private string _pathToJobSettingsFile;

        private IBuilderSettings _settings;

        private IZeusJobSettingsFileBuilder _settingsBuilder;

        private string JOB_FILENAME = "JobSettings.xml";

        private string PATH_TO_ZEUS_EXE = @"Generator\Zeuscmd.exe";

        /// <summary>
        /// Initializes a new instance of the SchemaBuilder class.
        /// </summary>
        /// <param name="settingsBuilder"></param>
        public ZeusSchemaBuilder(IZeusJobSettingsFileBuilder settingsBuilder)
        {
            _settingsBuilder = settingsBuilder;
        }

        public DataSet GetSchema(IBuilderSettings settings)
        {
            _settings = settings;
            CreateOutputFolder();
            BuildJobSettingsFile();
            RunProcess();

            return DataSetFromXsd();
        }

        private void BuildJobSettingsFile()
        {
            new ZeusJobSettingsFileBuilder(_settings).BuildJobSettingsFile(_pathToJobSettingsFile);
        }

        private void CreateOutputFolder()
        {
            _outputFolder = new Folder();
            _pathToJobSettingsFile = Path.Combine(_outputFolder.Path, JOB_FILENAME);
            if (!Directory.Exists(_outputFolder.Path))
                Directory.CreateDirectory(_outputFolder.Path);
        }

        private ProcessStartInfo CreateProcessInfo()
        {
            string arguments = string.Format("-i {0}", _pathToJobSettingsFile);

            return new ProcessStartInfo(PATH_TO_ZEUS_EXE, arguments);
        }

        private DataSet DataSetFromXsd()
        {
            DataSet dataset = new DataSet();
            dataset.ReadXmlSchema(Path.Combine(_outputFolder.Path, OUTPUT_DATASET_FILENAME));
            return dataset;
        }

        private void RunProcess()
        {
            var info = CreateProcessInfo();

            Process process = Process.Start(info);

            process.WaitForExit(PROCESS_TIMEOUT);

            if (process.HasExited == false)
                if (process.Responding)
                    process.CloseMainWindow();
                else
                    process.Kill();

        }

    }
}
