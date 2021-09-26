using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.Constants;
using Newtonsoft.Json;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    [JsonObject]
    public class AllSkills : INotifyPropertyChanged, IEnumerable<Skill>
    {
        private Skill acrobatics;
        private Skill animalHandling;
        private Skill arcana;
        private Skill athletics;
        private Skill deception;
        private Skill history;
        private Skill insight;
        private Skill intimidation;
        private Skill investigation;
        private Skill medicine;
        private Skill survival;
        private Skill stealth;
        private Skill sleightOfHand;
        private Skill religion;
        private Skill persuasion;
        private Skill performance;
        private Skill perception;
        private Skill nature;

        public Skill Acrobatics
        {
            get
            {
                return acrobatics;
            }
            set
            {
                acrobatics = value;
                OnPropertyChanged("Acrobatics");
            }
        }

        public Skill AnimalHandling
        {
            get
            {
                return animalHandling;
            }
            set
            {
                animalHandling = value;
                OnPropertyChanged("AnimalHandling");
            }
        }


        public Skill Arcana
        {
            get
            {
                return arcana;
            }
            set
            {
                arcana = value;
                OnPropertyChanged("Arcana");
            }
        }

        public Skill Athletics
        {
            get
            {
                return athletics;
            }
            set
            {
                athletics = value;
                OnPropertyChanged("Athletics");
            }
        }

        public Skill Deception
        {
            get
            {
                return deception;
            }
            set
            {
                deception = value;
                OnPropertyChanged("Deception");
            }
        }

        public Skill History
        {
            get
            {
                return history;
            }
            set
            {
                history = value;
                OnPropertyChanged("History");
            }
        }

        public Skill Insight
        {
            get
            {
                return insight;
            }
            set
            {
                insight = value;
                OnPropertyChanged("Insight");
            }
        }

        public Skill Intimidation
        {
            get
            {
                return intimidation;
            }
            set
            {
                intimidation = value;
                OnPropertyChanged("Intimidation");
            }
        }

        public Skill Investigation
        {
            get
            {
                return investigation;
            }
            set
            {
                investigation = value;
                OnPropertyChanged("Investigation");
            }
        }

        public Skill Medicine
        {
            get
            {
                return medicine;
            }
            set
            {
                medicine = value;
                OnPropertyChanged("Medicine");
            }
        }

        public Skill Nature
        {
            get
            {
                return nature;
            }
            set
            {
                nature = value;
                OnPropertyChanged("Nature");
            }
        }

        public Skill Perception
        {
            get
            {
                return perception;
            }
            set
            {
                perception = value;
                OnPropertyChanged("Perception");
            }
        }

        public Skill Performance
        {
            get
            {
                return performance;
            }
            set
            {
                performance = value;
                OnPropertyChanged("Performance");
            }
        }

        public Skill Persuasion
        {
            get
            {
                return persuasion;
            }
            set
            {
                persuasion = value;
                OnPropertyChanged("Persuasion");
            }
        }

        public Skill Religion
        {
            get
            {
                return religion;
            }
            set
            {
                religion = value;
                OnPropertyChanged("Religion");
            }
        }

        public Skill SleightOfHand
        {
            get
            {
                return sleightOfHand;
            }
            set
            {
                sleightOfHand = value;
                OnPropertyChanged("SleightOfHand");
            }
        }

        public Skill Stealth
        {
            get
            {
                return stealth;
            }
            set
            {
                stealth = value;
                OnPropertyChanged("Stealth");
            }
        }

        public Skill Survival
        {
            get
            {
                return survival;
            }
            set
            {
                survival = value;
                OnPropertyChanged("Survival");
            }
        }

        public AllSkills() : this(new Skill(SkillStrings.Acrobatics, "0"), 
            new Skill(SkillStrings.AnimalHandling, "0"), 
            new Skill(SkillStrings.Arcana, "0"), 
            new Skill(SkillStrings.Athletics, "0"), 
            new Skill(SkillStrings.Deception, "0"), 
            new Skill(SkillStrings.History, "0"),
            new Skill(SkillStrings.Insight, "0"),
            new Skill(SkillStrings.Intimidation, "0"),
            new Skill(SkillStrings.Investigation, "0"),
            new Skill(SkillStrings.Medicine, "0"),
            new Skill(SkillStrings.Nature, "0"),
            new Skill(SkillStrings.Perception, "0"),
            new Skill(SkillStrings.Performance, "0"),
            new Skill(SkillStrings.Persuasion, "0"),
            new Skill(SkillStrings.Religion, "0"),
            new Skill(SkillStrings.SleightOfHand, "0"),
            new Skill(SkillStrings.Stealth, "0"),
            new Skill(SkillStrings.Survival, "0")) { }

        private AllSkills(Skill Acrobatics, 
            Skill AnimalHandling, 
            Skill Arcana,
            Skill Athletics,
            Skill Deception,
            Skill History,
            Skill Insight,
            Skill Intimidation,
            Skill Investigation,
            Skill Medicine,
            Skill Nature,
            Skill Perception,
            Skill Performance,
            Skill Persuasion,
            Skill Religion,
            Skill SleightOfHand,
            Skill Stealth,
            Skill Survival)
        {
            this.Acrobatics = Acrobatics;
            this.AnimalHandling = AnimalHandling;
            this.Arcana = Arcana;
            this.Athletics = Athletics;
            this.Deception = Deception;
            this.History = History;
            this.Insight = Insight;
            this.Intimidation = Intimidation;
            this.Investigation = Investigation;
            this.Medicine = Medicine;
            this.Nature = Nature;
            this.Perception = Perception;
            this.Performance = Performance;
            this.Persuasion = Persuasion;
            this.Religion = Religion;
            this.SleightOfHand = SleightOfHand;
            this.Stealth = Stealth;
            this.Survival = Survival;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerator<Skill> GetEnumerator()
        {
            foreach (var skill in new List<Skill>()
            {
                Acrobatics,
                AnimalHandling,
                Arcana,
                Athletics,
                Deception,
                History,
                Insight,
                Intimidation,
                Investigation,
                Medicine,
                Nature,
                Perception,
                Performance,
                Persuasion,
                Religion,
                SleightOfHand,
                Stealth,
                Survival
            })
            {
                yield return skill;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
