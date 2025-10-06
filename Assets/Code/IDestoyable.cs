using System;

namespace Code
{
    public interface IDestoyable<T>
    {
        public event Action<T> OnDestroyed;
    }
}