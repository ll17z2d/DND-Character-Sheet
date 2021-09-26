using System.Collections.Generic;
using DND_Character_Sheet.Models.Common_Models;

namespace DND_Character_Sheet.Models.API_Models
{
    public class Classes
    {
        public string index { get; set; }
        public string name { get; set; }
        public int hit_die { get; set; }
        public List<ChoiceVerAPIRef> proficiency_choices { get; set; }
        public List<Proficiency> proficiencies { get; set; }
        public List<Ability_Score> saving_throws { get; set; }
        public List<Starting_Equipment> starting_equipment { get; set; }
        //public object starting_equipment { get; set; }
        public string class_levels { get; set; }
        public List<Subclasses> subclasses { get; set; }
        public Spellcasting spellcasting { get; set; }
        public string spells { get; set; }
        public string url { get; set; }
    }
}
