using System;
using System.Collections.Generic;
using System.Text;
using DND_Character_Sheet.Models.API_Models;

namespace DND_Character_Sheet.Models.Common_Models
{
    public class Starting_Equipment
    {
        public string index { get; set; }
        public string name { get; set; }
        public APIReference equipment { get; set; }
        public int quantity { get; set; }
        public Classes @class { get; set; }
        public List<Weapons> starting_equipment { get; set; }
        public List<ChoiceVerString> starting_equipment_options { get; set; }
        public string url { get; set; }
    }
}
