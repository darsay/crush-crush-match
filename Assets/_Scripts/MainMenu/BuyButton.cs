using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuyButton : MonoBehaviour
{
    public int Id;

    [SerializeField]
    protected int price;

    Button _button;

    [SerializeField] TextMeshProUGUI priceTag;

    private void Awake()
    {
        _button = GetComponent<Button>();
        priceTag.text = price.ToString();
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
