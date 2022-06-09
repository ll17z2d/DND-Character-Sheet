using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DND_Character_Sheet_Unit_Tests.Models
{
    [TestClass]
    public class TestCharacterNotes
    {
        private const string PlaceholderCharacterAppearance = "/Images/Placeholder Character Appearance.png";
        private const string InvalidCharacterAppearance = "/Images/Invalid Character Appearance.png";

        public Mock<IFileOperationsWrapper> MockFileOperationsWrapper { get; set; }

        [TestMethod]
        public void TestInitialiseCharacterAppearanceFilePath()
        {
            //arrange
            var expected = PlaceholderCharacterAppearance;
            MockFileOperationsWrapper = new Mock<IFileOperationsWrapper>();

            //act
            var actual = new CharacterNotes(PlaceholderCharacterAppearance, "", "", "", "", "", 
                new Money(), "", "", "", new SerializeCharacterWrapper(), MockFileOperationsWrapper.Object).CharacterAppearanceFilePath;

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSuccessfulChangeCharacterAppearanceFilePathFromDefault()
        {
            //arrange
            var expected = "SaveLocation";
            MockFileOperationsWrapper = new Mock<IFileOperationsWrapper>();
            MockFileOperationsWrapper.Setup(x => x.FileExists("SaveLocation")).Returns(true);
            var model = new CharacterNotes(PlaceholderCharacterAppearance, "", "", "", "", "", new Money(),
                "", "", "", new SerializeCharacterWrapper(), MockFileOperationsWrapper.Object);

            //act
            model.CharacterAppearanceFilePath = "SaveLocation";
            var actual = model.CharacterAppearanceFilePath;


            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUnsuccessfulChangeCharacterAppearanceFilePathFromDefault()
        {
            //arrange
            var expected = InvalidCharacterAppearance;
            MockFileOperationsWrapper = new Mock<IFileOperationsWrapper>();
            MockFileOperationsWrapper.Setup(x => x.FileExists("SaveLocation")).Returns(false);
            var model = new CharacterNotes(PlaceholderCharacterAppearance, "", "", "", "", "", new Money(),
                "", "", "", new SerializeCharacterWrapper(), MockFileOperationsWrapper.Object);

            //act
            model.CharacterAppearanceFilePath = "SaveLocation";
            var actual = model.CharacterAppearanceFilePath;


            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSuccessfulChangeCharacterAppearanceFilePathFromValue()
        {
            //arrange
            var expected = "DifferentSaveLocation";
            MockFileOperationsWrapper = new Mock<IFileOperationsWrapper>();
            MockFileOperationsWrapper.Setup(x => x.FileExists(It.IsAny<string>())).Returns(true);
            var model = new CharacterNotes(PlaceholderCharacterAppearance, "", "", "", "", "", new Money(),
                "", "", "", new SerializeCharacterWrapper(), MockFileOperationsWrapper.Object);

            //act
            model.CharacterAppearanceFilePath = "SaveLocation";
            model.CharacterAppearanceFilePath = "DifferentSaveLocation";
            var actual = model.CharacterAppearanceFilePath;

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUnsuccessfulChangeCharacterAppearanceFilePathFromValue()
        {
            //arrange
            var expected = InvalidCharacterAppearance;
            MockFileOperationsWrapper = new Mock<IFileOperationsWrapper>();
            MockFileOperationsWrapper.Setup(x => x.FileExists("SaveLocation")).Returns(false);
            var model = new CharacterNotes(PlaceholderCharacterAppearance, "", "", "", "", "", new Money(),
                "", "", "", new SerializeCharacterWrapper(), MockFileOperationsWrapper.Object);

            //act
            model.CharacterAppearanceFilePath = "SaveLocation";
            MockFileOperationsWrapper.Setup(x => x.FileExists("SaveLocation")).Returns(false);
            var actual = model.CharacterAppearanceFilePath;

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
