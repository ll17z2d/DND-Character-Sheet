using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DND_Character_Sheet.Annotations;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class MainStats : INotifyPropertyChanged
    {
        public MainStats() : this(0, 0, 0, 0, 0, 0, "", "", "", "", "", "") { }

        public MainStats(int Str, int Dex, int Con, int Intl, int Wis, int Cha, string StrSvThr, string DexSvThr, string ConSvThr, string IntlSvThr, string WisSvThr, string ChaSvThr)
        {
            this.Str = Str;
            this.Dex = Dex;
            this.Con = Con;
            this.Intl = Intl;
            this.Wis = Wis;
            this.Cha = Cha;

            this.StrSvThr = StrSvThr;
            this.DexSvThr = DexSvThr;
            this.ConSvThr = ConSvThr;
            this.IntlSvThr = IntlSvThr;
            this.WisSvThr = WisSvThr;
            this.ChaSvThr = ChaSvThr;
        }

        private int str;

        public int Str
        {
            get
            {
                return str;
            }
            set
            {
                str = value;
                OnPropertyChanged("Str");
                StrMod = StatToModifier(value);
            }
        }

        private int dex;

        public int Dex
        {
            get
            {
                return dex;
            }
            set
            {
                dex = value;
                OnPropertyChanged("Dex");
                DexMod = StatToModifier(value);
            }
        }

        private int con;

        public int Con
        {
            get
            {
                return con;
            }
            set
            {
                con = value;
                OnPropertyChanged("Con");
                ConMod = StatToModifier(value);
            }
        }

        private int intl;

        public int Intl
        {
            get
            {
                return intl;
            }
            set
            {
                intl = value;
                OnPropertyChanged("Intl");
                IntlMod = StatToModifier(value);
            }
        }

        private int wis;

        public int Wis
        {
            get
            {
                return wis;
            }
            set
            {
                wis = value;
                OnPropertyChanged("Wis");
                WisMod = StatToModifier(value);
            }
        }

        private int cha;

        public int Cha
        {
            get
            {
                return cha;
            }
            set
            {
                cha = value;
                OnPropertyChanged("Cha");
                ChaMod = StatToModifier(value);
            }
        }

        private string strMod;

        public string StrMod
        {
            get
            {
                return strMod;
            }
            set
            {
                strMod = value;
                OnPropertyChanged("StrMod");
            }
        }

        private string dexMod;

        public string DexMod
        {
            get
            {
                return dexMod;

            }
            set
            {
                dexMod = value;
                OnPropertyChanged("DexMod");
            }
        }

        private string conMod;

        public string ConMod
        {
            get
            {
                return conMod;

            }
            set
            {
                conMod = value;
                OnPropertyChanged("ConMod");
            }
        }

        private string intlMod;

        public string IntlMod
        {
            get
            {
                return intlMod;

            }
            set
            {
                intlMod = value;
                OnPropertyChanged("IntlMod");
            }
        }

        private string wisMod;

        public string WisMod
        {
            get
            {
                return wisMod;
            }
            set
            {
                wisMod = value;
                OnPropertyChanged("WisMod");
            }
        }


        private string chaMod;

        public string ChaMod
        {
            get
            {
                return chaMod;
            }
            set
            {
                chaMod = value;
                OnPropertyChanged("ChaMod");
            }
        }

        private string strSvThr;

        public string StrSvThr
        {
            get
            {
                return strSvThr;
            }
            set
            {
                strSvThr = value;
                OnPropertyChanged("StrSvThr");
            }
        }

        private string dexSvThr;

        public string DexSvThr
        {
            get
            {
                return dexSvThr;

            }
            set
            {
                dexSvThr = value;
                OnPropertyChanged("DexSvThr");
            }
        }

        private string conSvThr;

        public string ConSvThr
        {
            get
            {
                return conSvThr;

            }
            set
            {
                conSvThr = value;
                OnPropertyChanged("ConSvThr");
            }
        }

        private string intlSvThr;

        public string IntlSvThr
        {
            get
            {
                return intlSvThr;

            }
            set
            {
                intlSvThr = value;
                OnPropertyChanged("IntlSvThr");
            }
        }

        private string wisSvThr;

        public string WisSvThr
        {
            get
            {
                return wisSvThr;
            }
            set
            {
                wisSvThr = value;
                OnPropertyChanged("WisSvThr");
            }
        }


        private string chaSvThr;

        public string ChaSvThr
        {
            get
            {
                return chaSvThr;
            }
            set
            {
                chaSvThr = value;
                OnPropertyChanged("ChaSvThr");
            }
        }

        private static string StatToModifier(int stat)
        {
            int modifier = (int)Math.Floor((double)(stat - 10) / 2);

            if (modifier >= 0)
            {
                return modifier.ToString().Insert(0, "+");
            }

            return modifier.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
