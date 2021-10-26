using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DND_Character_Sheet.Models.Common_Models;

namespace DND_Character_Sheet.Models.Common_API_Models
{
    public class Damage
    {
        public string damage_dice { get; set; }
        public APIReference damage_type { get; set; }
        public Dictionary<string,string> damage_at_slot_level { get; set; }
    }
}
