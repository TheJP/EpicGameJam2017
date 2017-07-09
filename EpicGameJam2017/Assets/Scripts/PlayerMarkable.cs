using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface PlayerMarkable
{
    /// <summary>Flag that indicates, if this object should be marked right now.</summary>
    bool ShouldBeMarked { get; }
    /// <summary>Player for to whom this markable object belongs.</summary>
    Players Player { get; }
    /// <summary>The position that will be marked.</summary>
    Vector3 Position { get; }
}
