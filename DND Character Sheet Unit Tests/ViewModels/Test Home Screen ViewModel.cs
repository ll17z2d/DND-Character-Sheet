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
    public class TestHomeScreenViewModel
    {
        public HomeScreenViewModel HomeScreenViewModel { get; set; }
        public Mock<IDialogWindowWrapper> MockDialogWindowWrapper { get; set; }
        public Mock<IStaticClassWrapper> MockStaticClassWrapper { get; set; }
        public Mock<IOpenNewViewWrapper> MockOpenNewViewWrapper { get; set; }

        [TestMethod]
        public void OpenCharacter_Successful()
        {
            //arrange
            var expected1 = true;
            var expected2 = 1;
            GetUnderTest();
            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.OpenFileDialog).Returns(new OpenFileDialog());
            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.ShowDialog()).Returns(true);
            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.FilterIndex).Returns(1);
            MockOpenNewViewWrapper.Setup(x => x.OpenCharacterSheetWindow(It.IsAny<ICharacterModel>(), 
                    It.IsAny<IDialogWindowWrapper>(), It.IsAny<IStaticClassWrapper>(), 
                    It.IsAny<IOpenNewViewWrapper>()))
                .Returns(true);
            MockStaticClassWrapper.Setup(x => x.SerializeCharacterWrapper.OpenCharacterFromFileJSON(It.IsAny<string>()))
                .Returns(new CharacterModel());

            //act
            var actual = HomeScreenViewModel.OpenCharacter();

            //assert
            Assert.AreEqual(expected1, actual);
            MockDialogWindowWrapper.Verify(x => x.OpenFileDialogWrapper.ShowDialog(), Times.Exactly(expected2));
        }

        [TestMethod]
        public void OpenCharacter_Cancelled()
        {
            //arrange
            var expected1 = false;
            var expected2 = 1;
            GetUnderTest();
            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.OpenFileDialog).Returns(new OpenFileDialog());
            MockDialogWindowWrapper.Setup(x => x.OpenFileDialogWrapper.ShowDialog()).Returns(false);

            //act
            var actual = HomeScreenViewModel.OpenCharacter();

            //assert
            Assert.AreEqual(expected1, actual);
            MockDialogWindowWrapper.Verify(x => x.OpenFileDialogWrapper.ShowDialog(), Times.Exactly(expected2));
        }

        [TestMethod]
        public void NewCharacter_Successful()
        {
            //arrange
            var expected1 = true;
            var expected2 = 1;
            GetUnderTest();
            MockOpenNewViewWrapper.Setup(x => x.OpenCharacterCreatorWindow(It.IsAny<IDialogWindowWrapper>(),
                It.IsAny<IStaticClassWrapper>(), It.IsAny<IOpenNewViewWrapper>())).Returns(true);

            //act
            var actual = HomeScreenViewModel.NewCharacter();

            //assert
            Assert.AreEqual(expected1, actual);
            MockOpenNewViewWrapper.Verify(x => x.OpenCharacterCreatorWindow(It.IsAny<IDialogWindowWrapper>(),
                It.IsAny<IStaticClassWrapper>(), It.IsAny<IOpenNewViewWrapper>()), Times.Exactly(expected2));
        }

        public void GetUnderTest()
        {
            MockDialogWindowWrapper = new Mock<IDialogWindowWrapper>();

            MockStaticClassWrapper = new Mock<IStaticClassWrapper>();

            MockOpenNewViewWrapper = new Mock<IOpenNewViewWrapper>();

            HomeScreenViewModel = new HomeScreenViewModel(MockDialogWindowWrapper.Object, 
                MockStaticClassWrapper.Object, MockOpenNewViewWrapper.Object);
        }
    }
}
