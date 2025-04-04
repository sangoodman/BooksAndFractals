using UnityEngine;

namespace BooksAndFractals
{
    public static class ObjectManipulation
    {
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

        public static Transform GetChildWithName(this Transform tr, string name)
        {
            foreach (GameObject child in GetChildrenOfObject(tr.gameObject))
            {
                if (child.name == name) return child.transform;
            }

            return null;
        }

        public static GameObject GetChildWithName(this GameObject obj, string name)
        {
            foreach (GameObject child in GetChildrenOfObject(obj))
            {
                if (child.name == name) return child;
            }

            return null;
        }
        public static bool ExistsChildWithName(this GameObject obj, string name)
        {
            foreach (GameObject child in GetChildrenOfObject(obj))
            {
                if (child.name == name) return true;
            }

            return false;
        }

        public static GameObject GetChildOnPath(this GameObject obj, string path)
        {
            string[] array = path.Split('/', StringSplitOptions.None);
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
        public static void DeleteAllChildren(this GameObject obj)
        {
            foreach (GameObject child in GetChildrenOfObject(obj))
            {
                GameObject.Destroy(child);
            }
        }
        public static void DisableAllChildren(this GameObject obj)
        {
            foreach (GameObject child in GetChildrenOfObject(obj))
            {
                child.SetActive(false);
            }
        }
        public static void EnableAllChildren(this GameObject obj)
        {
            foreach (GameObject child in GetChildrenOfObject(obj))
            {
                child.SetActive(true);
            }
        }
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
        public static List<Light> FindAllLightsInScene()
        {
            List<Light> lights = new List<Light>();

            foreach (GameObject obj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
            {
                lights.AddRange(obj.GetComponentsInChildren<Light>(true)); // Include inactive lights
            }

            return lights;
        }
    }
}
