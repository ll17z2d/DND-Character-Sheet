namespace DND_Character_Sheet.Models.API_Models
{
    public class WeaponsInventory
    {
        public string WeaponName { get; set; }
        public string AttackBonus { get; set; }
        public string Damage { get; set; }

        public WeaponsInventory() : this("", "", "") { }

        public WeaponsInventory(string weaponName, string attackBonus, string damage)
        {
            WeaponName = weaponName;
            AttackBonus = attackBonus;
            Damage = damage;
        }
    }
}
