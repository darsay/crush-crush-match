using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreButtonContainer : MonoBehaviour
{
    [SerializeField] List<Button> storeButtons = new List<Button>();

    List<bool> _buttonStatus = new List<bool> ();

    private void OnEnable()
    {
        GameDataEventManager.OnBuyMade += SetBuy;
    }

    private void OnDisable()
    {
        GameDataEventManager.OnBuyMade -= SetBuy;
    }

    private void Start()
    {
       // _buttonStatus = ServiceLocator.GetService<GameProgressionService>().GameData.ShopItems.Contains(Id);
        SetButtons ();
    }

    void SetBuy(int id)
    {
       // _buttonStatus = ServiceLocator.GetService<GameProgressionService>().GameData.ShopItems;

        storeButtons[id].interactable = false;
    }

    void SetButtons()
    {
        for (int i = 0; i < _buttonStatus.Count; i++)
        {
            if (!_buttonStatus[i]) storeButtons[i].interactable = false;
        }
    }
}
