using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class Spell
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPrepared { get; set; }

    }
}
