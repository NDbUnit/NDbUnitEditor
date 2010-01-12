using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDbUnitDataEditor.Abstractions;
using System.IO;

namespace NDbUnitDataEditor.ZeusSchemaBuilder
{
    public class ConnectionStringValidator
    {
        private bool ContainesRequiredProviderSetting(string connectionString)
        {
            var elements = connectionString.Split(new char[] { char.Parse(";") }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i].ToLowerInvariant() == "provider=sqloledb")
                    return true;
            }

            return false;
        }

        public List<ConnectionStringValidatorBrokenRule> Validate(string connectionString)
        {
            var brokenRules = new List<ConnectionStringValidatorBrokenRule>();

            if (string.IsNullOrEmpty(connectionString))
                brokenRules.Add(new ConnectionStringValidatorBrokenRule("Connection String cannot be null or Empty!", InvalidationType.EmptyOrNull));
            else
                if (!ContainesRequiredProviderSetting(connectionString))
                    brokenRules.Add(new ConnectionStringValidatorBrokenRule("Connection String must contain the provider setting 'provider=sqloledb'!", InvalidationType.MissingProviderSetting));

            return brokenRules;
        }


        public bool IsValid(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                return false;

            if (!ContainesRequiredProviderSetting(connectionString))
                return false;

            return true;
        }

    }
}
