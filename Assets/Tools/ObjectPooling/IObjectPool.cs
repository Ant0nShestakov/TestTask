using UnityEngine;


/// <summary>
/// Generic object pool for GameObjects.
/// </summary>
/// <typeparam name="T">Type of pooled object extended MonoBehaviour</typeparam>
public interface IObjectPool<T> where T : MonoBehaviour, IPooledObject<T>
{
    /// <summary>
    /// Try retrieved item from the object pool, creating a new one if the pool is empty and pool isExpandable.
    /// </summary>
    /// <returns>Success retrieved item from the pool.</returns>
    public bool TryPop(out T poolingObject, Transform transform);

    /// <summary>
    /// Try retrieved item from the object pool, creating a new one if the pool is empty and pool isExpandable.
    /// </summary>
    /// <returns>Success retrieved item from the pool.</returns>
    public bool TryPop(out T poolingObject);

    /// <summary>
    /// Adds an item to the object pool.
    /// </summary>
    /// <param name="item">The GameObject to be added to the pool.</param>
    public void Push(T poolingObject);
}
