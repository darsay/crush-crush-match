using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public abstract class StoreItemView : MonoBehaviour
{
    [SerializeField]
    protected Image Background;

    [SerializeField]
    protected Transform itemsParent;

    [SerializeField]
    protected List<Image> images = null;

    [SerializeField]
    protected TMP_Text amount = null;

    [SerializeField]
    protected TMP_Text priceText = null;

    [SerializeField]
    protected Image iconPrefab;

    private int _id;

    protected StoreItemConfig _config;

    Button _button;

    protected void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Buy);
    }

    protected void OnDestroy()
    {
        _button.onClick.RemoveListener(Buy);
    }

    public void SetData(StoreItemConfig config)
    {
        _config = config;
        UpdateVisuals();
    }

    protected async void UpdateVisuals()
    {
        if (_config == null)
            return;

        _id = _config.id;
        amount.text = "x" + _config.RewardAmount.ToString();
        priceText.text = _config.Price.ToString();

        for (int i = 0; i < _config.Items.Count; i++)
        {
            images.Add(Instantiate(iconPrefab, itemsParent));
            images[i].sprite = await Addressables.LoadAssetAsync<Sprite>(_config.Items[i]).Task;

        }

        var _buttonStatus = ServiceLocator.GetService<GameProgressionService>().GameData.ShopItems[_id];
    }

    public abstract void Buy();

}
