using System;
using System.Collections.Generic;
using System.Linq;

using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Enums;
using FateGrandCalculator.Models;

namespace FateGrandCalculator.Core.Management
{
    public class MaterialCalculation
    {
        private ServantNiceJson _currentServantNiceJson;
        private RequiredItemMaterials _requiredItemMaterials;

        public MaterialCalculation() { }

        public RequiredItemMaterials HowMuchIsNeeded(ChaldeaServant currentServant, ChaldeaServant goalServant, 
            GrailCostNiceJson grailCostNiceJson, ServantNiceJson currentServantNiceJson)
        {
            // Validate the two servant objects are the same servant
            if (currentServant.ServantBasicInfo != goalServant.ServantBasicInfo)
            {
                return null;
            }

            // Set up
            _requiredItemMaterials = new RequiredItemMaterials();

            // Allows nice JSON info to be saved prior, if provided
            if (currentServantNiceJson != null && currentServantNiceJson.Id == currentServant.ServantBasicInfo.Id)
            {
                _currentServantNiceJson = currentServantNiceJson;
            }
            else
            {
                return _requiredItemMaterials;
            }

            /* Calculate ascension materials, QP, and/or grails */
            if (currentServant.ServantLevel < goalServant.ServantLevel)
            {
                AscensionLevel[] allAscensionLevels = AscensionLevels(currentServant.ServantBasicInfo.Rarity);

                // Find the required ascension levels with removing ascensions above the goal
                List<AscensionLevel> requiredAscensionLevels = allAscensionLevels
                    .Where(i => currentServant.ServantLevel < i.LevelCap)
                    .ToList();

                requiredAscensionLevels.RemoveAll(c => goalServant.ServantLevel < c.LevelCap);

                // Ascension materials
                if (requiredAscensionLevels.Count > 0)
                {
                    foreach (AscensionLevel ascensionLevel in requiredAscensionLevels)
                    {
                        ItemMaterials ascensionItemMaterials = AscensionItemMaterials(ascensionLevel);
                        if (ascensionItemMaterials == null)
                        {
                            continue;
                        }

                        _requiredItemMaterials.Qp += ascensionItemMaterials.Qp;
                        AddItemMaterials(ascensionItemMaterials.Items);
                    }
                }
                
                // Grails
                if (goalServant.ServantLevel > allAscensionLevels[^1].LevelCap)
                {
                    IEnumerable<GrailInfo> grailInfoList = GetGrailRarityInfo(goalServant.ServantBasicInfo.Rarity, grailCostNiceJson)
                        .Where(g => g.AddLevelMax + currentServant.ServantLevel <= goalServant.ServantLevel);

                    foreach (GrailInfo grailInfo in grailInfoList)
                    {
                        _requiredItemMaterials.Qp += grailInfo.Qp;
                        _requiredItemMaterials.GrailCount++;
                    }
                }

                // Overall ember count
                CalculateEmbersNeeded(currentServant, goalServant, _currentServantNiceJson.ExpGrowth);
            }

            /* Calculate skill materials and QP */
            for (int i = 0; i < 3; i++)
            {
                int currentSkillLevel = currentServant.SkillLevels[i];
                int goalSkillLevel = goalServant.SkillLevels[i];

                if (currentSkillLevel < goalSkillLevel)
                {
                    for (int j = currentSkillLevel; j < goalSkillLevel; j++)
                    {
                        switch (j)
                        {
                            case 1:
                                AddItemMaterials(_currentServantNiceJson.SkillMaterials.FirstSkill.Items);
                                _requiredItemMaterials.Qp += _currentServantNiceJson.SkillMaterials.FirstSkill.Qp;
                                break;
                            case 2:
                                AddItemMaterials(_currentServantNiceJson.SkillMaterials.SecondSkill.Items);
                                _requiredItemMaterials.Qp += _currentServantNiceJson.SkillMaterials.SecondSkill.Qp;
                                break;
                            case 3:
                                AddItemMaterials(_currentServantNiceJson.SkillMaterials.ThirdSkill.Items);
                                _requiredItemMaterials.Qp += _currentServantNiceJson.SkillMaterials.ThirdSkill.Qp;
                                break;
                            case 4:
                                AddItemMaterials(_currentServantNiceJson.SkillMaterials.FourthSkill.Items);
                                _requiredItemMaterials.Qp += _currentServantNiceJson.SkillMaterials.FourthSkill.Qp;
                                break;
                            case 5:
                                AddItemMaterials(_currentServantNiceJson.SkillMaterials.FifthSkill.Items);
                                _requiredItemMaterials.Qp += _currentServantNiceJson.SkillMaterials.FifthSkill.Qp;
                                break;
                            case 6:
                                AddItemMaterials(_currentServantNiceJson.SkillMaterials.SixthSkill.Items);
                                _requiredItemMaterials.Qp += _currentServantNiceJson.SkillMaterials.SixthSkill.Qp;
                                break;
                            case 7:
                                AddItemMaterials(_currentServantNiceJson.SkillMaterials.SeventhSkill.Items);
                                _requiredItemMaterials.Qp += _currentServantNiceJson.SkillMaterials.SeventhSkill.Qp;
                                break;
                            case 8:
                                AddItemMaterials(_currentServantNiceJson.SkillMaterials.EighthSkill.Items);
                                _requiredItemMaterials.Qp += _currentServantNiceJson.SkillMaterials.EighthSkill.Qp;
                                break;
                            case 9:
                                AddItemMaterials(_currentServantNiceJson.SkillMaterials.NinthSkill.Items);
                                _requiredItemMaterials.Qp += _currentServantNiceJson.SkillMaterials.NinthSkill.Qp;
                                break;
                        }
                    }
                }
            }

            return _requiredItemMaterials;
        }

