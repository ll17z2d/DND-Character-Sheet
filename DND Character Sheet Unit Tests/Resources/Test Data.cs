using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DND_Character_Sheet.Models.API_Models;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Wrappers;

namespace DND_Character_Sheet_Unit_Tests.Resources
{
    public static class TestData
    {
        public static string GetTestDNDAPIClass()
            => "{\"index\":\"monk\",\"name\":\"Monk\",\"hit_die\":8,\"proficiency_choices\":[{\"choose\":1,\"type\":\"proficiencies\",\"from\":[{\"index\":\"alchemists-supplies\",\"name\":\"Alchemist's supplies\",\"url\":\"/api/proficiencies/alchemists-supplies\"},{\"index\":\"brewers-supplies\",\"name\":\"Brewer's supplies\",\"url\":\"/api/proficiencies/brewers-supplies\"},{\"index\":\"calligraphers-supplies\",\"name\":\"Calligrapher's supplies\",\"url\":\"/api/proficiencies/calligraphers-supplies\"},{\"index\":\"carpenters-tools\",\"name\":\"Carpenter's tools\",\"url\":\"/api/proficiencies/carpenters-tools\"},{\"index\":\"cartographers-tools\",\"name\":\"Cartographer's tools\",\"url\":\"/api/proficiencies/cartographers-tools\"},{\"index\":\"cobblers-tools\",\"name\":\"Cobbler's tools\",\"url\":\"/api/proficiencies/cobblers-tools\"},{\"index\":\"cooks-utensils\",\"name\":\"Cook's utensils\",\"url\":\"/api/proficiencies/cooks-utensils\"},{\"index\":\"glassblowers-tools\",\"name\":\"Glassblower's tools\",\"url\":\"/api/proficiencies/glassblowers-tools\"},{\"index\":\"jewelers-tools\",\"name\":\"Jeweler's tools\",\"url\":\"/api/proficiencies/jewelers-tools\"},{\"index\":\"leatherworkers-tools\",\"name\":\"Leatherworker's tools\",\"url\":\"/api/proficiencies/leatherworkers-tools\"},{\"index\":\"masons-tools\",\"name\":\"Mason's tools\",\"url\":\"/api/proficiencies/masons-tools\"},{\"index\":\"painters-supplies\",\"name\":\"Painter's supplies\",\"url\":\"/api/proficiencies/painters-supplies\"},{\"index\":\"potters-tools\",\"name\":\"Potter's tools\",\"url\":\"/api/proficiencies/potters-tools\"},{\"index\":\"smiths-tools\",\"name\":\"Smith's tools\",\"url\":\"/api/proficiencies/smiths-tools\"},{\"index\":\"tinkers-tools\",\"name\":\"Tinker's tools\",\"url\":\"/api/proficiencies/tinkers-tools\"},{\"index\":\"weavers-tools\",\"name\":\"Weaver's tools\",\"url\":\"/api/proficiencies/weavers-tools\"},{\"index\":\"woodcarvers-tools\",\"name\":\"Woodcarver's tools\",\"url\":\"/api/proficiencies/woodcarvers-tools\"},{\"index\":\"disguise-kit\",\"name\":\"Disguise kit\",\"url\":\"/api/proficiencies/disguise-kit\"},{\"index\":\"forgery-kit\",\"name\":\"Forgery kit\",\"url\":\"/api/proficiencies/forgery-kit\"}]},{\"choose\":1,\"type\":\"proficiencies\",\"from\":[{\"index\":\"bagpipes\",\"name\":\"Bagpipes\",\"url\":\"/api/proficiencies/bagpipes\"},{\"index\":\"drum\",\"name\":\"Drum\",\"url\":\"/api/proficiencies/drum\"},{\"index\":\"dulcimer\",\"name\":\"Dulcimer\",\"url\":\"/api/proficiencies/dulcimer\"},{\"index\":\"flute\",\"name\":\"Flute\",\"url\":\"/api/proficiencies/flute\"},{\"index\":\"lute\",\"name\":\"Lute\",\"url\":\"/api/proficiencies/lute\"},{\"index\":\"lyre\",\"name\":\"Lyre\",\"url\":\"/api/proficiencies/lyre\"},{\"index\":\"horn\",\"name\":\"Horn\",\"url\":\"/api/proficiencies/horn\"},{\"index\":\"pan-flute\",\"name\":\"Pan flute\",\"url\":\"/api/proficiencies/pan-flute\"},{\"index\":\"shawm\",\"name\":\"Shawm\",\"url\":\"/api/proficiencies/shawm\"},{\"index\":\"viol\",\"name\":\"Viol\",\"url\":\"/api/proficiencies/viol\"}]},{\"choose\":2,\"type\":\"proficiencies\",\"from\":[{\"index\":\"skill-acrobatics\",\"name\":\"Skill: Acrobatics\",\"url\":\"/api/proficiencies/skill-acrobatics\"},{\"index\":\"skill-athletics\",\"name\":\"Skill: Athletics\",\"url\":\"/api/proficiencies/skill-athletics\"},{\"index\":\"skill-history\",\"name\":\"Skill: History\",\"url\":\"/api/proficiencies/skill-history\"},{\"index\":\"skill-insight\",\"name\":\"Skill: Insight\",\"url\":\"/api/proficiencies/skill-insight\"},{\"index\":\"skill-religion\",\"name\":\"Skill: Religion\",\"url\":\"/api/proficiencies/skill-religion\"},{\"index\":\"skill-stealth\",\"name\":\"Skill: Stealth\",\"url\":\"/api/proficiencies/skill-stealth\"}]}],\"proficiencies\":[{\"index\":\"simple-weapons\",\"name\":\"Simple weapons\",\"url\":\"/api/proficiencies/simple-weapons\"},{\"index\":\"shortswords\",\"name\":\"Shortswords\",\"url\":\"/api/proficiencies/shortswords\"}],\"saving_throws\":[{\"index\":\"str\",\"name\":\"STR\",\"url\":\"/api/ability-scores/str\"},{\"index\":\"dex\",\"name\":\"DEX\",\"url\":\"/api/ability-scores/dex\"}],\"starting_equipment\":[{\"equipment\":{\"index\":\"dart\",\"name\":\"Dart\",\"url\":\"/api/equipment/dart\"},\"quantity\":10}],\"starting_equipment_options\":[{\"choose\":1,\"type\":\"equipment\",\"from\":[{\"equipment\":{\"index\":\"shortsword\",\"name\":\"Shortsword\",\"url\":\"/api/equipment/shortsword\"},\"quantity\":1},{\"equipment_option\":{\"choose\":1,\"type\":\"equipment\",\"from\":{\"equipment_category\":{\"index\":\"simple-weapons\",\"name\":\"Simple Weapons\",\"url\":\"/api/equipment-categories/simple-weapons\"}}}}]},{\"choose\":1,\"type\":\"equipment\",\"from\":[{\"equipment\":{\"index\":\"dungeoneers-pack\",\"name\":\"Dungeoneer's Pack\",\"url\":\"/api/equipment/dungeoneers-pack\"},\"quantity\":1},{\"equipment\":{\"index\":\"explorers-pack\",\"name\":\"Explorer's Pack\",\"url\":\"/api/equipment/explorers-pack\"},\"quantity\":1}]}],\"class_levels\":\"/api/classes/monk/levels\",\"subclasses\":[{\"index\":\"open-hand\",\"name\":\"Open Hand\",\"url\":\"/api/subclasses/open-hand\"}],\"url\":\"/api/classes/monk\"}";

        public static ICharacterModel GetInitialisedCharacterModel()
        {
            var character = new CharacterModel(
                new MainStats(10, 10, 10, 10, 10, 10, "+3", "+3", "+3", "+3", "+3", "+3"),
                new AllSkills(),
                new HPStats(20, 16, 2, "2d6"),
                new DetailsStats("Tempus Namus", "Monk 4", "Hermit", "My Name", "Human", "Aight", "350"),
                new MiscStats("+4", 18, "+1", "+35", "1", "15"),
                new CharacterNotes("", "QN", "EQ", "SN", "AD", "PR",
                    new Money("2", "50", "200", "500"),
                    new SerializeCharacterWrapper(),
                    new FileOperationsWrapper()),
                new WeaponNotes(new ObservableCollection<WeaponsInventory>()
                    {new("Crossbow", "+6", "1d4")}, "temp weapons"),
                new AllSpells())
            {
                FilePath = "Testing FilePath",
            };

            foreach (var skill in character.AllSkills)
            {
                skill.SkillScore = "+5";
                skill.IsProficient = true;
            }

            return character;
        }
    }
}
