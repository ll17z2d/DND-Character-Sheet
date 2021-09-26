using System;
using System.Collections.Generic;
using System.Text;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class HPStats
    {
        public HPStats() : this(0, 0, 0) { }
        public HPStats(int MaxHP, int CurrentHP, int TempHP)
        {
            this.MaxHP = MaxHP;
            this.CurrentHP = CurrentHP;
            this.TempHP = TempHP;
        }

        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int TempHP { get; set; }
    }
}
