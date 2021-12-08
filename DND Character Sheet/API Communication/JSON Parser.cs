using System;
using DND_Character_Sheet.Enums;

namespace DND_Character_Sheet.APICommunication
{
    public static class EnumParser
    {
        public static DND_Search_Types ParseEnum(string selectedsearchtype)
        {
            Enum.TryParse<DND_Search_Types>(selectedsearchtype, out var searchtypeenum);
            return searchtypeenum;
        }
    }
}
