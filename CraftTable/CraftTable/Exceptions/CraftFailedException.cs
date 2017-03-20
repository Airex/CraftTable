using System;

namespace CraftTable.Exceptions
{
    public class CraftFailedException : Exception
    {
        public CraftFailedException(bool resourcesReclaimed)
        {
            ResourcesReclaimed = resourcesReclaimed;
        }
        public bool ResourcesReclaimed { get; set; }
    }
}