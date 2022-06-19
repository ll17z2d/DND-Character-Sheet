using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Wrappers;
//using iText.Kernel.Pdf;
using System.IO;

namespace DND_Character_Sheet.Serialization
{
    public static class SerializeCharacter
    {
        public static void SaveCharacterToFileJSON(ICharacterModel character, IJsonConvertWrapper jsonConvertWrapper, IFileOperationsWrapper fileOperationsWrapper) 
            => fileOperationsWrapper.JSONFileWriteAllText(character.FilePath, jsonConvertWrapper.SerializeObject(character));

        public static void SaveCharacterToFilePDF(ICharacterModel character, ISerializePDF serializePDF) 
            => serializePDF.Serialize(character);

        public static ICharacterModel OpenCharacterFromFileJSON(string filePath, IJsonConvertWrapper jsonConvertWrapper, IFileOperationsWrapper fileOperationsWrapper)
        {
            var character = jsonConvertWrapper.DeserializeObject<CharacterModel>(fileOperationsWrapper.FileReadAllText(filePath));

            foreach (var spellLevelViewModel in character.AllSpells)
                spellLevelViewModel.RemoveExtraDeserializedSpells();

            return character;
        }

        public static ICharacterModel OpenCharacterFromFilePDF(string filePath, ISerializePDF serializePDF) 
            => serializePDF.Deserialize(filePath);
    }
}
