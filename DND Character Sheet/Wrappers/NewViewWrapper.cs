using System;
using System.Collections.Generic;
using System.Text;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.ViewModels;
using DND_Character_Sheet.Views;
// ReSharper disable PossibleInvalidOperationException

namespace DND_Character_Sheet.Wrappers
{
    public interface IOpenNewViewWrapper
    {
        public bool OpenNotesWindow(CharacterNotes characterNotes, string characterFilePath, IDialogWindowWrapper dialogWindowWrapper);

        public bool OpenSkillsWindow(AllSkills allSkills, bool isReadOnly);

        public bool OpenSpellsWindow(AllSpells allSpells);

        public bool OpenSpellDetailsWindow(Spell spell);

        public bool OpenCharacterCreatorWindow(IDialogWindowWrapper dialogWindowWrapper, IStaticClassWrapper staticClassWrapper, 
            IOpenNewViewWrapper openNewViewWrapper);

        public bool OpenCharacterSheetWindow(ICharacterModel character, IDialogWindowWrapper dialogWindowWrapper,
            IStaticClassWrapper staticClassWrapper, IOpenNewViewWrapper openNewViewWrapper);
    }

    public class OpenNewViewWrapper : IOpenNewViewWrapper
    {
        public bool OpenNotesWindow(CharacterNotes characterNotes, string characterFilePath, IDialogWindowWrapper dialogWindowWrapper) 
            => (bool)new NotesDialogView(new NotesDialogViewModel(characterNotes, characterFilePath, 
                dialogWindowWrapper)).ShowDialog();

        public bool OpenSkillsWindow(AllSkills allSkills, bool isReadOnly) 
            => (bool)new SkillsDialogView(new SkillsDialogViewModel(allSkills, isReadOnly)).ShowDialog();

        public bool OpenSpellsWindow(AllSpells allSpells) 
            => (bool)new SpellsDialogView(new SpellsDialogViewModel(allSpells)).ShowDialog();

        public bool OpenSpellDetailsWindow(Spell spell) 
            => (bool)new SpellDetailsView(spell).ShowDialog();

        public bool OpenCharacterCreatorWindow(IDialogWindowWrapper dialogWindowWrapper, IStaticClassWrapper staticClassWrapper,
            IOpenNewViewWrapper openNewViewWrapper) 
            => (bool)new CharacterCreatorView(new CharacterCreatorViewModel(dialogWindowWrapper, staticClassWrapper,
                openNewViewWrapper)).ShowDialog();

        public bool OpenCharacterSheetWindow(ICharacterModel character, IDialogWindowWrapper dialogWindowWrapper, 
            IStaticClassWrapper staticClassWrapper, IOpenNewViewWrapper openNewViewWrapper) 
            => (bool)new CharacterSheetView(new CharacterSheetViewModel(character, dialogWindowWrapper, 
                staticClassWrapper, openNewViewWrapper)).ShowDialog();
    }
}