using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using DND_Character_Sheet.Commands;
using DND_Character_Sheet.Enums;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Wrappers;

namespace DND_Character_Sheet.ViewModels
{
    public class CharacterCreatorViewModel : BaseViewModel
    {
        private SkillsDialogViewModel skillsDialogViewModel;

        public SkillsDialogViewModel SkillsDialogViewModel //TODO: Investigate what value this is when updating the skills section, maybe this is updated but not passed on to Character through that action delegate I did, if so then need to refactor
        {
            get
            {
                return skillsDialogViewModel;
            }
            set
            {
                skillsDialogViewModel = value;
                OnPropertyChanged("SkillsDialogViewModel");
            }
        }

        private NotesDialogViewModel notesDialogViewModel;

        public NotesDialogViewModel NotesDialogViewModel
        {
            get
            {
                return notesDialogViewModel;
            }
            set
            {
                notesDialogViewModel = value;
                OnPropertyChanged("NotesDialogViewModel");
            }
        }

        public ICommand ResetCharacterCommand { get; set; }
        public ICommand AutoGenerateSkillModsCommand { get; set; }

        public CharacterCreatorViewModel(ICharacterModel character, IDialogWindowWrapper dialogWindowWrapper, ITextFormatterWrapper textFormatterWrapper, ISerializeCharacterWrapper serializeCharacterWrapper) 
            : base(character, dialogWindowWrapper, textFormatterWrapper, serializeCharacterWrapper)
        {
            Initialise();
        }

        private void Initialise()
        {
            InitialiseViewModels();
            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            SaveCharacterCommand = new MethodCommands(SaveCharacterAs);
            ResetCharacterCommand = new MethodCommands(ResetCharacter);
            AutoGenerateSkillModsCommand = new MethodCommands(AutoGenerateSkillModifiers);
        }

        private void InitialiseViewModels()
        {
            SkillsDialogViewModel = new SkillsDialogViewModel(Character.AllSkills, false);
            NotesDialogViewModel = new NotesDialogViewModel(Character.CharacterNotes, Character.FilePath, DialogWindowWrapper);
        }

        public bool ResetCharacter()
        {
            var result = DialogWindowWrapper.MessageBoxWrapper.Show("Are you sure you want to reset this character?", "Reset Character", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                InitialiseCharacter(new CharacterModel());
                InitialiseViewModels();
                return true;
            }

            return false;
        }

        public bool AutoGenerateSkillModifiers()
        {
            var result = DialogWindowWrapper.MessageBoxWrapper.Show("Are you sure you want to auto-generate your character's stats? This will overwrite any existing skill modifiers you've already set.", "Auto Generate Skill Modifiers", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                var (isSuccessful, skillErrorList) = GenerateModifiers();
                if (isSuccessful)
                {
                    DialogWindowWrapper.MessageBoxWrapper.Show("Your stats have all successfully been generated!",
                        "Successful Stat Modifier Generation", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
                    return true;
                }

                DialogWindowWrapper.MessageBoxWrapper.Show($"Error - Ensure you've set all your main stats and you've set a proficiency bonus. There seems to be issues with the following stat values: {TextFormatterWrapper.ListToString(skillErrorList)}", "Cannot Generate Skill Modifiers", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return false;
            }

            return false;
        }

        private (bool, List<string>) GenerateModifiers()
        {
            List<string> skillErrorList = new List<string>();
            if (int.TryParse(Character.MiscStats.Proficiency.Trim().Replace(" ", ""), out int profBonus))
            {
                var isSuccessful = true;

                foreach (var skill in Character.AllSkills)
                {
                    switch (skill.StatType)
                    {
                        case StatType.Str:
                            isSuccessful = SetUpdatedStatModifier(skill, Character.MainStats.StrMod, profBonus);
                            break;
                        case StatType.Dex:
                            isSuccessful = SetUpdatedStatModifier(skill, Character.MainStats.DexMod, profBonus);
                            break;
                        case StatType.Con:
                            isSuccessful = SetUpdatedStatModifier(skill, Character.MainStats.ConMod, profBonus);
                            break;
                        case StatType.Int:
                            isSuccessful = SetUpdatedStatModifier(skill, Character.MainStats.IntlMod, profBonus);
                            break;
                        case StatType.Wis:
                            isSuccessful = SetUpdatedStatModifier(skill, Character.MainStats.WisMod, profBonus);
                            break;
                        case StatType.Cha:
                            isSuccessful = SetUpdatedStatModifier(skill, Character.MainStats.ChaMod, profBonus);
                            break;
                    }

                    CheckAnyGenerationErrors(isSuccessful, skill.StatType, skillErrorList);
                }

                return (skillErrorList.Count == 0, skillErrorList);
            }

            skillErrorList.Add("Proficiency Bonus");

            return (false, skillErrorList);
        }

        private void CheckAnyGenerationErrors(bool isSuccessful, StatType statName, List<string> skillErrorList)
        {
            if (!isSuccessful)
            {
                skillErrorList.Add(statName.ToString());
            }
        }

        private bool SetUpdatedStatModifier(Skill searchedSkill, string statMod, int profBonus)
        {
            if (int.TryParse(statMod.Trim(), out int intModifier))
            {
                searchedSkill.SkillScore = ConvertModifierToString(searchedSkill.IsProficient ? intModifier + profBonus : intModifier);
                return true;
            }

            return false;
        }

        private string ConvertModifierToString(int generatedModifier)
        {
            if (generatedModifier >= 0)
            {
                return generatedModifier.ToString().Insert(0, "+");
            }

            return generatedModifier.ToString();
        }
    }
}
