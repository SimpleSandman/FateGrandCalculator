﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using FateGrandOrderPOC.Shared;
using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
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
        const string SKADI_CASTER = "503900";
        const string PARACELSUS_CASTER = "501000";

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
                SkillLevels = new int[3] { 10, 10, 10 },
                IsSupportServant = false,
                ServantInfo = AtlasAcademyRequest.GetServantInfo(DANTES_AVENGER)
            };

            PartyMember partyMemberAttacker = AddPartyMember(chaldeaAttackServant, chaldeaKscope);

            // TODO: Get more funcTypes and apply them to party member properties
            partyMemberAttacker.NpCharge = GetCraftEssenceValue(partyMemberAttacker, "gainNp");

            _party.Add(partyMemberAttacker);
            #endregion

            #region Party Member 2
            Servant chaldeaCaster = new Servant
            {
                ServantLevel = 90,
                NpLevel = 1,
                FouHealth = 1000,
                FouAttack = 1000,
                SkillLevels = new int[3] { 10, 10, 10 },
                IsSupportServant = false,
                ServantInfo = AtlasAcademyRequest.GetServantInfo(SKADI_CASTER)
            };

            PartyMember partyMemberCaster = AddPartyMember(chaldeaCaster);

            _party.Add(partyMemberCaster);
            #endregion

            #region Party Member Support
            Servant supportCaster = new Servant
            {
                ServantLevel = 90,
                NpLevel = 1,
                FouHealth = 1000,
                FouAttack = 1000,
                SkillLevels = new int[3] { 10, 10, 10 },
                IsSupportServant = true,
                ServantInfo = AtlasAcademyRequest.GetServantInfo(SKADI_CASTER)
            };

            PartyMember partyMemberSupportCaster = AddPartyMember(supportCaster);

            _party.Add(partyMemberSupportCaster);
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

            /* Simulate node combat */
            // TODO: Specify defense down buffs to specific enemy mobs instead of always assuming AOE.
            //       Possibly add a debuff list property for the EnemyMob object
            NpChargeCheck(partyMemberAttacker);
            Console.WriteLine(">>>>>> Fight 1/3 <<<<<<\n");

            // TODO: Reduce cooldown counters for all front-line party members
            // TODO: Pop any skill cooldowns == 0 out of the list for all front-line party members
            // TODO: Reduce skill effect counter by 1 per turn
            if (!partyMemberCaster.SkillCooldowns.Exists(s => s.SkillId == 3))
            {
                // Do cool stuffs here
            }

            _combatFormula.NoblePhantasmChainSimulator(ref _party, ref enemyMobs, WaveNumberEnum.First, 0.50f, 0.00f, 1.00f, 0.00f);

            BuffPartyMember(partyMemberCaster, 3, partyMemberAttacker);

            NpChargeCheck(partyMemberAttacker);
            Console.WriteLine("\n>>>>>> Fight 2/3 <<<<<<\n");
            _combatFormula.NoblePhantasmChainSimulator(ref _party, ref enemyMobs, WaveNumberEnum.Second, 0.50f, 0.00f, 1.00f, 0.00f);

            BuffPartyMember(partyMemberSupportCaster, 3, partyMemberAttacker);

            NpChargeCheck(partyMemberAttacker);
            Console.WriteLine("\n>>>>>> Fatal 3/3 <<<<<<\n");
            _combatFormula.NoblePhantasmChainSimulator(ref _party, ref enemyMobs, WaveNumberEnum.Third, 0.50f, 1.36f, 1.00f, 0.13f);

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

        private PartyMember AddPartyMember(Servant chaldeaServant, CraftEssence chaldeaCraftEssence = null)
        {
            int servantTotalAtk = chaldeaServant.ServantInfo.AtkGrowth[chaldeaServant.ServantLevel - 1] + chaldeaServant.FouAttack;
            int servantTotalHp = chaldeaServant.ServantInfo.HpGrowth[chaldeaServant.ServantLevel - 1] + chaldeaServant.FouHealth;

            if (chaldeaCraftEssence != null)
            {
                servantTotalAtk += chaldeaCraftEssence.CraftEssenceInfo.AtkGrowth[chaldeaCraftEssence.CraftEssenceLevel - 1];
                servantTotalHp += chaldeaCraftEssence.CraftEssenceInfo.HpGrowth[chaldeaCraftEssence.CraftEssenceLevel - 1];
            }

            return new PartyMember
            {
                Id = _party.Count,
                Servant = chaldeaServant,
                EquippedCraftEssence = chaldeaCraftEssence,
                TotalAttack = servantTotalAtk,
                TotalHealth = servantTotalHp,
                ActiveStatuses = new List<ActiveStatus>(),
                SkillCooldowns = new List<SkillCooldown>(),
                NoblePhantasm = chaldeaServant  // Set NP for party member at start of fight
                    .ServantInfo                // (assume highest upgraded NP by priority)
                    .NoblePhantasms
                    .Aggregate((agg, next) =>
                        next.Priority >= agg.Priority ? next : agg)
            };
        }

        /// <summary>
        /// Buff a party member with the desired skill based on the actor's list of available skills
        /// </summary>
        /// <param name="partyMemberActor">The acting party member that is giving the buff</param>
        /// <param name="actorSkillNumber">Skill position number (left = 1, middle = 2, right = 3)</param>
        /// <param name="partyMemberTarget">The targeted party member that is receiving the buff</param>
        private void BuffPartyMember(PartyMember partyMemberActor, int actorSkillNumber, PartyMember partyMemberTarget)
        {
            if (partyMemberActor.SkillCooldowns.Exists(s => s.SkillId == actorSkillNumber))
            {
                Console.WriteLine($"WARNING: Cannot buff using {partyMemberActor.Servant.ServantInfo.Name}'s {actorSkillNumber} skill because of cooldown!");
                return; // don't buff again
            }

            // Get highest priority skill
            SkillServant skill = partyMemberActor
                .Servant
                .ServantInfo
                .Skills
                .FindAll(s => s.Num == actorSkillNumber)
                .Aggregate((agg, next) =>
                    next.Priority >= agg.Priority ? next : agg);

            List<FunctionServant> servantFunctions = (from f in skill.Functions
                where (f.FuncTargetType == "ptOne"        // party member
                    || f.FuncTargetType == "ptAll"        // party
                    || f.FuncTargetType == "ptFull"       // party (including reserve)
                    || f.FuncTargetType == "ptOther"      // party except self
                    || f.FuncTargetType == "ptOtherFull"  // party except self (including reserve)
                    || f.FuncTargetType == "ptselectSub") // reserve party member
                    && f.FuncTargetTeam != "enemy"
                select f).ToList();

            if (servantFunctions == null || servantFunctions.Count == 0)
            {
                return; // didn't find any buffs that apply to other members
            }

            int currentSkillLevel = partyMemberActor.Servant.SkillLevels[actorSkillNumber - 1];

            string support = "";
            if (partyMemberActor.Servant.IsSupportServant)
            {
                support = "(Support) ";
            }

            // TODO: Add more buffs here
            foreach (FunctionServant servantFunction in servantFunctions)
            {
                switch (servantFunction.FuncType)
                {
                    case "gainNp":
                        partyMemberTarget.NpCharge += servantFunction.Svals[currentSkillLevel - 1].Value / 100.0f;
                        Console.WriteLine($"{partyMemberActor.Servant.ServantInfo.Name} {support}" + "has buffed " +
                            $"{partyMemberTarget.Servant.ServantInfo.Name}'s NP charge by " +
                            $"{servantFunction.Svals[currentSkillLevel - 1].Value / 100.0f}% and is now at {partyMemberTarget.NpCharge}%");
                        break;
                    default:
                        break;
                }
            }

            partyMemberActor.SkillCooldowns.Add(new SkillCooldown 
            { 
                SkillId = actorSkillNumber, 
                Cooldown = skill.Cooldown[currentSkillLevel - 1] 
            });
        }

        private float GetCraftEssenceValue(PartyMember partyMember, string funcType) 
        {
            if (partyMember.EquippedCraftEssence == null)
            {
                throw new NotSupportedException();
            }

            List<Skill> skills = new List<Skill>();

            if (!partyMember.EquippedCraftEssence.Mlb)
            {
                skills = partyMember.EquippedCraftEssence.CraftEssenceInfo.Skills.FindAll(s => s.Priority == 1);
            }
            else 
            {
                skills = partyMember.EquippedCraftEssence.CraftEssenceInfo.Skills.FindAll(s => s.Priority == 2);
            }

            Function function = new Function();

            foreach (Skill skill in skills) 
            {
                function = skill.Functions.Find(n => n.FuncType == funcType);
                if (function != null)
                {
                    break;
                }
            }

            return (float)function.Svals[0].Value / 100.0f;
        }
        #endregion
    }
}
