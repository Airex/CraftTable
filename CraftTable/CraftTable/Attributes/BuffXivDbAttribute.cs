using System;

namespace CraftTable.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BuffXivDbAttribute : Attribute
    {
        public BuffXivDbAttribute(int buffId)
        {
            BuffId = buffId;

        }

        public int BuffId { get; set; }

    }
}