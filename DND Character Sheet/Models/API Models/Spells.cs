using System.Collections.Generic;
using DND_Character_Sheet.Models.Common_API_Models;
using DND_Character_Sheet.Models.Common_Models;

namespace DND_Character_Sheet.Models.API_Models
{
    public class Spells
    {
        public string index { get; set; }
        public string name { get; set; }
        public List<string> desc { get; set; }
        public List<string> higher_level { get; set; }
        public string range { get; set; }
        public List<string> components { get; set; }
        public string material { get; set; }
        public bool ritual { get; set; }
        public string duration { get; set; }
        public bool concentration { get; set; }
        public string casting_time { get; set; }
        public int level { get; set; }
        public string attack_type { get; set; }
        public Damage damage { get; set; }
        public Magic_Schools school { get; set; }
        public List<APIReference> classes { get; set; }
        public List<Classes> subclasses { get; set; }
        public string url { get; set; }

    }
}
