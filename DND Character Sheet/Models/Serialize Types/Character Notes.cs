using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.Wrappers;

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
        private string backstory;
        private string treasure;
        private string featuresAndTraits;

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
                if (characterAppearanceFilePath != PlaceholderCharacterAppearance
                    && characterAppearanceFilePath != InvalidCharacterAppearance
                    && !FileOperationsWrapper.FileExists(characterAppearanceFilePath))
                    characterAppearanceFilePath = InvalidCharacterAppearance;
                return characterAppearanceFilePath;
            }
            set
            {
                if (value != PlaceholderCharacterAppearance && !FileOperationsWrapper.FileExists(value))
                    characterAppearanceFilePath = InvalidCharacterAppearance;
                else
                    characterAppearanceFilePath = value;
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

        public string Backstory
        {
            get
            {
                return backstory;
            }
            set
            {
                backstory = value;
                OnPropertyChanged("Backstory");
            }
        }

        public string Treasure
        {
            get
            {
                return treasure;
            }
            set
            {
                treasure = value;
                OnPropertyChanged("Treasure");
            }
        }

        public string FeaturesAndTraits
        {
            get
            {
                return featuresAndTraits;
            }
            set
            {
                featuresAndTraits = value;
                OnPropertyChanged("FeaturesAndTraits");
            }
        }

        public ISerializeCharacterWrapper SerializeCharacterWrapper { get; set; }

        public IFileOperationsWrapper FileOperationsWrapper { get; set; }

        public CharacterNotes() : this(PlaceholderCharacterAppearance, "", "", "", "", "", new Money(), 
            "", "", "", new SerializeCharacterWrapper(), new FileOperationsWrapper()) { }

        public CharacterNotes(string characterAppearanceFilePath, string quickNotes, string equipment, string sessionNotes, string abilityDesc, 
            string proficiencies, Money money, string backstory, string treasure, string featuresAndTraits,
            ISerializeCharacterWrapper serializeCharacterWrapper, IFileOperationsWrapper fileOperationsWrapper)
        {
            SerializeCharacterWrapper = serializeCharacterWrapper;
            FileOperationsWrapper = fileOperationsWrapper;
            CharacterAppearanceFilePath = characterAppearanceFilePath;
            QuickNotes = quickNotes;
            Equipment = equipment;
            SessionNotes = sessionNotes;
            AbilityDesc = abilityDesc;
            Proficiencies = proficiencies;
            Money = money;
            Backstory = backstory;
            Treasure = treasure;
            FeaturesAndTraits = featuresAndTraits;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
