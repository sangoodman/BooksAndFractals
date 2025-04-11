using Il2Cpp;
using UnityEngine;

namespace BooksAndFractals
{
    /// <summary>
    /// Used to control everything NGUI related.
    /// </summary>
    public class NGUIManipulator
    {
        /// <summary>
        /// Changes text for buttons.
        /// </summary>
        /// <param name="text">The new text</param>
        /// <param name="button">The NGUI GameObject that has Label as a child.</param>
        public static void ChangeTextForButton(string text, GameObject button)
        {
            button.GetChildWithName("Label").GetComponent<UILabel>().text = text;
        }
    }
}
