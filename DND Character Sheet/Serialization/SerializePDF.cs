using DND_Character_Sheet.Models.Serialize_Types;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND_Character_Sheet.Serialization
{
    public interface ISerializePDF
    {
        public bool Serialize(ICharacterModel character);
    }

    public class SerializePDF : ISerializePDF
    {
        public bool Serialize(ICharacterModel character)
        {
            string fileNameExisting = @"C:\Users\zakar\OneDrive\Documents\DnD Sheet Saves\PDF Testing\Official 5E Character Sheet.pdf";
            string fileNameNew = @"C:\Users\zakar\OneDrive\Documents\DnD Sheet Saves\PDF Testing\Generated Official 5E Character Sheet.pdf";

            using (var existingFileStream = new FileStream(fileNameExisting, FileMode.Open))
            using (var newFileStream = new FileStream(fileNameNew, FileMode.Create))
            {
                // Open existing PDF
                var pdfReader = new PdfReader(existingFileStream);

                // PdfStamper, which will create
                var stamper = new PdfStamper(pdfReader, newFileStream);

                var form = stamper.AcroFields;
                var fieldKeys = form.Fields.Keys;

                //PDF field values seem to only be stored as strings, so when I deserialize, that's when I will have to convert to their relevant types
                //Will have to somehow check the "Prepared Spells" checkboxes, and add the character pic to the "Character Appearance" box
                //var stringDict = new Dictionary<string, string>();
                var dict = new Dictionary<string, object>()
                {
                    {"ClassLevel", character.DetailsStats.ClassAndLevel}, //Extract just the level from here by indexing out just the numbers
                    {"Background", character.DetailsStats.Background},
                    {"PlayerName", character.DetailsStats.PlayerName},
                    {"CharacterName", character.DetailsStats.CharacterName},
                    {"Race ", character.DetailsStats.Race},
                    {"Alignment", character.DetailsStats.Alignment},
                    {"XP", character.DetailsStats.ExperiencePoints},
                    {"STR", character.MainStats.Str.ToString()},
                    {"Inspiration", character.MiscStats.Inspiration},
                    {"ProfBonus", character.MiscStats.Proficiency},
                    {"AC", character.MiscStats.AC.ToString()},
                    {"Initiative", character.MiscStats.Initiative},
                    {"Speed", character.MiscStats.Speed},
                    {"PersonalityTraits ", "temp"},
                    {"STRmod", character.MainStats.StrMod},
                    {"ST Strength", character.MainStats.StrSvThr},
                    {"DEX", character.MainStats.Dex.ToString()},
                    {"Ideals", "temp"},
                    {"DEXmod ", character.MainStats.DexMod},
                    {"Bonds", "temp"},
                    {"CON", character.MainStats.Con.ToString()},
                    {"HDTotal", character.HPStats.HitDie},
                    {"Check Box 12", "1"},
                    {"Check Box 13", "1"},
                    {"Check Box 14", "1"},
                    {"CONmod", character.MainStats.ConMod},
                    {"Check Box 15", "1"},
                    {"Check Box 16", "2"},
                    {"Check Box 17", ""},
                    {"HD", character.HPStats.HitDie},
                    {"Flaws", "temp"},
                    {"INT", character.MainStats.Intl.ToString()},
                    {"ST Dexterity", character.MainStats.DexSvThr},
                    {"ST Constitution", character.MainStats.ConSvThr},
                    {"ST Intelligence", character.MainStats.IntlSvThr},
                    {"ST Wisdom", character.MainStats.WisSvThr},
                    {"ST Charisma", character.MainStats.ChaSvThr},
                    {"Acrobatics", character.AllSkills.Acrobatics.SkillScore},
                    {"Animal", character.AllSkills.AnimalHandling.SkillScore},
                    {"Athletics", character.AllSkills.Athletics.SkillScore},
                    {"Deception ", character.AllSkills.Deception.SkillScore},
                    {"History ", character.AllSkills.History.SkillScore},
                    {"Wpn Name", character.WeaponNotes.WeaponsInventory[0].WeaponName},
                    {"Wpn1 AtkBonus", character.WeaponNotes.WeaponsInventory[0].AttackBonus},
                    {"Wpn1 Damage", character.WeaponNotes.WeaponsInventory[0].Damage},
                    {"Insight", character.AllSkills.Insight.SkillScore},
                    {"Intimidation", character.AllSkills.Intimidation.SkillScore},
                    {"Wpn Name 2", character.WeaponNotes.WeaponsInventory[1].WeaponName},
                    {"Wpn2 AtkBonus ", character.WeaponNotes.WeaponsInventory[1].AttackBonus},
                    {"Wpn Name 3", character.WeaponNotes.WeaponsInventory[2].WeaponName},
                    {"Wpn3 AtkBonus  ", character.WeaponNotes.WeaponsInventory[2].AttackBonus},
                    {"Check Box 11", "temp"},
                    {"Check Box 18", "temp"},
                    {"Check Box 19", "temp"},
                    {"Check Box 20", "temp"},
                    {"Check Box 21", "temp"},
                    {"Check Box 22", "temp"},
                    {"INTmod", character.MainStats.IntlMod},
                    {"Wpn2 Damage ", character.WeaponNotes.WeaponsInventory[1].Damage},
                    {"Investigation ", character.AllSkills.Investigation.SkillScore},
                    {"WIS", character.MainStats.Wis.ToString()},
                    {"Arcana", character.AllSkills.Arcana.SkillScore},
                    {"Perception ", character.AllSkills.Perception.SkillScore},
                    {"WISmod", character.MainStats.WisMod},
                    {"CHA", character.MainStats.Cha.ToString()},
                    {"Nature", character.AllSkills.Nature.SkillScore},
                    {"Performance", character.AllSkills.Performance.SkillScore},
                    {"Medicine", character.AllSkills.Medicine.SkillScore},
                    {"Religion", character.AllSkills.Religion.SkillScore},
                    {"Stealth ", character.AllSkills.Stealth.SkillScore},
                    {"Check Box 23", "temp"},
                    {"Check Box 24", "temp"},
                    {"Check Box 25", "temp"},
                    {"Check Box 26", "temp"},
                    {"Check Box 27", "temp"},
                    {"Check Box 28", "temp"},
                    {"Check Box 29", "temp"},
                    {"Check Box 30", "temp"},
                    {"Check Box 31", "temp"},
                    {"Check Box 32", "temp"},
                    {"Check Box 33", "temp"},
                    {"Check Box 34", "temp"},
                    {"Check Box 35", "temp"},
                    {"Check Box 36", "temp"},
                    {"Check Box 37", "temp"},
                    {"Check Box 38", "temp"},
                    {"Check Box 39", "temp"},
                    {"Check Box 40", "temp"},
                    {"Persuasion", character.AllSkills.Persuasion.SkillScore},
                    {"HPMax", character.HPStats.MaxHP.ToString()},
                    {"HPCurrent", character.HPStats.CurrentHP.ToString()},
                    {"HPTemp", character.HPStats.TempHP.ToString()},
                    {"Wpn3 Damage ", character.WeaponNotes.WeaponsInventory[2].Damage},
                    {"SleightofHand", character.AllSkills.SleightOfHand.SkillScore},
                    {"CHamod", character.MainStats.ChaMod},
                    {"Survival", character.AllSkills.Survival.SkillScore},
                    {"AttacksSpellcasting", character.WeaponNotes.WeaponDetails},
                    {"Passive", character.MiscStats.PassivePerception},
                    {"CP", character.CharacterNotes.Money.Bronze},
                    {"ProficienciesLang", character.CharacterNotes.Proficiencies},
                    {"SP", character.CharacterNotes.Money.Silver},
                    {"EP", "temp"},
                    {"GP", character.CharacterNotes.Money.Gold},
                    {"PP", character.CharacterNotes.Money.Platinum},
                    {"Equipment", character.CharacterNotes.Equipment},
                    {"Features and Traits", character.CharacterNotes.AbilityDesc},
                    {"CharacterName 2", character.DetailsStats.CharacterName},
                    {"Age", "temp"},
                    {"Height", "temp"},
                    {"Weight", "temp"},
                    {"Eyes", "temp"},
                    {"Skin", "temp"},
                    {"Hair", "temp"},
                    {"CHARACTER IMAGE", "temp"},
                    {"Faction Symbol Image", "temp"},
                    {"Allies", character.CharacterNotes.SessionNotes},
                    {"FactionName", "temp"},
                    {"Backstory", "temp"},
                    {"Feat+Traits", "temp"},
                    {"Treasure", "temp"},
                    {"Spellcasting Class 2", string.Join("", character.DetailsStats.ClassAndLevel.Where(x => !char.IsDigit(x) && x != ' ').ToList())}, //Get out details
                    {"SpellcastingAbility 2", character.AllSpells.SpellcastingAbility},
                    {"SpellSaveDC  2", character.AllSpells.SpellSaveDC},
                    {"SpellAtkBonus 2", character.AllSpells.SpellAttackBonus},
                    {"SlotsTotal 19", character.AllSpells.FirstLevelSpellViewModel.SlotsTotal},
                    {"SlotsRemaining 19", character.AllSpells.FirstLevelSpellViewModel.SlotsExpended},
                    {"Spells 1014", "temp"},
                    {"Spells 1015", "temp"},
                    {"Spells 1016", "temp"},
                    {"Spells 1017", "temp"},
                    {"Spells 1018", "temp"},
                    {"Spells 1019", "temp"},
                    {"Spells 1020", "temp"},
                    {"Spells 1021", "temp"},
                    {"Spells 1022", "temp"},
                    {"Check Box 314", "temp"},
                    {"Check Box 3031", "temp"},
                    {"Check Box 3032", "temp"},
                    {"Check Box 3033", "temp"},
                    {"Check Box 3034", "temp"},
                    {"Check Box 3035", "temp"},
                    {"Check Box 3036", "temp"},
                    {"Check Box 3037", "temp"},
                    {"Check Box 3038", "temp"},
                    {"Check Box 3039", "temp"},
                    {"Check Box 3040", "temp"},
                    {"Check Box 321", "temp"},
                    {"Check Box 320", "temp"},
                    {"Check Box 3060", "temp"},
                    {"Check Box 3061", "temp"},
                    {"Check Box 3062", "temp"},
                    {"Check Box 3063", "temp"},
                    {"Check Box 3064", "temp"},
                    {"Check Box 3065", "temp"},
                    {"Check Box 3066", "temp"},
                    {"Check Box 315", "temp"},
                    {"Check Box 3041", "temp"},
                    {"Spells 1023", "temp"},
                    {"Check Box 251", "temp"},
                    {"Check Box 309", "temp"},
                    {"Check Box 3010", "temp"},
                    {"Check Box 3011", "temp"},
                    {"Check Box 3012", "temp"},
                    {"Check Box 3013", "temp"},
                    {"Check Box 3014", "temp"},
                    {"Check Box 3015", "temp"},
                    {"Check Box 3016", "temp"},
                    {"Check Box 3017", "temp"},
                    {"Check Box 3018", "temp"},
                    {"Check Box 3019", "temp"},
                    {"Spells 1024", "temp"},
                    {"Spells 1025", "temp"},
                    {"Spells 1026", "temp"},
                    {"Spells 1027", "temp"},
                    {"Spells 1028", "temp"},
                    {"Spells 1029", "temp"},
                    {"Spells 1030", "temp"},
                    {"Spells 1031", "temp"},
                    {"Spells 1032", "temp"},
                    {"Spells 1033", "temp"},
                    {"SlotsTotal 20", character.AllSpells.SecondLevelSpellViewModel.SlotsTotal},
                    {"SlotsRemaining 20", character.AllSpells.SecondLevelSpellViewModel.SlotsExpended},
                    {"Spells 1034", "temp"},
                    {"Spells 1035", "temp"},
                    {"Spells 1036", "temp"},
                    {"Spells 1037", "temp"},
                    {"Spells 1038", "temp"},
                    {"Spells 1039", "temp"},
                    {"Spells 1040", "temp"},
                    {"Spells 1041", "temp"},
                    {"Spells 1042", "temp"},
                    {"Spells 1043", "temp"},
                    {"Spells 1044", "temp"},
                    {"Spells 1045", "temp"},
                    {"Spells 1046", "temp"},
                    {"SlotsTotal 21", character.AllSpells.ThirdLevelSpellViewModel.SlotsTotal},
                    {"SlotsRemaining 21", character.AllSpells.ThirdLevelSpellViewModel.SlotsExpended},
                    {"Spells 1047", "temp"},
                    {"Spells 1048", "temp"},
                    {"Spells 1049", "temp"},
                    {"Spells 1050", "temp"},
                    {"Spells 1051", "temp"},
                    {"Spells 1052", "temp"},
                    {"Spells 1053", "temp"},
                    {"Spells 1054", "temp"},
                    {"Spells 1055", "temp"},
                    {"Spells 1056", "temp"},
                    {"Spells 1057", "temp"},
                    {"Spells 1058", "temp"},
                    {"Spells 1059", "temp"},
                    {"SlotsTotal 22", character.AllSpells.FourthLevelSpellViewModel.SlotsTotal},
                    {"SlotsRemaining 22", character.AllSpells.FourthLevelSpellViewModel.SlotsExpended},
                    {"Spells 1060", "temp"},
                    {"Spells 1061", "temp"},
                    {"Spells 1062", "temp"},
                    {"Spells 1063", "temp"},
                    {"Spells 1064", "temp"},
                    {"Check Box 323", "temp"},
                    {"Check Box 322", "temp"},
                    {"Check Box 3067", "temp"},
                    {"Check Box 3068", "temp"},
                    {"Check Box 3069", "temp"},
                    {"Check Box 3070", "temp"},
                    {"Check Box 3071", "temp"},
                    {"Check Box 3072", "temp"},
                    {"Check Box 3073", "temp"},
                    {"Spells 1065", "temp"},
                    {"Spells 1066", "temp"},
                    {"Spells 1067", "temp"},
                    {"Spells 1068", "temp"},
                    {"Spells 1069", "temp"},
                    {"Spells 1070", "temp"},
                    {"Spells 1071", "temp"},
                    {"Check Box 317", "temp"},
                    {"Spells 1072", "temp"},
                    {"SlotsTotal 23", character.AllSpells.FifthLevelSpellViewModel.SlotsTotal},
                    {"SlotsRemaining 23", character.AllSpells.FifthLevelSpellViewModel.SlotsExpended},
                    {"Spells 1073", "temp"},
                    {"Spells 1074", "temp"},
                    {"Spells 1075", "temp"},
                    {"Spells 1076", "temp"},
                    {"Spells 1077", "temp"},
                    {"Spells 1078", "temp"},
                    {"Spells 1079", "temp"},
                    {"Spells 1080", "temp"},
                    {"Spells 1081", "temp"},
                    {"SlotsTotal 24", character.AllSpells.SixthLevelSpellViewModel.SlotsTotal},
                    {"SlotsRemaining 24", character.AllSpells.SixthLevelSpellViewModel.SlotsExpended},
                    {"Spells 1082", "temp"},
                    {"Spells 1083", "temp"},
                    {"Spells 1084", "temp"},
                    {"Spells 1085", "temp"},
                    {"Spells 1086", "temp"},
                    {"Spells 1087", "temp"},
                    {"Spells 1088", "temp"},
                    {"Spells 1089", "temp"},
                    {"Spells 1090", "temp"},
                    {"SlotsTotal 25", character.AllSpells.SeventhLevelSpellViewModel.SlotsTotal},
                    {"SlotsRemaining 25", character.AllSpells.SeventhLevelSpellViewModel.SlotsExpended},
                    {"Spells 1091", "temp"},
                    {"Spells 1092", "temp"},
                    {"Spells 1093", "temp"},
                    {"Spells 1094", "temp"},
                    {"Spells 1095", "temp"},
                    {"Spells 1096", "temp"},
                    {"Spells 1097", "temp"},
                    {"Spells 1098", "temp"},
                    {"Spells 1099", "temp"},
                    {"SlotsTotal 26", character.AllSpells.EighthLevelSpellViewModel.SlotsTotal},
                    {"SlotsRemaining 26", character.AllSpells.SeventhLevelSpellViewModel.SlotsExpended},
                    {"Spells 10100", "temp"},
                    {"Spells 10101", "temp"},
                    {"Spells 10102", "temp"},
                    {"Spells 10103", "temp"},
                    {"Check Box 316", "temp"},
                    {"Check Box 3042", "temp"},
                    {"Check Box 3043", "temp"},
                    {"Check Box 3044", "temp"},
                    {"Check Box 3045", "temp"},
                    {"Check Box 3046", "temp"},
                    {"Check Box 3047", "temp"},
                    {"Check Box 3048", "temp"},
                    {"Check Box 3049", "temp"},
                    {"Check Box 3050", "temp"},
                    {"Check Box 3051", "temp"},
                    {"Check Box 3052", "temp"},
                    {"Spells 10104", "temp"},
                    {"Check Box 325", "temp"},
                    {"Check Box 324", "temp"},
                    {"Check Box 3074", "temp"},
                    {"Check Box 3075", "temp"},
                    {"Check Box 3076", "temp"},
                    {"Check Box 3077", "temp"},
                    {"Spells 10105", "temp"},
                    {"Spells 10106", "temp"},
                    {"Check Box 3078", "temp"},
                    {"SlotsTotal 27", character.AllSpells.NinthLevelSpellViewModel.SlotsTotal},
                    {"SlotsRemaining 27", character.AllSpells.NinthLevelSpellViewModel.SlotsExpended},
                    {"Check Box 313", "temp"},
                    {"Check Box 310", "temp"},
                    {"Check Box 3020", "temp"},
                    {"Check Box 3021", "temp"},
                    {"Check Box 3022", "temp"},
                    {"Check Box 3023", "temp"},
                    {"Check Box 3024", "temp"},
                    {"Check Box 3025", "temp"},
                    {"Check Box 3026", "temp"},
                    {"Check Box 3027", "temp"},
                    {"Check Box 3028", "temp"},
                    {"Check Box 3029", "temp"},
                    {"Check Box 3030", "temp"},
                    {"Spells 10107", "temp"},
                    {"Spells 10108", "temp"},
                    {"Spells 10109", "temp"},
                    {"Spells 101010", "temp"},
                    {"Spells 101011", "temp"},
                    {"Spells 101012", "temp"},
                    {"Check Box 319", "temp"},
                    {"Check Box 318", "temp"},
                    {"Check Box 3053", "temp"},
                    {"Check Box 3054", "temp"},
                    {"Check Box 3055", "temp"},
                    {"Check Box 3056", "temp"},
                    {"Check Box 3057", "temp"},
                    {"Check Box 3058", "temp"},
                    {"Check Box 3059", "temp"},
                    {"Check Box 327", "temp"},
                    {"Check Box 326", "temp"},
                    {"Check Box 3079", "temp"},
                    {"Check Box 3080", "temp"},
                    {"Check Box 3081", "temp"},
                    {"Check Box 3082", "temp"},
                    {"Spells 101013", "temp"},
                    {"Check Box 3083", "temp"},
                };

                var counter = 0;
                foreach (string fieldKey in fieldKeys)
                {
                    //if (fieldKey.GetType() == typeof(string))
                    //{
                    //    form.SetField(fieldKey, "REPLACED!");
                    //}


                    counter++;
                    //form.SetField(fieldKey, dict.GetValueOrDefault(fieldKey));
                    if (dict.GetValueOrDefault(fieldKey).GetType() == typeof(string))
                    {
                        form.SetField(fieldKey, dict.GetValueOrDefault(fieldKey).ToString());
                    }
                    else
                    {
                        form.GetFieldItem("Check Box 12");
                        //form.SetListSelection(fieldKey, dict.GetValueOrDefault(fieldKey));
                    }

                    var p = form.GetFieldItem("Check Box 12");
                    var q = form.GetFieldItem("ClassLevel");
                    var s = form.GetField("Check Box 12");
                    var t = form.GetField("ClassLevel");

                    if (!dict.TryGetValue(fieldKey, out var value))
                    {
                        throw new Exception($"Hmm, it seems {fieldKey} isn't properly implemented");
                    }
                }

                stamper.Close();
                pdfReader.Close();
            }

            var x = string.Join("", character.DetailsStats.ClassAndLevel.Where(x => !char.IsDigit(x) && x != ' ').ToList());
            return true;
        }

        private bool SerializePageOne()
        {


            return true;
        }
    }
}
