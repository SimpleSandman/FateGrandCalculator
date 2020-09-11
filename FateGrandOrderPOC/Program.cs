using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using FateGrandOrderPOC.Shared;
using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.Enums;
using FateGrandOrderPOC.Shared.Models;

namespace FateGrandOrderPOC
{
    public class Program
    {
        private readonly CombatFormula _combatFormula = new CombatFormula();
        private List<PartyMember> _party = new List<PartyMember>();

        /* Servants */
        const string ARTORIA_PENDRAGON_SABER = "100100";
        const string MHX_ASSASSIN = "601800";
        const string TOMOE_GOZEN_ARCHER = "202100";
        const string SUMMER_USHI_ASSASSIN = "603400";
        const string KIARA_ALTER_EGO = "1000300";
        const string ABBY_FOREIGNER = "2500100";
        const string MHXX_FOREIGNER = "2500300";
        const string LANCELOT_BERSERKER = "700200";
        const string DANTES_AVENGER = "1100200";

        /* Craft Essences */
        const string KSCOPE_CE = "9400340";
        const string AERIAL_DRIVE_CE = "9402750";
        const string BLACK_GRAIL_CE = "9400480";

        static void Main()
        {
            Program program = new Program();
            program.Calculations();
        }

        /// <summary>
        /// Main calculations
        /// </summary>
        private void Calculations()
        {
            /* Party data */
            #region Party Member
            CraftEssence chaldeaKscope = new CraftEssence
            {
                CraftEssenceLevel = 100,
                Mlb = true,
                CraftEssenceInfo = AtlasAcademyRequest.GetCraftEssenceInfo(KSCOPE_CE)
            };

            Servant chaldeaAttackServant = new Servant
            {
                ServantLevel = 90,
                NpLevel = 1,
                FouHealth = 1000,
                FouAttack = 1000,
                SkillLevel1 = 10,
                SkillLevel2 = 10,
                SkillLevel3 = 10,
                IsSupportServant = false,
                ServantInfo = AtlasAcademyRequest.GetServantInfo(DANTES_AVENGER)
            };

            PartyMember partyMember = AddPartyMember(chaldeaAttackServant, chaldeaKscope);

            _party.Add(partyMember);
            #endregion

            /* Enemy node data */
            #region First Wave
            EnemyMob enemyMob1 = new EnemyMob
            {
                Name = "Walkure",
                ClassName = ClassRelationEnum.Rider,
                AttributeName = AttributeRelationEnum.Sky,
                Gender = GenderRelationEnum.Female,
                WaveNumber = WaveNumberEnum.First,
                Health = 13933.0f,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Divine", "Humanoid", "Female"
                }
            };

            EnemyMob enemyMob2 = new EnemyMob
            {
                Name = "Walkure",
                ClassName = ClassRelationEnum.Rider,
                AttributeName = AttributeRelationEnum.Sky,
                Gender = GenderRelationEnum.Female,
                WaveNumber = WaveNumberEnum.First,
                Health = 14786.0f,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Divine", "Humanoid", "Female"
                }
            };

