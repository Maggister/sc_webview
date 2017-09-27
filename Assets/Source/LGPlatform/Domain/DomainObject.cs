// DomainObject.cs
// created by Yaroslav Nevmerzhytskiy
// e-mail: y.nevmerzhytskiy@twinwingames.com
// Copyright 2017 Luck Genome

using System;

namespace LGPlatform.Domain
{
    /// <summary>
    /// Represents object from Domain layer
    /// </summary>
    [Serializable]
    public class DomainObject
    {
        /// <summary>
        /// Unique id of the object
        /// </summary>
        public String Id;

        public DomainObject()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}