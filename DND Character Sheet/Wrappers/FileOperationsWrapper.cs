﻿using System;
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

        public void JSONFileWriteAllText(string filePath, [CanBeNull] string contents);

        public void PDFFileWriteAllText(string filePath, [CanBeNull] string contents);

        public string FileReadAllText(string filePath);
    }

    public class FileOperationsWrapper : IFileOperationsWrapper
    {
        public bool FileExists(string filePath) 
            => File.Exists(filePath);

        public void JSONFileWriteAllText(string filePath, string contents) 
            => File.WriteAllText(filePath, contents);

        public void PDFFileWriteAllText(string filePath, [CanBeNull] string contents)
        {
            throw new NotImplementedException();
        }

        public string FileReadAllText(string filePath)
            => File.ReadAllText(filePath);
    }
}
