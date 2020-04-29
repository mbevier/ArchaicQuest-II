﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WhoPK.GameLogic.Item
{
    public class Container
    {
        public enum ContainerSize
        {
            ExtraSmall,
            Small,
            Medium,
            Large,
            ExtraLarge,
            Infinite
        }

        public ContainerSize Size { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public bool CanOpen { get; set; }
        public bool IsOpen { get; set; }
        public bool CanLock { get; set; }
        public bool IsLocked { get; set; }
        public int GoldPieces { get; set; }
        // needs to match Key lock guid
        public Guid? AssociatedKeyId { get; set; }
        public Item.LockStrength LockDifficulty { get; set; }
    }
}
