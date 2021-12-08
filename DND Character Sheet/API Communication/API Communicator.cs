using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DND_Character_Sheet.APICommunication;
using DND_Character_Sheet.Enums;
using DND_Character_Sheet.Serialization;
using DND_Character_Sheet.Wrappers;
using Newtonsoft.Json;
using Testing_Raxer_Chroma_SDK;

namespace DND_Character_Sheet.APICommunication
{
    public interface IAPICommunicator
    {
        private const string BaseEndpoint = "https://www.dnd5eapi.co/api/";

        public string SelectedSearchType { get; set; }
        public string SearchTextbox { get; set; }
        public string FinalEndpoint { get; set; }

        public (List<string>, bool) GetJson();
    }
    public class APICommunicator : IAPICommunicator
    {
        private const string BaseEndpoint = "https://www.dnd5eapi.co/api/";

        public string SelectedSearchType { get; set; }
        public string SearchTextbox { get; set; }
        public string FinalEndpoint { get; set; }

        public APICommunicator(string selectedSearchType, string searchTextbox)
        {
            SelectedSearchType = selectedSearchType;
            SearchTextbox = searchTextbox;
            FinalEndpoint = Endpoint_Maker.CreateEndpoint(selectedSearchType, searchTextbox, BaseEndpoint);
        }

        public (List<string>, bool) GetJson()
        {
            if (FinalEndpoint != null)
            {
                var outputJson = AccessRESTAPI(FinalEndpoint);
                if (outputJson == "{\"errorMessages\":[\"The remote server returned an error: (404) Not Found.\"],\"errors\":{}}")
                {
                    return (
                        new List<string>()
                        {
                            "Oops! I couldn't search that up for you. Check your spelling is correct and that your search type and search term match"
                        }, false);
                }

                if (outputJson == "\"{\\\"errorMessages\\\":[\\\"No such host is known. No such host is known.\\\"],\\\"errors\\\":{}}\"")
                {
                    return (
                        new List<string>()
                        {
                            "Sorry, the search service is having some issues at the moment, please check your internet connection and try again later"
                        }, false);
                }

                return DeserializeJSON(outputJson, EnumParser.ParseEnum(SelectedSearchType));
            }

            return (
                new List<string>()
                {   
                    "Oops! That wasn't a valid search option, please try again"
                }, false);
        }

        private (List<string>, bool) DeserializeJSON(string outputJson, DND_Search_Types searchtypeenum) 
            => new DeserializeAPI(searchtypeenum, outputJson, new TextFormatterWrapper()).Deserialize();

        private string AccessRESTAPI(string endpoint)
            => new RESTClient(endpoint, httpVerb.GET).makeRequest();
    }
}