        public Dictionary<string, int> GroupItemParents(List<ItemParent> itemParents)
        {
            Dictionary<string, int> friendlyList = new Dictionary<string, int>();

            foreach (IGrouping<int, ItemParent> ids in itemParents.GroupBy(i => i.ItemObject.Id))
            {
                friendlyList.Add(ids.First().ItemObject.Name, ids.Sum(i => i.Amount));
            }

            return friendlyList;
        }

        #region Private Method
        private void CalculateEmbersNeeded(ChaldeaServant currentServant, ChaldeaServant goalServant, List<int> expGrowth)
        {
            // Reference: https://gamepress.gg/grandorder/exp-calculator
            // Reference: https://fategrandorder.fandom.com/wiki/Leveling
            const float FOUR_STAR_EMBER = 27000.0f;
            const float FOUR_STAR_EMBER_CLASS_BONUS = 32400.0f;
            const float FIVE_STAR_EMBER = 81000.0f;
            const float FIVE_STAR_EMBER_CLASS_BONUS = 97200.0f;
            int expNeeded = expGrowth[goalServant.ServantLevel - 1] - expGrowth[currentServant.ServantLevel - 1];

            _requiredItemMaterials.FourStarEmber += (int)Math.Ceiling(expNeeded / FOUR_STAR_EMBER);
            _requiredItemMaterials.FourStarEmberClassBonus += (int)Math.Ceiling(expNeeded / FOUR_STAR_EMBER_CLASS_BONUS);
            _requiredItemMaterials.FiveStarEmber += (int)Math.Ceiling(expNeeded / FIVE_STAR_EMBER);
            _requiredItemMaterials.FiveStarEmberClassBonus += (int)Math.Ceiling(expNeeded / FIVE_STAR_EMBER_CLASS_BONUS);
        }

        private ItemMaterials AscensionItemMaterials(AscensionLevel ascensionLevel)
        {
            if (ascensionLevel.Ascension == AscensionEnum.Default)
            {
                return _currentServantNiceJson.AscensionMaterials.FirstAsc;
            }
            else if (ascensionLevel.Ascension == AscensionEnum.First)
            {
                return _currentServantNiceJson.AscensionMaterials.SecondAsc;
            }
            else if (ascensionLevel.Ascension == AscensionEnum.Second)
            {
                return _currentServantNiceJson.AscensionMaterials.ThirdAsc;
            }
            else if (ascensionLevel.Ascension == AscensionEnum.Third)
            {
                return _currentServantNiceJson.AscensionMaterials.FourthAsc;
            }

            return null;
        }

        private void AddItemMaterials(List<ItemParent> items)
        {
            foreach (ItemParent item in items)
            {
                _requiredItemMaterials.Items.Add(item);
            }
        }

