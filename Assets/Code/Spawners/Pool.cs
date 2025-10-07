using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Spawners
{
    [Serializable]
    public abstract class Pool<T> where T : MonoBehaviour
    {
        private int _startAmount;
        private GameObject _container;
        
        protected T Prefab;
        protected Stack<T> Stack = new();

        protected Pool(T prefab, int startAmount, GameObject container)
        {
            Prefab = prefab;
            _startAmount = startAmount;
            _container = container;
        }

        public void Return(T template)
        {
            if(template == null)
                return;
            
            template.gameObject.SetActive(false); 
            Stack.Push(template);
            template.transform.parent = _container.transform;
        }

        public T Get()
        {
            if (Stack.TryPop(out T template) == false)
            {
                Stack.Push(Create());
                template = Stack.Pop();
            }
        
            template.gameObject.SetActive(true);
            template.transform.parent = null;
            
            return template;
        }
    
        protected void CreateStartCount()
        {
            for (int i = 0; i < _startAmount; i++)
            {
                T obj = Create();
                Return(obj);
            }
        }

        protected abstract T Create();
    }
}