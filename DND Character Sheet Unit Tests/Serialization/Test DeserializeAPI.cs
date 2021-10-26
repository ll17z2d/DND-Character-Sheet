using System;
using System.Collections.Generic;
using System.Text;
using DND_Character_Sheet.Enums;
using DND_Character_Sheet.Serialization;
using DND_Character_Sheet.Wrappers;
using DND_Character_Sheet_Unit_Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DND_Character_Sheet_Unit_Tests.Serialization
{
    [TestClass]
    public class TestDeserializeAPI
    {
        public DeserializeAPI DeserializeAPI { get; set; }
        private void GetUnderTest(DND_Search_Types searchTypeEnum, string outputJson, ITextFormatterWrapper textFormatterWrapper) 
            => DeserializeAPI = new DeserializeAPI(searchTypeEnum, outputJson, textFormatterWrapper);

        [TestMethod]
        public void DeserializeClasses()
        {
            //arrange
            var expected1 = GetDeserializedClass();
            var expected2 = true;
            var searchTypeEnum = DND_Search_Types.Classes;
            var outputJson = TestData.GetTestDNDClass();
            var textFormatterWrapper = new TextFormatterWrapper();

            //act
            GetUnderTest(searchTypeEnum, outputJson, textFormatterWrapper);
            var actual = DeserializeAPI.Deserialize();

            //assert
            CollectionAssert.AreEqual(expected1, actual.Item1);
            Assert.AreEqual(expected2, actual.Item2);
        }

        private List<string> GetDeserializedClass()
            => new List<string>()
            {
                "Class:  Monk\n",
                "Monks are proficient in simple weapons and shortswords\nas well as saving throw proficiencies in str and dex\n",
                "Monks also have a hit die of 1d8\n",
                "Monks can pick 1 from the list of proficiencies below:\nalchemist's supplies, brewer's supplies, calligrapher's supplies, carpenter's tools, cartographer's tools, cobbler's tools, cook's utensils, glassblower's tools, jeweler's tools, leatherworker's tools, mason's tools, painter's supplies, potter's tools, smith's tools, tinker's tools, weaver's tools, woodcarver's tools, disguise kit and forgery kit",
                "\nas well as pick 1 from the list of proficiencies below:\nbagpipes, drum, dulcimer, flute, lute, lyre, horn, pan flute, shawm and viol",
                "\nand finally pick 2 from the list of proficiencies below:\nskill: acrobatics, skill: athletics, skill: history, skill: insight, skill: religion and skill: stealth\n"
            };
    }
}
