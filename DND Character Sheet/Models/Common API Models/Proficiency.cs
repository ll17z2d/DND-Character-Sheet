using System;
using System.Collections.Generic;
using System.Text;
using DND_Character_Sheet.Models.API_Models;

namespace DND_Character_Sheet.Models.Common_Models
{
    public class Proficiency
    {
        public string index { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public List<Classes> classes { get; set; }
        public List<Races> races { get; set; }
        public string url { get; set; }
        public List<Equipment_Categories> references { get; set; }
    }
}
