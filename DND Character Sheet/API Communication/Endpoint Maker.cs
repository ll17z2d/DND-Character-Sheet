using System;
using DND_Character_Sheet.Enums;

namespace DND_Character_Sheet.APICommunication
{
    public class Endpoint_Maker
    {
        public static string CreateEndpoint(string searchtype, string searchtextbox, string endpoint)
        {
            var baseendpoint = endpoint;
            var formattedsearchtextbox = FormatSearchTextbox(searchtextbox);

            if (Enum.TryParse(searchtype, out DND_Search_Types searchtypeenum))
            {
                switch (searchtypeenum)
                {
                    case DND_Search_Types.Backgrounds:
                    case DND_Search_Types.Classes:
                    case DND_Search_Types.Subclasses:
                    case DND_Search_Types.Features:
                    case DND_Search_Types.Races:
                    case DND_Search_Types.Subraces:
                    case DND_Search_Types.Conditions:
                    case DND_Search_Types.Spells:
                        baseendpoint = string.Concat(baseendpoint, searchtypeenum.ToString().ToLower());
                        break;
                    case DND_Search_Types.Armour:
                    case DND_Search_Types.Weapons:
                        baseendpoint = string.Concat(baseendpoint, "equipment");
                        break;
                }
                return string.Concat(baseendpoint, "/", formattedsearchtextbox);
            }
            return null;
        }

        private static string FormatSearchTextbox(string searchtextbox)
            => searchtextbox.Trim().ToLower().Replace(" ", "-");
    }
}
