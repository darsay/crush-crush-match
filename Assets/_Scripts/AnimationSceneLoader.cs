using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationSceneLoader : MonoBehaviour
{
    public int SceneToLoad;

    public bool IsAnimationFinished;

    public void LoadScene()
    {
        IsAnimationFinished = true;
    }
}
