using UnityEngine;
using Object = UnityEngine.Object;
using System.Reflection;

namespace BooksAndFractals
{
    public static class AssetLoader
    {
        public static T LoadAsset<T>(string AssetName, string Folder, string FileName) where T: Object
        {
            string text = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Mods", Folder, FileName);
            Il2CppAssetBundle il2cppAssetBundle = Il2CppAssetBundleManager.LoadFromFile(text);
            T returningAsset = il2cppAssetBundle.Load<T>(AssetName);
            returningAsset.hideFlags = HideFlags.DontUnloadUnusedAsset;
            il2cppAssetBundle.Unload(false);
            return returningAsset != null ? returningAsset : null;
        }
    }
}
