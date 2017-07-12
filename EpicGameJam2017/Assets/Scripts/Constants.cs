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

    /// <summary>
    /// Dictionary that associates players with their color.
    /// (Warning: This dictionary is potentially mutable, but the values are not intended to be changed at runtime!)
    /// </summary>
    public static readonly Dictionary<Players, Color> PlayerColors = new Dictionary<Players, Color>()
    {
        {Players.A, Color.green},
        {Players.B, Color.magenta},
        {Players.C, Color.blue},
        {Players.D, Color.red},
        {Players.E, Color.black},
        {Players.F, new Color(1f, 0.549f, 0f)},
        {Players.G, Color.cyan},
        {Players.H, new Color(0f, 0.4f, 0f)},
    };

    /// <summary>
    /// Color to use if no player is available
    /// </summary>
    public static readonly Color DefaultHexagonColor = new Color(0f, 0f, 0f, 0f);

    /// <summary>
    /// Color of the cheese on the grid
    /// </summary>
    public static readonly Color CheeseColor = new Color(242 / 255.0f, 236 / 255.0f, 189 / 255.0f);

    /// <summary>
    /// Tag of the knife decoration.
    /// </summary>
    public const string KnifeTag = "Knife";

    /// <summary>
    /// Tag of the world border.
    /// </summary>
    public const string WorldBorderTag = "WorldBorder";
}
