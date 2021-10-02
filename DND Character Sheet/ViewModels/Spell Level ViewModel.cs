using System.Collections.ObjectModel;
using DND_Character_Sheet.Models.Serialize_Types;

namespace DND_Character_Sheet.ViewModels
{
    public class SpellLevelViewModel
    {
        public ObservableCollection<Spell> Spells { get; set; }

        public int SpellLevel { get; set; }

        public int SlotsTotal { get; set; }

        public int SlotsExpended { get; set; }

        public SpellLevelViewModel(int numOfSpells, int spellLevel, int slotsTotal, int slotsExpended)
        {
            SpellLevel = spellLevel;
            SlotsTotal = slotsTotal;
            SlotsExpended = slotsExpended;
            Spells = new ObservableCollection<Spell>();

            for (int i = 0; i < numOfSpells; i++)
            {
                Spells.Add(new Spell());
            }

            foreach (var spell in Spells)
            {
                spell.Name = "Test";
                spell.Description = "";
            }
        }
    }
}
