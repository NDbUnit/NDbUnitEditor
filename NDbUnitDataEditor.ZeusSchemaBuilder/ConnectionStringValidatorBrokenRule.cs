using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDbUnitDataEditor.Abstractions;
using System.IO;

namespace NDbUnitDataEditor.ZeusSchemaBuilder
{
    public class ConnectionStringValidatorBrokenRule
    {
        public string Message { get; private set; }
        public InvalidationType Invalidation { get; private set; }

        /// <summary>
        /// Initializes a new instance of the ConnectionStringValidatorBrokenRule class.
        /// </summary>
        public ConnectionStringValidatorBrokenRule(string message, InvalidationType invalidation)
        {
            Message = message;
            Invalidation = invalidation;
        }
    }
}
