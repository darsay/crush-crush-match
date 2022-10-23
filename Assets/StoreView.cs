using System.Collections;
using UnityEngine;

public class StoreView : MonoBehaviour
{
    [SerializeField]
    private BoosterItemView _storeBoostersPrefab = null;

    [SerializeField]
    private SkinsItemView _storeSkinsPrefab = null;

    [SerializeField]
    private Transform _itemsParent = null;

    private StoreController _controller;
    private StoreConfig _storeConfig;

    private void Start()
    {
        _storeConfig = new StoreConfig();
        _controller = new StoreController(_storeConfig);

        _storeConfig.Items = ServiceLocator.GetService<GameConfigService>().StoreConfig;
        Initialize(_controller);
    }

    public void Initialize(StoreController controller)
    {
        _controller = controller;

        while (_itemsParent.childCount > 0)
        {
            Transform child = _itemsParent.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }


        foreach (StoreItemConfig shopItemModel in _controller.Config.Items)
        {
            if (shopItemModel.ContentType.Equals("Skins"))
            {
                Instantiate(_storeSkinsPrefab, _itemsParent).SetData(shopItemModel);
            }
            else
            {
                Instantiate(_storeBoostersPrefab, _itemsParent).SetData(shopItemModel);
            }
        }

    }
}
