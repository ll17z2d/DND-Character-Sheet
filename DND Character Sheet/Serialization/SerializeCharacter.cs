using System.IO;
using DND_Character_Sheet.Models.Serialize_Types;
using Newtonsoft.Json;

namespace DND_Character_Sheet.Serialization
{
    public static class SerializeCharacter
    {
        public static void SaveCharacterToFile(ICharacterModel character) 
            => File.WriteAllText(character.FilePath, JsonConvert.SerializeObject(character));

        public static ICharacterModel OpenCharacterFromFile(string filePath)
        {
            var character = JsonConvert.DeserializeObject<CharacterModel>(File.ReadAllText(filePath));

            foreach (var spellLevelViewModel in character.AllSpells)
                spellLevelViewModel.RemoveExtraDeserialisedSpells();

            return character;
        }
    }
}
