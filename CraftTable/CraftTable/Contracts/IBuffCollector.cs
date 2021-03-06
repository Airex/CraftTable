﻿using System.Collections.Generic;

namespace CraftTable.Contracts
{
    public interface IBuffCollector
    {
        void Add(IBuff buff);
        void Step(IBuffActions buffActions);
        void BuildCalculator(ActionInfo info, ICalculatorBuilder calculatorBuilder);
        void KillNotActive();
        IBuffAccessor GetBuffAccessor();
        IList<IBuff> GetBuffs();
        void PostAction(IBuffActions craftTable);
    }
}