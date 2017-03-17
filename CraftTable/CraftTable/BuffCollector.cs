using System.Collections.Generic;
using System.Linq;
using CraftTable.Contracts;

namespace CraftTable
{
    public class BuffCollector : IBuffCollector,IBuffAccessor
    {
        private readonly IList<IBuff> _list = new List<IBuff>();

        public void Add(IBuff buff)
        {
            var comparer = new SameBuffEqualityComparer();
            for (var i = 0; i < _list.Count; i++)
            {
                if (comparer.Equals(_list[i], buff))_list.RemoveAt(i--);
            }
            _list.Add(buff);
        }

        public void Step(IBuffActions buffActions)
        {
            foreach (var buff in _list)
            {
                buff.Step(buffActions);
            }
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

        public T GetBuff<T>() where T : IBuff
        {
            return _list.OfType<T>().FirstOrDefault();
        }
    }
}