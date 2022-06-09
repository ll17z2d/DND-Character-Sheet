using System.ComponentModel;
using System.Runtime.CompilerServices;
using DND_Character_Sheet.Annotations;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class DetailsStats : INotifyPropertyChanged
    {
        public DetailsStats() : this("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "") { }

        public DetailsStats(string characterName, string classAndLevel, string background, string playerName, string race, string alignment, string experiencePoints,
            string age, string height, string weight, string eyes, string skin, string hair, string personalityTraits, string ideals, string bonds, string flaws)
        {
            CharacterName = characterName;
            ClassAndLevel = classAndLevel;
            Background = background;
            PlayerName = playerName;
            Race = race;
            Alignment = alignment;
            ExperiencePoints = experiencePoints;
            Age = age;
            Height = height;
            Weight = weight;
            Eyes = eyes;
            Skin = skin;
            Hair = hair;
            PersonalityTraits = personalityTraits;
            Ideals = ideals;
            Bonds = bonds;
            Flaws = flaws;
        }

        private string characterName;

        public string CharacterName
        {
            get
            {
                return characterName;

            }
            set
            {
                characterName = value;
                OnPropertyChanged("CharacterName");
            }
        }

        private string classAndLevel;

        public string ClassAndLevel
        {
            get
            {
                return classAndLevel;
            }
            set
            {
                classAndLevel = value;
                OnPropertyChanged("ClassAndLevel");
            }
        }

        private string background;

        public string Background
        {
            get
            {
                return background;
            }
            set
            {
                background = value; 
                OnPropertyChanged("Background");
            }
        }

        private string playerName;

        public string PlayerName
        {
            get
            {
                return playerName;
            }
            set
            {
                playerName = value;
                OnPropertyChanged("PlayerName");
            }
        }

        private string race;

        public string Race
        {
            get
            {
                return race;
            }
            set
            {
                race = value;
                OnPropertyChanged("Race");
            }
        }

        private string alignment;

        public string Alignment
        {
            get
            {
                return alignment;
            }
            set
            {
                alignment = value; 
                OnPropertyChanged("Alignment");
            }
        }

        private string experiencePoints;

        public string ExperiencePoints
        {
            get
            {
                return experiencePoints; 

            }
            set
            {
                experiencePoints = value; 
                OnPropertyChanged("ExperiencePoints");
            }
        }

        private string age;

        public string Age
        {
            get 
            {
                return age; 
            }
            set 
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        private string height;

        public string Height
        {
            get 
            { 
                return height; 
            }
            set 
            { 
                height = value;
                OnPropertyChanged("Height");
            }
        }

        private string weight;

        public string Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
                OnPropertyChanged("Weight");
            }
        }

        private string eyes;

        public string Eyes
        {
            get
            {
                return eyes;
            }
            set
            {
                eyes = value;
                OnPropertyChanged("Eyes");
            }
        }

        private string skin;

        public string Skin
        {
            get
            {
                return skin;
            }
            set
            {
                skin = value;
                OnPropertyChanged("Skin");
            }
        }

        private string hair;

        public string Hair
        {
            get
            {
                return hair;
            }
            set
            {
                hair = value;
                OnPropertyChanged("Hair");
            }
        }

        private string personalityTraits;

        public string PersonalityTraits
        {
            get
            {
                return personalityTraits;
            }
            set
            {
                personalityTraits = value;
                OnPropertyChanged("PersonalityTraits");
            }
        }

        private string ideals;

        public string Ideals
        {
            get
            {
                return ideals;
            }
            set
            {
                ideals = value;
                OnPropertyChanged("Ideals");
            }
        }

        private string bonds;

        public string Bonds
        {
            get
            {
                return bonds;
            }
            set
            {
                bonds = value;
                OnPropertyChanged("Bonds");
            }
        }

        private string flaws;

        public string Flaws
        {
            get
            {
                return flaws;
            }
            set
            {
                flaws = value;
                OnPropertyChanged("Flaws");
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
