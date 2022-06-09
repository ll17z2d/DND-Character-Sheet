using System;
using System.Collections.Generic;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Views;
using System.ComponentModel;
// ReSharper disable PossibleInvalidOperationException

namespace DND_Character_Sheet.Wrappers
{
    public interface IOpenNewViewWrapper
    {
        public List<IView> ActiveSubWindows { get; set; }

        public IWindowWrapper WindowWrapper { get; set; }

        public bool OpenDetailsWindow(DetailsStats detailsStats);

        public bool OpenNotesWindow(CharacterNotes characterNotes, string characterFilePath, IDialogWindowWrapper dialogWindowWrapper);

        public bool OpenSkillsWindow(AllSkills allSkills, bool isReadOnly);

        public bool OpenSpellsWindow(AllSpells allSpells);

        public bool OpenSpellDetailsWindow(Spell spell);

        public bool OpenCharacterCreatorWindow(IDialogWindowWrapper dialogWindowWrapper, IStaticClassWrapper staticClassWrapper, 
            IOpenNewViewWrapper openNewViewWrapper);

        public bool OpenCharacterSheetWindow(ICharacterModel character, IDialogWindowWrapper dialogWindowWrapper,
            IStaticClassWrapper staticClassWrapper, IOpenNewViewWrapper openNewViewWrapper);

        public void CloseAllSubWindows();
    }

    public class OpenNewViewWrapper : IOpenNewViewWrapper
    {
        public List<IView> ActiveSubWindows { get; set; } = new List<IView>();

        public IWindowWrapper WindowWrapper { get; set; }

        public OpenNewViewWrapper(IWindowWrapper windowWrapper) 
            => WindowWrapper = windowWrapper;

        public bool OpenDetailsWindow(DetailsStats detailsStats)
            => CanOpenSubWindow(WindowWrapper.GetDetailsView(detailsStats, RemoveActiveSubWindowAction()));

        public bool OpenNotesWindow(CharacterNotes characterNotes, string characterFilePath, IDialogWindowWrapper dialogWindowWrapper) 
            => CanOpenSubWindow(WindowWrapper.GetNotesView(characterNotes, characterFilePath, dialogWindowWrapper, 
                RemoveActiveSubWindowAction()));

        public bool OpenSkillsWindow(AllSkills allSkills, bool isReadOnly)
            => CanOpenSubWindow(WindowWrapper.GetSkillsView(allSkills, isReadOnly,
                RemoveActiveSubWindowAction()));

        public bool OpenSpellsWindow(AllSpells allSpells)
            => CanOpenSubWindow(WindowWrapper.GetSpellsView(allSpells,
                RemoveActiveSubWindowAction()));

        public bool OpenSpellDetailsWindow(Spell spell) 
            => WindowWrapper.GetSpellDetailsView(spell);

        public bool OpenCharacterCreatorWindow(IDialogWindowWrapper dialogWindowWrapper, IStaticClassWrapper staticClassWrapper,
            IOpenNewViewWrapper openNewViewWrapper)
            => WindowWrapper.GetCharacterCreatorView(dialogWindowWrapper, staticClassWrapper, openNewViewWrapper);

        public bool OpenCharacterSheetWindow(ICharacterModel character, IDialogWindowWrapper dialogWindowWrapper, 
            IStaticClassWrapper staticClassWrapper, IOpenNewViewWrapper openNewViewWrapper) 
            => WindowWrapper.GetCharacterSheetView(character, dialogWindowWrapper, staticClassWrapper, openNewViewWrapper);

        public void CloseAllSubWindows()
        {
            if (ActiveSubWindows.Count != 0)
            {
                foreach (var window in new List<IView>(ActiveSubWindows))
                {
                    WindowWrapper.Close(window);
                    ActiveSubWindows.Remove(window);
                }
            }
        }

        private Action<object, CancelEventArgs> RemoveActiveSubWindowAction() 
            => (view, cancelEventArgs) => { ActiveSubWindows.RemoveAll(x => x.GetType() == view.GetType()); };

        private bool CanOpenSubWindow(IView window)
        {
            if (!ActiveSubWindows.Exists(x => x.DataContext?.GetType() == window.DataContext?.GetType()))
            {
                OpenSubWindow(window);
                return true;
            }
            return false;
        }

        private void OpenSubWindow(IView window)
        {
            ActiveSubWindows.Add(window);
            WindowWrapper.Show(window);
        }
    }
}