using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.ViewModels;
using DND_Character_Sheet.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DND_Character_Sheet.Wrappers
{
    public interface IWindowWrapper
    {
        public void Show(IView window);

        public void Close(IView window);

        public IView GetDetailsView(DetailsStats detailsStats, Action<object, CancelEventArgs> removeWindowFromActiveList);

        public IView GetNotesView(CharacterNotes characterNotes, string characterFilePath,
            IDialogWindowWrapper dialogWindowWrapper, Action<object, CancelEventArgs> removeWindowFromActiveList);

        public IView GetSpellsView(AllSpells allSpells, Action<object, CancelEventArgs> removeWindowFromActiveList);

        public IView GetSkillsView(AllSkills allSkills, bool isReadOnly, Action<object, CancelEventArgs> removeWindowFromActiveList);

        public bool GetSpellDetailsView(Spell spell);

        public bool GetCharacterCreatorView(IDialogWindowWrapper dialogWindowWrapper, IStaticClassWrapper staticClassWrapper,
            IOpenNewViewWrapper openNewViewWrapper);

        public bool GetCharacterSheetView(ICharacterModel character, IDialogWindowWrapper dialogWindowWrapper,
            IStaticClassWrapper staticClassWrapper, IOpenNewViewWrapper openNewViewWrapper);
    }

    public class WindowWrapper : IWindowWrapper
    {
        public void Show(IView window) 
            => window.Show();

        public void Close(IView window)
            => window.Close();

        public IView GetDetailsView(DetailsStats detailsStats, Action<object, CancelEventArgs> removeWindowFromActiveList)
            => new DetailsDialogView(new DetailsDialogViewModel(detailsStats), removeWindowFromActiveList);

        public IView GetNotesView(CharacterNotes characterNotes, string characterFilePath,
            IDialogWindowWrapper dialogWindowWrapper, Action<object, CancelEventArgs> removeWindowFromActiveList) 
            => new NotesDialogView(
                new NotesDialogViewModel(characterNotes, characterFilePath, dialogWindowWrapper),
                removeWindowFromActiveList);

        public IView GetSkillsView(AllSkills allSkills, bool isReadOnly, Action<object, CancelEventArgs> removeWindowFromActiveList)
            => new SkillsDialogView(new SkillsDialogViewModel(allSkills, isReadOnly), removeWindowFromActiveList);

        public IView GetSpellsView(AllSpells allSpells, Action<object, CancelEventArgs> removeWindowFromActiveList) 
            => new SpellsDialogView(new SpellsDialogViewModel(allSpells),
                removeWindowFromActiveList);

        public bool GetSpellDetailsView(Spell spell) 
            => (bool)new SpellDetailsView(spell).ShowDialog();

        public bool GetCharacterCreatorView(IDialogWindowWrapper dialogWindowWrapper, IStaticClassWrapper staticClassWrapper, IOpenNewViewWrapper openNewViewWrapper) 
            => (bool)new CharacterCreatorView(new CharacterCreatorViewModel(dialogWindowWrapper, 
                staticClassWrapper, openNewViewWrapper)).ShowDialog();

        public bool GetCharacterSheetView(ICharacterModel character, IDialogWindowWrapper dialogWindowWrapper, IStaticClassWrapper staticClassWrapper, IOpenNewViewWrapper openNewViewWrapper)
            => (bool)new CharacterSheetView(new CharacterSheetViewModel(character, dialogWindowWrapper,
                staticClassWrapper, openNewViewWrapper)).ShowDialog();
    }
}
