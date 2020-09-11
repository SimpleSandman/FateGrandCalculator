using System.Collections.Generic;

using FateGrandOrderPOC.Shared.Models;

namespace FateGrandOrderPOC.Shared.Extensions
{
    public static class ListPartyMemberExtensions
    {
        /// <summary>
        /// Update a party member in the list with new info based on ID
        /// </summary>
        /// <param name="party"></param>
        /// <param name="partyMember"></param>
        public static void Update(this List<PartyMember> party, PartyMember partyMember)
        {
            int partyMemberId = party.FindIndex(p => p.Id == partyMember.Id);
            party[partyMemberId] = partyMember;
        }
    }
}
