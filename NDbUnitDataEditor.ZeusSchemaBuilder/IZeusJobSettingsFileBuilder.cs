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
    public interface IZeusJobSettingsFileBuilder
    {
        void BuildJobSettingsFile(string outputFilePathname);
    }
}
