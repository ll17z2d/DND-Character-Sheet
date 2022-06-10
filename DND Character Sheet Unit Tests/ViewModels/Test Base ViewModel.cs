using System;
using System.Collections.Generic;
using System.Text;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.ViewModels;
using DND_Character_Sheet.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using Moq;

namespace DND_Character_Sheet_Unit_Tests.ViewModels
{
    [TestClass]
    public class TestBaseViewModel
    {
        public CharacterCreatorViewModel CharacterCreatorViewModel { get; set; }
        public Mock<IDialogWindowWrapper> MockDialogWindowWrapper { get; set; }
        public Mock<ISerializeCharacterWrapper> MockSerializeCharacterWrapper { get; set; }

        [TestMethod]
        public void OpenCharacterJSON_TestSuccessOnDialogWindow()
        {
            //arrange
            var expected1 = 4;
            var expected2 = true;
            var expected3 = 1;
            GetUnderTestOpenCharacter(expected2, true);

            //act
            var actual = CharacterCreatorViewModel.OpenCharacter();

            //assert
            MockDialogWindowWrapper.Verify(x => x.OpenFileDialogWrapper.OpenFileDialog, Times.Exactly(expected1));
            Assert.AreEqual(expected2, actual);
            MockSerializeCharacterWrapper.Verify(x => x.OpenCharacterFromFileJSON(It.IsAny<string>()), 
                Times.Exactly(expected3));
        }

        [TestMethod]
        public void OpenCharacterJSON_TestCancelledOnDialogWindow()
        {
            //arrange
            var expected1 = 3;
            var expected2 = false;
            var expected3 = 0;
            GetUnderTestOpenCharacter(expected2, true);

            //act
            var actual = CharacterCreatorViewModel.OpenCharacter();

            //assert
            MockDialogWindowWrapper.Verify(x => x.OpenFileDialogWrapper.OpenFileDialog, Times.Exactly(expected1));
            Assert.AreEqual(expected2, actual);
            MockSerializeCharacterWrapper.Verify(x => x.OpenCharacterFromFileJSON(It.IsAny<string>()), 
                Times.Exactly(expected3));
        }

        [TestMethod]
        public void SaveCharacterJSON_TestNewlyCreatedCharacter()
        {
            //arrange
            var expected1 = 4;
            var expected2 = true;
            var expected3 = 1;
            GetUnderTestSaveCharacter(expected2, true);

            //act
            var actual = CharacterCreatorViewModel.SaveCharacter();

            //assert
            MockDialogWindowWrapper.Verify(x => x.SaveFileDialogWrapper.SaveFileDialog, Times.Exactly(expected1));
            Assert.AreEqual(expected2, actual);
            MockSerializeCharacterWrapper.Verify(x => x.SaveCharacterToFileJSON(It.IsAny<ICharacterModel>()), 
                Times.Exactly(expected3));
        }

        [TestMethod]
        public void SaveCharacterPDF_TestNewlyCreatedCharacter()
        {
            //arrange
            var expected1 = 4;
            var expected2 = true;
            var expected3 = 1;
            GetUnderTestSaveCharacter(expected2, false);

            //act
            var actual = CharacterCreatorViewModel.SaveCharacterAs();

            //assert
            MockDialogWindowWrapper.Verify(x => x.SaveFileDialogWrapper.SaveFileDialog, Times.Exactly(expected1));
            Assert.AreEqual(expected2, actual);
            MockSerializeCharacterWrapper.Verify(x => x.SaveCharacterToFilePDF(It.IsAny<ICharacterModel>()),
                Times.Exactly(expected3));
        }

        [TestMethod]
        public void SaveCharacterJSON_TestUncreatedCharacterCancelledOnDialogWindow()
        {
            //arrange
            var expected1 = 3;
            var expected2 = false;
            var expected3 = 0;
            GetUnderTestSaveCharacter(expected2, true);

            //act
            var actual = CharacterCreatorViewModel.SaveCharacter();

            //assert
            MockDialogWindowWrapper.Verify(x => x.SaveFileDialogWrapper.SaveFileDialog, Times.Exactly(expected1));
            Assert.AreEqual(expected2, actual);
            MockSerializeCharacterWrapper.Verify(x => x.SaveCharacterToFileJSON(It.IsAny<ICharacterModel>()), 
                Times.Exactly(expected3));
        }

        [TestMethod]
        public void SaveCharacterJSON_TestUncreatedCharacterSuccessOnDialogWindow()
        {
            //arrange
            var expected1 = 4;
            var expected2 = true;
            var expected3 = 1;
            GetUnderTestSaveCharacter(expected2, true);

            //act
            var actual = CharacterCreatorViewModel.SaveCharacter();

            //assert
            MockDialogWindowWrapper.Verify(x => x.SaveFileDialogWrapper.SaveFileDialog, Times.Exactly(expected1));
            Assert.AreEqual(expected2, actual);
            MockSerializeCharacterWrapper.Verify(x => x.SaveCharacterToFileJSON(It.IsAny<ICharacterModel>()), 
                Times.Exactly(expected3));
        }

        private void GetUnderTestSaveCharacter(bool dialogWindowResult, bool isJSON)
        {
            MockDialogWindowWrapper = new Mock<IDialogWindowWrapper>();
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.SaveFileDialog).Returns(new SaveFileDialog());
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.ShowDialog()).Returns(dialogWindowResult);
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.FilterIndex).Returns(isJSON ? 1 : 2);

            MockSerializeCharacterWrapper = new Mock<ISerializeCharacterWrapper>();
            MockSerializeCharacterWrapper.Setup(x => x.SaveCharacterToFileJSON(It.IsAny<ICharacterModel>()));

            CharacterCreatorViewModel = new CharacterCreatorViewModel(MockDialogWindowWrapper.Object, 
                new StaticClassWrapper(new TextFormatterWrapper(), MockSerializeCharacterWrapper.Object), 
                new OpenNewViewWrapper(new WindowWrapper()));
        }

        private void GetUnderTestOpenCharacter(bool dialogWindowResult, bool isJSON)
        {
            MockDialogWindowWrapper = new Mock<IDialogWindowWrapper>();
            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.OpenFileDialog).Returns(new OpenFileDialog());
            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.ShowDialog()).Returns(dialogWindowResult);
            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.FilterIndex).Returns(isJSON ? 1 : 2);

            MockSerializeCharacterWrapper = new Mock<ISerializeCharacterWrapper>();
            MockSerializeCharacterWrapper.Setup(x => x.OpenCharacterFromFileJSON(It.IsAny<string>())).Returns(new CharacterModel());

            CharacterCreatorViewModel = new CharacterCreatorViewModel(MockDialogWindowWrapper.Object,
                new StaticClassWrapper(new TextFormatterWrapper(), MockSerializeCharacterWrapper.Object), 
                new OpenNewViewWrapper(new WindowWrapper()));
        }
    }
}
