using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolMono<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public bool Autoexpand { get; set; }
    public Transform Container { get; }
    private List<T> _pool;

    public PoolMono(T prefab, int count)
    {
        Prefab = prefab;
        Container = null;
        CreatePool(count);
    }

    public PoolMono(T prefab, int count, Transform container)
    {
        Prefab = prefab;
        Container = container;
        CreatePool(count);
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var monoElement in _pool)
        {
            if (!monoElement.gameObject.activeInHierarchy)
            {
                element = monoElement;
                element.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            return element;
        }

        if (Autoexpand)
        {
            return CreateObject(true);
        }

        throw new Exception("no free elements & autoexpand is off in pool");
    }

    public IEnumerable<T> GetAllElements()
    {
        foreach (var poolObject in _pool)
        {
            yield return poolObject;
        }
    }

    public void DisableAll()
    {
        foreach (var poolObject in _pool)
        {
            poolObject.gameObject.SetActive(false);
        }
    }
    
    private void CreatePool(int count)
    {
        _pool = new List<T>();
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(Prefab, Container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(createdObject);
        return createdObject;
    }
    
    
}
