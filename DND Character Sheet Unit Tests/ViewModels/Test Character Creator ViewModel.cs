using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using DND_Character_Sheet.Models.API_Models;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.ViewModels;
using DND_Character_Sheet.Wrappers;
using DND_Character_Sheet_Unit_Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using Moq;
using ObjectsComparer;

namespace DND_Character_Sheet_Unit_Tests.ViewModels
{
    [TestClass]
    public class TestCharacterCreatorViewModel
    {
        public CharacterCreatorViewModel CharacterCreatorViewModel { get; set; }
        public Mock<IDialogWindowWrapper> MockDialogWindowWrapper { get; set; }
        public Mock<ITextFormatterWrapper> MockTextFormatterWrapper { get; set; }
        public Mock<ISerializeCharacterWrapper> MockSerializeCharacterWrapper { get; set; }

        [TestMethod]
        public void ExitWindow_TestCloseWindowOnBlankCharacter()
        {
            //arrange
            var expected1 = false;
            var expected2 = 0;
            GetUnderTest();
            var cancelEventArgs = new CancelEventArgs();

            //act
            CharacterCreatorViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected2));
            Assert.AreEqual(cancelEventArgs.Cancel, expected1);
        }

        [TestMethod]
        public void ExitWindow_TestNoOnCloseWindow()
        {
            //arrange
            var expected1 = false;
            var expected2 = 1;
            GetUnderTest(MessageBoxResult.No);
            var cancelEventArgs = new CancelEventArgs();
            ChangeCharacterStrValue(2);

            //act
            CharacterCreatorViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected2));
            Assert.AreEqual(cancelEventArgs.Cancel, expected1);
        }

        [TestMethod]
        public void ExitWindow_TestYesOnCloseWindow()
        {
            //arrange
            var expected1 = false;
            var expected2 = 1;
            GetUnderTest(MessageBoxResult.Yes, true);
            var cancelEventArgs = new CancelEventArgs();
            ChangeCharacterStrValue(2);

            //act
            CharacterCreatorViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected2));
            Assert.AreEqual(cancelEventArgs.Cancel, expected1);
        }

        [TestMethod]
        public void ExitWindow_TestCancelOnCloseWindow()
        {
            //arrange
            var expected1 = true;
            var expected2 = 1;
            GetUnderTest(MessageBoxResult.Cancel);
            var cancelEventArgs = new CancelEventArgs();
            ChangeCharacterStrValue(2);

            //act
            CharacterCreatorViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected2));
            Assert.AreEqual(cancelEventArgs.Cancel, expected1);
        }

        [TestMethod]
        public void ExitWindow_TestCancelOnSaveDialogOnCloseWindow()
        {
            //arrange
            var expected1 = true;
            var expected2 = 1;
            GetUnderTest(MessageBoxResult.Yes);
            var cancelEventArgs = new CancelEventArgs();
            ChangeCharacterStrValue(2);

            //act
            CharacterCreatorViewModel.ExitWindow(new object(), cancelEventArgs);

            //assert
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected2));
            Assert.AreEqual(cancelEventArgs.Cancel, expected1);
        }

        [TestMethod]
        public void AutoGenerateSkillModifiers_TestNoOnConfirmationWindow()
        {
            //arrange
            var expected1 = "0";
            var expected2 = 1;
            GetUnderTest(MessageBoxResult.No);

            //act
            CharacterCreatorViewModel.AutoGenerateSkillModifiers();

            //assert
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected2));
            foreach (var skill in CharacterCreatorViewModel.Character.AllSkills)
            {
                Assert.AreEqual(skill.SkillScore, expected1);
            }
        }

        [TestMethod]
        public void AutoGenerateSkillModifiers_TestYesOnConfirmationWindow()
        {
            //arrange
            var expected1 = "-5";
            var expected2 = true;
            var expected3 = 2;
            GetUnderTest(MessageBoxResult.Yes);

            //act
            var actual = CharacterCreatorViewModel.AutoGenerateSkillModifiers();

            //assert
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected3));
            foreach (var skill in CharacterCreatorViewModel.Character.AllSkills)
            {
                Assert.AreEqual(skill.SkillScore, expected1);
            }
            Assert.AreEqual(expected2, actual);
        }

        [TestMethod]
        public void AutoGenerateSkillModifiers_TestYesOnConfirmationWindowWithProficiency()
        {
            //arrange
            var expected1 = "-1";
            var expected2 = true;
            var expected3 = 2;
            GetUnderTest(MessageBoxResult.Yes);
            CharacterCreatorViewModel.Character.MiscStats.Proficiency = " + 4  ";
            foreach (var skill in CharacterCreatorViewModel.Character.AllSkills)
            {
                skill.IsProficient = true;
            }

            //act
            var actual = CharacterCreatorViewModel.AutoGenerateSkillModifiers();

            //assert
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected3));
            foreach (var skill in CharacterCreatorViewModel.Character.AllSkills)
            {
                Assert.AreEqual(skill.SkillScore, expected1);
            }
            Assert.AreEqual(expected2, actual);
        }

        [TestMethod]
        public void AutoGenerateSkillModifiers_TestErrorWithProficiency()
        {
            //arrange
            var expected1 = "0";
            var expected2 = false;
            var expected3 = 2;
            GetUnderTest(MessageBoxResult.Yes);
            CharacterCreatorViewModel.Character.MiscStats.Proficiency = " a b  ";
            foreach (var skill in CharacterCreatorViewModel.Character.AllSkills)
            {
                skill.IsProficient = true;
            }

            //act
            var actual = CharacterCreatorViewModel.AutoGenerateSkillModifiers();

            //assert
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected3));
            foreach (var skill in CharacterCreatorViewModel.Character.AllSkills)
            {
                Assert.AreEqual(skill.SkillScore, expected1);
            }
            Assert.AreEqual(expected2, actual);
        }

        [TestMethod]
        public void ResetCharacter_TestNoOnConfirmationWindow()
        {
            //arrange
            var expected1 = true;
            var expected2 = false;
            var expected3 = 1;
            GetUnderTest(MessageBoxResult.No);
            var newCharacterStats = TestData.GetEditedCharacterModel();

            //act
            CharacterCreatorViewModel.Character = newCharacterStats;
            var actual = CharacterCreatorViewModel.ResetCharacter();

            //assert
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected3));
            Assert.AreEqual(expected1, new ObjectsComparer.Comparer<ICharacterModel>().Compare(newCharacterStats, CharacterCreatorViewModel.Character, out var dif));
            Assert.AreEqual(expected2, actual);
        }

        [TestMethod]
        public void ResetCharacter_TestYesOnConfirmationWindow()
        {
            //arrange
            var expected1 = false;
            var expected2 = true;
            var expected3 = 1;
            GetUnderTest(MessageBoxResult.Yes);
            var newCharacterStats = TestData.GetEditedCharacterModel();

            //act
            CharacterCreatorViewModel.Character = newCharacterStats;
            var actual = CharacterCreatorViewModel.ResetCharacter();

            //assert
            MockDialogWindowWrapper.Verify(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()), Times.Exactly(expected3));
            Assert.AreEqual(expected1, new ObjectsComparer.Comparer<ICharacterModel>().Compare(newCharacterStats, CharacterCreatorViewModel.Character, out var dif));
            Assert.AreEqual(expected2, actual);
        }

        private void GetUnderTest(MessageBoxResult messageBoxResult = MessageBoxResult.None, bool saveDialogResult = false)
        {
            MockDialogWindowWrapper = new Mock<IDialogWindowWrapper>();
            MockDialogWindowWrapper.Setup(x => x.MessageBoxWrapper.Show(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>()))
                .Returns(messageBoxResult);
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.SaveFileDialog)
                .Returns(new SaveFileDialog() { FileName = String.Empty });
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.ShowDialog())
                .Returns(saveDialogResult);
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.FilterIndex)
                .Returns(1);

            MockTextFormatterWrapper = new Mock<ITextFormatterWrapper>();
            MockTextFormatterWrapper.Setup(x => x.ExtractFileNameFromPath(It.IsAny<string>()))
                .Returns("Test Window Title");

            MockSerializeCharacterWrapper = new Mock<ISerializeCharacterWrapper>();
            MockSerializeCharacterWrapper.Setup(x => x.SaveCharacterToFileJSON(It.IsAny<ICharacterModel>()));

            CharacterCreatorViewModel = new CharacterCreatorViewModel(MockDialogWindowWrapper.Object,
                new StaticClassWrapper(MockTextFormatterWrapper.Object, MockSerializeCharacterWrapper.Object), new OpenNewViewWrapper(new WindowWrapper()));
        }

        private void ChangeCharacterStrValue(int str)
            => CharacterCreatorViewModel.Character.MainStats.Str = str;
    }
}
