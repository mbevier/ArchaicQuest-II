using System;
using System.Collections.Generic;
using System.Text;

namespace WhoPK.GameLogic.Generator
{
    public enum AreaState
    {
        Depth,
        PlayerNeedsSpawnPoint,
        RepeatCount
    }

    public enum IslandShape
    {
        Random,
        Rectangular,
        Round
    }

    public enum IslandSixr
    {
        Random,
        Small,
        Medium,
        Large
    }

    public enum Theme
    {
        Forest,
        Mountain,
        Lake,
        Castle,
        House,
        Road,
        Swamp,
        Cave,
        City
    }
}
