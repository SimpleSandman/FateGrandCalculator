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
            ServantNiceJson currentServantNiceJson = null)
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

            // Calculate ascension materials, QP, and/or grails
            if (currentServant.ServantLevel < goalServant.ServantLevel)
            {
                AscensionLevel[] allAscensionLevels = AscensionLevels(currentServant.ServantBasicInfo.Rarity);

                IEnumerable<AscensionLevel> ascensionLevels = allAscensionLevels
                    .Where(i => i.LevelCap > currentServant.ServantLevel)
                    .Where(i => goalServant.ServantLevel <= i.LevelCap);

                await GetCurrentServantNiceInfoAsync(currentServant.ServantBasicInfo.Id.ToString());

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
                
                if (goalServant.ServantLevel > allAscensionLevels[^1].LevelCap)
                {
                    // TODO: Calculate the grails, QP, and embers
                }

                CalculateEmbersNeeded(currentServant, goalServant, _currentServantNiceJson.ExpGrowth);
            }

            // Calculate skill materials and QP
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
