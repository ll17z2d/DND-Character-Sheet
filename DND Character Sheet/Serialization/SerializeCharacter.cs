using System.IO;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Wrappers;
using Newtonsoft.Json;

namespace DND_Character_Sheet.Serialization
{
    public static class SerializeCharacter
    {
        public static void SaveCharacterToFile(ICharacterModel character, IJsonConvertWrapper jsonConvertWrapper, IFileOperationsWrapper fileOperationsWrapper) 
            => fileOperationsWrapper.FileWriteAllText(character.FilePath, jsonConvertWrapper.SerializeObject(character));

        public static ICharacterModel OpenCharacterFromFile(string filePath, IJsonConvertWrapper jsonConvertWrapper, IFileOperationsWrapper fileOperationsWrapper)
        {
            var character = jsonConvertWrapper.DeserializeObject<CharacterModel>(fileOperationsWrapper.FileReadAllText(filePath));

            foreach (var spellLevelViewModel in character.AllSpells)
                spellLevelViewModel.RemoveExtraDeserialisedSpells();

            return character;
        }
    }
}
