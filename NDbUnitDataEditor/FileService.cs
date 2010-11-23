using System;
using NDbUnit.Utility;
using NDbUnitDataEditor.Commands;
using System.IO;

namespace NDbUnitDataEditor
{
	public interface IFileService
	{
		bool FileExists(string filePath);
	}
    public class FileService : IFileService
	{
		public bool FileExists(string filePath)
		{
			return File.Exists(filePath);
		}
	}
}
