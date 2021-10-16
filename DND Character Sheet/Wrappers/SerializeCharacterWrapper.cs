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
        public void SaveCharacterToFile(ICharacterModel character);

        public ICharacterModel OpenCharacterFromFile(string filePath);
    }

    public class SerializeCharacterWrapper : ISerializeCharacterWrapper
    {
        public void SaveCharacterToFile(ICharacterModel character) 
            => SerializeCharacter.SaveCharacterToFile(character, new JsonConvertWrapper(), new FileOperationsWrapper());

        public ICharacterModel OpenCharacterFromFile(string filePath) 
            => SerializeCharacter.OpenCharacterFromFile(filePath, new JsonConvertWrapper(), new FileOperationsWrapper());
    }
}
