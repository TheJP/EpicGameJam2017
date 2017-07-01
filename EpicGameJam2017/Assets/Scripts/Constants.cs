using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Constants
{
    /// <summary>
    /// Button that switches between train and unicorn.
    /// </summary>
    public const string SwitchButton = "Switch";
    /// <summary>
    /// Action Button. Train: Shoot with turret, Unicorn: Move forward
    /// </summary>
    public const string ActionButton = "Action";
    /// <summary>
    /// Special Button. Train: Switch turret amo, Unicorn: Activate powerup
    /// </summary>
    public const string SpecialButton = "Special";
    /// <summary>
    /// Horizontal Axis. Train: Move turret head left / right, Unicorn: Turn left / right
    /// </summary>
    public const string HorizontalAxis = "Horizontal";

    public static readonly Dictionary<Players, Color> PlayerColors = new Dictionary<Players, Color>()
    {
        {Players.None, new Color(0,0,0,0)},
        {Players.A, Color.yellow},
        {Players.B, Color.blue}
    };


}
