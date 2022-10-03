using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppFocusChecker : MonoBehaviour
{
    //private void OnApplicationFocus(bool hasFocus)
    //{
    //    Debug.Log("OnApplicationFocus " + hasFocus);
    //}

    private void OnApplicationPause(bool pauseStatus)
    {
        GameDataEventManager.NotifyOnAplicationPaused(pauseStatus);
    }

    //private void OnApplicationFocusEvent(bool focused)
    //{
    //    Debug.Log("OnApplicationFocusEvent " + focused);
    //}
}
