using UnityEngine;
using Object = UnityEngine.Object;
using Il2Cpp;

namespace BooksAndFractals
{
    /// <summary>
    /// Methods to get different objects/script dynamically.
    /// </summary>
    public static class ReferenceSearcher
    {
        /// <summary>
        /// Gets a player object.
        /// </summary>
        public static GameObject GetPlayerObject()
        {
            return Controls.Instance.gameObject;
        }
        /// <summary>
        /// Gets a player script.
        /// </summary>
        public static Controls GetPlayerScript()
        {
            return Controls.Instance;
        }
        /// <summary>
        /// Gets a menu object.
        /// </summary>
        public static GameObject GetMenuObject()
        {
            return MenuController.m_instance.gameObject;
        }
        /// <summary>
        /// Gets a menu script.
        /// </summary>
        public static MenuController GetMenuScript()
        {
            return MenuController.m_instance;
        }
        /// <summary>
        /// Gets an InGameUIManager script.
        /// </summary>
        public static InGameUIManager GetUiManager()
        {
            return InGameUIManager.Instance;
        }
        /// <summary>
        /// Gets an InGameUIManager object.
        /// </summary>
        public static GameObject GetUiManagerObject()
        {
            return InGameUIManager.Instance.gameObject;
        }
        /// <summary>
        /// Gets a taser object.
        /// </summary>
        public static GameObject GetGunObject()
        {
            return GunController.Instance.gameObject;
        }
        /// <summary>
        /// Gets a taser script.
        /// </summary>
        public static GunController GetGunController()
        {
            return GunController.Instance;
        }
    }
}