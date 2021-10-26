using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DND_Character_Sheet.Enums;
using DND_Character_Sheet.Models;
using DND_Character_Sheet.Models.API_Models;
using DND_Character_Sheet.Useful_Methods;
using DND_Character_Sheet.Wrappers;
using Newtonsoft.Json;

namespace DND_Character_Sheet.Serialization
{
    public class DeserializeAPI
    {
        public DND_Search_Types SearchTypeEnum { get; set; }
        public string OutputJson { get; set; }
        public List<string> OutputText { get; set; }
        public ITextFormatterWrapper TextFormatterWrapper { get; set; }

        public DeserializeAPI(DND_Search_Types searchtypeenum, string outputjson, ITextFormatterWrapper textFormatterWrapper)
        {
            this.SearchTypeEnum = searchtypeenum;
            this.OutputJson = outputjson;
            OutputText = new List<string>();
            TextFormatterWrapper = textFormatterWrapper;
        }

        public (List<string>, bool) Deserialize()
        {
            bool isSuccessful = false;
            switch (SearchTypeEnum)
            {
                case DND_Search_Types.Backgrounds:
                    isSuccessful = BackgroundsOutput();
                    break;
                case DND_Search_Types.Classes:
                    isSuccessful = ClassesOutput();
                    break;
                //case DND_Search_Types.Subclasses:
                //    var subclasses = DeserializeClass<Subclasses>(OutputJson);
                //    break;
                case DND_Search_Types.Features:
                    isSuccessful = FeaturesOutput();
                    break;
                case DND_Search_Types.Races:
                    isSuccessful = RacesOutput();
                    break;
                case DND_Search_Types.Subraces:
                    isSuccessful = SubracesOutput();
                    break;
                case DND_Search_Types.Conditions:
                    isSuccessful = ConditionsOutput();
                    break;
                case DND_Search_Types.Spells:
                    isSuccessful = SpellsOutput();
                    break;
                case DND_Search_Types.Armour:
                    isSuccessful = ArmourOutput();
                    break;
                case DND_Search_Types.Weapons:
                    isSuccessful = WeaponsOutput();
                    break;
            }

            return (OutputText, isSuccessful);
        }

        private T DeserializeClass<T>(string outputJson) 
            => JsonConvert.DeserializeObject<T>(outputJson);

        private bool BackgroundsOutput()
        {
            var backgrounds = DeserializeClass<Backgrounds>(OutputJson);
            if (backgrounds != null)
            {
                OutputText.Add($"Background: {backgrounds.name}\n");
                OutputText.Add(
                    $"The {backgrounds.name} background feature is {backgrounds.feature.name}, the details of which are below: \n{TextFormatterWrapper.ListToString(backgrounds.feature.desc, true)}\n");
                OutputText.Add(
                    $"The {backgrounds.name} background lets you pick {backgrounds.bonds.choose} from the list of bonds below:\n" +
                    $"{TextFormatterWrapper.ListToString(backgrounds.bonds.from)}\n");
                //OutputText.Add($"The {backgrounds.name} background lets you pick {backgrounds.ideals.choose} from the list of tools below:\n" +
                //               $"{TextFormatterWrapper.FormatList(backgrounds.ideals.from, "name")}\n");
                OutputText.Add(
                    $"The {backgrounds.name} background lets you pick {backgrounds.flaws.choose} from the list of flaws below:\n" +
                    $"{TextFormatterWrapper.ListToString(backgrounds.flaws.from)}\n");
                OutputText.Add(
                    $"The {backgrounds.name} background starts with {TextFormatterWrapper.FormatList(backgrounds.starting_proficiencies, "name")}");
            }

            return backgrounds != null;
        }

