using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneLoaderButton : MonoBehaviour
{

    [SerializeField] protected int sceneToLoadId;
    protected Button _button;

    protected void Awake()
    {
        _button = GetComponent<Button>();
    }

    protected void OnEnable()
    {
        _button.onClick.AddListener(LoadScene);
    }

    protected void OnDisable()
    {
        _button.onClick.RemoveListener(LoadScene);
    }

    virtual protected void LoadScene()
    {
        SceneLoader.Instance.LoadScene(sceneToLoadId);   
    }
}
