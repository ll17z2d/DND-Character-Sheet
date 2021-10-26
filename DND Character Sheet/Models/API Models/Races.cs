using System.Collections.Generic;
using DND_Character_Sheet.Models.Common_Models;

namespace DND_Character_Sheet.Models.API_Models
{
    public class Races
    {
        public string index { get; set; }
        public string name { get; set; }
        public int speed { get; set; }
        public List<Ability_Bonus> ability_bonuses { get; set; }
        public string alignment { get; set; }
        public string age { get; set; }
        public string size { get; set; }
        public string size_description { get; set; }
        public List<Proficiency> starting_proficiencies { get; set; }
        public List<Proficiency> languages { get; set; }
        public string language_desc { get; set; }
        public List<Traits> traits { get; set; }
        public List<Subraces> subraces { get; set; }
        public string url { get; set; }
    }
}
