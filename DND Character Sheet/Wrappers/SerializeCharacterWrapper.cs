using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Serialization;

namespace DND_Character_Sheet.Wrappers
{
    public interface IFileOperationsWrapper
    {
        public void SaveCharacterToFile(ICharacterModel character);

        public ICharacterModel OpenCharacterFromFile(string filePath);

        public bool FileExists(string filePath);
    }

    public class FileOperationsWrapper : IFileOperationsWrapper
    {
        public void SaveCharacterToFile(ICharacterModel character) 
            => SerializeCharacter.SaveCharacterToFile(character);

        public ICharacterModel OpenCharacterFromFile(string filePath) 
            => SerializeCharacter.OpenCharacterFromFile(filePath);

        public bool FileExists(string filePath) => File.Exists(filePath);
    }
}
