using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.Models.API_Models;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public interface ICharacterModel
    {
        public MainStats MainStats { get; set; }
        public AllSkills AllSkills { get; set; }
        public HPStats HPStats { get; set; }
        public MiscStats MiscStats { get; set; }
        public CharacterNotes CharacterNotes { get; set; }
        public ObservableCollection<Weapons_Inventory> WeaponsInventory { get; set; }
        public string FilePath { get; set; }
    }

    public class CharacterModel : ICharacterModel, INotifyPropertyChanged
    {
        public CharacterModel() : this(null, null, null, null, null) { }
        public CharacterModel(MainStats MainStats, AllSkills AllSkills, HPStats HPStats, MiscStats MiscStats, CharacterNotes CharacterNotes)
        {
            this.MainStats = MainStats ?? new MainStats();
            this.AllSkills = AllSkills ?? new AllSkills();
            this.HPStats = HPStats ?? new HPStats();
            this.MiscStats = MiscStats ?? new MiscStats();
            this.CharacterNotes = CharacterNotes ?? new CharacterNotes();
        }

        private MainStats mainStats;

        public MainStats MainStats
        {
            get
            {
                return mainStats;

            }
            set
            {
                mainStats = value;
                OnPropertyChanged("MainStats");
            }
        }


        private AllSkills allSkills;

        public AllSkills AllSkills
        {
            get
            {
                return allSkills;
            }
            set
            {
                allSkills = value;
                OnPropertyChanged("AllSkills");
            }
        }

        public HPStats HPStats { get; set; }
        public MiscStats MiscStats { get; set; }
        public CharacterNotes CharacterNotes { get; set; }

        public ObservableCollection<Weapons_Inventory> WeaponsInventory { get; set; } = new();

        public string FilePath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
