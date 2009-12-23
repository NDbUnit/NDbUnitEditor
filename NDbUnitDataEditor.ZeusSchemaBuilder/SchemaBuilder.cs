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
    public class SchemaBuilder : ISchemaBuilder
    {

        public DataSet GetSchema(IBuilderSettings settings)
        {
            return new ZeusSchemaBuilder(new ZeusJobSettingsFileBuilder(settings)).GetSchema(settings);
        }
    }
}
