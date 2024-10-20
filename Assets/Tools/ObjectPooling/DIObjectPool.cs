using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Generic object pool for GameObjects.
/// </summary>
/// <typeparam name="T">Type of pooled object extended MonoBehaviour</typeparam>
public class DIObjectPool<T> : IObjectPool<T> where T : MonoBehaviour, IPooledObject<T>
{
    private int _count;
    private readonly bool _isExpandable;
    private readonly T _poolingObject;
    private readonly Stack<T> _stackPoolingObject;
    private readonly Transform _root;
    private readonly DiContainer _container;

    private readonly float _lifeTimeObject;

    /// <summary>
    /// Initializes a new instance of the ObjectPool class.
    /// </summary>
    /// <param name="container">The Dependency injection container. Only zenject.</param>
    /// <param name="poolingObject">The prefab of the object to be pooled.</param>
    /// <param name="startCount">The initial size of the object pool.</param>
    /// <param name="isExpandable">Will there be an extension if the stack is empty.</param>
    public DIObjectPool(DiContainer container, T poolingObject, int startCount, Transform root, 
        bool isExpandable = false, float lifeTimeObject = 0)
    {
        _stackPoolingObject = new Stack<T>();
        _poolingObject = poolingObject;
        _count = startCount;
        _isExpandable = isExpandable;
        _lifeTimeObject = lifeTimeObject;
        _root = root;
        _container = container;

        Initialize();
    }

    /// <summary>
    /// Initialize the object pool to reach the specified start size.
    /// </summary>
    private void Initialize()
    {
        for(int i = 0; i < _count; i++)
        {
            var cloneObject = _container.InstantiatePrefabForComponent<T>(_poolingObject, _root);

            cloneObject.gameObject.SetActive(false);
            _stackPoolingObject.Push(cloneObject);
        }
    }

    /// <summary>
    /// Try retrieved item from the object pool, creating a new one if the pool is empty and pool isExpandable.
    /// </summary>
    /// <returns>Success retrieved item from the pool.</returns>
    public bool TryPop(out T poolingObject, Transform transform)
    {
        if(_stackPoolingObject.Count == 0)
        {
            if (!_isExpandable) throw new System.Exception($"ObjectPool by object {nameof(T)} is empty and not expandable");

            Debug.Log($"ObjectPool by object {nameof(T)} is empty. Instantiate object and +count");

            poolingObject = GameObject.Instantiate(_poolingObject, transform);

            SettingObject(poolingObject, transform);
            return true;
        }

        poolingObject = _stackPoolingObject.Pop();
        SettingObject(poolingObject, transform);

        _count--;

        return true;
    }

    /// <summary>
    /// Try retrieved item from the object pool, creating a new one if the pool is empty and pool isExpandable.
    /// </summary>
    /// <returns>Success retrieved item from the pool.</returns>
    public bool TryPop(out T poolingObject)
    {
        if(_stackPoolingObject.Count == 0)
        {
            if (!_isExpandable) throw new System.Exception($"ObjectPool by object {nameof(T)} is empty and not expandable");

            Debug.Log($"ObjectPool by object {nameof(T)} is empty. Instantiate object and +count");

            poolingObject = GameObject.Instantiate(_poolingObject, _root);

            SettingObject(poolingObject, _root);
            return true;
        }

        poolingObject = _stackPoolingObject.Pop();
        SettingObject(poolingObject, _root);

        _count--;

        return true;
    }

    private void SettingObject(T poolingObject, Transform transform)
    {
        poolingObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
        poolingObject.PushEvent += Push;

        if (poolingObject is ILifetimePooledObject<T> poolingLifetimeObject)
            poolingLifetimeObject.SetLifetime(_lifeTimeObject);

        poolingObject.gameObject.SetActive(true);
    }

    /// <summary>
    /// Adds an item to the object pool.
    /// </summary>
    /// <param name="item">The GameObject to be added to the pool.</param>
    public void Push(T poolingObject)
    {
        _count++;
        _stackPoolingObject.Push(poolingObject);
        poolingObject.gameObject.SetActive(false);
    }
}
