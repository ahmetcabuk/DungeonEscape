
using System.Collections.Generic;
using UnityEngine;

public static class DontDestroyOnLoadManager
{
    static List<GameObject> _ddolObjects = new List<GameObject>();

    public static void DontDestroyOnLoadd<T>(T go) where T : MonoBehaviour
    {
        UnityEngine.Object.DontDestroyOnLoad(go.gameObject);
        _ddolObjects.Add(go.gameObject);
    }

    public static void DontDestroyOnLoadd(this GameObject go)
    {
        UnityEngine.Object.DontDestroyOnLoad(go);
        _ddolObjects.Add(go);
    }

    public static void DestroyAll()
    {
        foreach (var go in _ddolObjects)
            if (go != null)
                UnityEngine.Object.Destroy(go);

        _ddolObjects.Clear();
    }
}