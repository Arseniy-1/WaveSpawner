using UnityEngine;

namespace Code.Spawners.Bullet
{
    public interface IMoveable
    {
        Rigidbody2D Rigidbody2D { get; }
    }
}