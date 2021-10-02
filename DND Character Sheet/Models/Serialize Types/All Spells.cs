﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.ViewModels;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class AllSpells : INotifyPropertyChanged
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

        public AllSpells() : this(new SpellLevelViewModel(8, 0, 0, 0),
            new SpellLevelViewModel(12, 1, 0, 0),
            new SpellLevelViewModel(13, 2, 0, 0),
            new SpellLevelViewModel(13, 3, 0, 0),
            new SpellLevelViewModel(13, 4, 0, 0),
            new SpellLevelViewModel(9, 5, 0, 0),
            new SpellLevelViewModel(9, 6, 0, 0),
            new SpellLevelViewModel(9, 7, 0, 0),
            new SpellLevelViewModel(7, 8, 0, 0),
            new SpellLevelViewModel(7, 9, 0, 0)) { }

        private AllSpells(SpellLevelViewModel cantripSpellViewModel, SpellLevelViewModel firstLevelSpellViewModel,
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
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}