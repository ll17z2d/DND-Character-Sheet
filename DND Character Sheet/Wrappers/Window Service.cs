using System;
using System.Collections.Generic;
using System.Text;
using DND_Character_Sheet.ViewModels;
using DND_Character_Sheet.Views;
// ReSharper disable PossibleInvalidOperationException

namespace DND_Character_Sheet.Wrappers
{
    public interface IWindowServiceWrapper
    {
        public bool OpenNotesWindow(NotesDialogViewModel notesDialogViewModel);

        public bool OpenSkillsWindow(SkillsDialogViewModel skillsDialogViewModel);

        public bool OpenCharacterCreatorWindow(CharacterCreatorViewModel characterCreatorViewModel);

        public bool OpenCharacterSheetWindow(CharacterSheetViewModel characterSheetViewModel);
    }

    public class WindowServiceWrapper : IWindowServiceWrapper
    {
        public bool OpenNotesWindow(NotesDialogViewModel notesDialogViewModel) 
            => (bool)new NotesDialogView(notesDialogViewModel).ShowDialog();

        public bool OpenSkillsWindow(SkillsDialogViewModel skillsDialogViewModel) 
            => (bool)new SkillsDialogView(skillsDialogViewModel).ShowDialog();

        public bool OpenCharacterCreatorWindow(CharacterCreatorViewModel characterCreatorViewModel) 
            => (bool)new CharacterCreatorView(characterCreatorViewModel).ShowDialog();

        public bool OpenCharacterSheetWindow(CharacterSheetViewModel characterSheetViewModel) 
            => (bool)new CharacterSheetView(characterSheetViewModel).ShowDialog();
    }
}
