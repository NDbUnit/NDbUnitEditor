using System;
using NDbUnit.Utility;
using NDbUnitDataEditor.Commands;
using System.IO;

namespace NDbUnitDataEditor
{
	public interface IFileService
	{
		bool DirectoryExists(string path);
		bool FileExists(string filePath);
	}
    public class FileService : IFileService
	{
		public bool FileExists(string filePath)
		{
			return File.Exists(filePath);
		}

		public bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}
	}
}
