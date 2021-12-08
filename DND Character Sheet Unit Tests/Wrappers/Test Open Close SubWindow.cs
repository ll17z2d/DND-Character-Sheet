using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Views;
using DND_Character_Sheet.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using Moq;
using System;
using System.ComponentModel;

namespace DND_Character_Sheet_Unit_Tests.Wrappers
{
    [TestClass]
    public class Test_Open_Close_SubWindow
    {
        public IOpenNewViewWrapper OpenNewViewWrapper { get; set; }
        public Mock<IWindowWrapper> MockWindowWrapper { get; set; }

        [TestMethod]
        public void ActiveSubWindows_EmptyWhenWrapperInitialised()
        {
            //arrange
            GetUnderTest();

            //act
            var numActiveSubWindows = OpenNewViewWrapper.ActiveSubWindows.Count;

            //assert
            Assert.IsFalse(numActiveSubWindows == 0);
        }

        [TestMethod]
        public void ActiveSubWindows_ListSizeOneWhenOpenNotesWindow()
        {
            //arrange
            GetUnderTest();

            //act
            var isNotesWindowOpened = OpenNewViewWrapper.OpenNotesWindow(new CharacterNotes(), string.Empty,
                new DialogWindowWrapper(
                    new OpenFileDialogWrapper(new OpenFileDialog()),
                    new SaveFileDialogWrapper(new SaveFileDialog()),
                    new MessageBoxWrapper()));

            //assert
            Assert.IsTrue(OpenNewViewWrapper.ActiveSubWindows.Count == 1);
            Assert.IsTrue(isNotesWindowOpened);
        }

        [TestMethod]
        public void ActiveSubWindows_ListSizeOneWhenOpenSkillsWindow()
        {
            //arrange
            GetUnderTest();

            //act
            var isSkillsWindowOpened = OpenNewViewWrapper.OpenSkillsWindow(new AllSkills(), false);

            //assert
            Assert.IsTrue(OpenNewViewWrapper.ActiveSubWindows.Count == 1);
            Assert.IsTrue(isSkillsWindowOpened);
        }

        [TestMethod]
        public void ActiveSubWindows_ListSizeOneWhenOpenSpellsWindow()
        {
            //arrange
            GetUnderTest();

            //act
            var isSpellsWindowOpened = OpenNewViewWrapper.OpenSpellsWindow(new AllSpells());

            //assert
            Assert.IsTrue(OpenNewViewWrapper.ActiveSubWindows.Count == 1);
            Assert.IsTrue(isSpellsWindowOpened);
        }

        [TestMethod]
        public void CloseAllSubWindows_NoCrashWhenCloseAllSubWindowsCalledOnEmptyActiveSubWindows()
        {
            //arrange
            GetUnderTest();

            //act
            OpenNewViewWrapper.CloseAllSubWindows();

            //assert
            Assert.IsTrue(OpenNewViewWrapper.ActiveSubWindows.Count == 0);
        }

        [TestMethod]
        public void CloseAllSubWindows_EmptyActiveSubWindowsWhenSubWindowsCalled()
        {
            //arrange
            GetUnderTest();
            OpenNewViewWrapper.OpenNotesWindow(new CharacterNotes(), string.Empty,
                new DialogWindowWrapper(
                    new OpenFileDialogWrapper(new OpenFileDialog()),
                    new SaveFileDialogWrapper(new SaveFileDialog()),
                    new MessageBoxWrapper()));
            OpenNewViewWrapper.OpenSkillsWindow(new AllSkills(), false);
            OpenNewViewWrapper.OpenSpellsWindow(new AllSpells());

            //act
            OpenNewViewWrapper.CloseAllSubWindows();

            //assert
            Assert.IsTrue(OpenNewViewWrapper.ActiveSubWindows.Count == 0);
        }

        private void GetUnderTest()
        {
            MockWindowWrapper = new Mock<IWindowWrapper>();
            MockWindowWrapper.Setup(x => x.Show(It.IsAny<IView>()));
            MockWindowWrapper.Setup(x => x.Close(It.IsAny<IView>()));
            MockWindowWrapper.Setup(x => x.GetNotesView(It.IsAny<CharacterNotes>(), It.IsAny<string>(), 
                It.IsAny<IDialogWindowWrapper>(), It.IsAny<Action<object, CancelEventArgs>>()))
                .Returns(new Mock<IView>().Object);
            MockWindowWrapper.Setup(x => x.GetSkillsView(It.IsAny<AllSkills>(), It.IsAny<bool>(),
                It.IsAny<Action<object, CancelEventArgs>>()))
                .Returns(new Mock<IView>().Object);
            MockWindowWrapper.Setup(x => x.GetSpellsView(It.IsAny<AllSpells>(), 
                It.IsAny<Action<object, CancelEventArgs>>()))
                .Returns(new Mock<IView>().Object);

            OpenNewViewWrapper = new OpenNewViewWrapper(MockWindowWrapper.Object);
        }
    }
}
