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
    public class TestMainStats
    {
        [TestMethod]
        public void TestStatModifierAutoUpdates()
        {
            //arrange
            var expectedStrMod = "+1";
            var expectedDexMod = "-1";
            var expectedConMod = "+2";
            var expectedIntlMod = "+5";
            var expectedWisMod = "+0";
            var expectedChaMod = "+4";

            //act
            var actual = new MainStats
            {
                Str = 13,
                Dex = 8,
                Con = 15,
                Intl = 20,
                Wis = 11,
                Cha = 18
            };

            //assert
            Assert.AreEqual(expectedStrMod, actual.StrMod);
            Assert.AreEqual(expectedDexMod, actual.DexMod);
            Assert.AreEqual(expectedConMod, actual.ConMod);
            Assert.AreEqual(expectedIntlMod, actual.IntlMod);
            Assert.AreEqual(expectedWisMod, actual.WisMod);
            Assert.AreEqual(expectedChaMod, actual.ChaMod);
        }

        [TestMethod]
        public void TestStatModifierUpdatesAfterAutoChanged()
        {
            //arrange
            var expectedMod = "+9";

            //act
            var actual = new MainStats
            {
                Str = 13,
                Dex = 8,
                Con = 15,
                Intl = 20,
                Wis = 11,
                Cha = 18
            };

            actual.StrMod = "+9";
            actual.DexMod = "+9";
            actual.ConMod = "+9";
            actual.IntlMod = "+9";
            actual.WisMod = "+9";
            actual.ChaMod = "+9";

            //assert
            Assert.AreEqual(expectedMod, actual.StrMod);
            Assert.AreEqual(expectedMod, actual.DexMod);
            Assert.AreEqual(expectedMod, actual.ConMod);
            Assert.AreEqual(expectedMod, actual.IntlMod);
            Assert.AreEqual(expectedMod, actual.WisMod);
            Assert.AreEqual(expectedMod, actual.ChaMod);
        }

        [TestMethod]
        public void TestStatModifierAutoUpdatesAfterAutoChanged()
        {
            //arrange
            var expectedStrMod = "+3";
            var expectedDexMod = "+3";
            var expectedConMod = "+3";
            var expectedIntlMod = "+3";
            var expectedWisMod = "+3";
            var expectedChaMod = "+3";

            //act
            var actual = new MainStats
            {
                Str = 13,
                Dex = 8,
                Con = 15,
                Intl = 20,
                Wis = 11,
                Cha = 18
            };

            actual.StrMod = "+9";
            actual.DexMod = "+9";
            actual.ConMod = "+9";
            actual.IntlMod = "+9";
            actual.WisMod = "+9";
            actual.ChaMod = "+9";

            actual.Str = 17;
            actual.Dex = 17;
            actual.Con = 17;
            actual.Wis = 17;
            actual.Intl = 17;
            actual.Cha = 17;

            //assert
            Assert.AreEqual(expectedStrMod, actual.StrMod);
            Assert.AreEqual(expectedDexMod, actual.DexMod);
            Assert.AreEqual(expectedConMod, actual.ConMod);
            Assert.AreEqual(expectedIntlMod, actual.IntlMod);
            Assert.AreEqual(expectedWisMod, actual.WisMod);
            Assert.AreEqual(expectedChaMod, actual.ChaMod);
        }
    }
}
