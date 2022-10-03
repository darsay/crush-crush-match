using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;

public class UnityIAPGameService : IIAPGameService, IStoreListener
{
    private bool _isInitialized = false;

    private IStoreController _storeController;

    private TaskStatus _purchaseTaskStatus = TaskStatus.Created;
    private TaskStatus _initializeTaskStatus = TaskStatus.Created;

    public bool IsReady() => _isInitialized;

    public async Task Initialize(Dictionary<string, string> products)
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());


        foreach (KeyValuePair<string, string> productEntry in products)
        {
            builder.AddProduct(productEntry.Value, ProductType.Consumable, new IDs
        {
                { GooglePlay.Name, productEntry.Value}
        });
        }

        _initializeTaskStatus = TaskStatus.Running;
        UnityPurchasing.Initialize(this, builder);

        while(_initializeTaskStatus == TaskStatus.Running)
        {
            await Task.Delay(100);
        }
    }

    public void Clear()
    {
    }

    public async Task<bool> StartPurchase(string product)
    {
        if (!_isInitialized)
            return false;

        _purchaseTaskStatus = TaskStatus.Running;
        _storeController.InitiatePurchase(product);

        while(_purchaseTaskStatus == TaskStatus.Running)
        {
            await Task.Delay(500);
        }

        return _purchaseTaskStatus == TaskStatus.RanToCompletion;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _isInitialized = true;

        _storeController = controller;

        _initializeTaskStatus = TaskStatus.RanToCompletion;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        _isInitialized = false;
        _initializeTaskStatus = TaskStatus.RanToCompletion;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        _purchaseTaskStatus = TaskStatus.RanToCompletion;
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchased fail: " + failureReason);

        _purchaseTaskStatus = TaskStatus.Faulted;
    }

    

    public string GetLocalizedPrice(string product)
    {
        if(!_isInitialized)
            return string.Empty;

        Product unityProduct = _storeController.products.WithID(product);
        return unityProduct?.metadata?.localizedPriceString;
    }
}