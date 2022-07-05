using DND_Character_Sheet.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND_Character_Sheet_Unit_Tests.Useful_Methods
{
    [TestClass]
    public class Test_Text_Formatter
    {
        [DataTestMethod]
        [DataRow("json")]
        [DataRow("pdf")]
        public void TestExtractFileNameFromPathJSON(string fileType)
        {
            //arrange
            var characterName = "Cool Character";
            var filePath = $"C:\\Cool Folder\\{characterName}.{fileType}";
            var TextFormatterWrapper = new TextFormatterWrapper();
            var expected = characterName;

            //act
            var actual = TextFormatterWrapper.ExtractFileNameFromPath(filePath);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
