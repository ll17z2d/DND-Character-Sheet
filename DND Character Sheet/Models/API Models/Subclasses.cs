using System.Collections.Generic;

namespace DND_Character_Sheet.Models.API_Models
{
    public class Subclasses
    {
        public string index { get; set; }
        public Classes @class { get; set; }
        public string name { get; set; }
        public string subclass_flavor { get; set; }
        public string desc { get; set; }
        public List<object> spells { get; set; }
        public string subclass_levels { get; set; }
        public string url { get; set; }
    }
}
