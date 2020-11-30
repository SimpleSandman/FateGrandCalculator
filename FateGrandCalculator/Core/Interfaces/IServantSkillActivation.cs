using System.Collections.Generic;

using FateGrandCalculator.Models;

namespace FateGrandCalculator.Core.Interfaces
{
    public interface IServantSkillActivation
    {
        void SkillActivation(PartyMember partyMemberActor, int actorSkillPositionNumber, List<PartyMember> party, int partyMemberPosition, List<EnemyMob> enemies, int enemyPosition);
        void SkillActivation(MysticCode mysticCode, int mysticCodeSkillPositionNumber, List<PartyMember> party, int partyMemberPosition, List<EnemyMob> enemies, int enemyPosition, int reservePartyMemberIndex = -1);
        void AdjustSkillCooldowns(List<PartyMember> party);
        void AdjustSkillCooldowns(MysticCode mysticCode);
    }
}
