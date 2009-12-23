using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NDbUnitDataEditor.ZeusSchemaBuilder
{
    public class Folder
    {
        private string _folderPath;

        /// <summary>
        /// Initializes a new instance of the Folder class.
        /// </summary>
        public Folder()
        {
            _folderPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Initializes a new instance of the Folder class.
        /// </summary>
        /// <param name="folderPath"></param>
        public Folder(string folderPath)
        {
            _folderPath = folderPath;
        }

        public string Path
        {
            get
            {
                return System.IO.Path.GetFullPath(_folderPath);
            }
        }

        public bool Create(bool safeOperation)
        {
            try
            {
                if (!Directory.Exists(Path))
                    _folderPath = Directory.CreateDirectory(Path).FullName;
            }
            catch (Exception)
            {
                if (safeOperation)
                    return false;
                else
                    throw;
            }

            return true;
        }

        public void Create()
        {
            Create(false);
        }

        public bool Delete(bool recursive, bool safeOperation)
        {
            try
            {
                if (Directory.Exists(Path))
                    Directory.Delete(Path, recursive);
            }
            catch (Exception)
            {
                if (safeOperation)
                    return false;
                else
                    throw;
            }

            return true;
        }

        public void Delete(bool recursive)
        {
            Delete(recursive, false);
        }

        public void Delete()
        {
            Delete(false, false);
        }

    }
}
