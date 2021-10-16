using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DND_Character_Sheet.Annotations;

namespace DND_Character_Sheet.Wrappers
{
    public interface IFileOperationsWrapper
    {
        public bool FileExists(string filePath);

        public void FileWriteAllText(string filePath, [CanBeNull] string contents);

        public string FileReadAllText(string filePath);
    }

    public class FileOperationsWrapper : IFileOperationsWrapper
    {
        public bool FileExists(string filePath) 
            => File.Exists(filePath);

        public void FileWriteAllText(string filePath, string contents) 
            => File.WriteAllText(filePath, contents);

        public string FileReadAllText(string filePath) 
            => File.ReadAllText(filePath);
    }
}
