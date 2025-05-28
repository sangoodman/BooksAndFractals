using Il2Cpp;
using UnityEngine;
using UnityEngine.Audio;

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
        /// <summary>
        /// Assigns every AudioSource in ObjectsName to SFX mixer group and syncs the volume with the slider.
        /// </summary>
        /// <param name="ObjectsName">The gameObject that has all the AudioSources.</param>
        public static void AssignToSFX(string ObjectsName)
        {
            AudioMixerGroup outputAudioMixerGroup = GameObject.Find("Player/AdditionalAudioSource1_FootSteps").GetComponent<AudioSource>().outputAudioMixerGroup;
            foreach (var sound in ObjectManipulation.FindObjectsWithName(ObjectsName))
            {
                sound.GetComponent<AudioSource>().outputAudioMixerGroup = outputAudioMixerGroup;
                sound.GetComponent<AudioSource>().volume = LittleEndianHexToFloat(FractalSave.GetString("SFX_Volume_Slider"));
            }
        }
        /// <summary>
        /// Assigns a specific audiosource to SFX mixer group and syncs the volume with the slider.        
        /// /// </summary>
        /// <param name="audioSource">The desired audiosource.</param>
        public static void AssignToSFX(AudioSource audioSource)
        {
            AudioMixerGroup outputAudioMixerGroup = GameObject.Find("Player/AdditionalAudioSource1_FootSteps").GetComponent<AudioSource>().outputAudioMixerGroup;
            audioSource.GetComponent<AudioSource>().outputAudioMixerGroup = outputAudioMixerGroup;
            audioSource.GetComponent<AudioSource>().volume = LittleEndianHexToFloat(FractalSave.GetString("SFX_Volume_Slider"));
        }
        /// <summary>
        /// Transfroms specific hex value (usually in some strings from the save file, like volume settings) to good old float.
        /// </summary>
        /// <param name="hex">The hex value.</param>
        /// <returns>float</returns>
        public static float LittleEndianHexToFloat(string hex)
        {
            // Convert hex string (e.g., "0000803F") to byte array in little-endian order
            byte[] bytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                bytes[i] = byte.Parse(hex.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }

            // Convert byte array to float
            return System.BitConverter.ToSingle(bytes, 0);
        }
        /// <summary>
        /// Returns the color associated with the given ColorPackType.
        /// </summary>
        /// <typeparam name="cpt">The ColorPackType enum (e.g. ORANGE, WHITE...)</typeparam>
        /// <returns>The color associated with the given ColorPackType, or black</returns>
        public static Color ColorPackToColor(ColorPack.ColorPackType cpt)
        {
            Color newColor = Color.black;
            switch (cpt)
            {
                case ColorPack.ColorPackType.ORANGE:
                    newColor = new Color(1, 0.4627f, 0);
                    break;
                case ColorPack.ColorPackType.BLUE:
                    newColor = new Color(0.0235f, 0.5922f, 1);
                    break;
                case ColorPack.ColorPackType.GOLD:
                    newColor = new Color(1, 0.8431f, 0);
                    break;
                case ColorPack.ColorPackType.PINK:
                    newColor = new Color(1, 0.2217f, 0.9313f);
                    break;
                case ColorPack.ColorPackType.BLACK:
                    newColor = new Color(0.3098f, 0.3098f, 0.3098f);
                    break;
                case ColorPack.ColorPackType.CYAN:
                    newColor = new Color(0, 0.851f, 1);
                    break;
                case ColorPack.ColorPackType.GREEN:
                    newColor = new Color(0.3845f, 1, 0.3821f);
                    break;
                case ColorPack.ColorPackType.DARK_CYAN:
                    newColor = new Color(0, 0.6722f, 0.7925f);
                    break;
                case ColorPack.ColorPackType.DARK_GREEN:
                    newColor = new Color(0, 0.7358f, 0.0833f);
                    break;
                case ColorPack.ColorPackType.RED:
                    newColor = new Color(1, 0, 0.0167f);
                    break;
                case ColorPack.ColorPackType.DARK_RED:
                    newColor = new Color(0.6431f, 0, 0.0107f);
                    break;
                case ColorPack.ColorPackType.WHITE:
                    newColor = Color.white;
                    break;
                case ColorPack.ColorPackType.DARK_BLUE:
                    newColor = new Color(0.1085f, 0.2086f, 1);
                    break;
                case ColorPack.ColorPackType.DARK_YELLOW:
                    newColor = new Color(0.7804f, 0.8196f, 0);
                    break;
                case ColorPack.ColorPackType.YELLOW:
                    newColor = new Color(0.9647f, 1, 0.3922f);
                    break;
                case ColorPack.ColorPackType.DARK_PURPLE:
                    newColor = new Color(0.4415f, 0, 0.6887f);
                    break;
                case ColorPack.ColorPackType.DARK_ORANGE:
                    newColor = new Color(0.7453f, 0.3287f, 0);
                    break;
                case ColorPack.ColorPackType.DARK_PINK:
                    newColor = new Color(0.8962f, 0, 0.82f);
                    break;
                case ColorPack.ColorPackType.BROWN:
                    newColor = new Color(0.7264f, 0.3656f, 0);
                    break;
                case ColorPack.ColorPackType.GRAY:
                    newColor = new Color(0.5843f, 0.5843f, 0.5843f);
                    break;
                case ColorPack.ColorPackType.PURPLE:
                    newColor = new Color(0.6392f, 0, 1);
                    break;
            }
            return newColor;
        }

        /// <summary>
        /// Turns an array to list, adds some values, and returns an array.
        /// </summary>
        /// <typeparam name="T">The type of given array</typeparam>
        /// <param name="array">The name of array</param>
        /// <param name="additionalValues">Values to add into the new array</param>
        /// <returns>The new array with added values.</returns>
        public static T[] AddToArray<T>(T[] array, params T[] additionalValues)
        {
            // Convert array to list
            List<T> list = new List<T>(array);

            // Add additional values
            list.AddRange(additionalValues);

            // Convert list back to array and return
            return list.ToArray();
        }
        /// <summary>
        /// If your mod does something illegal that could resolve in leaderboard cheating, or will be used for future FSPC speedrun.com Category Extensions, use this function to turn off Steamworks.
        /// </summary>
        public static void DisableSteamworks()
        {
            GameObject.Destroy(SteamManager.Instance.gameObject);
        }
    }
}