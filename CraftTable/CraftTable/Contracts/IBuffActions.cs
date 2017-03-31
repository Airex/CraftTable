
using CraftTable.Buffs;

namespace CraftTable.Contracts
{
    public interface IBuffActions
    {
        void RestoreCraftPoints(int craftPoints);
        void RestoreDurability(int durability);
        void QueueAbility(Ability finishingTouches);
    }
}