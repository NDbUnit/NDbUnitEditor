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
        private Folder _outputFolder;

        private string _pathToJobSettingsFile;

        private IZeusJobSettingsFileBuilder _settingsBuilder;

        private IBuilderSettings _settings;

        private string JOBFILENAME = "JobSettings.xml";

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
            _pathToJobSettingsFile = Path.Combine(_outputFolder.Path, JOBFILENAME);
        }

        private ProcessStartInfo CreateProcessInfo()
        {
            string arguments = string.Format("-i {0}", _pathToJobSettingsFile);

            return new ProcessStartInfo(PATH_TO_ZEUS_EXE, arguments);
        }

        private DataSet DataSetFromXsd()
        {
            throw new NotImplementedException();
        }

        private void RunProcess()
        {
            var process = CreateProcessInfo();
            Process.Start(process);
        }

    }
}
