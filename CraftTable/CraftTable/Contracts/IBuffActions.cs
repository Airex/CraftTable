using System.Security.Cryptography.X509Certificates;

namespace CraftTable.Contracts
{
    public interface IBuffActions
    {
        void RestoreCraftPoints(int craftPoints);
        void RestoreDurability(int durability);
    }
}