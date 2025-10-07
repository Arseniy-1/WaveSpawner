using System.Collections;
using UnityEngine;

namespace Code.Services.SceneLoader
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator load);
    }
}