using System.Collections.Generic;
using DND_Character_Sheet.APICommunication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DND_Character_Sheet_Unit_Tests.API_Communication
{
    [TestClass]
    public class Test_APICommunicator //TODO:Seems like I'll have to rethink + refactor the way I did the APICommunicator class bc there are several dependencies that aren't DI'd which make testing the class much harder. I've injected the one for the call to Endpoint which works. look into the answers at https://softwareengineering.stackexchange.com/questions/5757/is-static-universally-evil-for-unit-testing-and-if-so-why-does-resharper-recom
    {
        public APICommunicator APICommunicator { get; set; }
        public Mock<IAPICommunicator> APICommunicatorMock { get; set; }

        private void GetUnderTest(string selectedSearchType, string searchTextbox)
        {
            APICommunicator = new APICommunicator(selectedSearchType, searchTextbox);
        }

        private void GetUnderTestMoq(string SelectedSearchType, string SearchTextbox)
        {
            APICommunicatorMock = new Mock<IAPICommunicator>();
            APICommunicatorMock.Setup(x => x.SearchTextbox).Returns(SearchTextbox);
            APICommunicatorMock.Setup(x => x.SelectedSearchType).Returns(SelectedSearchType);
        }

        [TestMethod]
        public void GetJson_EmptyTextBox()
        {
            //arrange
            string SelectedSearchType = null;
            string SearchTextbox = "test";
            var expected1 = new List<string>()
            {
                "Oops! That wasn't a valid search option, please try again"
            };
            var expected2 = false;

            //act
            GetUnderTest(SelectedSearchType, SearchTextbox);
            var actual = APICommunicator.GetJson();

            //assert
            Assert.AreEqual(expected1[0], actual.Item1[0]);
            Assert.AreEqual(expected2, actual.Item2);
        }

        [TestMethod]
        public void GetJson_InvalidTextBox()
        {
            //arrange
            string SelectedSearchType = "test";
            string SearchTextbox = "test";
            var expected1 = new List<string>()
            {
                "Oops! That wasn't a valid search option, please try again"
            };
            var expected2 = false;

            //act
            GetUnderTest(SelectedSearchType, SearchTextbox);
            APICommunicator.FinalEndpoint = null;
            var actual = APICommunicator.GetJson();

            //assert
            Assert.AreEqual(expected1[0], actual.Item1[0]);
            Assert.AreEqual(expected2, actual.Item2);
        }

        [TestMethod]
        public void GetJson_FailedEndpointAccess()
        {
            //arrange
            string SelectedSearchType = "Backgrounds";
            string SearchTextbox = "test";
            var expected1 = new List<string>()
            {
                "Oops! I couldn't search that up for you. Check your spelling is correct and that your search type and search term match"
            };
            var expected2 = false;

            //act
            GetUnderTest(SelectedSearchType, SearchTextbox);
            var actual = APICommunicator.GetJson();

            //assert
            Assert.AreEqual(expected1[0], actual.Item1[0]);
            Assert.AreEqual(expected2, actual.Item2);
        }


        //Use Moq for when we want to add complex types to the constructor of the class we're testing, https://stackoverflow.com/a/49740489 has good examples using an IPrinter class

        //[TestMethod]
        //public void GetJson_InvalidTextBoxMoqTemp()
        //{
        //    //arrange
        //    string SelectedSearchType = "test";
        //    string SearchTextbox = "test";
        //    var expected1 = new List<string>()
        //    {
        //        "Oops! I couldn't search that up for you. Check your spelling is correct and that your search type and search term match"
        //    };
        //    var expected2 = false;
        //    GetUnderTestMoq(SelectedSearchType, SearchTextbox);
        //    APICommunicatorMock.Setup(x => x.SearchTextbox).Returns("test");
        //    //APICommunicatorMock.Setup(x => x.FinalEndpoint).Returns("test");

        //    //act
        //    var actual = APICommunicator.GetJson();


        //    //assert
        //    APICommunicatorMock.Verify(x => x.GetJson());

        //    Assert.AreEqual(expected1[0], actual.Item1[0]);
        //    Assert.AreEqual(expected2, actual.Item2);
        //}
    }
}
