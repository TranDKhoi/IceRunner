using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager :MonoBehaviour, IUnityAdsInitializationListener,IUnityAdsShowListener
{
    private static AdManager _ins;
    public static AdManager ins
    {
        get { return _ins; }
    }

    [SerializeField] private string gameID;
    [SerializeField] private string rewardedVideoPlacementId;
    [SerializeField] private bool testMode;
    public Action OnAdDone;

    private void Awake()
    {
        _ins = this;
        Advertisement.Initialize(gameID, testMode,this);
    }

    public void ShowAd()
    {
        ShowOptions so = new ShowOptions();
        Advertisement.Show(rewardedVideoPlacementId, so, this);
    }

    public void OnInitializationComplete()
    {
      
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        
    }


    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        OnAdDone.Invoke();
    }
}
