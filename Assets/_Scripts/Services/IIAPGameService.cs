using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IIAPGameService : IService
{
    public Task Initialize(Dictionary<string, string> products);
    public bool IsReady();
    public Task<bool> StartPurchase(string product);

    public string GetLocalizedPrice(string product);
}
