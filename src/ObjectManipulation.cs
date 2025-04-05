using UnityEngine;
using Il2CppInterop.Runtime;

namespace BooksAndFractals
{
    /// <summary>
    /// Tools related to finding, removing, adding, referencing objects.
    /// </summary>
    public static class ObjectManipulation
    {
        /// <summary>
        /// Gets all children of a specific object.
        /// </summary>
        /// <param name="obj">Object which has the children.</param>
        /// <param name="includeInactive">Should the function return inactive objects too?</param>
        /// <returns>An array of children as GameObjects.</returns>
        public static GameObject[] GetChildrenOfObject(this GameObject obj, bool includeInactive = true)
        {
            List<GameObject> children = new List<GameObject>();

            for (int i = 0; i < obj.transform.childCount; i++)
            {
                GameObject child = obj.transform.GetChild(i).gameObject;
                if (child.activeSelf || includeInactive)
                {
                    children.Add(child);
                }
            }

            return children.ToArray();
        }

        /// <summary>
        /// Gets a child in a specific Transform with a specific name.
        /// </summary>
        /// <param name="tr">The object's transform with a desired child.</param>
        /// <param name="name">The name of the child.</param>
        /// <returns>Either null if it doesn't exist, or child's transform.</returns>
        public static Transform GetChildWithName(this Transform tr, string name)
        {
            foreach (GameObject child in GetChildrenOfObject(tr.gameObject))
            {
                if (child.name == name) return child.transform;
            }

            return null;
        }

        /// <summary>
        /// Gets a child in a specific GameObject with a specific name.
        /// </summary>
        /// <param name="tr">The object's GameObject with a desired child.</param>
        /// <param name="name">The name of the child.</param>
        /// <returns>Either null if it doesn't exist, or child's GameObject.</returns>
        public static GameObject GetChildWithName(this GameObject obj, string name)
        {
            foreach (GameObject child in GetChildrenOfObject(obj))
            {
                if (child.name == name) return child;
            }

            return null;
        }
        /// <summary>
        /// Checks if the child exists.
        /// </summary>
        /// <param name="obj">The object that has a desired child.</param>
        /// <param name="name">The name of the child.</param>
        /// <returns>True = exists, False = doesn't.</returns>
        public static bool ExistsChildWithName(this GameObject obj, string name)
        {
            foreach (GameObject child in GetChildrenOfObject(obj))
            {
                if (child.name == name) return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a child on specific path.
        /// </summary>
        /// <param name="obj">Object that has a child.</param>
        /// <param name="path">The path to desired child.</param>
        /// <returns>Child as a GameObject.</returns>
        public static GameObject GetChildOnPath(this GameObject obj, string path)
        {
            string[] array = path.Split('/', System.StringSplitOptions.None);
            GameObject gameObject = obj;
            foreach (string text in array)
            {
                if (text == "..")
                {
                    gameObject = gameObject.transform.parent.gameObject;
                }
                else
                {
                    gameObject = gameObject.GetChildWithName(text);
                }
            }
            return gameObject;
        }
        /// <summary>
        /// Finds an object with a specific name.
        /// </summary>
        /// <param name="name">The object's name</param>
        /// <returns>GameObject.</returns>
        public static List<GameObject> FindObjectsWithName(string name)
        {
            List<GameObject> matchingObjects = new List<GameObject>();
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(true);

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == name)
                {
                    matchingObjects.Add(obj);
                }
            }

            return matchingObjects;
        }
        /// <summary>
        /// Finds an object with a specific class.
        /// </summary>
        /// <typeparam name="T">The class type.</typeparam>
        /// <returns>GameObject, or null.</returns>
        public static T FindObjectsWithClass<T>() where T : MonoBehaviour
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                T component = obj.GetComponent<T>();
                if (component != null)
                {
                    return component;
                }
            }
            return null;
        }
        /// <summary>
        /// Finds all objects with a specific class.
        /// </summary>
        /// <typeparam name="T">The class type.</typeparam>
        /// <returns>List of objects.</returns>
        public static List<T> FindAllObjectsWithClass<T>() where T : MonoBehaviour
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            List<T> objects = new List<T>();

            foreach (GameObject obj in allObjects)
            {
                T component = obj.GetComponent<T>();
                if (component != null)
                {
                    objects.AddRange(obj.GetComponentsInChildren<T>(true));
                }
            }
            return objects;
        }
        /// <summary>
        /// Deletes all children of a GameObject.
        /// </summary>
        /// <param name="obj">The parent with children you want to delete.</param>
        public static void DeleteAllChildren(this GameObject obj)
        {
            foreach (GameObject child in GetChildrenOfObject(obj))
            {
                GameObject.Destroy(child);
            }
        }
        /// <summary>
        /// Disables all children of a GameObject.
        /// </summary>
        /// <param name="obj">The parent with children you want to disable.</param>
        public static void DisableAllChildren(this GameObject obj)
        {
            foreach (GameObject child in GetChildrenOfObject(obj))
            {
                child.SetActive(false);
            }
        }
        /// <summary>
        /// Enables all children of a GameObject.
        /// </summary>
        /// <param name="obj">The parent with children you want to enable.</param>
        public static void EnableAllChildren(this GameObject obj)
        {
            foreach (GameObject child in GetChildrenOfObject(obj))
            {
                child.SetActive(true);
            }
        }
        /// <summary>
        /// Removes a component from a GameObject.
        /// </summary>
        /// <typeparam name="T">The type to be removed.</typeparam>
        /// <param name="obj">The object that has the components.</param>
        /// <returns>True if successful, false if not.</returns>
        public static bool RemoveComponent<T>(this GameObject obj) where T : Component
        {
            if (obj.TryGetComponent<T>(out T component))
            {
                GameObject.Destroy(component);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Gets a component in object.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="obj">The GameObject</param>
        /// <returns>The component of parent, if not found, the components in children.</returns>
        public static T[] TryGetComponents<T>(this GameObject obj) where T : Component
        {
            if (obj.TryGetComponent<T>(out T component))
            {
                return obj.GetComponents<T>();
            }
            else
            {
                return obj.GetComponentsInChildren<T>();
            }
        }
        /// <summary>
        /// Finds a GameObject by a specific name globally.
        /// </summary>
        /// <param name="objectName">The name of the object.</param>
        /// <returns>First matching object, if not found - null.</returns>
        public static GameObject FindGameObjectByName(string objectName)
        {
            GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == objectName)
                {
                    return obj; // Return the first matching object
                }
            }

            return null; // Return null if no object is found
        }
        public static Transform FindInactiveChild(Transform parent, string childName)
        {
            foreach (Transform child in parent.GetComponentsInChildren<Transform>(true)) // true includes inactive objects
            {
                if (child.name == childName)
                {
                    return child; // Return the first matching child
                }
            }

            return null; // Return null if not found
        }

        /// <summary>
        /// Creates a Helper game object, that assigns types to itself.
        /// </summary>
        /// <param name="componentTypes">Types to be assigned.</param>
        /// <returns>Helper game object.</returns>
        public static GameObject CreateHelperObject(params System.Type[] componentTypes)
        {
            GameObject helperObject = GameObject.Find("HelperObject");

            // Create the helper object if it doesn’t exist
            if (helperObject == null)
            {
                helperObject = new GameObject("HelperObject");
            }

            // Loop through each component type and add it if missing
            foreach (var type in componentTypes)
            {
                // Convert System.Type to Il2CppSystem.Type
                if (!helperObject.GetComponent(Il2CppType.From(type)))
                {
                    helperObject.AddComponent(Il2CppType.From(type));
                }
            }

            return helperObject;
        }

    }
}
