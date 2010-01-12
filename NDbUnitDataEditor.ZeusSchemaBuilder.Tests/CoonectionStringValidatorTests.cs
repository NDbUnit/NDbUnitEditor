using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

namespace NDbUnitDataEditor.ZeusSchemaBuilder.Tests
{
    [TestFixture]
    public class CoonectionStringValidatorTests
    {
        [TestFixture]
        public class When_ConnectionString_Does_Not_Contain_Proper_Provider_Type
        {
            private string _connectionStringWithNoProvider;

            private ConnectionStringValidator _validator;

            [SetUp]
            public void _Setup()
            {
                _connectionStringWithNoProvider = "Data Source=local;user id=sa;password=password";
                _validator = new ConnectionStringValidator();
            }

            [Test]
            public void Broken_Rules_Contains_Expected_InvalidationType()
            {
                var brokenRules = _validator.Validate(_connectionStringWithNoProvider);
                Assert.AreEqual(1, brokenRules.Where(br => br.Invalidation == InvalidationType.MissingProviderSetting).Count());
            }

            [Test]
            public void Validate_Can_Report_InValid()
            {
                Assert.IsFalse(_validator.IsValid(_connectionStringWithNoProvider));
            }

        }

        [TestFixture]
        public class When_ConnectionString_Is_Null_Or_Empty
        {

            private ConnectionStringValidator _validator;

            [SetUp]
            public void _Setup()
            {
                _validator = new ConnectionStringValidator();
            }

            [Test]
            [Row(null)]
            [Row("")]
            public void Broken_Rules_Contains_Expected_InvalidationType(string connectionString)
            {
                var brokenRules = _validator.Validate(connectionString);
                Assert.AreEqual(1, brokenRules.Where(br => br.Invalidation == InvalidationType.EmptyOrNull).Count());
            }

            [Test]
            [Row(null)]
            [Row("")]
            public void Validate_Can_Report_InValid(string connectionString)
            {
                Assert.IsFalse(_validator.IsValid(connectionString));
            }

        }

        [TestFixture]
        public class When_ConnectionString_Contains_Proper_Provider_Type
        {
            private ConnectionStringValidator _validator;

            private string _validConnString;

            [SetUp]
            public void _Setup()
            {
                _validConnString = "Provider=SQLOLEDB;Data Source=local;user id=sa;password=password";
                _validator = new ConnectionStringValidator();
            }

            [Test]
            public void Broken_Rules_Are_Empty()
            {
                Assert.IsEmpty(_validator.Validate(_validConnString));

            }

            [Test]
            public void Validate_Can_Report_Valid()
            {
                Assert.IsTrue(_validator.IsValid(_validConnString));
            }

        }
    }
}
