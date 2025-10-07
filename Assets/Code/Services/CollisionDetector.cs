using System;
using UnityEngine;

namespace Code.Services
{
    public class CollisionDetector : MonoBehaviour
    {
        public event Action<Collider2D> CollisionDetected;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            HandleCollision(collision);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            HandleCollision(collision.collider);
        }
    
        private void OnCollisionStay2D(Collision2D collision)
        {
            HandleCollision(collision.collider);
        }

        private void HandleCollision(Collider2D collider)
        {
            CollisionDetected?.Invoke(collider);           
        }
    }
}