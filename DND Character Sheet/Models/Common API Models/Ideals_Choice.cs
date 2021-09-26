using System;
using System.Collections.Generic;
using System.Text;

namespace DND_Character_Sheet.Models.Common_Models
{
    public class Ideals_Choice
    {
        public int choose { get; set; }
        public string type { get; set; }
        public List<Ideals_From> from { get; set; } //APIReference
    }
}
