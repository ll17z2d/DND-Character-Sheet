using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DND_Character_Sheet.Commands;
using DND_Character_Sheet.Wrappers;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class Spell
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPrepared { get; set; }

        public IOpenNewViewWrapper OpenNewViewWrapper { get; set; }

        public ICommand OpenSpellDetailsCommand { get; set; }

        public Spell() : this("Enter Spell Here", "Press '...' to edit spell details", false, new OpenNewViewWrapper()) { }

        private Spell(string name, string description, bool isPrepared, IOpenNewViewWrapper openNewViewWrapper)
        {
            Name = name;
            Description = description;
            IsPrepared = isPrepared;
            OpenNewViewWrapper = openNewViewWrapper;
            OpenSpellDetailsCommand = new MethodCommands(OpenSpellDetails);
        }

        public bool OpenSpellDetails() 
            => OpenNewViewWrapper.OpenSpellDetailsWindow(this);
    }
}
