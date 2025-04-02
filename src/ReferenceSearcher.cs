using UnityEngine;
using Object = UnityEngine.Object;
using Il2Cpp;

namespace BooksAndFractals
{
    public static class ReferenceSearcher
    {
        public static GameObject GetPlayerObject()
        {
            return Controls.Instance.gameObject;
        }
        public static Controls GetPlayerScript()
        {
            return Controls.Instance;
        }
        public static GameObject GetMenuObject()
        {
            return MenuController.m_instance.gameObject;
        }
        public static MenuController GetMenuScript()
        {
            return MenuController.m_instance;
        }
        public static InGameUIManager GetUiManager()
        {
            return InGameUIManager.Instance;
        }
        public static GameObject GetUiManagerObject()
        {
            return InGameUIManager.Instance.gameObject;
        }
        public static GameObject GetGunObject()
        {
            return GunController.Instance.gameObject;
        }
        public static GunController GetGunController()
        {
            return GunController.Instance;
        }
    }
}
