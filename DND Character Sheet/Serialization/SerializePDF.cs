using DND_Character_Sheet.Constants;
using DND_Character_Sheet.Enums;
using DND_Character_Sheet.Models.API_Models;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.ViewModels;
using DND_Character_Sheet.Wrappers;
using iText.Forms;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace DND_Character_Sheet.Serialization
{
    public interface ISerializePDF
    {
        public Dictionary<string, string> FieldKeyToValueDict { get; set; }

        public void Serialize(ICharacterModel character);

        public ICharacterModel Deserialize(string filePath);
    }

    public class SerializePDF : ISerializePDF
    {
        public Dictionary<string, string> FieldKeyToValueDict { get; set; }

        public Dictionary<string, string> TempDict { get; set; }

        public void Serialize(ICharacterModel character)
        {
            InitialiseDictionary(character);
            var fileNameNew = @"C:\Users\zakar\OneDrive\Documents\DnD Sheet Saves\PDF Testing\Generated Official 5E Character Sheet.pdf";
            //var fileNameNew = character.FilePath;
            var fileNameExisting = Application.GetResourceStream(new Uri(@"pack://application:,,,/DND Character Sheet;component/Default Documents/Official 5E Character Sheet.pdf"));

            using (var existingFileStream = fileNameExisting.Stream) //Assembly.GetExecutingAssembly().GetManifestResourceStream("DND_Character_Sheet.Default_Documents.Official 5E Character Sheet.pdf")) //new FileStream(fileNameExisting, FileMode.Open))
            using (var newFileStream = new FileStream(fileNameNew, FileMode.Create))
            {
                var pdfDoc = new PdfDocument(new PdfReader(existingFileStream), new PdfWriter(newFileStream));
                var form = PdfAcroForm.GetAcroForm(pdfDoc, true);

                //PDF field values seem to only be stored as strings, so when I deserialize, that's when I will have to convert to their relevant types
                //Will have to somehow check the "Prepared Spells" checkboxes, and add the character pic to the "Character Appearance" box
                foreach (var fieldkey in FieldKeyToValueDict)
                {
                    if (fieldkey.Key.ToLower().Replace(" ", string.Empty).Contains("checkbox"))
                    {
                        var setValue = TernaryOperatorOnValue(
                            (form.GetField(fieldkey.Key).GetValueAsString() == "1" || form.GetField(fieldkey.Key).GetValueAsString().ToLower() == "true")
                            ? true : false, "1", "");
                    }
                    form.GetField(fieldkey.Key).SetValue(fieldkey.Value);
                    //form.GetField(fieldkey.Key).SetValue(AccessClassProperties.GetNestedPropertyValue(character, TempDict[fieldkey.Key]).ToString());
                }

                pdfDoc.Close();
            }
        }

        public ICharacterModel Deserialize(string filePath)
        { //Need a proper way to handle if an incorrect pdf is attempted to serialise

            //var character = new CharacterModel();
            //InitialiseDictionary(character);

            //var fileNameExisting = Application.GetResourceStream(new Uri(@"pack://application:,,,/DND Character Sheet;component/Default Documents/Official 5E Character Sheet.pdf"));
            //Put var existingFileStream = fileNameExisting.Stream

            var character = new CharacterModel();
            var fileNameExisting = @"C:\Users\zakar\OneDrive\Documents\DnD Sheet Saves\PDF Testing\Vimsor PDF.pdf"; //Switch this at the end

            using (var existingFileStream = new FileStream(fileNameExisting, FileMode.Open)) //Assembly.GetExecutingAssembly().GetManifestResourceStream("DND_Character_Sheet.Default_Documents.Official 5E Character Sheet.pdf")) //new FileStream(fileNameExisting, FileMode.Open))
            {
                var pdfDoc = new PdfDocument(new PdfReader(existingFileStream));
                var form = PdfAcroForm.GetAcroForm(pdfDoc, true);

                character = new CharacterModel(GetMainStats(form),
                GetAllSkills(form),
                GetHPStats(form),
                GetDetailsStats(form),
                GetMiscStats(form),
                GetCharacterNotes(form, filePath),
                GetWeaponNotes(form),
                GetAllSpells(form));

                character.FilePath = filePath;

                pdfDoc.Close();

                //foreach (var fieldkey in TempDict)
                //{
                //    //fieldkey.Value = 2;
                //    //var a = TempDict[character.DetailsStats.ClassAndLevel];
                //    //TempDict[character.DetailsStats.ClassAndLevel] = "New Class & Level";
                //    //var b = character.DetailsStats.ClassAndLevel;

                //    //var c = typeof(DetailsStats).GetProperty("ClassAndLevel");
                //    //var d = form.GetField(fieldkey.Key);
                //    //var test = form.GetField("bla bla");
                //    //var e = d.GetValue();
                //    //c.SetValue(character, e);

                //    //var g = form.GetField(fieldkey.Key).GetValueAsString();
                //    //var h = form.GetField(fieldkey.Key).GetValue();
                //    //var i = form.GetField(fieldkey.Key).GetFieldName();


                //    //var testResolver = new Resolver().Resolve("please work lol", "DetailsStats.ClassAndLevel");
                //    //character.GetType().GetProperty(fieldkey.Key).SetValue(character, "please work lol");

                //    try
                //    {
                //        object setValue;
                //        if (fieldkey.Key.ToLower().Replace(" ", string.Empty).Contains("checkbox"))
                //        {
                //            //var a = (form.GetField(fieldkey.Key).GetValueAsString() == "1" || form.GetField(fieldkey.Key).GetValueAsString().ToLower() == "true") ? true : false;
                //            //var x = form.GetField(fieldkey.Key).GetValueAsString();
                //            //var y = bool.Parse(form.GetField(fieldkey.Key).GetValueAsString());
                //            setValue = (form.GetField(fieldkey.Key).GetValueAsString() == "1" 
                //                || form.GetField(fieldkey.Key).GetValueAsString().ToLower() == "true") 
                //                ? true : false;
                //        } 
                //        //else if (fieldkey.Key.ToLower().Contains("wpn"))
                //        //{
                //        //    setValue = GetIndexedProperty()
                //        //} 
                //        else
                //        {
                //            setValue = form.GetField(fieldkey.Key).GetValueAsString();
                //        }
                //        AccessClassProperties.SetNestedPropertyValue(character, TempDict[fieldkey.Key], setValue);
                //    }
                //    catch (Exception)
                //    {
                //        pdfDoc.Close();
                //    }


                //    //var x = typeof(CharacterModel).GetProperty(fieldkey.Value.ToString());
                //    //var y = form.GetField(fieldkey.Key).GetValue();
                //    //x.SetValue(character, y);
                //    //typeof(CharacterModel).GetProperty(fieldkey.Value.ToString()).SetValue(character, form.GetField(fieldkey.Key).GetValue());
                //}


            }

            return character;
        }

        private AllSpells GetAllSpells(PdfAcroForm form)
        {
            var allSpells = new AllSpells(new SpellLevelViewModel((int)NumberOfSpells.Cantrip, (int)SpellLevel.Cantrip, "0", "0"),
            new SpellLevelViewModel((int)NumberOfSpells.FirstLevelSpell, (int)SpellLevel.FirstLevelSpell, GetStringFieldOrDefault(form, "SlotsTotal 19"), GetStringFieldOrDefault(form, "SlotsRemaining 19")),
            new SpellLevelViewModel((int)NumberOfSpells.SecondLevelSpell, (int)SpellLevel.SecondLevelSpell, GetStringFieldOrDefault(form, "SlotsTotal 20"), GetStringFieldOrDefault(form, "SlotsRemaining 20")),
            new SpellLevelViewModel((int)NumberOfSpells.ThirdLevelSpell, (int)SpellLevel.ThirdLevelSpell, GetStringFieldOrDefault(form, "SlotsTotal 21"), GetStringFieldOrDefault(form, "SlotsRemaining 21")),
            new SpellLevelViewModel((int)NumberOfSpells.FourthLevelSpell, (int)SpellLevel.FourthLevelSpell, GetStringFieldOrDefault(form, "SlotsTotal 22"), GetStringFieldOrDefault(form, "SlotsRemaining 22")),
            new SpellLevelViewModel((int)NumberOfSpells.FifthLevelSpell, (int)SpellLevel.FifthLevelSpell, GetStringFieldOrDefault(form, "SlotsTotal 23"), GetStringFieldOrDefault(form, "SlotsRemaining 23")),
            new SpellLevelViewModel((int)NumberOfSpells.SixthLevelSpell, (int)SpellLevel.SixthLevelSpell, GetStringFieldOrDefault(form, "SlotsTotal 24"), GetStringFieldOrDefault(form, "SlotsRemaining 24")),
            new SpellLevelViewModel((int)NumberOfSpells.SeventhLevelSpell, (int)SpellLevel.SeventhLevelSpell, GetStringFieldOrDefault(form, "SlotsTotal 25"), GetStringFieldOrDefault(form, "SlotsRemaining 25")),
            new SpellLevelViewModel((int)NumberOfSpells.EighthLevelSpell, (int)SpellLevel.EighthLevelSpell, GetStringFieldOrDefault(form, "SlotsTotal 26"), GetStringFieldOrDefault(form, "SlotsRemaining 26")),
            new SpellLevelViewModel((int)NumberOfSpells.NinthLevelSpell, (int)SpellLevel.NinthLevelSpell, GetStringFieldOrDefault(form, "SlotsTotal 27"), GetStringFieldOrDefault(form, "SlotsRemaining 27")));

            allSpells.CantripSpellViewModel.Spells = GetSpells(form, 
                new string[] { "1014", "1016", "1017", "1018", "1019", "1020", "1021", "1022" },
                new string[] { "", "", "", "", "", "", "", "" });
            allSpells.FirstLevelSpellViewModel.Spells = GetSpells(form, 
                new string[] { "1015", "1023", "1024", "1025", "1026", "1027", "1028", "1029", "1030", "1031", "1032", "1033" },
                new string[] { "251", "309", "3010", "3011", "3012", "3013", "3014", "3015", "3016", "3017", "3018", "3019" });
            allSpells.SecondLevelSpellViewModel.Spells = GetSpells(form, 
                new string[] { "1046", "1034", "1035", "1036", "1037", "1038", "1039", "1040", "1041", "1042", "1043", "1044", "1045" },
                new string[] { "313", "310", "3020", "3021", "3022", "3023", "3024", "3025", "3026", "3027", "3028", "3029", "3030" });
            allSpells.ThirdLevelSpellViewModel.Spells = GetSpells(form, 
                new string[] { "1048", "1047", "1049", "1050", "1051", "1052", "1053", "1054", "1055", "1056", "1057", "1058", "1059" },
                new string[] { "315", "314", "3031", "3032", "3033", "3034", "3035", "3036", "3037", "3038", "3039", "3040", "3041" });
            allSpells.FourthLevelSpellViewModel.Spells = GetSpells(form, 
                new string[] { "1061", "1060", "1062", "1063", "1064", "1065", "1066", "1067", "1068", "1069", "1070", "1071", "1072" },
                new string[] { "317", "316", "3042", "3043", "3044", "3045", "3046", "3047", "3048", "3049", "3050", "3051", "3052" });
            allSpells.FifthLevelSpellViewModel.Spells = GetSpells(form, 
                new string[] { "1074", "1073", "1075", "1076", "1077", "1078", "1079", "1080", "1081" },
                new string[] { "319", "318", "3053", "3054", "3055", "3056", "3057", "3058", "3059" });
            allSpells.SixthLevelSpellViewModel.Spells = GetSpells(form, 
                new string[] { "1083", "1082", "1084", "1085", "1086", "1087", "1088", "1089", "1090" },
                new string[] { "321", "320", "3060", "3061", "3062", "3063", "3064", "3065", "3066" });
            allSpells.SeventhLevelSpellViewModel.Spells = GetSpells(form, 
                new string[] { "1092", "1091", "1093", "1094", "1095", "1096", "1097", "1098", "1099" },
                new string[] { "323", "322", "3067", "3068", "3069", "3070", "3071", "3072", "3073" });
            allSpells.EighthLevelSpellViewModel.Spells = GetSpells(form, 
                new string[] { "10101", "10100", "10102", "10103", "10104", "10105", "10106" },
                new string[] { "325", "324", "3074", "3075", "3076", "3077", "3078" });
            allSpells.NinthLevelSpellViewModel.Spells = GetSpells(form, 
                new string[] { "10108", "10107", "10109", "101010", "101011", "101012", "101013"},
                new string[] { "327", "326", "3079", "3080", "3081", "3082", "3083" });

            return allSpells;
        }

        private ObservableCollection<Spell> GetSpells(PdfAcroForm form, string[] spellNameNumbers, string[] spellsPreparedNumbers)
        {
            var spells = new ObservableCollection<Spell>() { };
            var openNewViewWrapper = new OpenNewViewWrapper(new WindowWrapper());
            for (int i = 0; i < spellNameNumbers.Length; i++)
            {
                spells.Add(new Spell(GetStringFieldOrDefault(form, $"Spells {spellNameNumbers[i]}"), 
                    GetStringFieldOrDefault(form, $"Spells {spellNameNumbers[i]}"),
                    spellsPreparedNumbers[i] == "" ? false : GetCheckBoxFieldOrDefault(form, $"Check Box {spellsPreparedNumbers[i]}"), 
                    openNewViewWrapper));
            }

            return spells;
        }

        private WeaponNotes GetWeaponNotes(PdfAcroForm form) 
            => new WeaponNotes(new ObservableCollection<WeaponsInventory>()
            {
                new WeaponsInventory(GetStringFieldOrDefault(form, "Wpn Name"),
                GetStringFieldOrDefault(form, "Wpn1 AtkBonus"),
                GetStringFieldOrDefault(form, "Wpn1 Damage")),
                new WeaponsInventory(GetStringFieldOrDefault(form, "Wpn Name 2"),
                GetStringFieldOrDefault(form, "Wpn2 AtkBonus "),
                GetStringFieldOrDefault(form, "Wpn2 Damage ")),
                new WeaponsInventory(GetStringFieldOrDefault(form, "Wpn Name 3"),
                GetStringFieldOrDefault(form, "Wpn3 AtkBonus  "),
                GetStringFieldOrDefault(form, "Wpn3 Damage "))
            },
            GetStringFieldOrDefault(form, "AttacksSpellcasting")); //Check if this title is correct

        private CharacterNotes GetCharacterNotes(PdfAcroForm form, string filePath)
        { //Think about how to serialise and deserialise "Quick Notes", rn I've just set it as ""
            return new CharacterNotes("", //This is where character appearance is (Will try to get this working later)
                "", //Here is where Quick Notes is
                GetStringFieldOrDefault(form, "Equipment"),
                GetStringFieldOrDefault(form, "Allies"),
                GetStringFieldOrDefault(form, "Features and Traits"),
                GetStringFieldOrDefault(form, "ProficienciesLang"),
                new Money(GetStringFieldOrDefault(form, "PP"),
                GetStringFieldOrDefault(form, "GP"),
                GetStringFieldOrDefault(form, "SP"),
                GetStringFieldOrDefault(form, "CP"),
                GetStringFieldOrDefault(form, "EP")),
                GetStringFieldOrDefault(form, "Backstory"),
                GetStringFieldOrDefault(form, "Treasure"),
                GetStringFieldOrDefault(form, "Feat+Traits"),
                new SerializeCharacterWrapper(),
                new FileOperationsWrapper());
        }

        private MiscStats GetMiscStats(PdfAcroForm form) 
            => new MiscStats(GetStringFieldOrDefault(form, "ProfBonus"), 
                GetIntFieldOrDefault(form, "AC"),
                GetStringFieldOrDefault(form, "Initiative"), 
                GetStringFieldOrDefault(form, "Speed"),
                GetStringFieldOrDefault(form, "Inspiration"), 
                GetStringFieldOrDefault(form, "Passive"));

        private DetailsStats GetDetailsStats(PdfAcroForm form) 
            => new DetailsStats(GetStringFieldOrDefault(form, "CharacterName"),
                GetStringFieldOrDefault(form, "ClassLevel"),
                GetStringFieldOrDefault(form, "Background"),
                GetStringFieldOrDefault(form, "PlayerName"),
                GetStringFieldOrDefault(form, "Race "),
                GetStringFieldOrDefault(form, "Alignment"),
                GetStringFieldOrDefault(form, "XP"),
                GetStringFieldOrDefault(form, "Age"),
                GetStringFieldOrDefault(form, "Height"),
                GetStringFieldOrDefault(form, "Weight"),
                GetStringFieldOrDefault(form, "Eyes"),
                GetStringFieldOrDefault(form, "Skin"),
                GetStringFieldOrDefault(form, "Hair"),
                GetStringFieldOrDefault(form, "PersonalityTraits "),
                GetStringFieldOrDefault(form, "Ideals"),
                GetStringFieldOrDefault(form, "Bonds"),
                GetStringFieldOrDefault(form, "Flaws"));

        private HPStats GetHPStats(PdfAcroForm form) 
            => new HPStats(GetIntFieldOrDefault(form, "HPMax"), 
                GetIntFieldOrDefault(form, "HPCurrent"), 
                GetIntFieldOrDefault(form, "HPTemp"),
                GetStringFieldOrDefault(form, "HD"), 
                GetStringFieldOrDefault(form, "HDTotal"), 
                GetCheckBoxFieldOrDefault(form, "Check Box 12"),
                GetCheckBoxFieldOrDefault(form, "Check Box 13"), 
                GetCheckBoxFieldOrDefault(form, "Check Box 14"), 
                GetCheckBoxFieldOrDefault(form, "Check Box 15"),
                GetCheckBoxFieldOrDefault(form, "Check Box 16"), 
                GetCheckBoxFieldOrDefault(form, "Check Box 17"));

        private AllSkills GetAllSkills(PdfAcroForm form) 
            => new AllSkills(GetSkill(form, SkillStrings.Acrobatics, "Acrobatics", "Check Box 23"),
                GetSkill(form, SkillStrings.AnimalHandling, "Animal", "Check Box 24"),
                GetSkill(form, SkillStrings.Arcana, "Arcana", "Check Box 25"),
                GetSkill(form, SkillStrings.Athletics, "Athletics", "Check Box 26"),
                GetSkill(form, SkillStrings.Deception, "Deception ", "Check Box 27"),
                GetSkill(form, SkillStrings.History, "History ", "Check Box 28"),
                GetSkill(form, SkillStrings.Insight, "Insight", "Check Box 29"),
                GetSkill(form, SkillStrings.Intimidation, "Intimidation", "Check Box 30"),
                GetSkill(form, SkillStrings.Investigation, "Investigation ", "Check Box 31"),
                GetSkill(form, SkillStrings.Medicine, "Medicine", "Check Box 32"),
                GetSkill(form, SkillStrings.Nature, "Nature", "Check Box 33"),
                GetSkill(form, SkillStrings.Perception, "Perception ", "Check Box 34"),
                GetSkill(form, SkillStrings.Performance, "Performance", "Check Box 35"),
                GetSkill(form, SkillStrings.Persuasion, "Persuasion", "Check Box 36"),
                GetSkill(form, SkillStrings.Religion, "Religion", "Check Box 37"),
                GetSkill(form, SkillStrings.SleightOfHand, "SleightofHand", "Check Box 38"),
                GetSkill(form, SkillStrings.Stealth, "Stealth ", "Check Box 39"),
                GetSkill(form, SkillStrings.Survival, "Survival", "Check Box 40"));

        private Skill GetSkill(PdfAcroForm form, string name, string skillScore, string isProficient) 
            => new Skill(name, GetStringFieldOrDefault(form, skillScore), GetCheckBoxFieldOrDefault(form, isProficient));

        private MainStats GetMainStats(PdfAcroForm form) 
            => new MainStats(GetIntFieldOrDefault(form, "STR"), 
                GetIntFieldOrDefault(form, "DEX"), 
                GetIntFieldOrDefault(form, "CON"), 
                GetIntFieldOrDefault(form, "INT"),
                GetIntFieldOrDefault(form, "WIS"), 
                GetIntFieldOrDefault(form, "CHA"), 
                GetStringFieldOrDefault(form, "ST Strength"),
                GetStringFieldOrDefault(form, "ST Dexterity"),
                GetStringFieldOrDefault(form, "ST Constitution"), 
                GetStringFieldOrDefault(form, "ST Intelligence"), 
                GetStringFieldOrDefault(form, "ST Wisdom"), 
                GetStringFieldOrDefault(form, "ST Charisma"),
                GetCheckBoxFieldOrDefault(form, "Check Box 11"), 
                GetCheckBoxFieldOrDefault(form, "Check Box 18"), 
                GetCheckBoxFieldOrDefault(form, "Check Box 19"),
                GetCheckBoxFieldOrDefault(form, "Check Box 20"), 
                GetCheckBoxFieldOrDefault(form, "Check Box 21"), 
                GetCheckBoxFieldOrDefault(form, "Check Box 22"),
                GetStringFieldOrDefault(form, "STRmod"), 
                GetStringFieldOrDefault(form, "DEXmod "), 
                GetStringFieldOrDefault(form, "CONmod"),
                GetStringFieldOrDefault(form, "INTmod"), 
                GetStringFieldOrDefault(form, "WISmod"), 
                GetStringFieldOrDefault(form, "CHamod"));

        private bool GetCheckBoxFieldOrDefault(PdfAcroForm form, string fieldName) 
            => form.GetField(fieldName).GetValueAsString() == "" ? false : true;

        private string GetStringFieldOrDefault(PdfAcroForm form, string fieldName) 
            => form.GetField(fieldName).GetValueAsString() == null ? "" : form.GetField(fieldName).GetValueAsString();

        private int GetIntFieldOrDefault(PdfAcroForm form, string fieldName)
        { //Will need to test this out by seeing what happens when an int is "18(+1)" or "(+1)14"
            var x = (string)form.GetField(fieldName).GetValueAsString().Replace(" ", "");
            var y = new string(x.Where(x => char.IsDigit(x)).ToArray());
            var z = int.TryParse(x, out int r);
            return int.TryParse(new string(form.GetField(fieldName).GetValueAsString().Replace(" ", "").Where(x => char.IsDigit(x)).ToArray()), out int result) ? result : 0;
        }

        private string TernaryOperatorOnValue(bool value, string ifTrue, string ifFalse) 
            => value ? ifTrue : ifFalse;

        private void InitialiseDictionary(ICharacterModel character)
        {
            //TempDict = new Dictionary<string, string>()
            //{
            //    {"ClassLevel", "DetailsStats.ClassAndLevel"},
            //    {"STR", "MainStats.Str"}
            //};

            TempDict = new Dictionary<string, string>()
            {
                {"ClassLevel", "DetailsStats.ClassAndLevel"},
                {"Background", "DetailsStats.Background"},
                {"PlayerName", "DetailsStats.PlayerName"},
                {"CharacterName", "DetailsStats.CharacterName"},
                {"Race ", "DetailsStats.Race"},
                {"Alignment", "DetailsStats.Alignment"},
                {"XP", "DetailsStats.ExperiencePoints"},
                {"STR", "MainStats.Str"},
                {"Inspiration", "MiscStats.Inspiration"},
                {"ProfBonus", "MiscStats.Proficiency"},
                {"AC", "MiscStats.AC"},
                {"Initiative", "MiscStats.Initiative"},
                {"Speed", "MiscStats.Speed"},
                {"PersonalityTraits ", "DetailsStats.PersonalityTraits"},
                {"STRmod", "MainStats.StrMod"},
                {"ST Strength", "MainStats.StrSvThr"},
                {"DEX", "MainStats.Dex"},
                {"Ideals", "DetailsStats.Ideals"},
                {"DEXmod ", "MainStats.DexMod"},
                {"Bonds", "DetailsStats.Bonds"},
                {"CON", "MainStats.Con"},
                {"HDTotal", "HPStats.HitDie"},
                {"Check Box 12", "HPStats.SuccessSave1"}, //character.HPStats.SuccessSave1 ? "1" : ""},
                {"Check Box 13", "HPStats.SuccessSave2"},
                {"Check Box 14", "HPStats.SuccessSave3"},
                {"CONmod", "MainStats.ConMod"},
                {"Check Box 15", "HPStats.FailureSave1"},
                {"Check Box 16", "HPStats.FailureSave2"},
                {"Check Box 17", "HPStats.FailureSave3"},
                {"HD", "HPStats.HitDie"},
                {"Flaws", "DetailsStats.Flaws"},
                {"INT", "MainStats.Intl"},
                {"ST Dexterity", "MainStats.DexSvThr"},
                {"ST Constitution", "MainStats.ConSvThr"},
                {"ST Intelligence", "MainStats.IntlSvThr"},
                {"ST Wisdom", "MainStats.WisSvThr"},
                {"ST Charisma", "MainStats.ChaSvThr"},
                {"Acrobatics", "AllSkills.Acrobatics.SkillScore"},
                {"Animal", "AllSkills.AnimalHandling.SkillScore"},
                {"Athletics", "AllSkills.Athletics.SkillScore"},
                {"Deception ", "AllSkills.Deception.SkillScore"},
                {"History ", "AllSkills.History.SkillScore"},
                {"Wpn Name", character.WeaponNotes.WeaponsInventory.Count >= 1 ? character.WeaponNotes.WeaponsInventory[0].WeaponName : ""},
                {"Wpn1 AtkBonus", character.WeaponNotes.WeaponsInventory.Count >= 1 ? character.WeaponNotes.WeaponsInventory[0].AttackBonus : ""},
                {"Wpn1 Damage", character.WeaponNotes.WeaponsInventory.Count >= 1 ? character.WeaponNotes.WeaponsInventory[0].Damage : ""},
                {"Insight", "AllSkills.Insight.SkillScore"},
                {"Intimidation", "AllSkills.Intimidation.SkillScore"},
                {"Wpn Name 2", character.WeaponNotes.WeaponsInventory.Count >= 2 ? character.WeaponNotes.WeaponsInventory[1].WeaponName : ""},
                {"Wpn2 AtkBonus ", character.WeaponNotes.WeaponsInventory.Count >= 2 ? character.WeaponNotes.WeaponsInventory[1].AttackBonus : ""},
                {"Wpn Name 3", character.WeaponNotes.WeaponsInventory.Count >= 3 ? character.WeaponNotes.WeaponsInventory[2].WeaponName : ""},
                {"Wpn3 AtkBonus  ", character.WeaponNotes.WeaponsInventory.Count >= 3 ? character.WeaponNotes.WeaponsInventory[2].AttackBonus : ""},
                {"Check Box 11", "MainStats.StrSvThrProf"},
                {"Check Box 18", "MainStats.DexSvThrProf"},
                {"Check Box 19", "MainStats.ConSvThrProf"},
                {"Check Box 20", "MainStats.IntlSvThrProf"},
                {"Check Box 21", "MainStats.WisSvThrProf"},
                {"Check Box 22", "MainStats.ChaSvThrProf"},
                {"INTmod", "MainStats.IntlMod"},
                {"Wpn2 Damage ", character.WeaponNotes.WeaponsInventory.Count >= 2 ? character.WeaponNotes.WeaponsInventory[1].Damage : ""},
                {"Investigation ", "AllSkills.Investigation.SkillScore"},
                {"WIS", "MainStats.Wis"},
                {"Arcana", "AllSkills.Arcana.SkillScore"},
                {"Perception ", "AllSkills.Perception.SkillScore"},
                {"WISmod", "MainStats.WisMod"},
                {"CHA", "MainStats.Cha"},
                {"Nature", "AllSkills.Nature.SkillScore"},
                {"Performance", "AllSkills.Performance.SkillScore"},
                {"Medicine", "AllSkills.Medicine.SkillScore"},
                {"Religion", "AllSkills.Religion.SkillScore"},
                {"Stealth ", "AllSkills.Stealth.SkillScore"},
                {"Check Box 23", "AllSkills.Acrobatics.IsProficient"},
                {"Check Box 24", "AllSkills.AnimalHandling.IsProficient"},
                {"Check Box 25", "AllSkills.Arcana.IsProficient"},
                {"Check Box 26", "AllSkills.Athletics.IsProficient"},
                {"Check Box 27", "AllSkills.Deception.IsProficient"},
                {"Check Box 28", "AllSkills.History.IsProficient"},
                {"Check Box 29", "AllSkills.Insight.IsProficient"},
                {"Check Box 30", "AllSkills.Intimidation.IsProficient"},
                {"Check Box 31", "AllSkills.Investigation.IsProficient"},
                {"Check Box 32", "AllSkills.Medicine.IsProficient"},
                {"Check Box 33", "AllSkills.Nature.IsProficient"},
                {"Check Box 34", "AllSkills.Perception.IsProficient"},
                {"Check Box 35", "AllSkills.Performance.IsProficient"},
                {"Check Box 36", "AllSkills.Persuasion.IsProficient"},
                {"Check Box 37", "AllSkills.Religion.IsProficient"},
                {"Check Box 38", "AllSkills.SleightOfHand.IsProficient"},
                {"Check Box 39", "AllSkills.Stealth.IsProficient"},
                {"Check Box 40", "AllSkills.Survival.IsProficient"},
                {"Persuasion", "AllSkills.Persuasion.SkillScore"},
                {"HPMax", "HPStats.MaxHP"},
                {"HPCurrent", "HPStats.CurrentHP"},
                {"HPTemp", "HPStats.TempHP"},
                {"Wpn3 Damage ", character.WeaponNotes.WeaponsInventory.Count >= 3 ? character.WeaponNotes.WeaponsInventory[2].Damage : ""},
                {"SleightofHand", "AllSkills.SleightOfHand.SkillScore"},
                {"CHamod", "MainStats.ChaMod"},
                {"Survival", "AllSkills.Survival.SkillScore"},
                {"AttacksSpellcasting", "WeaponNotes.WeaponDetails"},
                {"Passive", "MiscStats.PassivePerception"},
                {"CP", "CharacterNotes.Money.Bronze"},
                {"ProficienciesLang", "CharacterNotes.Proficiencies"},
                {"SP", "CharacterNotes.Money.Silver"},
                {"EP", "CharacterNotes.Money.Electrum"},
                {"GP", "CharacterNotes.Money.Gold"},
                {"PP", "CharacterNotes.Money.Platinum"},
                {"Equipment", "CharacterNotes.Equipment"},
                {"Features and Traits", "CharacterNotes.AbilityDesc"},
                {"CharacterName 2", "DetailsStats.CharacterName"},
                {"Age", "DetailsStats.Age"},
                {"Height", "DetailsStats.Height"},
                {"Weight", "DetailsStats.Weight"},
                {"Eyes", "DetailsStats.Eyes"},
                {"Skin", "DetailsStats.Skin"},
                {"Hair", "DetailsStats.Hair"},
                {"CHARACTER IMAGE", ""},
                {"Faction Symbol Image", ""},
                {"Allies", "CharacterNotes.SessionNotes"},
                {"FactionName", ""},
                {"Backstory", "CharacterNotes.Backstory"},
                {"Feat+Traits", "CharacterNotes.FeaturesAndTraits"},
                {"Treasure", "CharacterNotes.Treasure"},
                {"Spellcasting Class 2", string.Join("", character.DetailsStats.ClassAndLevel.Where(x => !char.IsDigit(x) && x != ' ').ToString())},
                {"SpellcastingAbility 2", "AllSpells.SpellcastingAbility"},
                {"SpellSaveDC  2", "AllSpells.SpellSaveDC"},
                {"SpellAtkBonus 2", "AllSpells.SpellAttackBonus"},
                {"SlotsTotal 19", "AllSpells.FirstLevelSpellViewModel.SlotsTotal"},
                {"SlotsRemaining 19", "AllSpells.FirstLevelSpellViewModel.SlotsExpended"},
                {"Spells 1014", "AllSpells.CantripSpellViewModel.Spells[0].Name"},
                {"Spells 1015", "AllSpells.FirstLevelSpellViewModel.Spells[0].Name"},
                {"Spells 1016", "AllSpells.CantripSpellViewModel.Spells[1].Name"},
                {"Spells 1017", "AllSpells.CantripSpellViewModel.Spells[2].Name"},
                {"Spells 1018", "AllSpells.CantripSpellViewModel.Spells[3].Name"},
                {"Spells 1019", "AllSpells.CantripSpellViewModel.Spells[4].Name"},
                {"Spells 1020", "AllSpells.CantripSpellViewModel.Spells[5].Name"},
                {"Spells 1021", "AllSpells.CantripSpellViewModel.Spells[6].Name"},
                {"Spells 1022", "AllSpells.CantripSpellViewModel.Spells[7].Name"},
                {"Check Box 314", "AllSpells.ThirdLevelSpellViewModel.Spells[1].IsPrepared"},
                {"Check Box 3031", "AllSpells.ThirdLevelSpellViewModel.Spells[2].IsPrepared"},
                {"Check Box 3032", "AllSpells.ThirdLevelSpellViewModel.Spells[3].IsPrepared"},
                {"Check Box 3033", "AllSpells.ThirdLevelSpellViewModel.Spells[4].IsPrepared"},
                {"Check Box 3034", "AllSpells.ThirdLevelSpellViewModel.Spells[5].IsPrepared"},
                {"Check Box 3035", "AllSpells.ThirdLevelSpellViewModel.Spells[6].IsPrepared"},
                {"Check Box 3036", "AllSpells.ThirdLevelSpellViewModel.Spells[7].IsPrepared"},
                {"Check Box 3037", "AllSpells.ThirdLevelSpellViewModel.Spells[8].IsPrepared"},
                {"Check Box 3038", "AllSpells.ThirdLevelSpellViewModel.Spells[9].IsPrepared"},
                {"Check Box 3039", "AllSpells.ThirdLevelSpellViewModel.Spells[10].IsPrepared"},
                {"Check Box 3040", "AllSpells.ThirdLevelSpellViewModel.Spells[11].IsPrepared"},
                {"Check Box 321", "AllSpells.SixthLevelSpellViewModel.Spells[0].IsPrepared"},
                {"Check Box 320", "AllSpells.SixthLevelSpellViewModel.Spells[1].IsPrepared"},
                {"Check Box 3060", "AllSpells.SixthLevelSpellViewModel.Spells[2].IsPrepared"},
                {"Check Box 3061", "AllSpells.SixthLevelSpellViewModel.Spells[3].IsPrepared"},
                {"Check Box 3062", "AllSpells.SixthLevelSpellViewModel.Spells[4].IsPrepared"},
                {"Check Box 3063", "AllSpells.SixthLevelSpellViewModel.Spells[5].IsPrepared"},
                {"Check Box 3064", "AllSpells.SixthLevelSpellViewModel.Spells[6].IsPrepared"},
                {"Check Box 3065", "AllSpells.SixthLevelSpellViewModel.Spells[7].IsPrepared"},
                {"Check Box 3066", "AllSpells.SixthLevelSpellViewModel.Spells[8].IsPrepared"},
                {"Check Box 315", "AllSpells.ThirdLevelSpellViewModel.Spells[0].IsPrepared"}, //
                {"Check Box 3041", "AllSpells.ThirdLevelSpellViewModel.Spells[12].IsPrepared"},
                {"Spells 1023", "AllSpells.FirstLevelSpellViewModel.Spells[1].Name"},
                {"Check Box 251", "AllSpells.FirstLevelSpellViewModel.Spells[0].IsPrepared"},
                {"Check Box 309", "AllSpells.FirstLevelSpellViewModel.Spells[1].IsPrepared"},
                {"Check Box 3010", "AllSpells.FirstLevelSpellViewModel.Spells[2].IsPrepared"},
                {"Check Box 3011", "AllSpells.FirstLevelSpellViewModel.Spells[3].IsPrepared"},
                {"Check Box 3012", "AllSpells.FirstLevelSpellViewModel.Spells[4].IsPrepared"},
                {"Check Box 3013", "AllSpells.FirstLevelSpellViewModel.Spells[5].IsPrepared"},
                {"Check Box 3014", "AllSpells.FirstLevelSpellViewModel.Spells[6].IsPrepared"},
                {"Check Box 3015", "AllSpells.FirstLevelSpellViewModel.Spells[7].IsPrepared"},
                {"Check Box 3016", "AllSpells.FirstLevelSpellViewModel.Spells[8].IsPrepared"},
                {"Check Box 3017", "AllSpells.FirstLevelSpellViewModel.Spells[9].IsPrepared"},
                {"Check Box 3018", "AllSpells.FirstLevelSpellViewModel.Spells[10].IsPrepared"},
                {"Check Box 3019", "AllSpells.FirstLevelSpellViewModel.Spells[11].IsPrepared"},
                {"Spells 1024", "AllSpells.FirstLevelSpellViewModel.Spells[2].Name"},
                {"Spells 1025", "AllSpells.FirstLevelSpellViewModel.Spells[3].Name"},
                {"Spells 1026", "AllSpells.FirstLevelSpellViewModel.Spells[4].Name"},
                {"Spells 1027", "AllSpells.FirstLevelSpellViewModel.Spells[5].Name"},
                {"Spells 1028", "AllSpells.FirstLevelSpellViewModel.Spells[6].Name"},
                {"Spells 1029", "AllSpells.FirstLevelSpellViewModel.Spells[7].Name"},
                {"Spells 1030", "AllSpells.FirstLevelSpellViewModel.Spells[8].Name"},
                {"Spells 1031", "AllSpells.FirstLevelSpellViewModel.Spells[9].Name"},
                {"Spells 1032", "AllSpells.FirstLevelSpellViewModel.Spells[10].Name"},
                {"Spells 1033", "AllSpells.FirstLevelSpellViewModel.Spells[11].Name"},
                {"SlotsTotal 20", "AllSpells.SecondLevelSpellViewModel.SlotsTotal"},
                {"SlotsRemaining 20", "AllSpells.SecondLevelSpellViewModel.SlotsExpended"},
                {"Spells 1034", "AllSpells.SecondLevelSpellViewModel.Spells[1].Name"},
                {"Spells 1035", "AllSpells.SecondLevelSpellViewModel.Spells[2].Name"},
                {"Spells 1036", "AllSpells.SecondLevelSpellViewModel.Spells[3].Name"},
                {"Spells 1037", "AllSpells.SecondLevelSpellViewModel.Spells[4].Name"},
                {"Spells 1038", "AllSpells.SecondLevelSpellViewModel.Spells[5].Name"},
                {"Spells 1039", "AllSpells.SecondLevelSpellViewModel.Spells[6].Name"},
                {"Spells 1040", "AllSpells.SecondLevelSpellViewModel.Spells[7].Name"},
                {"Spells 1041", "AllSpells.SecondLevelSpellViewModel.Spells[8].Name"},
                {"Spells 1042", "AllSpells.SecondLevelSpellViewModel.Spells[9].Name"},
                {"Spells 1043", "AllSpells.SecondLevelSpellViewModel.Spells[10].Name"},
                {"Spells 1044", "AllSpells.SecondLevelSpellViewModel.Spells[11].Name"},
                {"Spells 1045", "AllSpells.SecondLevelSpellViewModel.Spells[12].Name"},
                {"Spells 1046", "AllSpells.SecondLevelSpellViewModel.Spells[0].Name"},
                {"SlotsTotal 21", "AllSpells.ThirdLevelSpellViewModel.SlotsTotal"},
                {"SlotsRemaining 21", "AllSpells.ThirdLevelSpellViewModel.SlotsExpended"},
                {"Spells 1047", "AllSpells.ThirdLevelSpellViewModel.Spells[1].Name"},
                {"Spells 1048", "AllSpells.ThirdLevelSpellViewModel.Spells[0].Name"},
                {"Spells 1049", "AllSpells.ThirdLevelSpellViewModel.Spells[2].Name"},
                {"Spells 1050", "AllSpells.ThirdLevelSpellViewModel.Spells[3].Name"},
                {"Spells 1051", "AllSpells.ThirdLevelSpellViewModel.Spells[4].Name"},
                {"Spells 1052", "AllSpells.ThirdLevelSpellViewModel.Spells[5].Name"},
                {"Spells 1053", "AllSpells.ThirdLevelSpellViewModel.Spells[6].Name"},
                {"Spells 1054", "AllSpells.ThirdLevelSpellViewModel.Spells[7].Name"},
                {"Spells 1055", "AllSpells.ThirdLevelSpellViewModel.Spells[8].Name"},
                {"Spells 1056", "AllSpells.ThirdLevelSpellViewModel.Spells[9].Name"},
                {"Spells 1057", "AllSpells.ThirdLevelSpellViewModel.Spells[10].Name"},
                {"Spells 1058", "AllSpells.ThirdLevelSpellViewModel.Spells[11].Name"},
                {"Spells 1059", "AllSpells.ThirdLevelSpellViewModel.Spells[12].Name"},
                {"SlotsTotal 22", "AllSpells.FourthLevelSpellViewModel.SlotsTotal"},
                {"SlotsRemaining 22", "AllSpells.FourthLevelSpellViewModel.SlotsExpended"},
                {"Spells 1060", "AllSpells.FourthLevelSpellViewModel.Spells[1].Name"},
                {"Spells 1061", "AllSpells.FourthLevelSpellViewModel.Spells[0].Name"},
                {"Spells 1062", "AllSpells.FourthLevelSpellViewModel.Spells[2].Name"},
                {"Spells 1063", "AllSpells.FourthLevelSpellViewModel.Spells[3].Name"},
                {"Spells 1064", "AllSpells.FourthLevelSpellViewModel.Spells[4].Name"},
                {"Check Box 323", "AllSpells.SeventhLevelSpellViewModel.Spells[0].IsPrepared"},
                {"Check Box 322", "AllSpells.SeventhLevelSpellViewModel.Spells[1].IsPrepared"},
                {"Check Box 3067", "AllSpells.SeventhLevelSpellViewModel.Spells[2].IsPrepared"},
                {"Check Box 3068", "AllSpells.SeventhLevelSpellViewModel.Spells[3].IsPrepared"},
                {"Check Box 3069", "AllSpells.SeventhLevelSpellViewModel.Spells[4].IsPrepared"},
                {"Check Box 3070", "AllSpells.SeventhLevelSpellViewModel.Spells[5].IsPrepared"},
                {"Check Box 3071", "AllSpells.SeventhLevelSpellViewModel.Spells[6].IsPrepared"},
                {"Check Box 3072", "AllSpells.SeventhLevelSpellViewModel.Spells[7].IsPrepared"},
                {"Check Box 3073", "AllSpells.SeventhLevelSpellViewModel.Spells[8].IsPrepared"},
                {"Spells 1065", "AllSpells.FourthLevelSpellViewModel.Spells[5].Name"},
                {"Spells 1066", "AllSpells.FourthLevelSpellViewModel.Spells[6].Name"},
                {"Spells 1067", "AllSpells.FourthLevelSpellViewModel.Spells[7].Name"},
                {"Spells 1068", "AllSpells.FourthLevelSpellViewModel.Spells[8].Name"},
                {"Spells 1069", "AllSpells.FourthLevelSpellViewModel.Spells[9].Name"},
                {"Spells 1070", "AllSpells.FourthLevelSpellViewModel.Spells[10].Name"},
                {"Spells 1071", "AllSpells.FourthLevelSpellViewModel.Spells[11].Name"},
                {"Check Box 317", "AllSpells.FourthLevelSpellViewModel.Spells[0].IsPrepared"},
                {"Spells 1072", "AllSpells.FourthLevelSpellViewModel.Spells[12].Name"},
                {"SlotsTotal 23", "AllSpells.FifthLevelSpellViewModel.SlotsTotal"},
                {"SlotsRemaining 23", "AllSpells.FifthLevelSpellViewModel.SlotsExpended"},
                {"Spells 1073", "AllSpells.FifthLevelSpellViewModel.Spells[1].Name"},
                {"Spells 1074", "AllSpells.FifthLevelSpellViewModel.Spells[0].Name"},
                {"Spells 1075", "AllSpells.FifthLevelSpellViewModel.Spells[2].Name"},
                {"Spells 1076", "AllSpells.FifthLevelSpellViewModel.Spells[3].Name"},
                {"Spells 1077", "AllSpells.FifthLevelSpellViewModel.Spells[4].Name"},
                {"Spells 1078", "AllSpells.FifthLevelSpellViewModel.Spells[5].Name"},
                {"Spells 1079", "AllSpells.FifthLevelSpellViewModel.Spells[6].Name"},
                {"Spells 1080", "AllSpells.FifthLevelSpellViewModel.Spells[7].Name"},
                {"Spells 1081", "AllSpells.FifthLevelSpellViewModel.Spells[8].Name"},
                {"SlotsTotal 24", "AllSpells.SixthLevelSpellViewModel.SlotsTotal"},
                {"SlotsRemaining 24", "AllSpells.SixthLevelSpellViewModel.SlotsExpended"},
                {"Spells 1082", "AllSpells.SixthLevelSpellViewModel.Spells[1].Name"},
                {"Spells 1083", "AllSpells.SixthLevelSpellViewModel.Spells[0].Name"},
                {"Spells 1084", "AllSpells.SixthLevelSpellViewModel.Spells[2].Name"},
                {"Spells 1085", "AllSpells.SixthLevelSpellViewModel.Spells[3].Name"},
                {"Spells 1086", "AllSpells.SixthLevelSpellViewModel.Spells[4].Name"},
                {"Spells 1087", "AllSpells.SixthLevelSpellViewModel.Spells[5].Name"},
                {"Spells 1088", "AllSpells.SixthLevelSpellViewModel.Spells[6].Name"},
                {"Spells 1089", "AllSpells.SixthLevelSpellViewModel.Spells[7].Name"},
                {"Spells 1090", "AllSpells.SixthLevelSpellViewModel.Spells[8].Name"},
                {"SlotsTotal 25", "AllSpells.SeventhLevelSpellViewModel.SlotsTotal"},
                {"SlotsRemaining 25", "AllSpells.SeventhLevelSpellViewModel.SlotsExpended"},
                {"Spells 1091", "AllSpells.SeventhLevelSpellViewModel.Spells[1].Name"},
                {"Spells 1092", "AllSpells.SeventhLevelSpellViewModel.Spells[0].Name"},
                {"Spells 1093", "AllSpells.SeventhLevelSpellViewModel.Spells[2].Name"},
                {"Spells 1094", "AllSpells.SeventhLevelSpellViewModel.Spells[3].Name"},
                {"Spells 1095", "AllSpells.SeventhLevelSpellViewModel.Spells[4].Name"},
                {"Spells 1096", "AllSpells.SeventhLevelSpellViewModel.Spells[5].Name"},
                {"Spells 1097", "AllSpells.SeventhLevelSpellViewModel.Spells[6].Name"},
                {"Spells 1098", "AllSpells.SeventhLevelSpellViewModel.Spells[7].Name"},
                {"Spells 1099", "AllSpells.SeventhLevelSpellViewModel.Spells[8].Name"},
                {"SlotsTotal 26", "AllSpells.EighthLevelSpellViewModel.SlotsTotal"},
                {"SlotsRemaining 26", "AllSpells.SeventhLevelSpellViewModel.SlotsExpended"},
                {"Spells 10100", "AllSpells.EighthLevelSpellViewModel.Spells[1].Name"},
                {"Spells 10101", "AllSpells.EighthLevelSpellViewModel.Spells[0].Name"},
                {"Spells 10102", "AllSpells.EighthLevelSpellViewModel.Spells[2].Name"},
                {"Spells 10103", "AllSpells.EighthLevelSpellViewModel.Spells[3].Name"},
                {"Check Box 316", "AllSpells.FourthLevelSpellViewModel.Spells[1].IsPrepared"},
                {"Check Box 3042", "AllSpells.FourthLevelSpellViewModel.Spells[2].IsPrepared"},
                {"Check Box 3043", "AllSpells.FourthLevelSpellViewModel.Spells[3].IsPrepared"},
                {"Check Box 3044", "AllSpells.FourthLevelSpellViewModel.Spells[4].IsPrepared"},
                {"Check Box 3045", "AllSpells.FourthLevelSpellViewModel.Spells[5].IsPrepared"},
                {"Check Box 3046", "AllSpells.FourthLevelSpellViewModel.Spells[6].IsPrepared"},
                {"Check Box 3047", "AllSpells.FourthLevelSpellViewModel.Spells[7].IsPrepared"},
                {"Check Box 3048", "AllSpells.FourthLevelSpellViewModel.Spells[8].IsPrepared"},
                {"Check Box 3049", "AllSpells.FourthLevelSpellViewModel.Spells[9].IsPrepared"},
                {"Check Box 3050", "AllSpells.FourthLevelSpellViewModel.Spells[10].IsPrepared"},
                {"Check Box 3051", "AllSpells.FourthLevelSpellViewModel.Spells[11].IsPrepared"},
                {"Check Box 3052", "AllSpells.FourthLevelSpellViewModel.Spells[12].IsPrepared"},
                {"Spells 10104", "AllSpells.EighthLevelSpellViewModel.Spells[4].Name"},
                {"Check Box 325", "AllSpells.EighthLevelSpellViewModel.Spells[0].IsPrepared"},
                {"Check Box 324", "AllSpells.EighthLevelSpellViewModel.Spells[1].IsPrepared"},
                {"Check Box 3074", "AllSpells.EighthLevelSpellViewModel.Spells[2].IsPrepared"},
                {"Check Box 3075", "AllSpells.EighthLevelSpellViewModel.Spells[3].IsPrepared"},
                {"Check Box 3076", "AllSpells.EighthLevelSpellViewModel.Spells[4].IsPrepared"},
                {"Check Box 3077", "AllSpells.EighthLevelSpellViewModel.Spells[5].IsPrepared"},
                {"Spells 10105", "AllSpells.EighthLevelSpellViewModel.Spells[5].Name"},
                {"Spells 10106", "AllSpells.EighthLevelSpellViewModel.Spells[6].Name"},
                {"Check Box 3078", "AllSpells.EighthLevelSpellViewModel.Spells[6].IsPrepared"},
                {"SlotsTotal 27", "AllSpells.NinthLevelSpellViewModel.SlotsTotal"},
                {"SlotsRemaining 27", "AllSpells.NinthLevelSpellViewModel.SlotsExpended"},
                {"Check Box 313", "AllSpells.SecondLevelSpellViewModel.Spells[0].IsPrepared"},
                {"Check Box 310", "AllSpells.SecondLevelSpellViewModel.Spells[1].IsPrepared"},
                {"Check Box 3020", "AllSpells.SecondLevelSpellViewModel.Spells[2].IsPrepared"},
                {"Check Box 3021", "AllSpells.SecondLevelSpellViewModel.Spells[3].IsPrepared"},
                {"Check Box 3022", "AllSpells.SecondLevelSpellViewModel.Spells[4].IsPrepared"},
                {"Check Box 3023", "AllSpells.SecondLevelSpellViewModel.Spells[5].IsPrepared"},
                {"Check Box 3024", "AllSpells.SecondLevelSpellViewModel.Spells[6].IsPrepared"},
                {"Check Box 3025", "AllSpells.SecondLevelSpellViewModel.Spells[7].IsPrepared"},
                {"Check Box 3026", "AllSpells.SecondLevelSpellViewModel.Spells[8].IsPrepared"},
                {"Check Box 3027", "AllSpells.SecondLevelSpellViewModel.Spells[9].IsPrepared"},
                {"Check Box 3028", "AllSpells.SecondLevelSpellViewModel.Spells[10].IsPrepared"},
                {"Check Box 3029", "AllSpells.SecondLevelSpellViewModel.Spells[11].IsPrepared"},
                {"Check Box 3030", "AllSpells.SecondLevelSpellViewModel.Spells[12].IsPrepared"},
                {"Spells 10107", "AllSpells.NinthLevelSpellViewModel.Spells[1].Name"},
                {"Spells 10108", "AllSpells.NinthLevelSpellViewModel.Spells[0].Name"},
                {"Spells 10109", "AllSpells.NinthLevelSpellViewModel.Spells[2].Name"},
                {"Spells 101010", "AllSpells.NinthLevelSpellViewModel.Spells[3].Name"},
                {"Spells 101011", "AllSpells.NinthLevelSpellViewModel.Spells[4].Name"},
                {"Spells 101012", "AllSpells.NinthLevelSpellViewModel.Spells[5].Name"},
                {"Check Box 319", "AllSpells.FifthLevelSpellViewModel.Spells[0].IsPrepared"},
                {"Check Box 318", "AllSpells.FifthLevelSpellViewModel.Spells[1].IsPrepared"},
                {"Check Box 3053", "AllSpells.FifthLevelSpellViewModel.Spells[2].IsPrepared"},
                {"Check Box 3054", "AllSpells.FifthLevelSpellViewModel.Spells[3].IsPrepared"},
                {"Check Box 3055", "AllSpells.FifthLevelSpellViewModel.Spells[4].IsPrepared"},
                {"Check Box 3056", "AllSpells.FifthLevelSpellViewModel.Spells[5].IsPrepared"},
                {"Check Box 3057", "AllSpells.FifthLevelSpellViewModel.Spells[6].IsPrepared"},
                {"Check Box 3058", "AllSpells.FifthLevelSpellViewModel.Spells[7].IsPrepared"},
                {"Check Box 3059", "AllSpells.FifthLevelSpellViewModel.Spells[8].IsPrepared"},
                {"Check Box 327", "AllSpells.NinthLevelSpellViewModel.Spells[0].IsPrepared"},
                {"Check Box 326", "AllSpells.NinthLevelSpellViewModel.Spells[1].IsPrepared"},
                {"Check Box 3079", "AllSpells.NinthLevelSpellViewModel.Spells[2].IsPrepared"},
                {"Check Box 3080", "AllSpells.NinthLevelSpellViewModel.Spells[3].IsPrepared"},
                {"Check Box 3081", "AllSpells.NinthLevelSpellViewModel.Spells[4].IsPrepared"},
                {"Check Box 3082", "AllSpells.NinthLevelSpellViewModel.Spells[5].IsPrepared"},
                {"Spells 101013", "AllSpells.NinthLevelSpellViewModel.Spells[6].Name"},
                {"Check Box 3083", "AllSpells.NinthLevelSpellViewModel.Spells[6].IsPrepared"},
            };

            FieldKeyToValueDict = new Dictionary<string, string>() //Think about whether I want to include the spell name & spell desc or just the spell name for serialisation
            {
                {"ClassLevel", character.DetailsStats.ClassAndLevel},
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
                {"PersonalityTraits ", character.DetailsStats.PersonalityTraits},
                {"STRmod", character.MainStats.StrMod},
                {"ST Strength", character.MainStats.StrSvThr},
                {"DEX", character.MainStats.Dex.ToString()},
                {"Ideals", character.DetailsStats.Ideals},
                {"DEXmod ", character.MainStats.DexMod},
                {"Bonds", character.DetailsStats.Bonds},
                {"CON", character.MainStats.Con.ToString()},
                {"HDTotal", character.HPStats.HitDie},
                {"Check Box 12", character.HPStats.SuccessSave1 ? "1" : ""},
                {"Check Box 13", character.HPStats.SuccessSave2 ? "1" : ""},
                {"Check Box 14", character.HPStats.SuccessSave3 ? "1" : ""},
                {"CONmod", character.MainStats.ConMod},
                {"Check Box 15", character.HPStats.FailureSave1 ? "1" : ""},
                {"Check Box 16", character.HPStats.FailureSave2 ? "1" : ""},
                {"Check Box 17", character.HPStats.FailureSave3 ? "1" : ""},
                {"HD", character.HPStats.HitDie},
                {"Flaws", character.DetailsStats.Flaws},
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
                {"Wpn Name", character.WeaponNotes.WeaponsInventory.Count >= 1 ? character.WeaponNotes.WeaponsInventory[0].WeaponName : ""},
                {"Wpn1 AtkBonus", character.WeaponNotes.WeaponsInventory.Count >= 1 ? character.WeaponNotes.WeaponsInventory[0].AttackBonus : ""},
                {"Wpn1 Damage", character.WeaponNotes.WeaponsInventory.Count >= 1 ? character.WeaponNotes.WeaponsInventory[0].Damage : ""},
                {"Insight", character.AllSkills.Insight.SkillScore},
                {"Intimidation", character.AllSkills.Intimidation.SkillScore},
                {"Wpn Name 2", character.WeaponNotes.WeaponsInventory.Count >= 2 ? character.WeaponNotes.WeaponsInventory[1].WeaponName : ""},
                {"Wpn2 AtkBonus ", character.WeaponNotes.WeaponsInventory.Count >= 2 ? character.WeaponNotes.WeaponsInventory[1].AttackBonus : ""},
                {"Wpn Name 3", character.WeaponNotes.WeaponsInventory.Count >= 3 ? character.WeaponNotes.WeaponsInventory[2].WeaponName : ""},
                {"Wpn3 AtkBonus  ", character.WeaponNotes.WeaponsInventory.Count >= 3 ? character.WeaponNotes.WeaponsInventory[2].AttackBonus : ""},
                {"Check Box 11", character.MainStats.StrSvThrProf ? "1" : ""},
                {"Check Box 18", character.MainStats.DexSvThrProf ? "1" : ""},
                {"Check Box 19", character.MainStats.ConSvThrProf ? "1" : ""},
                {"Check Box 20", character.MainStats.IntlSvThrProf ? "1" : ""},
                {"Check Box 21", character.MainStats.WisSvThrProf ? "1" : ""},
                {"Check Box 22", character.MainStats.ChaSvThrProf ? "1" : ""},
                {"INTmod", character.MainStats.IntlMod},
                {"Wpn2 Damage ", character.WeaponNotes.WeaponsInventory.Count >= 2 ? character.WeaponNotes.WeaponsInventory[1].Damage : ""},
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
                {"Check Box 23", character.AllSkills.Acrobatics.IsProficient ? "1" : ""},
                {"Check Box 24", character.AllSkills.AnimalHandling.IsProficient ? "1" : ""},
                {"Check Box 25", character.AllSkills.Arcana.IsProficient ? "1" : ""},
                {"Check Box 26", character.AllSkills.Athletics.IsProficient ? "1" : ""},
                {"Check Box 27", character.AllSkills.Deception.IsProficient ? "1" : ""},
                {"Check Box 28", character.AllSkills.History.IsProficient ? "1" : ""},
                {"Check Box 29", character.AllSkills.Insight.IsProficient ? "1" : ""},
                {"Check Box 30", character.AllSkills.Intimidation.IsProficient ? "1" : ""},
                {"Check Box 31", character.AllSkills.Investigation.IsProficient ? "1" : ""},
                {"Check Box 32", character.AllSkills.Medicine.IsProficient ? "1" : ""},
                {"Check Box 33", character.AllSkills.Nature.IsProficient ? "1" : ""},
                {"Check Box 34", character.AllSkills.Perception.IsProficient ? "1" : ""},
                {"Check Box 35", character.AllSkills.Performance.IsProficient ? "1" : ""},
                {"Check Box 36", character.AllSkills.Persuasion.IsProficient ? "1" : ""},
                {"Check Box 37", character.AllSkills.Religion.IsProficient ? "1" : ""},
                {"Check Box 38", character.AllSkills.SleightOfHand.IsProficient ? "1" : ""},
                {"Check Box 39", character.AllSkills.Stealth.IsProficient ? "1" : ""},
                {"Check Box 40", character.AllSkills.Survival.IsProficient ? "1" : ""},
                {"Persuasion", character.AllSkills.Persuasion.SkillScore},
                {"HPMax", character.HPStats.MaxHP.ToString()},
                {"HPCurrent", character.HPStats.CurrentHP.ToString()},
                {"HPTemp", character.HPStats.TempHP.ToString()},
                {"Wpn3 Damage ", character.WeaponNotes.WeaponsInventory.Count >= 3 ? character.WeaponNotes.WeaponsInventory[2].Damage : ""},
                {"SleightofHand", character.AllSkills.SleightOfHand.SkillScore},
                {"CHamod", character.MainStats.ChaMod},
                {"Survival", character.AllSkills.Survival.SkillScore},
                {"AttacksSpellcasting", character.WeaponNotes.WeaponDetails},
                {"Passive", character.MiscStats.PassivePerception},
                {"CP", character.CharacterNotes.Money.Bronze},
                {"ProficienciesLang", character.CharacterNotes.Proficiencies},
                {"SP", character.CharacterNotes.Money.Silver},
                {"EP", character.CharacterNotes.Money.Electrum},
                {"GP", character.CharacterNotes.Money.Gold},
                {"PP", character.CharacterNotes.Money.Platinum},
                {"Equipment", character.CharacterNotes.Equipment},
                {"Features and Traits", character.CharacterNotes.AbilityDesc},
                {"CharacterName 2", character.DetailsStats.CharacterName},
                {"Age", character.DetailsStats.Age},
                {"Height", character.DetailsStats.Height},
                {"Weight", character.DetailsStats.Weight},
                {"Eyes", character.DetailsStats.Eyes},
                {"Skin", character.DetailsStats.Skin},
                {"Hair", character.DetailsStats.Hair},
                {"CHARACTER IMAGE", ""},
                {"Faction Symbol Image", ""},
                {"Allies", character.CharacterNotes.SessionNotes},
                {"FactionName", ""},
                {"Backstory", character.CharacterNotes.Backstory},
                {"Feat+Traits", character.CharacterNotes.FeaturesAndTraits},
                {"Treasure", character.CharacterNotes.Treasure},
                {"Spellcasting Class 2", string.Join("", character.DetailsStats.ClassAndLevel.Where(x => !char.IsDigit(x) && x != ' ').ToString())},
                {"SpellcastingAbility 2", character.AllSpells.SpellcastingAbility},
                {"SpellSaveDC  2", character.AllSpells.SpellSaveDC},
                {"SpellAtkBonus 2", character.AllSpells.SpellAttackBonus},
                {"SlotsTotal 19", character.AllSpells.FirstLevelSpellViewModel.SlotsTotal},
                {"SlotsRemaining 19", character.AllSpells.FirstLevelSpellViewModel.SlotsExpended},
                {"Spells 1014", character.AllSpells.CantripSpellViewModel.Spells[0].Name},
                {"Spells 1015", character.AllSpells.FirstLevelSpellViewModel.Spells[0].Name},
                {"Spells 1016", character.AllSpells.CantripSpellViewModel.Spells[1].Name},
                {"Spells 1017", character.AllSpells.CantripSpellViewModel.Spells[2].Name},
                {"Spells 1018", character.AllSpells.CantripSpellViewModel.Spells[3].Name},
                {"Spells 1019", character.AllSpells.CantripSpellViewModel.Spells[4].Name},
                {"Spells 1020", character.AllSpells.CantripSpellViewModel.Spells[5].Name},
                {"Spells 1021", character.AllSpells.CantripSpellViewModel.Spells[6].Name},
                {"Spells 1022", character.AllSpells.CantripSpellViewModel.Spells[7].Name},
                {"Check Box 314", character.AllSpells.ThirdLevelSpellViewModel.Spells[1].IsPrepared ? "1" : ""},
                {"Check Box 3031", character.AllSpells.ThirdLevelSpellViewModel.Spells[2].IsPrepared ? "1" : ""},
                {"Check Box 3032", character.AllSpells.ThirdLevelSpellViewModel.Spells[3].IsPrepared ? "1" : ""},
                {"Check Box 3033", character.AllSpells.ThirdLevelSpellViewModel.Spells[4].IsPrepared ? "1" : ""},
                {"Check Box 3034", character.AllSpells.ThirdLevelSpellViewModel.Spells[5].IsPrepared ? "1" : ""},
                {"Check Box 3035", character.AllSpells.ThirdLevelSpellViewModel.Spells[6].IsPrepared ? "1" : ""},
                {"Check Box 3036", character.AllSpells.ThirdLevelSpellViewModel.Spells[7].IsPrepared ? "1" : ""},
                {"Check Box 3037", character.AllSpells.ThirdLevelSpellViewModel.Spells[8].IsPrepared ? "1" : ""},
                {"Check Box 3038", character.AllSpells.ThirdLevelSpellViewModel.Spells[9].IsPrepared ? "1" : ""},
                {"Check Box 3039", character.AllSpells.ThirdLevelSpellViewModel.Spells[10].IsPrepared ? "1" : ""},
                {"Check Box 3040", character.AllSpells.ThirdLevelSpellViewModel.Spells[11].IsPrepared ? "1" : ""},
                {"Check Box 321", character.AllSpells.SixthLevelSpellViewModel.Spells[0].IsPrepared ? "1" : ""},
                {"Check Box 320", character.AllSpells.SixthLevelSpellViewModel.Spells[1].IsPrepared ? "1" : ""},
                {"Check Box 3060", character.AllSpells.SixthLevelSpellViewModel.Spells[2].IsPrepared ? "1" : ""},
                {"Check Box 3061", character.AllSpells.SixthLevelSpellViewModel.Spells[3].IsPrepared ? "1" : ""},
                {"Check Box 3062", character.AllSpells.SixthLevelSpellViewModel.Spells[4].IsPrepared ? "1" : ""},
                {"Check Box 3063", character.AllSpells.SixthLevelSpellViewModel.Spells[5].IsPrepared ? "1" : ""},
                {"Check Box 3064", character.AllSpells.SixthLevelSpellViewModel.Spells[6].IsPrepared ? "1" : ""},
                {"Check Box 3065", character.AllSpells.SixthLevelSpellViewModel.Spells[7].IsPrepared ? "1" : ""},
                {"Check Box 3066", character.AllSpells.SixthLevelSpellViewModel.Spells[8].IsPrepared ? "1" : ""},
                {"Check Box 315", character.AllSpells.ThirdLevelSpellViewModel.Spells[0].IsPrepared ? "1" : ""}, //
                {"Check Box 3041", character.AllSpells.ThirdLevelSpellViewModel.Spells[12].IsPrepared ? "1" : ""},
                {"Spells 1023", character.AllSpells.FirstLevelSpellViewModel.Spells[1].Name},
                {"Check Box 251", character.AllSpells.FirstLevelSpellViewModel.Spells[0].IsPrepared ? "1" : ""},
                {"Check Box 309", character.AllSpells.FirstLevelSpellViewModel.Spells[1].IsPrepared ? "1" : ""},
                {"Check Box 3010", character.AllSpells.FirstLevelSpellViewModel.Spells[2].IsPrepared ? "1" : ""},
                {"Check Box 3011", character.AllSpells.FirstLevelSpellViewModel.Spells[3].IsPrepared ? "1" : ""},
                {"Check Box 3012", character.AllSpells.FirstLevelSpellViewModel.Spells[4].IsPrepared ? "1" : ""},
                {"Check Box 3013", character.AllSpells.FirstLevelSpellViewModel.Spells[5].IsPrepared ? "1" : ""},
                {"Check Box 3014", character.AllSpells.FirstLevelSpellViewModel.Spells[6].IsPrepared ? "1" : ""},
                {"Check Box 3015", character.AllSpells.FirstLevelSpellViewModel.Spells[7].IsPrepared ? "1" : ""},
                {"Check Box 3016", character.AllSpells.FirstLevelSpellViewModel.Spells[8].IsPrepared ? "1" : ""},
                {"Check Box 3017", character.AllSpells.FirstLevelSpellViewModel.Spells[9].IsPrepared ? "1" : ""},
                {"Check Box 3018", character.AllSpells.FirstLevelSpellViewModel.Spells[10].IsPrepared ? "1" : ""},
                {"Check Box 3019", character.AllSpells.FirstLevelSpellViewModel.Spells[11].IsPrepared ? "1" : ""},
                {"Spells 1024", character.AllSpells.FirstLevelSpellViewModel.Spells[2].Name},
                {"Spells 1025", character.AllSpells.FirstLevelSpellViewModel.Spells[3].Name},
                {"Spells 1026", character.AllSpells.FirstLevelSpellViewModel.Spells[4].Name},
                {"Spells 1027", character.AllSpells.FirstLevelSpellViewModel.Spells[5].Name},
                {"Spells 1028", character.AllSpells.FirstLevelSpellViewModel.Spells[6].Name},
                {"Spells 1029", character.AllSpells.FirstLevelSpellViewModel.Spells[7].Name},
                {"Spells 1030", character.AllSpells.FirstLevelSpellViewModel.Spells[8].Name},
                {"Spells 1031", character.AllSpells.FirstLevelSpellViewModel.Spells[9].Name},
                {"Spells 1032", character.AllSpells.FirstLevelSpellViewModel.Spells[10].Name},
                {"Spells 1033", character.AllSpells.FirstLevelSpellViewModel.Spells[11].Name},
                {"SlotsTotal 20", character.AllSpells.SecondLevelSpellViewModel.SlotsTotal},
                {"SlotsRemaining 20", character.AllSpells.SecondLevelSpellViewModel.SlotsExpended},
                {"Spells 1034", character.AllSpells.SecondLevelSpellViewModel.Spells[1].Name},
                {"Spells 1035", character.AllSpells.SecondLevelSpellViewModel.Spells[2].Name},
                {"Spells 1036", character.AllSpells.SecondLevelSpellViewModel.Spells[3].Name},
                {"Spells 1037", character.AllSpells.SecondLevelSpellViewModel.Spells[4].Name},
                {"Spells 1038", character.AllSpells.SecondLevelSpellViewModel.Spells[5].Name},
                {"Spells 1039", character.AllSpells.SecondLevelSpellViewModel.Spells[6].Name},
                {"Spells 1040", character.AllSpells.SecondLevelSpellViewModel.Spells[7].Name},
                {"Spells 1041", character.AllSpells.SecondLevelSpellViewModel.Spells[8].Name},
                {"Spells 1042", character.AllSpells.SecondLevelSpellViewModel.Spells[9].Name},
                {"Spells 1043", character.AllSpells.SecondLevelSpellViewModel.Spells[10].Name},
                {"Spells 1044", character.AllSpells.SecondLevelSpellViewModel.Spells[11].Name},
                {"Spells 1045", character.AllSpells.SecondLevelSpellViewModel.Spells[12].Name},
                {"Spells 1046", character.AllSpells.SecondLevelSpellViewModel.Spells[0].Name},
                {"SlotsTotal 21", character.AllSpells.ThirdLevelSpellViewModel.SlotsTotal},
                {"SlotsRemaining 21", character.AllSpells.ThirdLevelSpellViewModel.SlotsExpended},
                {"Spells 1047", character.AllSpells.ThirdLevelSpellViewModel.Spells[1].Name},
                {"Spells 1048", character.AllSpells.ThirdLevelSpellViewModel.Spells[0].Name},
                {"Spells 1049", character.AllSpells.ThirdLevelSpellViewModel.Spells[2].Name},
                {"Spells 1050", character.AllSpells.ThirdLevelSpellViewModel.Spells[3].Name},
                {"Spells 1051", character.AllSpells.ThirdLevelSpellViewModel.Spells[4].Name},
                {"Spells 1052", character.AllSpells.ThirdLevelSpellViewModel.Spells[5].Name},
                {"Spells 1053", character.AllSpells.ThirdLevelSpellViewModel.Spells[6].Name},
                {"Spells 1054", character.AllSpells.ThirdLevelSpellViewModel.Spells[7].Name},
                {"Spells 1055", character.AllSpells.ThirdLevelSpellViewModel.Spells[8].Name},
                {"Spells 1056", character.AllSpells.ThirdLevelSpellViewModel.Spells[9].Name},
                {"Spells 1057", character.AllSpells.ThirdLevelSpellViewModel.Spells[10].Name},
                {"Spells 1058", character.AllSpells.ThirdLevelSpellViewModel.Spells[11].Name},
                {"Spells 1059", character.AllSpells.ThirdLevelSpellViewModel.Spells[12].Name},
                {"SlotsTotal 22", character.AllSpells.FourthLevelSpellViewModel.SlotsTotal},
                {"SlotsRemaining 22", character.AllSpells.FourthLevelSpellViewModel.SlotsExpended},
                {"Spells 1060", character.AllSpells.FourthLevelSpellViewModel.Spells[1].Name},
                {"Spells 1061", character.AllSpells.FourthLevelSpellViewModel.Spells[0].Name},
                {"Spells 1062", character.AllSpells.FourthLevelSpellViewModel.Spells[2].Name},
                {"Spells 1063", character.AllSpells.FourthLevelSpellViewModel.Spells[3].Name},
                {"Spells 1064", character.AllSpells.FourthLevelSpellViewModel.Spells[4].Name},
                {"Check Box 323", character.AllSpells.SeventhLevelSpellViewModel.Spells[0].IsPrepared ? "1" : ""},
                {"Check Box 322", character.AllSpells.SeventhLevelSpellViewModel.Spells[1].IsPrepared ? "1" : ""},
                {"Check Box 3067", character.AllSpells.SeventhLevelSpellViewModel.Spells[2].IsPrepared ? "1" : ""},
                {"Check Box 3068", character.AllSpells.SeventhLevelSpellViewModel.Spells[3].IsPrepared ? "1" : ""},
                {"Check Box 3069", character.AllSpells.SeventhLevelSpellViewModel.Spells[4].IsPrepared ? "1" : ""},
                {"Check Box 3070", character.AllSpells.SeventhLevelSpellViewModel.Spells[5].IsPrepared ? "1" : ""},
                {"Check Box 3071", character.AllSpells.SeventhLevelSpellViewModel.Spells[6].IsPrepared ? "1" : ""},
                {"Check Box 3072", character.AllSpells.SeventhLevelSpellViewModel.Spells[7].IsPrepared ? "1" : ""},
                {"Check Box 3073", character.AllSpells.SeventhLevelSpellViewModel.Spells[8].IsPrepared ? "1" : ""},
                {"Spells 1065", character.AllSpells.FourthLevelSpellViewModel.Spells[5].Name},
                {"Spells 1066", character.AllSpells.FourthLevelSpellViewModel.Spells[6].Name},
                {"Spells 1067", character.AllSpells.FourthLevelSpellViewModel.Spells[7].Name},
                {"Spells 1068", character.AllSpells.FourthLevelSpellViewModel.Spells[8].Name},
                {"Spells 1069", character.AllSpells.FourthLevelSpellViewModel.Spells[9].Name},
                {"Spells 1070", character.AllSpells.FourthLevelSpellViewModel.Spells[10].Name},
                {"Spells 1071", character.AllSpells.FourthLevelSpellViewModel.Spells[11].Name},
                {"Check Box 317", character.AllSpells.FourthLevelSpellViewModel.Spells[0].IsPrepared ? "1" : ""},
                {"Spells 1072", character.AllSpells.FourthLevelSpellViewModel.Spells[12].Name},
                {"SlotsTotal 23", character.AllSpells.FifthLevelSpellViewModel.SlotsTotal},
                {"SlotsRemaining 23", character.AllSpells.FifthLevelSpellViewModel.SlotsExpended},
                {"Spells 1073", character.AllSpells.FifthLevelSpellViewModel.Spells[1].Name},
                {"Spells 1074", character.AllSpells.FifthLevelSpellViewModel.Spells[0].Name},
                {"Spells 1075", character.AllSpells.FifthLevelSpellViewModel.Spells[2].Name},
                {"Spells 1076", character.AllSpells.FifthLevelSpellViewModel.Spells[3].Name},
                {"Spells 1077", character.AllSpells.FifthLevelSpellViewModel.Spells[4].Name},
                {"Spells 1078", character.AllSpells.FifthLevelSpellViewModel.Spells[5].Name},
                {"Spells 1079", character.AllSpells.FifthLevelSpellViewModel.Spells[6].Name},
                {"Spells 1080", character.AllSpells.FifthLevelSpellViewModel.Spells[7].Name},
                {"Spells 1081", character.AllSpells.FifthLevelSpellViewModel.Spells[8].Name},
                {"SlotsTotal 24", character.AllSpells.SixthLevelSpellViewModel.SlotsTotal},
                {"SlotsRemaining 24", character.AllSpells.SixthLevelSpellViewModel.SlotsExpended},
                {"Spells 1082", character.AllSpells.SixthLevelSpellViewModel.Spells[1].Name},
                {"Spells 1083", character.AllSpells.SixthLevelSpellViewModel.Spells[0].Name},
                {"Spells 1084", character.AllSpells.SixthLevelSpellViewModel.Spells[2].Name},
                {"Spells 1085", character.AllSpells.SixthLevelSpellViewModel.Spells[3].Name},
                {"Spells 1086", character.AllSpells.SixthLevelSpellViewModel.Spells[4].Name},
                {"Spells 1087", character.AllSpells.SixthLevelSpellViewModel.Spells[5].Name},
                {"Spells 1088", character.AllSpells.SixthLevelSpellViewModel.Spells[6].Name},
                {"Spells 1089", character.AllSpells.SixthLevelSpellViewModel.Spells[7].Name},
                {"Spells 1090", character.AllSpells.SixthLevelSpellViewModel.Spells[8].Name},
                {"SlotsTotal 25", character.AllSpells.SeventhLevelSpellViewModel.SlotsTotal},
                {"SlotsRemaining 25", character.AllSpells.SeventhLevelSpellViewModel.SlotsExpended},
                {"Spells 1091", character.AllSpells.SeventhLevelSpellViewModel.Spells[1].Name},
                {"Spells 1092", character.AllSpells.SeventhLevelSpellViewModel.Spells[0].Name},
                {"Spells 1093", character.AllSpells.SeventhLevelSpellViewModel.Spells[2].Name},
                {"Spells 1094", character.AllSpells.SeventhLevelSpellViewModel.Spells[3].Name},
                {"Spells 1095", character.AllSpells.SeventhLevelSpellViewModel.Spells[4].Name},
                {"Spells 1096", character.AllSpells.SeventhLevelSpellViewModel.Spells[5].Name},
                {"Spells 1097", character.AllSpells.SeventhLevelSpellViewModel.Spells[6].Name},
                {"Spells 1098", character.AllSpells.SeventhLevelSpellViewModel.Spells[7].Name},
                {"Spells 1099", character.AllSpells.SeventhLevelSpellViewModel.Spells[8].Name},
                {"SlotsTotal 26", character.AllSpells.EighthLevelSpellViewModel.SlotsTotal},
                {"SlotsRemaining 26", character.AllSpells.SeventhLevelSpellViewModel.SlotsExpended},
                {"Spells 10100", character.AllSpells.EighthLevelSpellViewModel.Spells[1].Name},
                {"Spells 10101", character.AllSpells.EighthLevelSpellViewModel.Spells[0].Name},
                {"Spells 10102", character.AllSpells.EighthLevelSpellViewModel.Spells[2].Name},
                {"Spells 10103", character.AllSpells.EighthLevelSpellViewModel.Spells[3].Name},
                {"Check Box 316", character.AllSpells.FourthLevelSpellViewModel.Spells[1].IsPrepared ? "1" : ""},
                {"Check Box 3042", character.AllSpells.FourthLevelSpellViewModel.Spells[2].IsPrepared ? "1" : ""},
                {"Check Box 3043", character.AllSpells.FourthLevelSpellViewModel.Spells[3].IsPrepared ? "1" : ""},
                {"Check Box 3044", character.AllSpells.FourthLevelSpellViewModel.Spells[4].IsPrepared ? "1" : ""},
                {"Check Box 3045", character.AllSpells.FourthLevelSpellViewModel.Spells[5].IsPrepared ? "1" : ""},
                {"Check Box 3046", character.AllSpells.FourthLevelSpellViewModel.Spells[6].IsPrepared ? "1" : ""},
                {"Check Box 3047", character.AllSpells.FourthLevelSpellViewModel.Spells[7].IsPrepared ? "1" : ""},
                {"Check Box 3048", character.AllSpells.FourthLevelSpellViewModel.Spells[8].IsPrepared ? "1" : ""},
                {"Check Box 3049", character.AllSpells.FourthLevelSpellViewModel.Spells[9].IsPrepared ? "1" : ""},
                {"Check Box 3050", character.AllSpells.FourthLevelSpellViewModel.Spells[10].IsPrepared ? "1" : ""},
                {"Check Box 3051", character.AllSpells.FourthLevelSpellViewModel.Spells[11].IsPrepared ? "1" : ""},
                {"Check Box 3052", character.AllSpells.FourthLevelSpellViewModel.Spells[12].IsPrepared ? "1" : ""},
                {"Spells 10104", character.AllSpells.EighthLevelSpellViewModel.Spells[4].Name},
                {"Check Box 325", character.AllSpells.EighthLevelSpellViewModel.Spells[0].IsPrepared ? "1" : ""},
                {"Check Box 324", character.AllSpells.EighthLevelSpellViewModel.Spells[1].IsPrepared ? "1" : ""},
                {"Check Box 3074", character.AllSpells.EighthLevelSpellViewModel.Spells[2].IsPrepared ? "1" : ""},
                {"Check Box 3075", character.AllSpells.EighthLevelSpellViewModel.Spells[3].IsPrepared ? "1" : ""},
                {"Check Box 3076", character.AllSpells.EighthLevelSpellViewModel.Spells[4].IsPrepared ? "1" : ""},
                {"Check Box 3077", character.AllSpells.EighthLevelSpellViewModel.Spells[5].IsPrepared ? "1" : ""},
                {"Spells 10105", character.AllSpells.EighthLevelSpellViewModel.Spells[5].Name},
                {"Spells 10106", character.AllSpells.EighthLevelSpellViewModel.Spells[6].Name},
                {"Check Box 3078", character.AllSpells.EighthLevelSpellViewModel.Spells[6].IsPrepared ? "1" : ""},
                {"SlotsTotal 27", character.AllSpells.NinthLevelSpellViewModel.SlotsTotal},
                {"SlotsRemaining 27", character.AllSpells.NinthLevelSpellViewModel.SlotsExpended},
                {"Check Box 313", character.AllSpells.SecondLevelSpellViewModel.Spells[0].IsPrepared ? "1" : ""},
                {"Check Box 310", character.AllSpells.SecondLevelSpellViewModel.Spells[1].IsPrepared ? "1" : ""},
                {"Check Box 3020", character.AllSpells.SecondLevelSpellViewModel.Spells[2].IsPrepared ? "1" : ""},
                {"Check Box 3021", character.AllSpells.SecondLevelSpellViewModel.Spells[3].IsPrepared ? "1" : ""},
                {"Check Box 3022", character.AllSpells.SecondLevelSpellViewModel.Spells[4].IsPrepared ? "1" : ""},
                {"Check Box 3023", character.AllSpells.SecondLevelSpellViewModel.Spells[5].IsPrepared ? "1" : ""},
                {"Check Box 3024", character.AllSpells.SecondLevelSpellViewModel.Spells[6].IsPrepared ? "1" : ""},
                {"Check Box 3025", character.AllSpells.SecondLevelSpellViewModel.Spells[7].IsPrepared ? "1" : ""},
                {"Check Box 3026", character.AllSpells.SecondLevelSpellViewModel.Spells[8].IsPrepared ? "1" : ""},
                {"Check Box 3027", character.AllSpells.SecondLevelSpellViewModel.Spells[9].IsPrepared ? "1" : ""},
                {"Check Box 3028", character.AllSpells.SecondLevelSpellViewModel.Spells[10].IsPrepared ? "1" : ""},
                {"Check Box 3029", character.AllSpells.SecondLevelSpellViewModel.Spells[11].IsPrepared ? "1" : ""},
                {"Check Box 3030", character.AllSpells.SecondLevelSpellViewModel.Spells[12].IsPrepared ? "1" : ""},
                {"Spells 10107", character.AllSpells.NinthLevelSpellViewModel.Spells[1].Name},
                {"Spells 10108", character.AllSpells.NinthLevelSpellViewModel.Spells[0].Name},
                {"Spells 10109", character.AllSpells.NinthLevelSpellViewModel.Spells[2].Name},
                {"Spells 101010", character.AllSpells.NinthLevelSpellViewModel.Spells[3].Name},
                {"Spells 101011", character.AllSpells.NinthLevelSpellViewModel.Spells[4].Name},
                {"Spells 101012", character.AllSpells.NinthLevelSpellViewModel.Spells[5].Name},
                {"Check Box 319", character.AllSpells.FifthLevelSpellViewModel.Spells[0].IsPrepared ? "1" : ""},
                {"Check Box 318", character.AllSpells.FifthLevelSpellViewModel.Spells[1].IsPrepared ? "1" : ""},
                {"Check Box 3053", character.AllSpells.FifthLevelSpellViewModel.Spells[2].IsPrepared ? "1" : ""},
                {"Check Box 3054", character.AllSpells.FifthLevelSpellViewModel.Spells[3].IsPrepared ? "1" : ""},
                {"Check Box 3055", character.AllSpells.FifthLevelSpellViewModel.Spells[4].IsPrepared ? "1" : ""},
                {"Check Box 3056", character.AllSpells.FifthLevelSpellViewModel.Spells[5].IsPrepared ? "1" : ""},
                {"Check Box 3057", character.AllSpells.FifthLevelSpellViewModel.Spells[6].IsPrepared ? "1" : ""},
                {"Check Box 3058", character.AllSpells.FifthLevelSpellViewModel.Spells[7].IsPrepared ? "1" : ""},
                {"Check Box 3059", character.AllSpells.FifthLevelSpellViewModel.Spells[8].IsPrepared ? "1" : ""},
                {"Check Box 327", character.AllSpells.NinthLevelSpellViewModel.Spells[0].IsPrepared ? "1" : ""},
                {"Check Box 326", character.AllSpells.NinthLevelSpellViewModel.Spells[1].IsPrepared ? "1" : ""},
                {"Check Box 3079", character.AllSpells.NinthLevelSpellViewModel.Spells[2].IsPrepared ? "1" : ""},
                {"Check Box 3080", character.AllSpells.NinthLevelSpellViewModel.Spells[3].IsPrepared ? "1" : ""},
                {"Check Box 3081", character.AllSpells.NinthLevelSpellViewModel.Spells[4].IsPrepared ? "1" : ""},
                {"Check Box 3082", character.AllSpells.NinthLevelSpellViewModel.Spells[5].IsPrepared ? "1" : ""},
                {"Spells 101013", character.AllSpells.NinthLevelSpellViewModel.Spells[6].Name},
                {"Check Box 3083", character.AllSpells.NinthLevelSpellViewModel.Spells[6].IsPrepared ? "1" : ""},
            };
        }
    }
}
