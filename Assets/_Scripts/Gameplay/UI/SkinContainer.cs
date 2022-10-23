using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinContainer : MonoBehaviour
{
    public GameObject buttonPrefab;
    public bool IsPlayer;


    public List<int> icons = new List<int>();
    public List<GameObject> buttonObjects = new List<GameObject>();

    private void Awake()
    {
        GameDataEventManager.OnSkinAdded += OnIconAdd;
    }

    private void OnDestroy()
    {
        GameDataEventManager.OnSkinAdded -= OnIconAdd;

    }

    private void Start()
    {
        DrawAll();
    }
    
    void DrawAll()
    {
        icons = ServiceLocator.GetService<GameProgressionService>().GameData.IconsList;
    
        foreach (var item in buttonObjects)
        {
            Destroy(item.gameObject);
        }
    
        buttonObjects = new List<GameObject>();
    
        foreach (var icon in icons)
        {
            var button = Instantiate(buttonPrefab, transform);

            var iconSetter = button.GetComponent<IconSetterButton>();
            iconSetter.ButtonInitializer(IsPlayer, icon);
    
            buttonObjects.Add(button);
        }
    }

    public void OnIconAdd(int icon)
    {

        var button = Instantiate(buttonPrefab, transform);
        var iconSetter = button.GetComponent<IconSetterButton>();
        iconSetter.ButtonInitializer(IsPlayer, icon);

        buttonObjects.Add(button);   
    }
}
