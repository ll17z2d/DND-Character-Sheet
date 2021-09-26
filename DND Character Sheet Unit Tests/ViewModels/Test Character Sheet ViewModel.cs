using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using DND_Character_Sheet.APICommunication;
using DND_Character_Sheet.Constants;
using DND_Character_Sheet.Models.API_Models;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.ViewModels;
using DND_Character_Sheet.Wrappers;
using DND_Character_Sheet_Unit_Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using Moq;

namespace DND_Character_Sheet_Unit_Tests.ViewModels
{
    [TestClass]
    public class TestCharacterSheetViewModel
    {
        public CharacterSheetViewModel CharacterSheetViewModel { get; set; }
        public Mock<IDialogWindowWrapper> MockDialogWindowWrapper { get; set; }
        public Mock<ITextFormatterWrapper> MockTextFormatterWrapper { get; set; }

        [TestMethod]
        public void CanAPISearch_EmptyTextBox()
        {
            //arrange
            GetUnderTest();
            CharacterSheetViewModel.SearchTextbox = "  ";
            CharacterSheetViewModel.SelectedSearchType = "Test";

            //act
            var actual = CharacterSheetViewModel.CanAPISearch();

            //assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CanAPISearch_NullTextBox()
        {
            //arrange
            GetUnderTest();
            CharacterSheetViewModel.SearchTextbox = null;
            CharacterSheetViewModel.SelectedSearchType = "Test";

            //act
            var actual = CharacterSheetViewModel.CanAPISearch();

            //assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CanAPISearch_ValidTextBox()
        {
            //arrange
            GetUnderTest();
            CharacterSheetViewModel.SearchTextbox = "Test";
            CharacterSheetViewModel.SelectedSearchType = "Test";

            //act
            var actual = CharacterSheetViewModel.CanAPISearch();

            //assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CanAPISearch_NullSelectedSearchType()
        {
            //arrange
            GetUnderTest();
            CharacterSheetViewModel.SearchTextbox = "Test";
            CharacterSheetViewModel.SelectedSearchType = null;

            //act
            var actual = CharacterSheetViewModel.CanAPISearch();

            //assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void APISearch_TestEmptyTextBox()
        {
            //arrange
            var expected1 = "Oops! That wasn't a valid search option, please try again";
            var expected2 = false;
            GetUnderTest();
            CharacterSheetViewModel.SearchTextbox = "  ";
            CharacterSheetViewModel.SelectedSearchType = "Test";

            //act
            var actual = CharacterSheetViewModel.APISearch();

            //assert
            Assert.AreEqual(expected1, CharacterSheetViewModel.OutTextbox);
            Assert.AreEqual(expected2, actual);
        }

        [TestMethod]
        public void DiceRoll_NullSelectedDice()
        {
            //arrange
            GetUnderTest();
            CharacterSheetViewModel.SelectedDice = "null";

            //act
            var actual = CharacterSheetViewModel.DiceRoll();

            //assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void DiceRoll_InvalidSelectedDice()
        {
            //arrange
            GetUnderTest();
            CharacterSheetViewModel.SelectedDice = "14fa3fjm6";

            //act
            var actual = CharacterSheetViewModel.DiceRoll();

            //assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void DiceRoll_ValidSelectedDice()
        {
            //arrange
            GetUnderTest();
            CharacterSheetViewModel.SelectedDice = "1d4";

            //act
            var actual = CharacterSheetViewModel.DiceRoll();

            //assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SearchSkills_SkillsFound()
        {
            //arrange
            var expected1 = true;
            var expected2 = new List<string>()
                {SkillStrings.Perception, SkillStrings.Performance, SkillStrings.Persuasion};
            var expected3 = 3;
            GetUnderTest();
            CharacterSheetViewModel.SkillTextbox = "per";

            //act
            var actual = CharacterSheetViewModel.SearchSkills();

            //assert
            Assert.AreEqual(expected1, actual);
            CollectionAssert.AreEqual(expected2, CharacterSheetViewModel.SearchedSkills.Select(x => x.Name).ToList());
            Assert.AreEqual(expected3, CharacterSheetViewModel.SearchedSkills.Count);
        }

        [TestMethod]
        public void SearchSkills_NoSkillsFound()
        {
            //arrange
            var expected1 = false;
            var expected2 = new List<string>();
            var expected3 = 0;
            GetUnderTest();
            CharacterSheetViewModel.SkillTextbox = "za";

            //act
            var actual = CharacterSheetViewModel.SearchSkills();

            //assert
            Assert.AreEqual(expected1, actual);
            CollectionAssert.AreEqual(expected2, CharacterSheetViewModel.SearchedSkills.Select(x => x.Name).ToList());
            Assert.AreEqual(expected3, CharacterSheetViewModel.SearchedSkills.Count);
        }

        [TestMethod]
        public void OpenNotesWindow_()
        {
            //arrange
            var expected1 = true;
            var expected2 = 1;
            GetUnderTest();
            var mockNotesWindowWrapper = new Mock<IWindowWrapper>();
            mockNotesWindowWrapper.Setup(x => x.ShowDialog()).Returns(true);
            CharacterSheetViewModel.NotesWindowWrapper = mockNotesWindowWrapper.Object;

            //act
            var actual = CharacterSheetViewModel.OpenNotesWindow();

            //assert
            Assert.AreEqual(expected1, true);
            mockNotesWindowWrapper.Verify(x => x.ShowDialog(), Times.Exactly(expected2));
        }

        [TestMethod]
        public void ExitWindow_TestCancelOnCloseWindow()
        {
            //arrange
            var expected = true;
            GetUnderTest(MessageBoxResult.Cancel);
            var cancelEventArgs = new CancelEventArgs();

            //act
            CharacterSheetViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Once);
            Assert.AreEqual(cancelEventArgs.Cancel, expected);
        }

        [TestMethod]
        public void ExitWindow_TestYesOnCloseWindow()
        {
            //arrange
            var expected = false;
            GetUnderTest(MessageBoxResult.Yes);
            var cancelEventArgs = new CancelEventArgs();

            //act
            CharacterSheetViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Once);
            Assert.AreEqual(cancelEventArgs.Cancel, expected);
        }

        [TestMethod]
        public void ExitWindow_TestNoOnCloseWindow()
        {
            //arrange
            var expected = false;
            GetUnderTest(MessageBoxResult.No);
            var cancelEventArgs = new CancelEventArgs();

            //act
            CharacterSheetViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Once);
            Assert.AreEqual(cancelEventArgs.Cancel, expected);
        }

        private void GetUnderTest(MessageBoxResult messageBoxResult = MessageBoxResult.None)
        {
            var character = TestData.GetInitialisedCharacterModel();
            character.FilePath = new CharacterModel().FilePath;

            MockDialogWindowWrapper = new Mock<IDialogWindowWrapper>();
            MockDialogWindowWrapper.Setup(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()))
                .Returns(messageBoxResult);
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.SaveFileDialog)
                .Returns(new SaveFileDialog());
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.ShowDialog())
                .Returns(false);

            MockTextFormatterWrapper = new Mock<ITextFormatterWrapper>();
            MockTextFormatterWrapper.Setup(x => x.ExtractFileNameFromPath(It.IsAny<string>()))
                .Returns("Test Window Title");
            MockTextFormatterWrapper.Setup(x => x.ListToString(It.IsAny<List<string>>(), It.IsAny<bool>()))
                .Returns("Oops! That wasn't a valid search option, please try again");

            CharacterSheetViewModel = new CharacterSheetViewModel(character, 
                MockDialogWindowWrapper.Object, MockTextFormatterWrapper.Object, new SerializeCharacterWrapper());
        }
    }
}
