using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.IO;

namespace NDbUnitDataEditor.ZeusSchemaBuilder.Tests
{
    public class FolderTests
    {
        [TestFixture]
        public class When_Creating_Folder_And_Requesting_Temp_Root
        {
            private string _folderPath;

            [TearDown]
            public void _TearDown()
            {
                if (Directory.Exists(_folderPath))
                    Directory.Delete(_folderPath, true);
            }

            [Test]
            public void Folder_Is_Constructed_In_Temp_Folder()
            {
                var folder = new Folder();
                folder.Create();
                Assert.IsTrue(folder.Path.StartsWith(Path.GetTempPath()));

                _folderPath = folder.Path;
            }
        }

        [TestFixture]
        public class When_Creating_Folder_With_Specific_Path
        {
            private const string FOLDER_TO_CREATE = @"..\..\TestingFolderRoot\testfolder1";

            [SetUp]
            public void _Setup()
            {
                if (Directory.Exists(FOLDER_TO_CREATE))
                    Directory.Delete(FOLDER_TO_CREATE, true);

                Assert.IsFalse(Directory.Exists(FOLDER_TO_CREATE), string.Format("Test Precondition not satisfied: directory {0} should not exist before test run!", FOLDER_TO_CREATE));
            }

            [Test]
            public void Folder_Is_Created_In_Specific_Location()
            {
                var folder = new Folder(FOLDER_TO_CREATE);
                folder.Create();

                Assert.IsTrue(Directory.Exists(FOLDER_TO_CREATE));

            }

        }

        [TestFixture]
        public class When_Creating_New_Folder_Instance
        {
            private const string FOLDER_NAME = @"..\..\TestingFolderRoot\testfolder1";

            [Test]
            public void Path_Is_Set_To_FullyQualified_Path()
            {
                var folder = new Folder(FOLDER_NAME);

                Assert.AreEqual(Path.GetFullPath(FOLDER_NAME), folder.Path);
            }

        }

        [TestFixture]
        public class When_Deleting_Folder
        {
            private const string FOLDER_TO_DELETE = @"..\..\TestingFolderRoot\testfolder1";

            [SetUp]
            public void _Setup()
            {
                if (!Directory.Exists(FOLDER_TO_DELETE))
                    Directory.CreateDirectory(FOLDER_TO_DELETE);

                Assert.IsTrue(Directory.Exists(FOLDER_TO_DELETE), string.Format("Test Precondition not satisfied: directory {0} should exist before test run!", FOLDER_TO_DELETE));
            }

            [Test]
            public void Folder_Is_Removed()
            {
                var folder = new Folder(FOLDER_TO_DELETE);
                folder.Delete();

                Assert.IsFalse(Directory.Exists(FOLDER_TO_DELETE));
            }
        }
    }
}