            EnemyMob enemyMob3 = new EnemyMob
            {
                Name = "Muspell",
                ClassName = ClassRelationEnum.Berserker,
                AttributeName = AttributeRelationEnum.Earth,
                Gender = GenderRelationEnum.Male,
                WaveNumber = WaveNumberEnum.First,
                Health = 23456.0f,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Giant", "Humanoid", "Male", "Super Large"
                }
            };
            #endregion

            #region Second Wave
            EnemyMob enemyMob4 = new EnemyMob
            {
                Name = "Muspell",
                ClassName = ClassRelationEnum.Berserker,
                AttributeName = AttributeRelationEnum.Earth,
                Gender = GenderRelationEnum.Male,
                WaveNumber = WaveNumberEnum.Second,
                Health = 25554.0f,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Giant", "Humanoid", "Male", "Super Large"
                }
            };

            EnemyMob enemyMob5 = new EnemyMob
            {
                Name = "Walkure",
                ClassName = ClassRelationEnum.Rider,
                AttributeName = AttributeRelationEnum.Sky,
                Gender = GenderRelationEnum.Female,
                WaveNumber = WaveNumberEnum.Second,
                Health = 19047.0f,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Divine", "Humanoid", "Female"
                }
            };

            EnemyMob enemyMob6 = new EnemyMob
            {
                Name = "Muspell",
                ClassName = ClassRelationEnum.Berserker,
                AttributeName = AttributeRelationEnum.Earth,
                Gender = GenderRelationEnum.Male,
                WaveNumber = WaveNumberEnum.Second,
                Health = 26204.0f,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Giant", "Humanoid", "Male", "Super Large"
                }
            };
            #endregion

            #region Third Wave
            EnemyMob enemyMob7 = new EnemyMob
            {
                Name = "Walkure",
                ClassName = ClassRelationEnum.Rider,
                AttributeName = AttributeRelationEnum.Sky,
                Gender = GenderRelationEnum.Female,
                WaveNumber = WaveNumberEnum.Third,
                Health = 42926.0f,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Divine", "Humanoid", "Female"
                }
            };

            EnemyMob enemyMob8 = new EnemyMob
            {
                Name = "Walkure",
                ClassName = ClassRelationEnum.Rider,
                AttributeName = AttributeRelationEnum.Sky,
                Gender = GenderRelationEnum.Female,
                WaveNumber = WaveNumberEnum.Third,
                Health = 180753.0f,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Divine", "Humanoid", "Female"
                }
            };

            EnemyMob enemyMob9 = new EnemyMob
            {
                Name = "Muspell",
                ClassName = ClassRelationEnum.Berserker,
                AttributeName = AttributeRelationEnum.Earth,
                Gender = GenderRelationEnum.Male,
                WaveNumber = WaveNumberEnum.Third,
                Health = 61289.0f,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Giant", "Humanoid", "Male", "Super Large"
                }
            };
            #endregion

            List<EnemyMob> enemyMobs = new List<EnemyMob> 
            { 
                enemyMob1, enemyMob2, enemyMob3, 
                enemyMob4, enemyMob5, enemyMob6,
                enemyMob7, enemyMob8, enemyMob9
            };

            // Set NP for party member at start of fight (assume highest upgraded NP by priority)
            partyMember.NoblePhantasm = partyMember
                .Servant
                .ServantInfo
                .NoblePhantasms
                .Aggregate((agg, next) =>
                    next.Priority >= agg.Priority ? next : agg);

            partyMember.NpCharge = 100.0f; // ToDo: Make this based on skills/CEs/etc

            /* Simulate node combat */
            // TODO: Specify defense down buffs to specific enemy mobs instead of always assuming AOE.
            //       Possibly add a debuff list property for the EnemyMob object
            NpChargeCheck(partyMember);
            Console.WriteLine(">>>>>> Fight 1/3 <<<<<<\n");
            _combatFormula.NoblePhantasmSimulator(ref _party, ref enemyMobs, WaveNumberEnum.First, 0.50f, 0.00f, 1.00f, 0.00f);

            partyMember.NpCharge += 50.0f; // ToDo: Make this based on skills/CEs/etc

            NpChargeCheck(partyMember);
            Console.WriteLine("\n>>>>>> Fight 2/3 <<<<<<\n");
            _combatFormula.NoblePhantasmSimulator(ref _party, ref enemyMobs, WaveNumberEnum.Second, 0.50f, 0.00f, 1.00f, 0.00f);

            partyMember.NpCharge += 50.0f; // ToDo: Make this based on skills/CEs/etc

            NpChargeCheck(partyMember);
            Console.WriteLine("\n>>>>>> Fatal 3/3 <<<<<<\n");
            _combatFormula.NoblePhantasmSimulator(ref _party, ref enemyMobs, WaveNumberEnum.Third, 0.50f, 1.36f, 1.00f, 0.13f);

            Console.WriteLine("Battle ended! ^.^");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.Write("Press any key to exit...");
                Console.ReadKey(); // end program
            }
        }

        #region Private Methods
        /// <summary>
        /// Check if party member has enough NP charge for an attack. If so, add them to the queue
        /// </summary>
        /// <param name="partyMember"></param>
        private void NpChargeCheck(PartyMember partyMember)
        {
            if (partyMember.NpCharge < 100.0f)
            {
                Console.WriteLine($"{partyMember.Servant.ServantInfo.Name} only has {partyMember.NpCharge}% charge");
            }
            else
            {
                if (!_party.Exists(p => p.NpChainOrder == NpChainOrderEnum.First))
                {
                    partyMember.NpChainOrder = NpChainOrderEnum.First;
                }
                else if (!_party.Exists(p => p.NpChainOrder == NpChainOrderEnum.Second))
                {
                    partyMember.NpChainOrder = NpChainOrderEnum.Second;
                }
                else if (!_party.Exists(p => p.NpChainOrder == NpChainOrderEnum.Third))
                {
                    partyMember.NpChainOrder = NpChainOrderEnum.Third;
                }
                else
                {
                    Console.WriteLine("Error: Max NP chain limit is 3");
                    return;
                }
            }
        }

        private PartyMember AddPartyMember(Servant chaldeaServant, CraftEssence chaldeaCraftEssence)
        {
            int servantTotalAtk = chaldeaServant.ServantInfo.AtkGrowth[chaldeaServant.ServantLevel - 1]
                + chaldeaCraftEssence.CraftEssenceInfo.AtkGrowth[chaldeaCraftEssence.CraftEssenceLevel - 1]
                + chaldeaServant.FouAttack;

            int servantTotalHp = chaldeaServant.ServantInfo.HpGrowth[chaldeaServant.ServantLevel - 1]
                + chaldeaCraftEssence.CraftEssenceInfo.HpGrowth[chaldeaCraftEssence.CraftEssenceLevel - 1]
                + chaldeaServant.FouHealth;

            return new PartyMember
            {
                Id = _party.Count,
                Servant = chaldeaServant,
                EquippedCraftEssence = chaldeaCraftEssence,
                TotalAttack = servantTotalAtk,
                TotalHealth = servantTotalHp
            };
        }
        #endregion
    }
}
