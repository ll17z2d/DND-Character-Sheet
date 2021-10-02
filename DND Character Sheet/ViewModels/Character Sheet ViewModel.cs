using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using DND_Character_Sheet.APICommunication;
using DND_Character_Sheet.Commands;
using DND_Character_Sheet.Constants;
using DND_Character_Sheet.Enums;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Wrappers;

namespace DND_Character_Sheet.ViewModels
{
    public class CharacterSheetViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region Properties
        public ICommand NewCharacterCommand { get; set; }
        public ICommand OpenCharacterCommand { get; set; }
        public ICommand SaveCharacterAsCommand { get; set; }
        public ICommand APISearchCommand { get; set; }
        public ICommand SearchSkillsCommand { get; set; }
        public ICommand OpenSkillsWindowCommand { get; set; }
        public ICommand DiceRollCommand { get; set; }
        public ICommand OpenNotesWindowCommand { get; set; }

        private SkillsDialogViewModel skillsDialogViewModel;

        public SkillsDialogViewModel SkillsDialogViewModel
        {
            get
            {
                return skillsDialogViewModel;
            }
            set
            {
                skillsDialogViewModel = value;
                OnPropertyChanged("SkillsDialogViewModel");
            }
        }

        private string windowTitle;

        public string WindowTitle
        {
            get
            {
                return windowTitle;
            }
            set
            {
                windowTitle = value;
                OnPropertyChanged("WindowTitle");
            }
        }

        private string outTextbox;

        public string OutTextbox
        {
            get
            {
                return outTextbox;
            }
            set
            {
                outTextbox = value;
                OnPropertyChanged("OutTextbox");
            }
        }

        private string searchTextbox;

        public string SearchTextbox
        {
            get
            {
                return searchTextbox;
            }
            set
            {
                searchTextbox = value;
                OnPropertyChanged("SearchTextbox");
            }
        }

        private ObservableCollection<string> searchTypesCollection;

        public ObservableCollection<string> SearchTypesCollection
        {
            get
            {
                return searchTypesCollection;
            }
            set
            {
                searchTypesCollection = value;
                OnPropertyChanged("SearchTypesCollection");
            }
        }

        private string selectedSearchType;

        public string SelectedSearchType
        {
            get
            {
                return selectedSearchType;
            }
            set
            {
                selectedSearchType = value;
            }
        }

        private ObservableCollection<Skill> searchedSkills;

        public ObservableCollection<Skill> SearchedSkills
        {
            get
            {
                return searchedSkills;
            }
            set
            {
                searchedSkills = value;
                OnPropertyChanged("SearchedSkills");
            }
        }

        private string skillTextbox;

        public string SkillTextbox
        {
            get
            {
                return skillTextbox;
            }
            set
            {
                skillTextbox = value;
                OnPropertyChanged("SkillTextbox");
            }
        }

        private ObservableCollection<string> diceCollection;

        public ObservableCollection<string> DiceCollection //TODO: Consider having a different viewmodel for every user control, need to see if possible. If so then would help reduce pollution in window viewmodels
        {
            get
            {
                return diceCollection;
            }
            set
            {
                diceCollection = value;
                OnPropertyChanged("DiceCollection");
            }
        }

        private string selectedDice;

        public string SelectedDice
        {
            get
            {
                return selectedDice;
            }
            set
            {
                selectedDice = value;
                OnPropertyChanged("SelectedDice");
            }
        }

        private string diceResult;

        public string DiceResult
        {
            get
            {
                return diceResult;
            }
            set
            {
                diceResult = value;
                OnPropertyChanged("DiceResult");
            }
        }

        private ObservableCollection<MenuItem> menuCollection;

        public ObservableCollection<MenuItem> MenuCollection //TODO: Investigate how this could be used + whether this is even needed
        {
            get
            {
                return menuCollection;
            }
            set
            {
                menuCollection = value;
                OnPropertyChanged("MenuCollection");
            }
        }

        #endregion

        #region Contructor