        private bool ClassesOutput()
        {
            var classes = DeserializeClass<Classes>(OutputJson);
            if (classes != null)
            {
                OutputText.Add($"Class:  {classes.name}\n");
                OutputText.Add($"{classes.name}s are proficient in {TextFormatterWrapper.FormatList(classes.proficiencies, "name")}\n" +
                               $"as well as saving throw proficiencies in {TextFormatterWrapper.FormatList(classes.saving_throws, "name")}\n");
                OutputText.Add($"{classes.name}s also have a hit die of 1d{classes.hit_die}\n");
                OutputText.Add(
                    $"{classes.name}s can pick {classes.proficiency_choices[0].choose} from the list of proficiencies below:\n" +
                    $"{TextFormatterWrapper.FormatList(classes.proficiency_choices[0].from, "name")}");
                if (classes.proficiency_choices.Count > 1)
                {
                    OutputText.Add(
                        $"\nas well as pick {classes.proficiency_choices[1].choose} from the list of proficiencies below:\n" +
                        $"{TextFormatterWrapper.FormatList(classes.proficiency_choices[1].from, "name")}");
                }

                if (classes.proficiency_choices.Count > 2)
                {
                    OutputText.Add(
                        $"\nand finally pick {classes.proficiency_choices[2].choose} from the list of proficiencies below:\n" +
                        $"{TextFormatterWrapper.FormatList(classes.proficiency_choices[2].from, "name")}\n");
                }
            }

            return classes != null;
        }

        private bool FeaturesOutput()
        {
            var features = DeserializeClass<Features>(OutputJson);

            if (features != null)
            {
                OutputText.Add($"Feature:  {features.name}\n");
                OutputText.Add($"Level:  {features.level}\n");
                OutputText.Add($"Class: {features.@class.index}");
                OutputText.Add(
                    $"The details of this feature can be seen below: \n{TextFormatterWrapper.ListToString(features.desc, true)}\n");
            }

            return features != null;
        }

        private bool RacesOutput()
        {
            var races = DeserializeClass<Races>(OutputJson);

            if (races != null)
            {
                OutputText.Add($"Race:  {races.name}\n");
                OutputText.Add($"Speed:  {races.speed}\n");
                OutputText.Add($"Size:  {races.size_description}\n");
                OutputText.Add($"Language Proficiencies:  {races.language_desc}\n");
                OutputText.Add($"Alignment:  {races.alignment}\n");
                OutputText.Add($"Age:  {races.age}\n");
                OutputText.Add($"This race gets the following ability score improvements of {TextFormatterWrapper.ListToString(races.ability_bonuses.Select(x => x.bonus.ToString()).ToList()).Replace("\n","")} respectively:" + $"{TextFormatterWrapper.ListToString(races.ability_bonuses.Select(x => x.ability_score.name).ToList())}\n");
                var pluralProficiency = races.starting_proficiencies.Count == 1 ? "proficiency" : "proficiencies";
                OutputText.Add($"This race can pick {races.starting_proficiencies.Count} weapon {pluralProficiency} from the list of proficiencies below:\n" +
                               $"{TextFormatterWrapper.FormatList(races.starting_proficiencies, "name")}\n");
                OutputText.Add($"This race has the following traits below:\n" + $"{TextFormatterWrapper.FormatList(races.traits, "name")}\n");
            }

            return races != null;
        }

        private bool SubracesOutput()
        {
            var subraces = DeserializeClass<Subraces>(OutputJson);

            if (subraces != null)
            {
                OutputText.Add($"Subrace:  {subraces.name}\n");
                if (subraces.languages.Count != 0) 
                    OutputText.Add($"Languages:  {TextFormatterWrapper.FormatList(subraces.languages, "name")}\n");
                OutputText.Add($"This subrace gets the following ability score improvements of {TextFormatterWrapper.ListToString(subraces.ability_bonuses.Select(x => x.bonus.ToString()).ToList()).Replace("\n", "")} respectively:" + $"{TextFormatterWrapper.ListToString(subraces.ability_bonuses.Select(x => x.ability_score.name).ToList())}\n");
                var pluralProficiency = subraces.starting_proficiencies.Count == 1 ? "proficiency" : "proficiencies";
                if (subraces.starting_proficiencies.Count != 0)
                    OutputText.Add(
                        $"This subrace can pick {subraces.starting_proficiencies.Count} weapon {pluralProficiency} from the list of proficiencies below:\n" +
                        $"{TextFormatterWrapper.FormatList(subraces.starting_proficiencies, "name")}\n");
                if (subraces.racial_traits.Count != 0)
                    OutputText.Add($"This subrace has the following traits below:\n" +
                                   $"{TextFormatterWrapper.FormatList(subraces.racial_traits, "name")}\n");
            }

            return subraces != null;
        }

