using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DND_Character_Sheet.Annotations;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class MainStats : INotifyPropertyChanged
    {
        public MainStats() : this(0, 0, 0, 0, 0, 0, "", "", "", "", "", "", false, false, false, false, false, false) { }

        public MainStats(int Str, int Dex, int Con, int Intl, int Wis, int Cha, 
            string StrSvThr, string DexSvThr, string ConSvThr, string IntlSvThr, string WisSvThr, string ChaSvThr, 
            bool StrSvThrProf, bool DexSvThrProf, bool ConSvThrProf, bool IntlSvThrProf, bool WisSvThrProf, bool ChaSvThrProf)
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

            this.StrSvThrProf = StrSvThrProf;
            this.DexSvThrProf = DexSvThrProf;
            this.ConSvThrProf = ConSvThrProf;
            this.IntlSvThrProf = IntlSvThrProf;
            this.WisSvThrProf = WisSvThrProf;
            this.ChaSvThrProf = ChaSvThrProf;
        }

        public MainStats(int Str, int Dex, int Con, int Intl, int Wis, int Cha, 
            string StrSvThr, string DexSvThr, string ConSvThr, string IntlSvThr, string WisSvThr, string ChaSvThr,
            bool StrSvThrProf, bool DexSvThrProf, bool ConSvThrProf, bool IntlSvThrProf, bool WisSvThrProf, bool ChaSvThrProf,
            string StrMod, string DexMod, string ConMod, string IntlMod, string WisMod, string ChaMod) : this(Str, Dex, Con, Intl, Wis, Cha,
            StrSvThr, DexSvThr, ConSvThr, IntlSvThr, WisSvThr, ChaSvThr,
            StrSvThrProf, DexSvThrProf, ConSvThrProf, IntlSvThrProf, WisSvThrProf, ChaSvThrProf)
        {
            this.StrMod = StrMod;
            this.DexMod = DexMod;
            this.ConMod = ConMod;
            this.IntlMod = IntlMod;
            this.WisMod = WisMod;
            this.ChaMod = ChaMod;
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

        private bool strSvThrProf;

        public bool StrSvThrProf
        {
            get 
            { 
                return strSvThrProf; 
            }
            set 
            { 
                strSvThrProf = value;
                OnPropertyChanged("StrSvThrProf");
            }
        }

        private bool dexSvThrProf;

        public bool DexSvThrProf
        {
            get
            {
                return dexSvThrProf;
            }
            set
            {
                dexSvThrProf = value;
                OnPropertyChanged("DexSvThrProf");
            }
        }

        private bool conSvThrProf;

        public bool ConSvThrProf
        {
            get
            {
                return conSvThrProf;
            }
            set
            {
                conSvThrProf = value;
                OnPropertyChanged("ConSvThrProf");
            }
        }

        private bool intlSvThrProf;

        public bool IntlSvThrProf
        {
            get
            {
                return intlSvThrProf;
            }
            set
            {
                intlSvThrProf = value;
                OnPropertyChanged("IntlSvThrProf");
            }
        }

        private bool wisSvThrProf;

        public bool WisSvThrProf
        {
            get
            {
                return wisSvThrProf;
            }
            set
            {
                wisSvThrProf = value;
                OnPropertyChanged("WisSvThrProf");
            }
        }

        private bool chaSvThrProf;

        public bool ChaSvThrProf
        {
            get
            {
                return chaSvThrProf;
            }
            set
            {
                chaSvThrProf = value;
                OnPropertyChanged("ChaSvThrProf");
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
