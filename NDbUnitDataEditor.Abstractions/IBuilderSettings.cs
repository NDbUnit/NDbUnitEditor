using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NDbUnitDataEditor.Abstractions
{
    public interface IBuilderSettings
    {
        string TemplateFilePathname { get; }
        string UserMetaDataFileName { get; }
        string DatabaseTargetMappingFilename { get; }
        string LanguageMappingFilename { get; }
        string ConnectionString { get; }
        string DatabaseName { get; }
        string DatabaseDriver { get; }
        string DatabaseTargetType { get; }
        string DataSetName { get; }
        IEnumerable<string> TablesToProcess { get; }
    }
}
