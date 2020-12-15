using System.Collections.Generic;

using FateGrandCalculator.Models;

namespace FateGrandCalculator.Core.Interfaces
{
    public interface IServantSkillActivation
    {
        void SkillActivation(PartyMember partyMemberActor, int actorSkillPositionNumber, List<PartyMember> party, List<EnemyMob> enemies, int partyMemberPosition = 3, int enemyPosition = 3);
        void SkillActivation(MysticCode mysticCode, int mysticCodeSkillPositionNumber, List<PartyMember> party, List<EnemyMob> enemies, int partyMemberPosition = 3, int enemyPosition = 3, int reservePartyMemberIndex = -1);
        void AdjustSkillCooldowns(List<PartyMember> party);
        void AdjustSkillCooldowns(MysticCode mysticCode);
    }
}
