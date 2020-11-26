using System.Collections.Generic;
using System.Linq;

using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Models;

namespace FateGrandCalculator.ApplyStatuses
{
    public static class ApplyEnemyStatus
    {
        public static void ApplyFuncTargetType(int enemyPosition, Function servantFunction, int level, List<EnemyMob> enemies)
        {
            switch (servantFunction.FuncTargetType)
            {
                case "enemyAll":         // enemies
                case "enemyOther":       // other enemies besides target
                    ApplyStatus(servantFunction, level, enemies.Take(3).ToList());
                    break;
                case "enemyFull":        // enemies (including reserve)
                case "enemyOtherFull":   // other enemies (including reserve)
                    ApplyStatus(servantFunction, level, enemies);
                    break;
                case "enemy":            // one enemy
                    ApplyStatus(servantFunction, level, enemies[enemyPosition - 1]);
                    break;
                default:
                    break;
            }
        }

        public static void ApplyFuncTargetType(int enemyPosition, MysticCode mysticCode, Function mysticCodeFunction, List<EnemyMob> enemies)
        {
            switch (mysticCodeFunction.FuncTargetType)
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
        private static void ApplyStatus(Function servantFunction, int level, List<EnemyMob> enemies)
        {
            foreach (EnemyMob enemy in enemies)
            {
                ApplyPartyMemberStatus(servantFunction, level, enemy);
            }
        }

        private static void ApplyStatus(Function servantFunction, int level, EnemyMob enemy)
        {
            ApplyPartyMemberStatus(servantFunction, level, enemy);
        }

        private static void ApplyPartyMemberStatus(Function servantFunction, int level, EnemyMob enemy)
        {
            enemy.ActiveStatuses.Add(new ActiveStatus
            {
                StatusEffect = servantFunction,
                AppliedSkillLevel = level,
                ActiveTurnCount = servantFunction.Svals[level - 1].Turn
            });
        }
        #endregion

        #region Private Methods "Mystic Code"
        private static void ApplyStatus(MysticCode mysticCode, Function mysticCodeFunction, List<EnemyMob> enemies)
        {
            foreach (EnemyMob enemy in enemies)
            {
                ApplyPartyMemberStatus(mysticCode, mysticCodeFunction, enemy);
            }
        }

        private static void ApplyStatus(MysticCode mysticCode, Function mysticCodeFunction, EnemyMob enemy)
        {
            ApplyPartyMemberStatus(mysticCode, mysticCodeFunction, enemy);
        }

        private static void ApplyPartyMemberStatus(MysticCode mysticCode, Function mysticCodeFunction, EnemyMob enemy)
        {
            enemy.ActiveStatuses.Add(new ActiveStatus
            {
                StatusEffect = mysticCodeFunction,
                AppliedSkillLevel = mysticCode.MysticCodeLevel,
                ActiveTurnCount = mysticCodeFunction.Svals[mysticCode.MysticCodeLevel - 1].Turn
            });
        }
        #endregion
    }
}
