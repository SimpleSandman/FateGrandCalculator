using System;
using System.Collections.Generic;
using System.Text;

namespace FateGrandOrderPOC.Shared
{
    public abstract class BaseRelation
    {
        public abstract float GetAttackMultiplier(string attack);
        public abstract float GetAttackMultiplier(string attack, string defend);
    }
}
