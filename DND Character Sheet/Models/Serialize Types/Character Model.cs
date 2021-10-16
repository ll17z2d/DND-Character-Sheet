using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.Models.API_Models;
using DND_Character_Sheet.ViewModels;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public interface ICharacterModel
    {
        public MainStats MainStats { get; set; }
        public AllSkills AllSkills { get; set; }
        public HPStats HPStats { get; set; }
        public DetailsStats DetailsStats { get; set; }
        public MiscStats MiscStats { get; set; }
        public CharacterNotes CharacterNotes { get; set; }
        public WeaponNotes WeaponNotes { get; set; }
        public AllSpells AllSpells { get; set; }
        public string FilePath { get; set; }
    }

    public class CharacterModel : ICharacterModel, INotifyPropertyChanged
    {
        private MainStats mainStats;
        private AllSkills allSkills;
        private HPStats hpStats;
        private MiscStats miscStats;
        private CharacterNotes characterNotes;
        private WeaponNotes weaponNotes;
        private AllSpells allSpells;
        private string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public CharacterModel() : this(null, null, null, null, null, null, null, null) { }
        public CharacterModel(MainStats mainStats, AllSkills allSkills, HPStats hpStats, DetailsStats detailsStats, 
            MiscStats miscStats, CharacterNotes characterNotes, WeaponNotes weaponNotes, AllSpells allSpells)
        {
            MainStats = mainStats ?? new MainStats();
            AllSkills = allSkills ?? new AllSkills();
            HPStats = hpStats ?? new HPStats();
            DetailsStats = detailsStats ?? new DetailsStats();
            MiscStats = miscStats ?? new MiscStats();
            CharacterNotes = characterNotes ?? new CharacterNotes();
            WeaponNotes = weaponNotes ?? new WeaponNotes();
            AllSpells = allSpells ?? new AllSpells();
        }

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

        public HPStats HPStats
        {
            get
            {
                return hpStats;
            }
            set
            {
                hpStats = value;
                OnPropertyChanged("HPStats");
            }
        }

        private DetailsStats detailsStats;

        public DetailsStats DetailsStats
        {
            get
            {
                return detailsStats;
            }
            set
            {
                detailsStats = value; 
                OnPropertyChanged("DetailsStats");
            }
        }

        public MiscStats MiscStats
        {
            get
            {
                return miscStats;
            }
            set
            {
                miscStats = value;
                OnPropertyChanged("MiscStats");
            }
        }

        public CharacterNotes CharacterNotes
        {
            get
            {
                return characterNotes;
            }
            set
            {
                characterNotes = value;
                OnPropertyChanged("CharacterNotes");
            }
        }

        public WeaponNotes WeaponNotes
        {
            get
            {
                return weaponNotes;
            }
            set
            {
                weaponNotes = value;
                OnPropertyChanged("WeaponNotes");
            }
        }

        public AllSpells AllSpells
        {
            get
            {
                return allSpells;
            }
            set
            {
                allSpells = value;
                OnPropertyChanged("AllSpells");
            }
        }

        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
                OnPropertyChanged("FilePath");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
