using System.Collections.Generic;

namespace FateGrandCalculator.Models
{
    public class MasterChaldeaInfo
    {
        /// <summary>
        /// The display name for this Chaldea's master
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// NA or JP account
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// Level of the account to calculate party cost, exp caps, etc.
        /// </summary>
        public int AccountLevel { get; set; }
        /// <summary>
        /// Code used for people to add this user as a friend
        /// </summary>
        public string FriendCode { get; set; }
        /// <summary>
        /// List of servants the user owns with their levels and skills
        /// </summary>
        public List<ChaldeaServant> ChaldeaServants { get; set; }
        /// <summary>
        /// List of craft essences the user owns (with or without MLB)
        /// </summary>
        public List<CraftEssence> CraftEssences { get; set; }
        /// <summary>
        /// List of mystic codes the user owns with their levels
        /// </summary>
        public List<MysticCode> MysticCodes { get; set; }
    }
}
