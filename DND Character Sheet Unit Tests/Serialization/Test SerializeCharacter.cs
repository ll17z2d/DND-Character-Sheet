﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Serialization;
using DND_Character_Sheet.ViewModels;
using DND_Character_Sheet.Wrappers;
using DND_Character_Sheet_Unit_Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using Moq;

namespace DND_Character_Sheet_Unit_Tests.Serialization
{
    [TestClass]
    public class TestSerializeCharacter
    {
        public Mock<IDialogWindowWrapper> MockDialogWindowWrapper { get; set; }

        public Mock<ISerializeCharacterWrapper> MockSerializeCharacterWrapper { get; set; }

        public Mock<IFileOperationsWrapper> MockFileOperationsWrapper { get; set; }

        public Mock<IJsonConvertWrapper> MockJsonConvertWrapper { get; set; }

        public CharacterCreatorViewModel CharacterCreatorViewModel { get; set; }

        public ICharacterModel CharacterModel { get; set; }

        public string TempFilePath { get; set; } = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "Newly Created Character.json");

        [TestMethod]
        public void OpenCharacterFromFile_TestDeserializeSpells()
        {
            //arrange
            var expected = true;
            GetUnderTest_DeserializedSpells();
            MockJsonConvertWrapper.Setup(x => x.DeserializeObject<ICharacterModel>(It.IsAny<string>())).Returns(CharacterModel);
            MockFileOperationsWrapper.Setup(x => x.FileReadAllText(It.IsAny<string>())).Returns(string.Empty);

            //act
            var actual = SerializeCharacter.OpenCharacterFromFileJSON(It.IsAny<string>(), 
                MockJsonConvertWrapper.Object, MockFileOperationsWrapper.Object);

            //assert
            Assert.AreEqual(expected, new ObjectsComparer.Comparer<ICharacterModel>().Compare(new CharacterModel(), actual, out var dif));
        }

        [TestMethod]
        public void OpenCharacterFromFileJSON_Successful()
        {
            //arrange
            var expected = true;
            var character = TestData.GetInitialisedCharacterModel();
            GetUnderTest();

            //act
            CharacterCreatorViewModel.Character = character;
            CharacterCreatorViewModel.Character.FilePath = TempFilePath;
            CharacterCreatorViewModel.SaveCharacter();

            CharacterCreatorViewModel.OpenCharacter();
            File.Delete(TempFilePath);

            //assert
            Assert.AreEqual(expected, new ObjectsComparer.Comparer<ICharacterModel>().Compare(character, CharacterCreatorViewModel.Character, out var dif));
        }

        private void GetUnderTest_DeserializedSpells()
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

        private void GetUnderTest()
        {
            MockDialogWindowWrapper = new Mock<IDialogWindowWrapper>();
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.SaveFileDialog).Returns(new SaveFileDialog());
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.ShowDialog()).Returns(true);
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.FilterIndex).Returns(1);
            MockDialogWindowWrapper.Setup(x => x.SaveFileDialogWrapper.FileName).Returns(TempFilePath);

            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.OpenFileDialog).Returns(new OpenFileDialog());
            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.ShowDialog()).Returns(true);
            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.FilterIndex).Returns(1);
            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.FileName).Returns(TempFilePath);

            var MockWindowWrapper = new Mock<IOpenNewViewWrapper>();
            MockWindowWrapper.Setup(x => x.CloseAllSubWindows());

            CharacterCreatorViewModel = new CharacterCreatorViewModel(MockDialogWindowWrapper.Object,
                new StaticClassWrapper(new TextFormatterWrapper(), new SerializeCharacterWrapper()),
                MockWindowWrapper.Object);
        }
    }
}
