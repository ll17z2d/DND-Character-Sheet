namespace DND_Character_Sheet.Models.API_Models
{
    public class Weapons_Inventory
    {
        public string WeaponName { get; set; }
        public string AttackBonus { get; set; }
        public string Damage { get; set; }

        public Weapons_Inventory() : this("", "", "") { }

        public Weapons_Inventory(string weaponName, string attackBonus, string damage)
        {
            WeaponName = weaponName;
            AttackBonus = attackBonus;
            Damage = damage;
        }
    }
}
