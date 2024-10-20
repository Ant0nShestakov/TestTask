using System;

public interface IPooledObject<T>
{
    public abstract event Action<T> PushEvent;

    public void Push();
}

public interface ILifetimePooledObject<T> : IPooledObject<T> 
{
    public abstract void SetLifetime(float lifeTime);
}