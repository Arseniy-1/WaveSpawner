using UnityEngine;

namespace Code.Player
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