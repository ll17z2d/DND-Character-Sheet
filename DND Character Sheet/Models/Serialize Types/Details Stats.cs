using System.ComponentModel;
using System.Runtime.CompilerServices;
using DND_Character_Sheet.Annotations;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class DetailsStats : INotifyPropertyChanged
    {
        public DetailsStats() : this("", "", "", "", "", "", "") { }

        public DetailsStats(string characterName, string classAndLevel, string background, string playerName, string race, string alignment, string experiencePoints)
        {
            CharacterName = characterName;
            ClassAndLevel = classAndLevel;
            Background = background;
            PlayerName = playerName;
            Race = race;
            Alignment = alignment;
            ExperiencePoints = experiencePoints;
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
