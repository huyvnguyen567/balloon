using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private Dictionary<string, Queue<GameObject>> pooledObjects = new Dictionary<string, Queue<GameObject>>();
    [SerializeField] private int poolSize = 20;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject itemPrefab;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        InitializePool(bulletPrefab, "Bullet", poolSize) ;
        InitializePool(ballPrefab, "Ball", poolSize);
        InitializePool(itemPrefab, "Item", poolSize);
    }

    private void InitializePool(GameObject prefab, string objectType, int size)
    {
        Queue<GameObject> objectQueue = new Queue<GameObject>();
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            objectQueue.Enqueue(obj);
        }
        pooledObjects.Add(objectType, objectQueue);
    }

    public GameObject GetObjectFromPool(string objectType)
    {
        if (pooledObjects.ContainsKey(objectType))
        {
            Queue<GameObject> objectQueue = pooledObjects[objectType];
            if (objectQueue.Count > 0)
            {
                GameObject obj = objectQueue.Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                GameObject newObj = Instantiate(GetPrefabByType(objectType));
                newObj.SetActive(true);
                return newObj;
            }
        }
        else
        {
            return null;
        }

    }

    public void ReturnObjectToPool(string objectType, GameObject obj)
    {
        obj.SetActive(false);
        if (pooledObjects.ContainsKey(objectType))
        {
            pooledObjects[objectType].Enqueue(obj);
        }
    }

    private GameObject GetPrefabByType(string objectType)
    {
        switch (objectType)
        {
            case "Bullet":
                return bulletPrefab;
            case "Ball":
                return ballPrefab; 
            case "Item":
                return itemPrefab; 
                
            default:
                Debug.LogError("Prefab " + objectType + " not found");
                return null;
        }
    }
}
