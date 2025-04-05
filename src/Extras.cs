using UnityEngine;
using Il2CppInterop.Runtime;

namespace BooksAndFractals
{
    /// <summary>
    /// Tools that doesn't have a category, but are still useful.
    /// </summary>
    public static class Extras
    {
        /// <summary>
        /// Method that converts hex color to RGB.
        /// </summary>
        /// <param name="hex">The color in hex format</param>
        /// <returns>Color32 of the color, or magenta if something went wrong.</returns>
        public static Color HexToRGB(string hex)
        {
            // Remove # if present
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);

            if (hex.Length != 6 && hex.Length != 8)
            {
                Debug.LogWarning("Hex string must be 6 (RRGGBB) or 8 (RRGGBBAA) characters.");
                return Color.magenta; // Error color
            }

            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte a = 255;

            if (hex.Length == 8)
                a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);

            return new Color32(r, g, b, a);
        }
    }
}