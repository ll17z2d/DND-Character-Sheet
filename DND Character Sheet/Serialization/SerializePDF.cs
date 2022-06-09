using DND_Character_Sheet.Models.Serialize_Types;
using iText.Forms;
using iText.Kernel.Pdf;
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
                //var pdfReader = new PdfReader(existingFileStream);

                // PdfStamper, which will create
                //var stamper = new PdfStamper(pdfReader, newFileStream);

                //var form = stamper.AcroFields;
                //var fieldKeys = form.Fields.Keys;

                var pdfDoc = new PdfDocument(new PdfReader(existingFileStream), new PdfWriter(newFileStream));
                var form = PdfAcroForm.GetAcroForm(pdfDoc, true);
                var b = character.WeaponNotes.WeaponsInventory.Count >= 1 ? character.WeaponNotes.WeaponsInventory[0].WeaponName : ""; //character.WeaponNotes.WeaponsInventory[0] != null ? character.WeaponNotes.WeaponsInventory[0].WeaponName : "";

                //PDF field values seem to only be stored as strings, so when I deserialize, that's when I will have to convert to their relevant types
                //Will have to somehow check the "Prepared Spells" checkboxes, and add the character pic to the "Character Appearance" box
                //var stringDict = new Dictionary<string, string>();
                var dict = new Dictionary<string, object>()
                {
                    {"ClassLevel", character.DetailsStats.ClassAndLevel.Where(x => char.IsDigit(x)).ToString()}, //Extract just the level from here by indexing out just the numbers
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

                foreach (var fieldkey in dict)
                {
                    var a = form.GetField(fieldkey.Key);
                    form.GetField(fieldkey.Key).SetValue(fieldkey.Value.ToString());
                }

                pdfDoc.Close();

                //var counter = 0;
                //foreach (string fieldKey in form)
                //{
                //    //if (fieldKey.GetType() == typeof(string))
                //    //{
                //    //    form.SetField(fieldKey, "REPLACED!");
                //    //}


                //    counter++;
                //    //form.SetField(fieldKey, dict.GetValueOrDefault(fieldKey));
                //    if (dict.GetValueOrDefault(fieldKey).GetType() == typeof(string))
                //    {
                //        form.SetField(fieldKey, dict.GetValueOrDefault(fieldKey).ToString());
                //    }
                //    else
                //    {
                //        form.GetFieldItem("Check Box 12");
                //        //form.SetListSelection(fieldKey, dict.GetValueOrDefault(fieldKey));
                //    }

                //    var p = form.GetFieldItem("Check Box 12");
                //    var q = form.GetFieldItem("ClassLevel");
                //    var s = form.GetField("Check Box 12");
                //    var t = form.GetField("ClassLevel");

                //    if (!dict.TryGetValue(fieldKey, out var value))
                //    {
                //        throw new Exception($"Hmm, it seems {fieldKey} isn't properly implemented");
                //    }
                //}

                //stamper.Close();
                //pdfReader.Close();
            }

            //var x = string.Join("", character.DetailsStats.ClassAndLevel.Where(x => !char.IsDigit(x) && x != ' ').ToList());
            return true;
        }
    }
}
