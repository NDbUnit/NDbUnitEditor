using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDbUnitDataEditor.Abstractions;
using System.IO;

namespace NDbUnitDataEditor.ZeusSchemaBuilder
{
    public enum InvalidationType
    {
        EmptyOrNull,
        MissingProviderSetting
    }
}
