using System.Collections.Generic;
using DND_Character_Sheet.Models.Common_Models;

namespace DND_Character_Sheet.Models.API_Models
{
    public class Subraces
    {
        public string index { get; set; }
        public string name { get; set; }
        public int race { get; set; }
        public string desc { get; set; }
        public List<Ability_Bonus> ability_bonuses { get; set; }
        public List<Proficiency> starting_proficiencies { get; set; }
        public List<Proficiency> languages { get; set; }
        public List<Traits> traits { get; set; }
        public string url { get; set; }
    }
}
