using UnityEngine;
using Object = UnityEngine.Object;
using System.Reflection;

namespace BooksAndFractals
{
    /// <summary>
    /// Tools and utilities related to Asset Bundles.
    /// </summary>
    public static class AssetLoader
    {
        /// <summary>
        /// Loads an object inside of the asset bundle of the specified name.
        /// </summary>
        /// <typeparam name="T">The type of the object to load.</typeparam>
        /// <param name="AssetName">The name of the OBJECT to load.</param>
        /// <param name="Folder">The folder where the asset bundle file is located. (Needs to be inside of the Mods folder).</param>
        /// <param name="FileName">The name of the Asset Bundle to load.</param>
        /// <returns></returns>
        public static T LoadAsset<T>(string AssetName, string Folder, string FileName) where T: Object
        {
            string bundlePath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Mods", Folder, FileName);

            if (!File.Exists(bundlePath))
            {
                Debug.LogError($"The specified asset path \"{bundlePath}\" doesn't exist.");
                return null;
            }

            Il2CppAssetBundle bundle = Il2CppAssetBundleManager.LoadFromFile(bundlePath);
            T returningAsset = bundle.Load<T>(AssetName);

            if (returningAsset == null)
            {
                Debug.LogError("Error loading the asset of name: " + AssetName);
                return null;
            }

            returningAsset.hideFlags = HideFlags.DontUnloadUnusedAsset;
            bundle.Unload(false);
            return returningAsset != null ? returningAsset : null;
        }
    }
}
