using System;
using System.Collections.Generic;
using System.Text;

namespace DND_Character_Sheet.Models.Common_Models
{
    public class Spellcasting
    {
        public int level { get; set; }
        public Ability_Score spellcasting_ability { get; set; }
        public List<Backgrounds_Feature> info { get; set; }
    }
}
