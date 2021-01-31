using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FateGrandCalculator.AtlasAcademy.Interfaces;
using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Enums;
using FateGrandCalculator.Models;

namespace FateGrandCalculator.Core.Management
{
    public class MaterialCalculation
    {
        private readonly IAtlasAcademyClient _aaClient;
        private ServantNiceJson _currentServantNiceJson;
        private RequiredItemMaterials _requiredItemMaterials;

        public MaterialCalculation(IAtlasAcademyClient aaClient)
        {
            _aaClient = aaClient;
        }

        public async Task<RequiredItemMaterials> HowMuchIsNeededAsync(ChaldeaServant currentServant, ChaldeaServant goalServant, 
            ConstantExportJson constantExportJson, ServantNiceJson currentServantNiceJson = null)
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

            /* Calculate ascension materials, QP, and/or grails */
            if (currentServant.ServantLevel < goalServant.ServantLevel)
            {
                AscensionLevel[] allAscensionLevels = AscensionLevels(currentServant.ServantBasicInfo.Rarity);

                IEnumerable<AscensionLevel> ascensionLevels = allAscensionLevels
                    .Where(i => i.LevelCap > currentServant.ServantLevel)
                    .Where(i => goalServant.ServantLevel <= i.LevelCap);

                await GetCurrentServantNiceInfoAsync(currentServant.ServantBasicInfo.Id.ToString());

                // Ascension materials
                if (ascensionLevels.Count() > 0)
                {
                    foreach (AscensionLevel ascensionLevel in ascensionLevels)
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
                    IEnumerable<GrailInfo> grailInfoList = GetGrailRarityInfo(goalServant.ServantBasicInfo.Rarity, constantExportJson.GrailCostNiceJson)
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
            for (int i = 0; i < 2; i++)
            {
                if (currentServant.SkillLevels[i] < goalServant.SkillLevels[i])
                {
                    await GetCurrentServantNiceInfoAsync(currentServant.ServantBasicInfo.Id.ToString());


                }
            }

            return _requiredItemMaterials;
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

        private async Task GetCurrentServantNiceInfoAsync(string servantId)
        {
            // Ensures only one API call is needed to get the necessary info
            // no matter how many times this method is called
            if (_currentServantNiceJson == null)
            {
                _currentServantNiceJson = await _aaClient.GetServantInfo(servantId);
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
