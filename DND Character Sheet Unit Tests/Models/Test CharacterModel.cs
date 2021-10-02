using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DND_Character_Sheet.Models.Serialize_Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DND_Character_Sheet_Unit_Tests.Models
{
    [TestClass]
    public class TestCharacterModel
    {
        [TestMethod]
        public void TestInitialiseEmptyContructor()
        {
            //arrange
            //act
            var actual = new CharacterModel();

            //assert
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.AllSkills);
            Assert.IsNotNull(actual.CharacterNotes);
            Assert.IsNotNull(actual.FilePath);
            Assert.IsNotNull(actual.HPStats);
            Assert.IsNotNull(actual.MainStats);
            Assert.IsNotNull(actual.MiscStats);
            Assert.IsNotNull(actual.WeaponsInventory);
        }
    }
}
