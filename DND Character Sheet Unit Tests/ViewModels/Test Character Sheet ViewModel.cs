using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using DND_Character_Sheet.Constants;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.ViewModels;
using DND_Character_Sheet.Views;
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
        public Mock<IOpenNewViewWrapper> MockOpenNewViewWrapper { get; set; }
        public Mock<IWindowWrapper> MockWindowWrapper { get; set; }
        public Mock<ISerializeCharacterWrapper> MockSerializeCharacterWrapper { get; set; }

        [TestMethod]
        public void OpenCharacter_WindowTitleUpdatesWhenOpeningDifferentCharacterFromCharacterSheet()
        {
            //arrange
            GetUnderTest(MessageBoxResult.Yes, openDialogResult: true);
            var expected = CharacterSheetViewModel.Character.FilePath;

            //act
            var hasNewCharacterOpened = CharacterSheetViewModel.OpenCharacter();

            //assert
            Assert.IsTrue(hasNewCharacterOpened);
            Assert.IsTrue(CharacterSheetViewModel.WindowTitle != expected);
            MockDialogWindowWrapper.Verify(x => x.OpenFileDialogWrapper.ShowDialog(), Times.Once);
        }

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
        public void OpenNotesWindow_Successful()
        {
            //arrange
            var expected1 = true;
            var expected2 = 1;
            GetUnderTest();
            MockWindowWrapper.Setup(x => x.GetNotesView(It.IsAny<CharacterNotes>(),
                It.IsAny<string>(), It.IsAny<IDialogWindowWrapper>(), It.IsAny<Action<object, CancelEventArgs>>()))
                .Returns(new Mock<IView>().Object);

            //act
            var actual = CharacterSheetViewModel.OpenNotesWindow();

            //assert
            Assert.AreEqual(expected1, true);
            MockWindowWrapper.Verify(x => x.GetNotesView(It.IsAny<CharacterNotes>(),
                It.IsAny<string>(), It.IsAny<IDialogWindowWrapper>(), It.IsAny<Action<object, CancelEventArgs>>()), 
                Times.Exactly(expected2));
        }

        [TestMethod]
        public void OpenSkillsWindow_Successful()
        {
            //arrange
            var expected1 = true;
            var expected2 = 1;
            GetUnderTest();
            MockWindowWrapper.Setup(x => x.GetSkillsView(It.IsAny<AllSkills>(),
                It.IsAny<bool>(), It.IsAny<Action<object, CancelEventArgs>>()))
                .Returns(new Mock<IView>().Object);

            //act
            var actual = CharacterSheetViewModel.OpenSkillsWindow();

            //assert
            Assert.AreEqual(expected1, true);
            MockWindowWrapper.Verify(x => x.GetSkillsView(It.IsAny<AllSkills>(), It.IsAny<bool>(), 
                It.IsAny<Action<object, CancelEventArgs>>()), Times.Exactly(expected2));
        }

        [TestMethod]
        public void OpenSpellsWindow_Successful()
        {
            //arrange
            var expected1 = true;
            var expected2 = 1;
            GetUnderTest();
            MockWindowWrapper.Setup(x => x.GetSpellsView(It.IsAny<AllSpells>(), It.IsAny<Action<object, CancelEventArgs>>()))
                .Returns(new Mock<IView>().Object);

            //act
            var actual = CharacterSheetViewModel.OpenSpellsWindow();

            //assert
            Assert.AreEqual(expected1, true);
            MockWindowWrapper.Verify(x => x.GetSpellsView(It.IsAny<AllSpells>(), It.IsAny<Action<object, CancelEventArgs>>()), 
                Times.Exactly(expected2));
        }

        [TestMethod]
        public void OpenDetailsWindow_Successful()
        {
            //arrange
            var expected1 = true;
            var expected2 = 1;
            GetUnderTest();
            MockWindowWrapper.Setup(x => x.GetDetailsView(It.IsAny<DetailsStats>(),
                It.IsAny<Action<object, CancelEventArgs>>()))
                .Returns(new Mock<IView>().Object);

            //act
            var actual = CharacterSheetViewModel.OpenDetailsWindow();

            //assert
            Assert.AreEqual(expected1, true);
            MockWindowWrapper.Verify(x => x.GetDetailsView(It.IsAny<DetailsStats>(),
                It.IsAny<Action<object, CancelEventArgs>>()), Times.Exactly(expected2));
        }

        [TestMethod]
        public void ExitWindow_TestYesOnCloseWindowEditedJSON()
        {
            //arrange
            var expected1 = false;
            var expected2 = 1;
            var expected3 = 0;
            GetUnderTest(MessageBoxResult.Yes, true);
            var cancelEventArgs = new CancelEventArgs();

            //act
            CharacterSheetViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            Assert.AreEqual(expected1, cancelEventArgs.Cancel);
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected2));
            MockDialogWindowWrapper.Verify(x => x.SaveFileDialogWrapper.ShowDialog(), Times.Exactly(expected3));
        }

        [TestMethod]
        public void ExitWindow_TestNoOnCloseWindowEditedJSON()
        {
            //arrange
            var expected1 = false;
            var expected2 = 1;
            var expected3 = 0;
            GetUnderTest(MessageBoxResult.No);
            var cancelEventArgs = new CancelEventArgs();

            //act
            CharacterSheetViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            Assert.AreEqual(expected1, cancelEventArgs.Cancel);
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected2));
            MockDialogWindowWrapper.Verify(x => x.SaveFileDialogWrapper.ShowDialog(), Times.Exactly(expected3));
        }

        [TestMethod]
        public void ExitWindow_TestCancelOnCloseWindowEditedJSON()
        {
            //arrange
            var expected1 = true;
            var expected2 = 1;
            var expected3 = 0;
            GetUnderTest(MessageBoxResult.Cancel);
            var cancelEventArgs = new CancelEventArgs();

            //act
            CharacterSheetViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            Assert.AreEqual(expected1, cancelEventArgs.Cancel);
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected2));
            MockDialogWindowWrapper.Verify(x => x.SaveFileDialogWrapper.ShowDialog(), Times.Exactly(expected3));
        }

        [TestMethod]
        public void ExitWindow_TestYesOnCloseWindowEditedPDF()
        {
            //arrange
            var expected1 = false;
            var expected2 = 1;
            var expected3 = 0;
            GetUnderTest(MessageBoxResult.Yes, saveDialogResult: true, isJSON: false);
            var cancelEventArgs = new CancelEventArgs();

            //act
            CharacterSheetViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            Assert.AreEqual(expected1, cancelEventArgs.Cancel);
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected2));
            MockDialogWindowWrapper.Verify(x => x.SaveFileDialogWrapper.ShowDialog(), Times.Exactly(expected3));
        }

        [TestMethod]
        public void ExitWindow_TestNoOnCloseWindowEditedPDF()
        {
            var expected1 = false;
            var expected2 = 1;
            var expected3 = 0;
            GetUnderTest(MessageBoxResult.No, isJSON: false);
            var cancelEventArgs = new CancelEventArgs();

            //act
            CharacterSheetViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            Assert.AreEqual(expected1, cancelEventArgs.Cancel);
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected2));
            MockDialogWindowWrapper.Verify(x => x.SaveFileDialogWrapper.ShowDialog(), Times.Exactly(expected3));
        }

        [TestMethod]
        public void ExitWindow_TestCancelOnCloseWindowEditedPDF()
        {
            //arrange
            var expected1 = true;
            var expected2 = 1;
            var expected3 = 0;
            GetUnderTest(MessageBoxResult.Cancel, isJSON: false);
            var cancelEventArgs = new CancelEventArgs();

            //act
            CharacterSheetViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            Assert.AreEqual(expected1, cancelEventArgs.Cancel);
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected2));
            MockDialogWindowWrapper.Verify(x => x.SaveFileDialogWrapper.ShowDialog(), Times.Exactly(expected3));
        }

        [TestMethod]
        public void ExitWindow_TestUneditedJSON()
        {
            //arrange
            var expected1 = false;
            var expected2 = 0;
            var expected3 = 0;
            GetUnderTest(MessageBoxResult.Yes, saveDialogResult: false, isEditedCharacter: false);
            var cancelEventArgs = new CancelEventArgs();

            //act
            CharacterSheetViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            Assert.AreEqual(expected1, cancelEventArgs.Cancel);
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected2));
            MockDialogWindowWrapper.Verify(x => x.SaveFileDialogWrapper.ShowDialog(), Times.Exactly(expected3));
        }

        [TestMethod]
        public void CloseAllSubWindows_OpeningDifferentCharacterFromCharacterSheet()
        {
            //arrange
            var expected1 = true;
            GetUnderTest(MessageBoxResult.Yes, openDialogResult: true);
            CharacterSheetViewModel.OpenNotesWindow();
            CharacterSheetViewModel.OpenSkillsWindow();
            CharacterSheetViewModel.OpenSpellsWindow();

            //act
            var newActiveWindows = CharacterSheetViewModel.WindowServiceWrapper.ActiveSubWindows.Count;
            var hasNewCharacterOpened = CharacterSheetViewModel.OpenCharacter();

            //assert
            Assert.AreEqual(expected1, hasNewCharacterOpened);
            Assert.IsTrue(newActiveWindows == 3);
            MockDialogWindowWrapper.Verify(x => x.OpenFileDialogWrapper.ShowDialog(), Times.Once);
            Assert.IsTrue(CharacterSheetViewModel.WindowServiceWrapper.ActiveSubWindows.Count == 0);
        }

        private void GetUnderTest(MessageBoxResult messageBoxResult = MessageBoxResult.None, bool saveDialogResult = false, bool openDialogResult = false, bool isJSON = true, bool isEditedCharacter = true)
        {
            var character = TestData.GetEditedCharacterModel();
            character.FilePath = isJSON ? character.FilePath : character.FilePath.Replace(".json", ".pdf");

            MockDialogWindowWrapper = new Mock<IDialogWindowWrapper>();
            MockDialogWindowWrapper.Setup(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()))
                .Returns(messageBoxResult);
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.SaveFileDialog)
                .Returns(new SaveFileDialog() { FileName = string.Empty });
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.ShowDialog())
                .Returns(saveDialogResult);
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.FilterIndex)
                .Returns(1); //This should stay 1 bc I don't want to save pdfs as pdfs for these tests

            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.OpenFileDialog).Returns(new OpenFileDialog());
            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.ShowDialog()).Returns(openDialogResult);
            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.FilterIndex).Returns(isJSON ? 1 : 2);

            MockTextFormatterWrapper = new Mock<ITextFormatterWrapper>();
            MockTextFormatterWrapper.Setup(x => x.ExtractFileNameFromPath(It.IsAny<string>()))
                .Returns(TestData.GetEditedCharacterModel().FilePath);
            MockTextFormatterWrapper.Setup(x => x.IsJSON(It.IsAny<string>()))
                .Returns(isJSON);
            MockTextFormatterWrapper.Setup(x => x.ListToString(It.IsAny<List<string>>(), It.IsAny<bool>()))
                .Returns("Oops! That wasn't a valid search option, please try again");

            MockSerializeCharacterWrapper = new Mock<ISerializeCharacterWrapper>();
            MockSerializeCharacterWrapper.Setup(x => x.SaveCharacterToFileJSON(It.IsAny<ICharacterModel>()));
            if (isJSON)
                MockSerializeCharacterWrapper.Setup(x => x.OpenCharacterFromFileJSON(It.IsAny<string>())).Returns(isEditedCharacter ? new CharacterModel() : character);
            else
                MockSerializeCharacterWrapper.Setup(x => x.OpenCharacterFromFilePDF(It.IsAny<string>())).Returns(isEditedCharacter ? new CharacterModel() : character);

            MockWindowWrapper = new Mock<IWindowWrapper>();
            MockWindowWrapper.Setup(x => x.Show(It.IsAny<IView>()));
            MockWindowWrapper.Setup(x => x.Close(It.IsAny<IView>()));

            var mockNotesView = new Mock<IView>();
            var notesViewModel = new NotesDialogViewModel(new CharacterNotes(), string.Empty,
                new DialogWindowWrapper(new OpenFileDialogWrapper(new OpenFileDialog()),
                new SaveFileDialogWrapper(new SaveFileDialog()),
                new MessageBoxWrapper()));
            mockNotesView.Setup(x => x.DataContext).Returns(notesViewModel); //Normally the DataContext would return NotesDialogView when running the program normally, but can't initialise this in unit tests so am using NotesDialogViewModel for the tests instead
            MockWindowWrapper.Setup(x => x.GetNotesView(It.IsAny<CharacterNotes>(), It.IsAny<string>(),
                It.IsAny<IDialogWindowWrapper>(), It.IsAny<Action<object, CancelEventArgs>>()))
                .Returns(mockNotesView.Object);

            var mockSkillsView = new Mock<IView>();
            var skillsViewModel = new SkillsDialogViewModel(new AllSkills(), false);
            mockSkillsView.Setup(x => x.DataContext).Returns(skillsViewModel);
            MockWindowWrapper.Setup(x => x.GetSkillsView(It.IsAny<AllSkills>(), It.IsAny<bool>(),
                It.IsAny<Action<object, CancelEventArgs>>()))
                .Returns(mockSkillsView.Object);

            var mockSpellsView = new Mock<IView>();
            var spellsViewModel = new SpellsDialogViewModel(new AllSpells());
            mockSpellsView.Setup(x => x.DataContext).Returns(spellsViewModel);
            MockWindowWrapper.Setup(x => x.GetSpellsView(It.IsAny<AllSpells>(),
                It.IsAny<Action<object, CancelEventArgs>>()))
                .Returns(mockSpellsView.Object);

            CharacterSheetViewModel = new CharacterSheetViewModel(
                character,
                MockDialogWindowWrapper.Object,
                new StaticClassWrapper(MockTextFormatterWrapper.Object, MockSerializeCharacterWrapper.Object),
                new OpenNewViewWrapper(MockWindowWrapper.Object));
        }
    }
}
