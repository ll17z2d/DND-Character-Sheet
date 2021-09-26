using System;
using System.Collections.Generic;
using System.Text;

namespace DND_Character_Sheet.Constants
{
    public static class DiceStrings
    {
        public static string d2 = "1d2";
        public static string d4 = "1d4";
        public static string d5 = "1d5";
        public static string d6 = "1d6";
        public static string d7 = "1d7";
        public static string d8 = "1d8";
        public static string d10 = "1d10";
        public static string d12 = "1d12";
        public static string d20 = "1d20";
        public static string d48 = "1d48";
        public static string d100 = "1d100";

        public static List<string> AllDice = new List<string>()
        {
            d2,
            d4,
            d5,
            d6,
            d7,
            d8,
            d10,
            d12,
            d20,
            d48,
            d100
        };
    }
}
