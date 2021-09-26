using System.Collections.Generic;

namespace DND_Character_Sheet.Models.API_Models
{
    public class Conditions
    {
        public string index { get; set; }
        public string name { get; set; }
        public List<string> desc { get; set; }
        public string url { get; set; }
    }
}
