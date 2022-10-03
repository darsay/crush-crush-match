using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuyButton : MonoBehaviour
{
    public int Id;

    [SerializeField]
    protected int price;

    [SerializeField]
    protected bool isIAP;

    protected Button _button;

    [SerializeField] protected TextMeshProUGUI priceTag;

    protected IIAPGameService _iapService;

    private void Awake()
    {
        _button = GetComponent<Button>();
        priceTag.text = price.ToString();

        _iapService = ServiceLocator.GetService<IIAPGameService>();

    }

    private void OnEnable()
    {
        _button.onClick.AddListener(BuyPack);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(BuyPack);
    }

    public abstract void BuyPack();


    public abstract void BuyElement(ItemToBuy item);
}
