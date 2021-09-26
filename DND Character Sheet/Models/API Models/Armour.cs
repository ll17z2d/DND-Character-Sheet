using DND_Character_Sheet.Models.Common_Models;

namespace DND_Character_Sheet.Models.API_Models
{
    public class Armour
    {
        public string index { get; set; }
        public string name { get; set; }
        public Equipment_Categories equipment_category { get; set; }
        public string armor_category { get; set; }
        public object armor_class { get; set; }
        public int str_minimum { get; set; }
        public bool stealth_disadvantage { get; set; }
        public int weight { get; set; }
        public Cost cost { get; set; }
        public string url { get; set; }
    }
}
