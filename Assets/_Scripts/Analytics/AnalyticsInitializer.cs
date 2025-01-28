using System;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class AnalyticsInitializer : MonoBehaviour
{
    private void Start()
    {
        InitializeAnalytics();
    }

    async void InitializeAnalytics()
    {
        try
        {
            await UnityServices.InitializeAsync();
            AnalyticsService.Instance.StartDataCollection();
            Debug.Log("Analytics Initialized");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error initializing Analytics Service {e}");
        }
    }
}
