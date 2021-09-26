using System;
using System.Collections.Generic;
using System.Text;

namespace DND_Character_Sheet.Models.Common_Models
{
    public class Equipment_Categories
    {
        public string index { get; set; }
        public string name { get; set; }
        public List<APIReference> equipment { get; set; }
    }
}
