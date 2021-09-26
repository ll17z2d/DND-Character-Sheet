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
                case DND_Search_Types.Subclasses:
                    var subclasses = DeserializeClass<Subclasses>(OutputJson);
                    break;
                case DND_Search_Types.Features:
                    var features = DeserializeClass<Features>(OutputJson);
                    break;
                case DND_Search_Types.Races:
                    var races = DeserializeClass<Races>(OutputJson);
                    break;
                case DND_Search_Types.Subraces:
                    var subraces = DeserializeClass<Subraces>(OutputJson);
                    break;
                case DND_Search_Types.Conditions:
                    var conditions = DeserializeClass<Conditions>(OutputJson);
                    break;
                case DND_Search_Types.Spells:
                    var spells = DeserializeClass<Spells>(OutputJson);
                    break;
                case DND_Search_Types.Armour:
                    var armour = DeserializeClass<Armour>(OutputJson);
                    break;
                case DND_Search_Types.Weapons:
                    var weapons = DeserializeClass<Weapons>(OutputJson);
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
                OutputText.Add($"Name: {backgrounds.name}\n");
                OutputText.Add(
                    $"The {backgrounds.name} background feature is {backgrounds.feature.name}, the details of which are below: \n{TextFormatterWrapper.ListToString(backgrounds.feature.desc, true)}\n");
                OutputText.Add(
                    $"The {backgrounds.name} background lets you pick {backgrounds.bonds.choose} from the list of tools below:\n" +
                    $"{TextFormatterWrapper.ListToString(backgrounds.bonds.from)}\n");
                //OutputText.Add($"The {backgrounds.name} background lets you pick {backgrounds.ideals.choose} from the list of tools below:\n" +
                //               $"{TextFormatterWrapper.FormatList(backgrounds.ideals.from, "name")}\n");
                OutputText.Add(
                    $"The {backgrounds.name} background lets you pick {backgrounds.flaws.choose} from the list of tools below:\n" +
                    $"{TextFormatterWrapper.ListToString(backgrounds.flaws.from)}\n");
                OutputText.Add(
                    $"The {backgrounds.name} background starts with {TextFormatter.FormatList(backgrounds.starting_proficiencies, "name")}");
            }

            return backgrounds != null;
        }

        private bool ClassesOutput()
        {
            var classes = DeserializeClass<Classes>(OutputJson);
            if (classes != null)
            {
                OutputText.Add($"Name:  {classes.name}\n");
                OutputText.Add($"{classes.name}s are proficient in {TextFormatter.FormatList(classes.proficiencies, "name")}\n" +
                               $"as well as saving throw proficiencies in {TextFormatter.FormatList(classes.saving_throws, "name")}\n");
                OutputText.Add($"{classes.name}s also have a hit die of 1d{classes.hit_die}\n");
                OutputText.Add(
                    $"{classes.name}s can pick {classes.proficiency_choices[0].choose} from the list of proficiencies below:\n" +
                    $"{TextFormatter.FormatList(classes.proficiency_choices[0].from, "name")}");
                if (classes.proficiency_choices.Count > 1)
                {
                    OutputText.Add(
                        $"\nas well as pick {classes.proficiency_choices[1].choose} from the list of proficiencies below:\n" +
                        $"{TextFormatter.FormatList(classes.proficiency_choices[1].from, "name")}");
                }

                if (classes.proficiency_choices.Count > 2)
                {
                    OutputText.Add(
                        $"\nand finally pick {classes.proficiency_choices[2].choose} from the list of proficiencies below:\n" +
                        $"{TextFormatter.FormatList(classes.proficiency_choices[2].from, "name")}\n");
                }

                //OutputText.Add($"{classes.name}s can pick {classes.proficiency_choices[0].choose} from the list of tools below:\n" +
                //               $"{StringFormatter.FormatList(classes.proficiency_choices[0].from, "name")}\n" +
                //               $"as well as pick {classes.proficiency_choices[1].choose} from the list of equipment below:\n" +
                //               $"{StringFormatter.FormatList(classes.proficiency_choices[1].from, "name")}\n" +
                //               $"and finally pick {classes.proficiency_choices[2].choose} from the list of skills below:\n" +
                //               $"{StringFormatter.FormatList(classes.proficiency_choices[2].from, "name")}\n");
            }

            return classes != null;
        }

        



    }
}
