using System;
using System.Collections.Generic;
using System.Text;

namespace DND_Character_Sheet.Models.Common_Models
{
    public class Ability_Bonus
    {
        public int bonus { get; set; }
        public APIReference ability_score { get; set; }
    }
}
