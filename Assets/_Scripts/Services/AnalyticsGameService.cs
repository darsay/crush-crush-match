using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Analytics;
using UnityEngine;

public class AnalyticsGameService : IService
{
    public async Task Initialize()
    {
        try
        {
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
            //we will use this later
            Debug.Log("Accepted consents: " + consentIdentifiers.Count);
        }
        catch (ConsentCheckException e)
        {
            // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
            Debug.LogError("Error asking for analytics permissions " + e.Message);
        }
    }

    public void SendEvent(string eventName, Dictionary<string, object> parameters = null)
    {
        parameters ??= new Dictionary<string, object>();
        AnalyticsService.Instance.CustomData(eventName, parameters);
    }

    public void Clear()
    {
    }
}