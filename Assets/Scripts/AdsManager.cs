using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Advertisements;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;

    [Header("Config")]
    [SerializeField] private string gameID = "";
    [SerializeField] private bool testMode = true;
    [SerializeField] private string rewardedVideoPlacementId;
    [SerializeField] private string regularPlacementId;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        Advertisement.Initialize(gameID, testMode);
    }

    public void ShowRegularAd(Action<ShowResult> callback)
    {
#if UNITY_ADS
        if (Advertisement.IsReady(regularPlacementId))
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = callback;
            Advertisement.Show(regularPlacementId, so);
        } else
        {
            //Debug.Log("Ad no ready");
            UImanager.instance.OpenEndPanel();
        }
#else
        Debug.Log("Ads not supported");
#endif
    }

    public void ShowRewardedAd(Action<ShowResult> callback)
    {
#if UNITY_ADS
        if (Advertisement.IsReady(rewardedVideoPlacementId))
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = callback;
            Advertisement.Show(rewardedVideoPlacementId, so);
        }
        else
        {
            //Debug.Log("Ad no ready");
            UImanager.instance.OpenEndPanel();
        }
#else
        Debug.Log("Ads not supported");
#endif
    }
}