        public CharacterSheetViewModel(ICharacterModel character, IDialogWindowWrapper dialogWindowWrapper, 
            IStaticClassWrapper staticClassWrapper, IOpenNewViewWrapper windowServiceWrapper) 
            : base(character, dialogWindowWrapper, staticClassWrapper, windowServiceWrapper)
        {
            //https://pokemon-go1.p.rapidapi.com/pokemon_evolutions.json

            InitialiseProperties();
            InitialiseCommands();
        }

        #endregion

        #region Initialisation

        private void InitialiseCommands()
        {
            NewCharacterCommand = new MethodCommands(NewCharacter);
            OpenCharacterCommand = new MethodCommands(OpenCharacter);
            SaveCharacterCommand = new MethodCommands(SaveCharacter);
            SaveCharacterAsCommand = new MethodCommands(SaveCharacterAs);
            APISearchCommand = new MethodCommands(APISearch, CanAPISearch);
            SearchSkillsCommand = new MethodCommands(SearchSkills);
            OpenSkillsWindowCommand = new MethodCommands(OpenSkillsWindow);
            DiceRollCommand = new MethodCommands(DiceRoll);
            OpenNotesWindowCommand = new MethodCommands(OpenNotesWindow);
        }

        private void InitialiseProperties()
        {
            InitialiseAPISearchOptions();
            InitialiseDiceRolls();
            InitialiseWindowTitle();
        }

        private void InitialiseWindowTitle() 
            => WindowTitle = Character.FilePath == Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                ? "Test"
                : StaticClassWrapper.TextFormatterWrapper.ExtractFileNameFromPath(Character.FilePath);

        private void InitialiseDiceRolls() 
            => DiceCollection = new ObservableCollection<string>(DiceStrings.AllDice);

        private void InitialiseAPISearchOptions() =>
            SearchTypesCollection = new ObservableCollection<string>()
            {
                DND_Search_Types.Backgrounds.ToString(),
                DND_Search_Types.Classes.ToString(),
                DND_Search_Types.Subclasses.ToString(),
                DND_Search_Types.Features.ToString(),
                DND_Search_Types.Races.ToString(),
                DND_Search_Types.Subraces.ToString(),
                DND_Search_Types.Weapons.ToString(),
                DND_Search_Types.Armour.ToString(),
                DND_Search_Types.Spells.ToString(),
                DND_Search_Types.Conditions.ToString()
            };

        #endregion

        #region Methods

        public bool CanAPISearch()
        {
            if (SearchTextbox == null || SearchTextbox.Trim() == "")
            {
                OutTextbox = "Type Something In To The Searchbar First!";
                return false;
            }
            if (SelectedSearchType == null)
            {
                OutTextbox = "Select A Search Type First!";
                return false;
            }

            if (OutTextbox != "Press Go!")
                return true;

            OutTextbox = "Press Go!";
            return true;
        }

        public bool APISearch()
        {
            var (outputText, isSuccessful) = APICommunicator.GetJson();
            OutTextbox = StaticClassWrapper.TextFormatterWrapper.ListToString(outputText);
            return isSuccessful;
        }

        public bool SearchSkills()
        {
            SearchedSkills = new ObservableCollection<Skill>();

            foreach (var skill in Character.AllSkills)
                if (skill.Name.ToLower().StartsWith(SkillTextbox.Replace(" ", string.Empty).ToLower()))
                    SearchedSkills.Add(skill);

            return SearchedSkills.Count != 0;
        }

        public bool DiceRoll()
        {
            if (SelectedDice != null)
            {
                if (int.TryParse(SelectedDice.Substring(2), out int result))
                {
                    DiceResult = new Random().Next(1, result + 1).ToString();
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool NewCharacter() 
            => WindowServiceWrapper.OpenCharacterCreatorWindow(DialogWindowWrapper,
                StaticClassWrapper, WindowServiceWrapper);

        public bool OpenNotesWindow() 
            => WindowServiceWrapper.OpenNotesWindow(Character.CharacterNotes, Character.FilePath, DialogWindowWrapper);

        public bool OpenSkillsWindow() 
            => WindowServiceWrapper.OpenSkillsWindow(Character.AllSkills, true);

        private IAPICommunicator APICommunicator
            => new APICommunicator(SelectedSearchType, SearchTextbox);

        #endregion

    }
}
