using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.Models.API_Models;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class WeaponNotes : INotifyPropertyChanged
    {
        public WeaponNotes() : this(new ObservableCollection<WeaponsInventory>(), "") { }

        public WeaponNotes(ObservableCollection<WeaponsInventory> weaponsInventory, string weaponDetails)
        {
            WeaponsInventory = weaponsInventory;
            WeaponDetails = weaponDetails;
        }

        private ObservableCollection<WeaponsInventory> weaponsInventory = new();

        public ObservableCollection<WeaponsInventory> WeaponsInventory
        {
            get
            {
                return weaponsInventory;
            }
            set
            {
                weaponsInventory = value;
                OnPropertyChanged("WeaponsInventory");
            }
        }

        private string weaponDetails;

        public string WeaponDetails
        {
            get
            {
                return weaponDetails;
            }
            set
            {
                weaponDetails = value; 
                OnPropertyChanged("WeaponDetails");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
