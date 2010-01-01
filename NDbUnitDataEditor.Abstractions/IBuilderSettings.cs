using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NDbUnitDataEditor.Abstractions
{
    public interface IBuilderSettings
    {
        string TemplateFileFullPath { get; }
        string UserMetaDataFileFullPath { get; }
        string DatabaseTargetMappingFileFullPath { get; }
        string LanguageMappingFileFullPath { get; }
        string ConnectionString { get; }
        string DatabaseName { get; }
        string DatabaseDriver { get; }
        string DatabaseTargetType { get; }
        string DataSetName { get; }
        IEnumerable<string> TablesToProcess { get; }
    }
}
