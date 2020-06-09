using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public string AppId, Bannerid, interstitialId,RewardId;
    public bool testdevice = false;
    public static AdManager instance;
    BannerView bannerView;
    InterstitialAd interstitialAd;
    RewardBasedVideoAd rewardAd;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(inititilaize => { });
        this.CreateBanner(CreateRequest());
        this.CreateInterstitialAd(CreateRequest());
        this.rewardAd = RewardBasedVideoAd.Instance;
        this.CreateRewardAd(CreateRequest());

        this.rewardAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
        this.rewardAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
        this.rewardAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        this.rewardAd.OnAdClosed += HandleRewardBasedVideoClosed;
    }
    private AdRequest CreateRequest()
    {
        AdRequest request;
        Debug.Log(testdevice);
        if(testdevice)
        {
            request = new AdRequest.Builder().AddTestDevice(SystemInfo.deviceUniqueIdentifier).Build();
        }
        else
            request = new AdRequest.Builder().Build();
        return request;
    }
    // Update is called once per frame
    public void CreateInterstitialAd(AdRequest adRequest)
    {
        this.interstitialAd = new InterstitialAd(interstitialId);
        this.interstitialAd.LoadAd(adRequest);
    }
    public void CreateRewardAd(AdRequest adRequest)
    {      
        this.rewardAd.LoadAd(adRequest,RewardId);
    }

    private void HandleRewardBasedVideoClosed(object sender, EventArgs e)
    {
        CreateRewardAd(CreateRequest());
    }

    private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Debug.Log("Failed to load");
    }

    private void HandleRewardBasedVideoLoaded(object sender, EventArgs e)
    {
        Debug.Log("Ad loaded");
    }

    public void showRewardAd()
    {
        if(this.rewardAd.IsLoaded())
        {
            this.rewardAd.Show();
        }
        this.rewardAd.LoadAd(CreateRequest(), RewardId);
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        Lives.instance.gameOver.SetActive(false);
        Lives.instance.Reward.SetActive(true);
    }
    public void showInterstitialAd()
    {
        if(this.interstitialAd.IsLoaded())
        {
            this.interstitialAd.Show();
        }
        this.interstitialAd.LoadAd(CreateRequest());
    }

    public void CreateBanner(AdRequest adRequest)
    {
        this.bannerView = new BannerView(Bannerid,AdSize.SmartBanner,AdPosition.Bottom);
        this.bannerView.LoadAd(adRequest);
        HideBanner();
    }
    public void HideBanner()
    {
        bannerView.Hide();
    }
    public void showBannerAd()
    {
        bannerView.Show();
    }
}
