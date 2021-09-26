using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DND_Character_Sheet_Unit_Tests.Serialization
{
    public class TestSerializeCharacter //TODO: Finish this test class
    {
        public CharacterModel Character { get; set; }
        [TestInitialize]
        public void Setup()
        {
            var mainStats = new MainStats(20, 13, 14, 8, 10, 12, "+3", "+2", "+4", "+1", "+2", "+2");
            //var allSkills = new AllSkills(new Skill("Testing", "+3", true), new Skill("Testing", "-1"), new Skill("Testing", "+3"), new Skill("Testing", "+2", true), new Skill("Testing", "-1"), new Skill("Testing", "+3"), new Skill("Testing", "+2", true), new Skill("Testing", "-1"), new Skill("Testing", "+3"), new Skill("Testing", "+2", true), new Skill("Testing", "-1"), new Skill("Testing", "+3"), new Skill("Testing", "+2", true), new Skill("Testing", "-1"), new Skill("Testing", "+3"), new Skill("Testing", "+2", true), new Skill("Testing", "-1"), new Skill("Testing", "+3"));
            var allSkills = new AllSkills();
            var hpStats = new HPStats(42, 36, 3);
            var miscStats = new MiscStats("+2", 19, "+4", "+35");
            var characterNotes = new CharacterNotes();
            CharacterModel character = new CharacterModel(mainStats, allSkills, hpStats, miscStats, characterNotes);
            character.FilePath = "C:\\TestFile.json";

            Character = character;
        }

        [TestMethod]
        public void SerialiseCharacterTest()
        {
            //arrange


            //act
            //SerializeCharacter.SaveCharacterToFile(Character);

            //assert
        }
    }
}
