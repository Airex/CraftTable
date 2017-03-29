using System;

namespace CraftTable.Exceptions
{
    public class CraftFailedException : CraftTableException
    {
        public CraftFailedException(bool resourcesReclaimed)
        {
            ResourcesReclaimed = resourcesReclaimed;
        }
        public bool ResourcesReclaimed { get; set; }
    }
}