using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Serialization;
using DND_Character_Sheet.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DND_Character_Sheet_Unit_Tests.Serialization
{
    [TestClass]
    public class TestSerializeCharacter
    {
        public Mock<IFileOperationsWrapper> MockFileOperationsWrapper { get; set; }

        public Mock<IJsonConvertWrapper> MockJsonConvertWrapper { get; set; }

        public ICharacterModel CharacterModel { get; set; }

        [TestMethod]
        public void OpenCharacterFromFile_TestDeserializeSpells()
        {
            //arrange
            var expected = true;
            GetUnderTest();
            MockJsonConvertWrapper.Setup(x => x.DeserializeObject<ICharacterModel>(It.IsAny<string>())).Returns(CharacterModel);
            MockFileOperationsWrapper.Setup(x => x.FileReadAllText(It.IsAny<string>())).Returns(string.Empty);

            //act
            var actual = SerializeCharacter.OpenCharacterFromFile(It.IsAny<string>(), 
                MockJsonConvertWrapper.Object, MockFileOperationsWrapper.Object);

            //assert
            Assert.AreEqual(expected, new ObjectsComparer.Comparer<ICharacterModel>().Compare(new CharacterModel(), actual, out var dif));
        }

        public void GetUnderTest()
        {
            MockJsonConvertWrapper = new Mock<IJsonConvertWrapper>();
            MockFileOperationsWrapper = new Mock<IFileOperationsWrapper>();

            CharacterModel = new CharacterModel();
            foreach (var spellLevelViewModel in CharacterModel.AllSpells)
            {
                var x = spellLevelViewModel.Spells.Count;
                for (int i = 0; i < x; i++) 
                    spellLevelViewModel.Spells.Add(new Spell());
            }
        }
    }
}
