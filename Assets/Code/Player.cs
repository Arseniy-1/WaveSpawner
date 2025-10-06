using UnityEngine;

namespace Code
{
    public class Player : MonoBehaviour, ITarget
    {
        public Transform TargetTransform { get; private set; }

        private void Awake()
        {
            TargetTransform = transform;
        }
    }
}