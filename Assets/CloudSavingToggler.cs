using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudSavingToggler : MonoBehaviour
{
    [SerializeField]
    Toggle toggle;

    private void OnEnable()
    {
        toggle.isOn = PlayerPrefs.GetInt("CloudProgression", 0) != 0 ? true : false;
    }

    public void OnSaveMethodChange(bool isCloud)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("CloudProgression", 1);
        }
        else
        {
            PlayerPrefs.SetInt("CloudProgression", 0);
        }

        ServiceLocator.GetService<GameProgressionService>().ChangeProvider(toggle.isOn);
    }
}
