using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.ViewModels;
using Newtonsoft.Json;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    [JsonObject]
    public class AllSpells : INotifyPropertyChanged, IEnumerable<SpellLevelViewModel>
    {
        private SpellLevelViewModel cantripSpellViewModel;
        private SpellLevelViewModel firstLevelSpellViewModel;
        private SpellLevelViewModel secondLevelSpellViewModel;
        private SpellLevelViewModel thirdLevelSpellViewModel;
        private SpellLevelViewModel fourthLevelSpellViewModel;
        private SpellLevelViewModel fifthLevelSpellViewModel;
        private SpellLevelViewModel sixthLevelSpellViewModel;
        private SpellLevelViewModel seventhLevelSpellViewModel;
        private SpellLevelViewModel eighthLevelSpellViewModel;
        private SpellLevelViewModel ninthLevelSpellViewModel;

        public SpellLevelViewModel CantripSpellViewModel
        {
            get
            {
                return cantripSpellViewModel;
            }
            set
            {
                cantripSpellViewModel = value;
                OnPropertyChanged("CantripSpellViewModel");
            }
        }

        public SpellLevelViewModel FirstLevelSpellViewModel
        {
            get
            {
                return firstLevelSpellViewModel;
            }
            set
            {
                firstLevelSpellViewModel = value;
                OnPropertyChanged("FirstLevelSpellViewModel");
            }
        }

        public SpellLevelViewModel SecondLevelSpellViewModel
        {
            get
            {
                return secondLevelSpellViewModel;
            }
            set
            {
                secondLevelSpellViewModel = value;
                OnPropertyChanged("SecondLevelSpellViewModel");
            }
        }

        public SpellLevelViewModel ThirdLevelSpellViewModel
        {
            get
            {
                return thirdLevelSpellViewModel;
            }
            set
            {
                thirdLevelSpellViewModel = value;
                OnPropertyChanged("ThirdLevelSpellViewModel");
            }
        }

        public SpellLevelViewModel FourthLevelSpellViewModel
        {
            get
            {
                return fourthLevelSpellViewModel;
            }
            set
            {
                fourthLevelSpellViewModel = value;
                OnPropertyChanged("FourthLevelSpellViewModel");
            }
        }

        public SpellLevelViewModel FifthLevelSpellViewModel
        {
            get
            {
                return fifthLevelSpellViewModel;
            }
            set
            {
                fifthLevelSpellViewModel = value;
                OnPropertyChanged("FifthLevelSpellViewModel");
            }
        }

        public SpellLevelViewModel SixthLevelSpellViewModel
        {
            get
            {
                return sixthLevelSpellViewModel;
            }
            set
            {
                sixthLevelSpellViewModel = value;
                OnPropertyChanged("SixthLevelSpellViewModel");
            }
        }

        public SpellLevelViewModel SeventhLevelSpellViewModel
        {
            get
            {
                return seventhLevelSpellViewModel;
            }
            set
            {
                seventhLevelSpellViewModel = value;
                OnPropertyChanged("SeventhLevelSpellViewModel");
            }
        }

        public SpellLevelViewModel EighthLevelSpellViewModel
        {
            get
            {
                return eighthLevelSpellViewModel;
            }
            set
            {
                eighthLevelSpellViewModel = value;
                OnPropertyChanged("EighthLevelSpellViewModel");
            }
        }

        public SpellLevelViewModel NinthLevelSpellViewModel
        {
            get
            {
                return ninthLevelSpellViewModel;
            }
            set
            {
                ninthLevelSpellViewModel = value;
                OnPropertyChanged("NinthLevelSpellViewModel");
            }
        }

        private string spellcastingAbility;

        public string SpellcastingAbility
        {
            get
            {
                return spellcastingAbility;
            }
            set
            {
                spellcastingAbility = value;
                OnPropertyChanged("SpellcastingAbility");
            }
        }

        private string spellSaveDC;

        public string SpellSaveDC
        {
            get
            {
                return spellSaveDC;
            }
            set
            {
                spellSaveDC = value; 
                OnPropertyChanged("SpellSaveDC");
            }
        }

        private string spellAttackBonus;

        public string SpellAttackBonus
        {
            get
            {
                return spellAttackBonus;
            }
            set
            {
                spellAttackBonus = value;
                OnPropertyChanged("SpellAttackBonus");
            }
        }



        public AllSpells() : this(new SpellLevelViewModel(8, 0),
            new SpellLevelViewModel(12, 1),
            new SpellLevelViewModel(13, 2),
            new SpellLevelViewModel(13, 3),
            new SpellLevelViewModel(13, 4),
            new SpellLevelViewModel(9, 5),
            new SpellLevelViewModel(9, 6),
            new SpellLevelViewModel(9, 7),
            new SpellLevelViewModel(7, 8),
            new SpellLevelViewModel(7, 9)) { }

        public AllSpells(SpellLevelViewModel cantripSpellViewModel, SpellLevelViewModel firstLevelSpellViewModel,
            SpellLevelViewModel secondLevelSpellViewModel, SpellLevelViewModel thirdLevelSpellViewModel,
            SpellLevelViewModel fourthLevelSpellViewModel, SpellLevelViewModel fifthLevelSpellViewModel,
            SpellLevelViewModel sixthLevelSpellViewModel, SpellLevelViewModel seventhLevelSpellViewModel,
            SpellLevelViewModel eighthLevelSpellViewModel, SpellLevelViewModel ninthLevelSpellViewModel)
        {
            CantripSpellViewModel = cantripSpellViewModel;
            FirstLevelSpellViewModel = firstLevelSpellViewModel;
            SecondLevelSpellViewModel = secondLevelSpellViewModel;
            ThirdLevelSpellViewModel = thirdLevelSpellViewModel;
            FourthLevelSpellViewModel = fourthLevelSpellViewModel;
            FifthLevelSpellViewModel = fifthLevelSpellViewModel;
            SixthLevelSpellViewModel = sixthLevelSpellViewModel;
            SeventhLevelSpellViewModel = seventhLevelSpellViewModel;
            EighthLevelSpellViewModel = eighthLevelSpellViewModel;
            NinthLevelSpellViewModel = ninthLevelSpellViewModel;
            SpellcastingAbility = "";
            spellSaveDC = "";
            SpellAttackBonus = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerator<SpellLevelViewModel> GetEnumerator()
        {
            foreach (var spellLevelViewModel in new List<SpellLevelViewModel>
            {
                CantripSpellViewModel,
                FirstLevelSpellViewModel,
                SecondLevelSpellViewModel,
                ThirdLevelSpellViewModel,
                FourthLevelSpellViewModel,
                FifthLevelSpellViewModel,
                SixthLevelSpellViewModel,
                SeventhLevelSpellViewModel,
                EighthLevelSpellViewModel,
                NinthLevelSpellViewModel
            })
            {
                yield return spellLevelViewModel;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
