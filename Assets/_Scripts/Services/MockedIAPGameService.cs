using System.Collections.Generic;
using System.Threading.Tasks;

public class MockedIAPGameService : IIAPGameService
{
    public bool IsReady()
    {
        return true;
    }

    public async Task Initialize(Dictionary<string, string> products)
    {
        await Task.Yield();
    }

    public async Task<bool> StartPurchase(string product)
    {
        await Task.Delay(2000);
        return true;
    }


    public string GetLocalizedPrice(string product)
    {
        return "0.99$";
    }

    public void Clear()
    {
    }

    
}