        private List<GrailInfo> GetGrailRarityInfo(int rarity, GrailCostNiceJson grailCost)
        {
            List<GrailInfo> grailInfoList = new List<GrailInfo>();

            if (rarity == 0)
            {
                grailInfoList.Add(grailCost.ZeroRarity.FirstGrail);
                grailInfoList.Add(grailCost.ZeroRarity.SecondGrail);
                grailInfoList.Add(grailCost.ZeroRarity.ThirdGrail);
                grailInfoList.Add(grailCost.ZeroRarity.FourthGrail);
                grailInfoList.Add(grailCost.ZeroRarity.FifthGrail);
                grailInfoList.Add(grailCost.ZeroRarity.SixthGrail);
                grailInfoList.Add(grailCost.ZeroRarity.SeventhGrail);
                grailInfoList.Add(grailCost.ZeroRarity.EighthGrail);
                grailInfoList.Add(grailCost.ZeroRarity.NinthGrail);
                grailInfoList.Add(grailCost.ZeroRarity.TenthGrail);
            }
            else if (rarity == 1)
            {
                grailInfoList.Add(grailCost.OneRarity.FirstGrail);
                grailInfoList.Add(grailCost.OneRarity.SecondGrail);
                grailInfoList.Add(grailCost.OneRarity.ThirdGrail);
                grailInfoList.Add(grailCost.OneRarity.FourthGrail);
                grailInfoList.Add(grailCost.OneRarity.FifthGrail);
                grailInfoList.Add(grailCost.OneRarity.SixthGrail);
                grailInfoList.Add(grailCost.OneRarity.SeventhGrail);
                grailInfoList.Add(grailCost.OneRarity.EighthGrail);
                grailInfoList.Add(grailCost.OneRarity.NinthGrail);
                grailInfoList.Add(grailCost.OneRarity.TenthGrail);
            }
            else if (rarity == 2)
            {
                grailInfoList.Add(grailCost.TwoRarity.FirstGrail);
                grailInfoList.Add(grailCost.TwoRarity.SecondGrail);
                grailInfoList.Add(grailCost.TwoRarity.ThirdGrail);
                grailInfoList.Add(grailCost.TwoRarity.FourthGrail);
                grailInfoList.Add(grailCost.TwoRarity.FifthGrail);
                grailInfoList.Add(grailCost.TwoRarity.SixthGrail);
                grailInfoList.Add(grailCost.TwoRarity.SeventhGrail);
                grailInfoList.Add(grailCost.TwoRarity.EighthGrail);
                grailInfoList.Add(grailCost.TwoRarity.NinthGrail);
                grailInfoList.Add(grailCost.TwoRarity.TenthGrail);
            }
            else if (rarity == 3)
            {
                grailInfoList.Add(grailCost.ThreeRarity.FirstGrail);
                grailInfoList.Add(grailCost.ThreeRarity.SecondGrail);
                grailInfoList.Add(grailCost.ThreeRarity.ThirdGrail);
                grailInfoList.Add(grailCost.ThreeRarity.FourthGrail);
                grailInfoList.Add(grailCost.ThreeRarity.FifthGrail);
                grailInfoList.Add(grailCost.ThreeRarity.SixthGrail);
                grailInfoList.Add(grailCost.ThreeRarity.SeventhGrail);
                grailInfoList.Add(grailCost.ThreeRarity.EighthGrail);
                grailInfoList.Add(grailCost.ThreeRarity.NinthGrail);
            }
            else if (rarity == 4)
            {
                grailInfoList.Add(grailCost.FourRarity.FirstGrail);
                grailInfoList.Add(grailCost.FourRarity.SecondGrail);
                grailInfoList.Add(grailCost.FourRarity.ThirdGrail);
                grailInfoList.Add(grailCost.FourRarity.FourthGrail);
                grailInfoList.Add(grailCost.FourRarity.FifthGrail);
                grailInfoList.Add(grailCost.FourRarity.SixthGrail);
                grailInfoList.Add(grailCost.FourRarity.SeventhGrail);
            }
            else if (rarity == 5)
            {
                grailInfoList.Add(grailCost.FiveRarity.FirstGrail);
                grailInfoList.Add(grailCost.FiveRarity.SecondGrail);
                grailInfoList.Add(grailCost.FiveRarity.ThirdGrail);
                grailInfoList.Add(grailCost.FiveRarity.FourthGrail);
                grailInfoList.Add(grailCost.FiveRarity.FifthGrail);
            }

            return grailInfoList;
        }

