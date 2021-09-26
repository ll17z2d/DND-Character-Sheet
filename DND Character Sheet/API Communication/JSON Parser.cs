using System;
using System.Collections.Generic;
using System.Text;
using DND_Character_Sheet.Enums;
using DND_Character_Sheet.Serialization;

namespace DND_Character_Sheet.API_Communication
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
