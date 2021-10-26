using System.Collections.Generic;
using DND_Character_Sheet.Models.Common_Models;

namespace DND_Character_Sheet.Models.API_Models
{
    public class Features
    {
        public string index { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public ClassAPIReference @class { get; set; }
        public List<string> desc { get; set; }
    }
}