        private bool ConditionsOutput()
        {
            var conditions = DeserializeClass<Conditions>(OutputJson);

            if (conditions != null)
            {
                OutputText.Add($"Condition:  {conditions.name}\n");
                OutputText.Add($"Description:  {TextFormatterWrapper.ListToString(conditions.desc)}\n");
            }

            return conditions != null;
        }

        private bool SpellsOutput()
        {
            var spells = DeserializeClass<Spells>(OutputJson);

            if (spells != null)
            {
                OutputText.Add($"Spell:  {spells.name}\n");
                OutputText.Add($"Level {spells.level} {spells.school.name} spell\n");
                OutputText.Add($"Description:  {TextFormatterWrapper.ListToString(spells.desc)}\n {TextFormatterWrapper.ListToString(spells.higher_level)}\n");
                OutputText.Add($"Range:  {spells.range}\n");
                OutputText.Add($"Requires Concentration:  {spells.concentration}\n");
                OutputText.Add($"Duration:  {spells.duration}\n");
                OutputText.Add($"Casting Time:  {spells.casting_time}\n");
                OutputText.Add($"Attack Type:  {spells.attack_type}\n");
                OutputText.Add($"Damage Type:  {spells.damage.damage_type.name}\n");
                OutputText.Add($"Damage At Slot Levels:  {TextFormatterWrapper.DictionaryToString(spells.damage.damage_at_slot_level, true)}\n");
                OutputText.Add($"Components: {TextFormatterWrapper.ListToString(spells.components)}\n(material required: {spells.material.ToLower()}\n");
            }

            return spells != null;
        }

        private bool ArmourOutput()
        {
            var armour = DeserializeClass<Armour>(OutputJson);

            if (armour != null)
            {
                OutputText.Add($"Armour:  {armour.name}\n");
                OutputText.Add($"Armour Category:  {armour.armor_category}\n");
                OutputText.Add($"Armour Class:  {armour.armor_class.@base} (can use DEX bonus: {armour.armor_class.dex_bonus.ToString().ToLower()}\n");
                OutputText.Add($"Minimum Strength:  {armour.str_minimum}\n");
                OutputText.Add($"Cost:  {armour.cost.quantity} {armour.cost.unit}\n");
                OutputText.Add($"Weight:  {armour.weight}\n");
                OutputText.Add($"Stealth Disadvantage:  {armour.stealth_disadvantage}\n");
            }

            return armour != null;
        }

        private bool WeaponsOutput()
        {
            var weapons = DeserializeClass<Weapons>(OutputJson);

            if (weapons != null)
            {
                OutputText.Add($"Weapon:  {weapons.name}\n");
                OutputText.Add($"Weapon Category:  {weapons.category_range}\n");
                OutputText.Add($"Damage:  {weapons.damage.damage_dice} ({weapons.damage.damage_type.name})\n");
                var normalRange = weapons.range.normal ?? 0;
                var longRange = weapons.range.@long ?? 0;
                OutputText.Add($"Range:  Normal = {normalRange}    Long = {longRange}\n");
                OutputText.Add($"Cost:  {weapons.cost.quantity} {weapons.cost.unit}\n");
                OutputText.Add($"Weight:  {weapons.weight}\n");
            }

            return weapons != null;
        }
    }
}
