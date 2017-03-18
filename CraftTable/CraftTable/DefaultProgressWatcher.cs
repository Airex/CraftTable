using System;
using CraftTable.Contracts;

namespace CraftTable
{
    public class DefaultProgressWatcher : IProgressWatcher
    {
        public void Log(string s)
        {
            Console.WriteLine(s);
        }
    }
}