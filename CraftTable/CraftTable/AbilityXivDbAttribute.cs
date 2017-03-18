﻿using System;
using System.Drawing;
using System.IO;

namespace CraftTable
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AbilityXivDbAttribute : Attribute
    {
        public AbilityXivDbAttribute(Crafter crafterLink, int abilityId)
        {
            AbilityId = abilityId;
            CrafterLink = crafterLink;
        }

        public Crafter CrafterLink { get; set; }
        public int AbilityId { get; set; }

    }
}