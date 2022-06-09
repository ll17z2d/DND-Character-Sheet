using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.Models.Serialize_Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DND_Character_Sheet.ViewModels
{
    public class DetailsDialogViewModel : INotifyPropertyChanged
    {
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


        public DetailsDialogViewModel(DetailsStats detailsStats)
        {
            DetailsStats = detailsStats;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
