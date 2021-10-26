using System;
using System.Collections.Generic;
using System.Text;

namespace DND_Character_Sheet.Models.Common_Models
{
    public class Traits
    {
        public string index { get; set; }
        public List<Proficiency> races { get; set; }
        public List<Proficiency> subraces { get; set; }
        public string name { get; set; }
        public List<string> desc { get; set; }
        public List<Proficiency> proficiencies { get; set; }
        public List<ChoiceVerString> proficiency_choices { get; set; }
        public string url { get; set; }
    }
}
