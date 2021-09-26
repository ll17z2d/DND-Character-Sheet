using System;
using System.Collections.Generic;
using System.Text;

namespace DND_Character_Sheet.Models.Common_Models
{
    public class ChoiceVerString
    {
        public int choose { get; set; }
        public string type { get; set; }
        public List<string> from { get; set; }
    }

    public class ChoiceVerAPIRef
    {
        public int choose { get; set; }
        public string type { get; set; }
        public List<APIReference> from { get; set; }
    }
}
