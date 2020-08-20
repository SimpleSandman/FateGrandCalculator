using System;
using System.Collections.Generic;
using System.Threading;

using FateGrandOrderPOC.Shared;
using FateGrandOrderPOC.Shared.AtlasAcademyJson;

namespace FateGrandOrderPOC
{
    public class Program
    {
        const string ARTORIA_PENDRAGON_SABER = "100100";
        const string MHX_ASSASSIN = "601800";
        const string TOMOE_GOZEN_ARCHER = "202100";
        const string SUMMER_USHI_ASSASSIN = "603400";
        const string KIARA_ALTER_EGO = "1000300";
        const string ABBY_FOREIGNER = "2500100";

        static void Main()
        {
            EquipNiceJson craftEssence = GetCraftEssenceInfo("9400340");
            Console.WriteLine(craftEssence.Name);
            Console.WriteLine(craftEssence.ExtraAssets.Faces.Equip.EquipLink?.Value<string>("9400340"));

            //Console.WriteLine(">>>>>>>> Attacking Servant <<<<<<<<");
            //ServantNiceJson atkServant = PrintRelevantServantInfo(TOMOE_GOZEN_ARCHER);

            //Console.WriteLine(">>>>>>>> Defending Servant <<<<<<<<");
            //ServantNiceJson defServant = PrintRelevantServantInfo(ARTORIA_PENDRAGON_SABER);

            //Console.WriteLine(">>>>>>>> Stats <<<<<<<<");
            //Console.WriteLine($"Attribute Multiplier: {ServantAttribute.GetAttackMultiplier(atkServant.Attribute, defServant.Attribute)}x");
            //Console.WriteLine($"Class Advantage Multiplier: {ClassRelation.GetAttackMultiplier(atkServant.ClassName, defServant.ClassName)}x");

            Console.ReadKey(); // end program
        }

        public static ServantNiceJson GetServantInfo(string servantId)
        {
            return ApiRequest.GetDesearlizeObjectAsync<ServantNiceJson>("https://api.atlasacademy.io/nice/NA/servant/" + servantId + "?lang=en").Result;
        }

        public static EquipNiceJson GetCraftEssenceInfo(string ceId)
        {
            return ApiRequest.GetDesearlizeObjectAsync<EquipNiceJson>("https://api.atlasacademy.io/nice/NA/equip/" + ceId + "?lore=true&lang=en").Result;
        }

        public static ServantNiceJson PrintRelevantServantInfo(string servantId)
        {
            ServantNiceJson servant = GetServantInfo(servantId);
            NoblePhantasm np = servant.NoblePhantasms[^1];

            Console.WriteLine($"Servant Name: {servant.Name}");
            Console.WriteLine($"Attribute: {servant.Attribute}");
            Console.WriteLine($"Class: {servant.ClassName}");
            Console.WriteLine($"NP Name: {np.Name} {np.Rank}");
            Console.WriteLine($"NP Card Type: {np.Card}"); // ToDo: Address Space Ishtar's NP choice
            Console.WriteLine($"Attack max (before grail): {servant.AtkMax}");
            Console.WriteLine($"Health max (before grail): {servant.HpMax}");
            Console.WriteLine($"Attack absolute max: {servant.AtkGrowth[^1]}");
            Console.WriteLine($"Health absolute max: {servant.HpGrowth[^1]}");
            Console.WriteLine();

            return servant;
        }

        public static List<ServantBasicJson> GetListBasicServantInfo()
        {
            return ApiRequest.GetDesearlizeObjectAsync<List<ServantBasicJson>>("https://api.atlasacademy.io/export/NA/basic_servant.json").Result;
        }

        public static void ServantStats(string servantId)
        {
            ServantNiceJson servantJson = GetServantInfo(servantId);
            
            // retry if no result
            if (servantJson == null)
            {
                Console.WriteLine($"\nI wasn't able to get any info on Servant ID {servantId}. Give me 5 seconds to retry...\n\n");
                Thread.Sleep(5000); // give API a breather
                servantJson = ApiRequest.GetDesearlizeObjectAsync<ServantNiceJson>("https://api.atlasacademy.io/nice/NA/servant/" + servantId + "?lang=en").Result;

                // skip to next servant lookup
                if (servantJson == null)
                {
                    Console.WriteLine($"\nUnable to pull info on Servant ID {servantId}. Moving onto next servant...\n\n");
                    return;
                }
            }

            Console.WriteLine($"ID: {servantJson.Id}");
            Console.WriteLine($"Servant Name: {servantJson.Name}");
            Console.WriteLine($"Class Name: {servantJson.ClassName}");
            Console.WriteLine($"Rarity: {servantJson.Rarity}");
            Console.WriteLine($"Cost: {servantJson.Cost}");
            Console.WriteLine($"Gender: {servantJson.Gender}");
            Console.WriteLine($"Attribute: {servantJson.Attribute}");
            Console.WriteLine($"Cards: {string.Join(", ", servantJson.Cards)}");
            Console.WriteLine($"First Ascension 1st Item Name: {servantJson.AscensionMaterials?.FirstAsc?.Items[0].Amount.ToString() ?? "N/A"} {servantJson.AscensionMaterials?.FirstAsc?.Items[0].ItemObject.Name ?? ""}");
            
            if (servantJson.AscensionMaterials?.SecondAsc?.Items.Count > 1)
                Console.WriteLine($"Second Ascension 2nd Item Name: {servantJson.AscensionMaterials?.SecondAsc?.Items[1].Amount.ToString() ?? "N/A"} {servantJson.AscensionMaterials?.SecondAsc?.Items[1].ItemObject.Name ?? ""}");
            else
                Console.WriteLine($"Second Ascension Only Item Name: {servantJson.AscensionMaterials?.SecondAsc?.Items[0].Amount.ToString() ?? "N/A"} {servantJson.AscensionMaterials?.SecondAsc?.Items[0].ItemObject.Name ?? ""}");
            
            Console.WriteLine($"Eigth Skill Level 1st Item Name: {servantJson.SkillMaterials?.EigthSkill?.Items[0].Amount.ToString() ?? "N/A"} {servantJson.SkillMaterials?.EigthSkill?.Items[0].ItemObject.Name ?? ""}");
            
            if (servantJson?.NoblePhantasms.Count > 0)
                Console.WriteLine($"Latest NP Name and Rank: {servantJson?.NoblePhantasms[^1].Name ?? "N/A"} {servantJson?.NoblePhantasms[^1].Rank ?? ""}");
            else
                Console.WriteLine("Latest NP Name and Rank: N/A");

            Console.WriteLine();
        }
    }
}
