using System.Collections.Generic;
using UnityEngine;

public class SimplePool2D : MonoBehaviour
{
    public GameObject prefab;
    public int prewarmCount = 20;

    private readonly Queue<GameObject> available = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < prewarmCount; i++)
        {
            var obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            available.Enqueue(obj);
        }
    }

    public GameObject Get(Vector2 position)
    {
        var obj = (available.Count > 0) ? available.Dequeue() : Instantiate(prefab, transform);
        obj.transform.position = position;
        obj.SetActive(true);
        return obj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform, true);
        available.Enqueue(obj);
    }
}
