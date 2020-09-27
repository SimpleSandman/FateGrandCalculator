using System;
using System.Collections.Generic;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;

namespace FateGrandOrderPOC.Shared.AtlasAcademy
{
    public interface IAtlasAcademyClient
    {
        public ServantNiceJson GetServantInfo(string servantId);

        public EquipNiceJson GetCraftEssenceInfo(string ceId);

        public ClassAttackRateNiceJson GetClassAttackRateInfo();

        public ConstantNiceJson GetConstantGameInfo();
        
        public List<ServantBasicJson> GetListBasicServantInfo();
    }
}
