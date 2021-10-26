using System.Collections.Generic;
using DND_Character_Sheet.Models.Common_API_Models;
using DND_Character_Sheet.Models.Common_Models;

namespace DND_Character_Sheet.Models.API_Models
{
    public class Weapons
    {
        public string index { get; set; }
        public string name { get; set; }
        public Equipment_Categories equipment_category { get; set; }
        public string weapon_category { get; set; }
        public string weapon_range { get; set; }
        public string category_range { get; set; }
        public Cost cost { get; set; }
        public Damage damage { get; set; }
        public object two_handed_damage { get; set; }
        public Range range { get; set; }
        public int weight { get; set; }
        public List<Weapon_Property> properties { get; set; }
        public string url { get; set; }
    }
}
