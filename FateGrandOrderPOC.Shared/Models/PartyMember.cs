namespace FateGrandOrderPOC.Shared.Models
{
    public class PartyMember
    {
        public ChaldeaServant Servant { get; set; }
        public ChaldeaCraftEssence EquippedCraftEssence { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
    }
}
