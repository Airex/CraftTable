using CraftTable.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CraftTable
{
    public class BuffCollector : IBuffCollector, IBuffAccessor
    {
        private readonly IList<IBuff> _list = new List<IBuff>();

        BuffActionRegistry _buffActionRegistry = new BuffActionRegistry();

        public void Add(IBuff buff)
        {
            var comparer = new SameBuffEqualityComparer();
            for (var i = 0; i < _list.Count; i++)
            {
                if (comparer.Equals(_list[i], buff)) _list.RemoveAt(i--);
            }
            _list.Add(buff);
        }

        public void Step(IBuffActions buffActions)
        {
            _buffActionRegistry.Reset();
            foreach (var buff in _list)
            {
                buff.Step(_buffActionRegistry);
            }
            _buffActionRegistry.ExecutePreAbility(buffActions);
        }

        public void BuildCalculator(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            foreach (var buff in _list)
            {
                buff.OnCalculate(info, calculatorBuilder);
            }
        }

        public void KillNotActive()
        {
            for (var index = 0; index < _list.Count; index++)
            {
                var buff = _list[index];
                if (!buff.IsActive) _list.RemoveAt(index--);
            }
        }

        public IBuffAccessor GetBuffAccessor()
        {
            return this;
        }

        public IList<IBuff> GetBuffs()
        {
            return new List<IBuff>(_list);
        }

        public void PostAction(IBuffActions craftTable)
        {
            _buffActionRegistry.ExecutePostAbility(craftTable);
        }

        public T GetBuff<T>() where T : IBuff
        {
            return _list.OfType<T>().FirstOrDefault();
        }
    }
}