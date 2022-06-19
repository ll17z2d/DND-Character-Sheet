using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Serialization;

namespace DND_Character_Sheet.Wrappers
{
    public interface ISerializeCharacterWrapper
    {
        public void SaveCharacterToFileJSON(ICharacterModel character);

        public void SaveCharacterToFilePDF(ICharacterModel character);

        public ICharacterModel OpenCharacterFromFileJSON(string filePath);

        public ICharacterModel OpenCharacterFromFilePDF(string filePath);
    }

    public class SerializeCharacterWrapper : ISerializeCharacterWrapper
    {
        public void SaveCharacterToFileJSON(ICharacterModel character) 
            => SerializeCharacter.SaveCharacterToFileJSON(character, new JsonConvertWrapper(), new FileOperationsWrapper());

        public void SaveCharacterToFilePDF(ICharacterModel character)
            => SerializeCharacter.SaveCharacterToFilePDF(character, new SerializePDF());

        public ICharacterModel OpenCharacterFromFileJSON(string filePath) 
            => SerializeCharacter.OpenCharacterFromFileJSON(filePath, new JsonConvertWrapper(), new FileOperationsWrapper());

        public ICharacterModel OpenCharacterFromFilePDF(string filePath)
            => SerializeCharacter.OpenCharacterFromFilePDF(filePath, new SerializePDF());
    }
}
