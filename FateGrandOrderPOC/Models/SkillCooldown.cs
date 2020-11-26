namespace FateGrandOrderPOC.Models
{
    public class SkillCooldown
    {
        /// <summary>
        /// Skill position number (left = 1, middle = 2, right = 3)
        /// </summary>
        public int SkillId { get; set; }
        public int Cooldown { get; set; } = -1;
    }
}
