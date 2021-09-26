using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DND_Character_Sheet.Annotations;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class CharacterNotes : INotifyPropertyChanged
    {
        private const string PlaceholderCharacterAppearance = "/Images/Placeholder Character Appearance.png";
        private const string InvalidCharacterAppearance = "/Images/Invalid Character Appearance.png";
        private string characterAppearanceFilePath;
        private string quickNotes;
        private string equipment;
        private string sessionNotes;
        private string abilityDesc;
        private string proficiencies;
        private Money money;

        public string QuickNotes
        {
            get
            {
                return quickNotes;
            }
            set
            {
                quickNotes = value;
                OnPropertyChanged("QuickNotes");
            }
        }

        public string CharacterAppearanceFilePath
        {
            get
            {
                if (!File.Exists(characterAppearanceFilePath) 
                    && characterAppearanceFilePath != PlaceholderCharacterAppearance
                    && characterAppearanceFilePath != InvalidCharacterAppearance)
                {
                    characterAppearanceFilePath = InvalidCharacterAppearance;
                }
                return characterAppearanceFilePath;
            }
            set
            {
                if (!File.Exists(value)) // && characterAppearanceFilePath != PlaceholderCharacterAppearance
                {
                    characterAppearanceFilePath = InvalidCharacterAppearance;
                }
                else
                {
                    characterAppearanceFilePath = value;
                }
                OnPropertyChanged("CharacterAppearanceFilePath");
            }
        }

        public string Equipment
        {
            get
            {
                return equipment;
            }
            set
            {
                equipment = value;
                OnPropertyChanged("Equipment");
            }
        }

        public string SessionNotes
        {
            get
            {
                return sessionNotes;
            }
            set
            {
                sessionNotes = value;
                OnPropertyChanged("SessionNotes");
            }
        }

        public string AbilityDesc
        {
            get
            {
                return abilityDesc;
            }
            set
            {
                abilityDesc = value;
                OnPropertyChanged("AbilityDesc");
            }
        }

        public string Proficiencies
        {
            get
            {
                return proficiencies;
            }
            set
            {
                proficiencies = value;
                OnPropertyChanged("Proficiencies");
            }
        }

        public Money Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
                OnPropertyChanged("Money");
            }
        }

        public CharacterNotes() : this(PlaceholderCharacterAppearance, "", "", "", "", "", new Money()) { }

        public CharacterNotes(string characterAppearanceFilePath, string quickNotes, string equipment, string sessionNotes, string abilityDesc, string proficiencies, Money money)
        {
            CharacterAppearanceFilePath = characterAppearanceFilePath;
            QuickNotes = quickNotes;
            Equipment = equipment;
            SessionNotes = sessionNotes;
            AbilityDesc = abilityDesc;
            Proficiencies = proficiencies;
            Money = money;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