        private AscensionLevel[] AscensionLevels(int rarity)
        {
            // Reference: https://fategrandorder.fandom.com/wiki/Ascension
            if (rarity == 0)
            {
                return new AscensionLevel[]
                {
                    new AscensionLevel { LevelCap = 25, Ascension = AscensionEnum.Default },
                    new AscensionLevel { LevelCap = 35, Ascension = AscensionEnum.First },
                    new AscensionLevel { LevelCap = 45, Ascension = AscensionEnum.Second },
                    new AscensionLevel { LevelCap = 55, Ascension = AscensionEnum.Third },
                    new AscensionLevel { LevelCap = 65, Ascension = AscensionEnum.Fourth }
                };
            }
            else if (rarity == 1)
            {
                return new AscensionLevel[]
                {
                    new AscensionLevel { LevelCap = 20, Ascension = AscensionEnum.Default },
                    new AscensionLevel { LevelCap = 30, Ascension = AscensionEnum.First },
                    new AscensionLevel { LevelCap = 40, Ascension = AscensionEnum.Second },
                    new AscensionLevel { LevelCap = 50, Ascension = AscensionEnum.Third },
                    new AscensionLevel { LevelCap = 60, Ascension = AscensionEnum.Fourth }
                };
            }
            else if (rarity == 2)
            {
                return new AscensionLevel[]
                {
                    new AscensionLevel { LevelCap = 25, Ascension = AscensionEnum.Default },
                    new AscensionLevel { LevelCap = 35, Ascension = AscensionEnum.First },
                    new AscensionLevel { LevelCap = 45, Ascension = AscensionEnum.Second },
                    new AscensionLevel { LevelCap = 55, Ascension = AscensionEnum.Third },
                    new AscensionLevel { LevelCap = 65, Ascension = AscensionEnum.Fourth }
                };
            }
            else if (rarity == 3)
            {
                return new AscensionLevel[]
                {
                    new AscensionLevel { LevelCap = 30, Ascension = AscensionEnum.Default },
                    new AscensionLevel { LevelCap = 40, Ascension = AscensionEnum.First },
                    new AscensionLevel { LevelCap = 50, Ascension = AscensionEnum.Second },
                    new AscensionLevel { LevelCap = 60, Ascension = AscensionEnum.Third },
                    new AscensionLevel { LevelCap = 70, Ascension = AscensionEnum.Fourth }
                };
            }
            else if (rarity == 4)
            {
                return new AscensionLevel[]
                {
                    new AscensionLevel { LevelCap = 40, Ascension = AscensionEnum.Default },
                    new AscensionLevel { LevelCap = 50, Ascension = AscensionEnum.First },
                    new AscensionLevel { LevelCap = 60, Ascension = AscensionEnum.Second },
                    new AscensionLevel { LevelCap = 70, Ascension = AscensionEnum.Third },
                    new AscensionLevel { LevelCap = 80, Ascension = AscensionEnum.Fourth }
                };
            }
            else if (rarity == 5)
            {
                return new AscensionLevel[]
                {
                    new AscensionLevel { LevelCap = 50, Ascension = AscensionEnum.Default },
                    new AscensionLevel { LevelCap = 60, Ascension = AscensionEnum.First },
                    new AscensionLevel { LevelCap = 70, Ascension = AscensionEnum.Second },
                    new AscensionLevel { LevelCap = 80, Ascension = AscensionEnum.Third },
                    new AscensionLevel { LevelCap = 90, Ascension = AscensionEnum.Fourth }
                };
            }

            return new AscensionLevel[]
            {
                new AscensionLevel { LevelCap = 0, Ascension = AscensionEnum.Default },
                new AscensionLevel { LevelCap = 0, Ascension = AscensionEnum.First },
                new AscensionLevel { LevelCap = 0, Ascension = AscensionEnum.Second },
                new AscensionLevel { LevelCap = 0, Ascension = AscensionEnum.Third },
                new AscensionLevel { LevelCap = 0, Ascension = AscensionEnum.Fourth }
            };
        }
        #endregion
    }
}
