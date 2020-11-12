using System.Collections.Generic;

using FateGrandOrderPOC.Shared.Models;

namespace FateGrandOrderPOC.Shared
{
    public interface IServantSkillActivation
    {
        void SkillActivation(PartyMember partyMemberActor, int actorSkillPositionNumber, List<PartyMember> party, int partyMemberPosition, List<EnemyMob> enemies, int enemyPosition);
        void SkillActivation(MysticCode mysticCode, int mysticCodeSkillPositionNumber, List<PartyMember> party, int partyMemberPosition, List<EnemyMob> enemies, int enemyPosition);
        List<PartyMember> AdjustSkillCooldowns(List<PartyMember> party);
        MysticCode AdjustSkillCooldowns(MysticCode mysticCode);
    }
}
