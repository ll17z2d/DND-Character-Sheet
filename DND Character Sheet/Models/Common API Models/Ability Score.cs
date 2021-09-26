using System;
using System.Collections.Generic;
using System.Text;

namespace DND_Character_Sheet.Models.Common_Models
{
    public class Ability_Score
    {
        public string index { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public List<string> desc { get; set; }
        public List<APIReference> skills { get; set; }
        public string url { get; set; }
    }
}
