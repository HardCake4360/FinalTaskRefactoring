using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    public GameObject bloodPrefab;

    private Dictionary<string, Queue<GameObject>> poolDict = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        ObjectPoolManager.Instance.CreatePool("Blood", bloodPrefab, 10);
    }

    public void CreatePool(string key, GameObject prefab, int size)
    {
        if (poolDict.ContainsKey(key)) return;

        Queue<GameObject> objectQueue = new Queue<GameObject>();
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectQueue.Enqueue(obj);
        }

        poolDict[key] = objectQueue;
    }

    public GameObject GetFromPool(string key, Vector3 pos, Quaternion rot)
    {
        if (!poolDict.ContainsKey(key))
        {
            Debug.LogWarning($"Ǯ {key}�� �������� �ʽ��ϴ�.");
            return null;
        }

        GameObject obj = poolDict[key].Dequeue();
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        poolDict[key].Enqueue(obj); // ������ ���� �ٽ� �ڷ� ����
        return obj;
    }
}
