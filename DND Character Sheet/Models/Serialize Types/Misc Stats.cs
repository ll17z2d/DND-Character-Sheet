using System;
using System.Collections.Generic;
using System.Text;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class MiscStats
    {
        public MiscStats() : this("0", 0, "0", "0") { }

        public MiscStats(string Proficiency, int AC, string Initiative, string Speed)
        {
            this.Proficiency = Proficiency;
            this.AC = AC;
            this.Initiative = Initiative;
            this.Speed = Speed;
        }

        public string Proficiency { get; set; }
        public int AC { get; set; }
        public string Initiative { get; set; }
        public string Speed { get; set; }
    }
}
