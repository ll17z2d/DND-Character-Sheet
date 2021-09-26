using System.Collections.Generic;
using DND_Character_Sheet.Models.Common_Models;

namespace DND_Character_Sheet.Models.API_Models
{
    public class Backgrounds
    {
        public string index { get; set; }
        public string name { get; set; }
        public List<Proficiency> starting_proficiencies { get; set; }
        public List<Proficiency> languages { get; set; }
        public List<Starting_Equipment_Choice> starting_equipment { get; set; }
        public List<Starting_Equipment_Options_Choice> starting_equipment_options { get; set; }
        public Backgrounds_Feature feature { get; set; }
        public ChoiceVerString personality_traits { get; set; }
        public Ideals_Choice ideals { get; set; }
        public ChoiceVerString bonds { get; set; }
        public ChoiceVerString flaws { get; set; }
        public string url { get; set; }
    }
}
