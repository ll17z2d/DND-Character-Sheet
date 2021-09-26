using System;
using System.Collections.Generic;
using System.Text;

namespace DND_Character_Sheet.Models.Common_Models
{
    public class Starting_Equipment_Options_Choice
    {
        public int choose { get; set; }
        public string type { get; set; }
        public List<APIReference> from { get; set; } //APIReference
    }
}
