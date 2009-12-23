using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NDbUnitDataEditor.Abstractions
{
    public interface ISchemaBuilder
    {
        DataSet GetSchema(IBuilderSettings settings);
    }
}
