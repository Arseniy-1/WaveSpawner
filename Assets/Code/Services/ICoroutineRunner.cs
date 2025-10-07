using System.Collections;
using UnityEngine;

namespace Code.Services
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator load);
    }
}