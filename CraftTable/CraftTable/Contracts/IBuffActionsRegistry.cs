using System;

namespace CraftTable.Contracts
{
    public interface IBuffActionsRegistry
    {
        void RegisterPreAbility(Action<IBuffActions> actionRegisterDelegate);
        void RegisterPostAbility(Action<IBuffActions> actionRegisterDelegate);
    }
}