using System;
using CraftTable.Contracts;

namespace CraftTable
{
    public class BuffActionRegistry : IBuffActionsRegistry
    {
        private Action<IBuffActions> _preAbility = (a) => { };
        private Action<IBuffActions> _postAbility = (a) => { };
        public void RegisterPreAbility(Action<IBuffActions> actionRegisterDelegate)
        {
            _preAbility += actionRegisterDelegate;
        }

        public void RegisterPostAbility(Action<IBuffActions> actionRegisterDelegate)
        {
            _postAbility += actionRegisterDelegate;
        }

        public void ExecutePreAbility(IBuffActions buffActions)
        {
            _preAbility(buffActions);
        }

        public void ExecutePostAbility(IBuffActions buffActions)
        {
            _postAbility(buffActions);
        }

        public void Reset()
        {
            _preAbility = (a) => { };
            _postAbility = (a) => { };
        }
    }
}