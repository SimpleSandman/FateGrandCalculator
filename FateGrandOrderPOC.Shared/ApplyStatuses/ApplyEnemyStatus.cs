using System.Collections.Generic;
using System.Linq;

using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.Models;

namespace FateGrandOrderPOC.Shared.ApplyStatuses
{
    public static class ApplyEnemyStatus
    {
        public static void ApplyFuncTargetType(string funcTargetType, int enemyPosition, FunctionServant servantFunction, int currentSkillLevel, 
            List<EnemyMob> enemies)
        {
            switch (funcTargetType)
            {
                case "enemyAll":         // enemies
                case "enemyOther":       // other enemies besides target
                    ApplyStatus(servantFunction, currentSkillLevel, enemies.Take(3).ToList());
                    break;
                case "enemyFull":        // enemies (including reserve)
                case "enemyOtherFull":   // other enemies (including reserve)
                    ApplyStatus(servantFunction, currentSkillLevel, enemies);
                    break;
                case "enemy":            // one enemy
                    ApplyStatus(servantFunction, currentSkillLevel, enemies[enemyPosition - 1]);
                    break;
                default:
                    break;
            }
        }

        public static void ApplyFuncTargetType(string funcTargetType, int enemyPosition, MysticCode mysticCode, FunctionServant mysticCodeFunction,
            List<EnemyMob> enemies)
        {
            switch (funcTargetType)
            {
                case "enemyAll":         // enemies
                case "enemyOther":       // other enemies besides target
                    ApplyStatus(mysticCode, mysticCodeFunction, enemies.Take(3).ToList());
                    break;
                case "enemyFull":        // enemies (including reserve)
                case "enemyOtherFull":   // other enemies (including reserve)
                    ApplyStatus(mysticCode, mysticCodeFunction, enemies);
                    break;
                case "enemy":            // one enemy
                    ApplyStatus(mysticCode, mysticCodeFunction, enemies[enemyPosition - 1]);
                    break;
                default:
                    break;
            }
        }

        #region Private Methods "Party Member"
        private static List<EnemyMob> ApplyStatus(FunctionServant servantFunction, int currentSkillLevel, List<EnemyMob> enemies)
        {
            foreach (EnemyMob enemy in enemies)
            {
                ApplyPartyMemberStatus(servantFunction, currentSkillLevel, enemy);
            }

            return enemies;
        }

        private static EnemyMob ApplyStatus(FunctionServant servantFunction, int currentSkillLevel, EnemyMob enemy)
        {
            return ApplyPartyMemberStatus(servantFunction, currentSkillLevel, enemy);
        }

        private static EnemyMob ApplyPartyMemberStatus(FunctionServant servantFunction, int currentSkillLevel, EnemyMob enemy)
        {
            enemy.ActiveStatuses.Add(new ActiveStatus
            {
                StatusEffect = servantFunction,
                AppliedSkillLevel = currentSkillLevel,
                ActiveTurnCount = servantFunction.Svals[currentSkillLevel - 1].Turn
            });
            
            return enemy;
        }
        #endregion

        #region Private Methods "Mystic Code"
        private static List<EnemyMob> ApplyStatus(MysticCode mysticCode, FunctionServant mysticCodeFunction, List<EnemyMob> enemies)
        {
            foreach (EnemyMob enemy in enemies)
            {
                ApplyPartyMemberStatus(mysticCode, mysticCodeFunction, enemy);
            }

            return enemies;
        }

        private static EnemyMob ApplyStatus(MysticCode mysticCode, FunctionServant mysticCodeFunction, EnemyMob enemy)
        {
            return ApplyPartyMemberStatus(mysticCode, mysticCodeFunction, enemy);
        }

        private static EnemyMob ApplyPartyMemberStatus(MysticCode mysticCode, FunctionServant mysticCodeFunction, EnemyMob enemy)
        {
            enemy.ActiveStatuses.Add(new ActiveStatus
            {
                StatusEffect = mysticCodeFunction,
                AppliedSkillLevel = mysticCode.MysticCodeLevel,
                ActiveTurnCount = mysticCodeFunction.Svals[mysticCode.MysticCodeLevel - 1].Turn
            });

            return enemy;
        }
        #endregion
    }
}
