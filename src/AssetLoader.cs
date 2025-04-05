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
        static Dictionary<string, Il2CppAssetBundle> loadedBundles = new();

        /// <summary>
        /// Loads an object inside of the asset bundle of the specified name.
        /// </summary>
        /// <typeparam name="T">The type of the object to load.</typeparam>
        /// <param name="AssetName">The name of the OBJECT to load.</param>
        /// <param name="Folder">The folder where the asset bundle file is located. (Needs to be inside of the Mods folder).</param>
        /// <param name="FileName">The name of the Asset Bundle to load.</param>
        /// <returns>The loaded object if it isn't null.</returns>
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

        /// <summary>
        /// Preloads a bundle in memory so you can load assets from it later.
        /// </summary>
        /// <param name="bundlePath">The path to the Asset Bundle file.</param>
        public static void PreloadBundle(string bundlePath)
        {
            if (!File.Exists(bundlePath))
            {
                Debug.LogError($"The specified asset path \"{bundlePath}\" doesn't exist.");
                return;
            }

            Il2CppAssetBundle bundle = Il2CppAssetBundleManager.LoadFromFile(bundlePath);

            loadedBundles.Add(Path.GetFileNameWithoutExtension(bundlePath), bundle);
        }
        /// <summary>
        /// Preloads a bundle in memory so you can load assets from it later.
        /// </summary>
        /// <param name="folder">The folder where the asset bundle file is located. (Needs to be inside of the Mods folder).</param>
        /// <param name="fileName">he name of the Asset Bundle to load.</param>
        public static void PreloadBundle(string folder, string fileName)
        {
            string bundlePath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Mods", folder, fileName);
            PreloadBundle(bundlePath);
        }
        /// <summary>
        /// Preloads a bundle embedded in the DLL in memory so you can load assets from it later.
        /// </summary>
        /// <param name="bundlePath">
        /// The name of the Asset Bundle to load (Or path in case the asset is inside of a subfolder).
        /// <para>For example, if your asset is inside of a folder called "Resources" then put "Resources/AssetName".</para>
        /// </param>
        public static void PreloadEmbeddedBundle(string bundlePath)
        {
            string[] embeddedFilesInAssembly = Assembly.GetCallingAssembly().GetManifestResourceNames();
            string assetFullName = Assembly.GetCallingAssembly().GetName().Name + "." + bundlePath.Replace('/', '.');

            if (!embeddedFilesInAssembly.Contains(assetFullName))
            {
                Debug.LogError("Couldn't find any embedded file in the DLL with name: " + bundlePath);
                return;
            }

            Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream(assetFullName);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes);
            Il2CppAssetBundle bundle = Il2CppAssetBundleManager.LoadFromMemory(bytes);

            loadedBundles.Add(bundlePath, bundle);
        }

        /// <summary>
        /// Loads an object from one of the currently loaded asset bundles with the specified name.
        /// </summary>
        /// <typeparam name="T">The type of object to return.</typeparam>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="bundleName">The name of the loaded bundle (the name of the bundle file).</param>
        /// <returns>The loaded object if it exists.</returns>
        public static T LoadAsset<T>(string assetName, string bundleName) where T : Object
        {
            if (!loadedBundles.ContainsKey(bundleName))
            {
                Debug.Log("Couldn't find loaded asset bundle with name:" + bundleName);
                return null;
            }

            T assetToReturn = loadedBundles[bundleName].Load<T>(assetName);
            if (assetToReturn == null)
            {
                Debug.LogError("Error loading the asset of name: " + assetName);
                return null;
            }

            return assetToReturn;
        }

        /// <summary>
        /// Unloads the specified asset bundle from memory.
        /// </summary>
        /// <param name="bundleName">The name of the bundle to unload.</param>
        /// <param name="unloadAssets">Should the already loaded assets from this bundle be unloaded as well?</param>
        public static void UnloadBundle(string bundleName, bool unloadAssets = false)
        {
            if (!loadedBundles.ContainsKey(bundleName))
            {
                Debug.Log("Couldn't find loaded asset bundle with name:" + bundleName);
                return;
            }

            loadedBundles[bundleName].Unload(unloadAssets);
            loadedBundles.Remove(bundleName);
        }
    }
}
