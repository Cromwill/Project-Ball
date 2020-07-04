using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppsFlyerSDK;
using System.Diagnostics;
using UnityEngine.UI;

// This class is intended to be used the the AppsFlyerObject.prefab

public class AppsFlyerObjectScript : MonoBehaviour, IAppsFlyerConversionData
{

    // These fields are set from the editor so do not modify!
    //******************************//
    public string devKey;
    public string appID;
    public bool isDebug;
    public bool getConversionData;
    public string[] urls;
    //******************************//
    public Text _testViewer;

    void Start()
    {
        // These fields are set from the editor so do not modify!
        //******************************//
        AppsFlyer.setIsDebug(isDebug);
        AppsFlyer.initSDK(devKey, appID, getConversionData ? this : null);
        AppsFlyer.setResolveDeepLinkURLs(urls);
        AppsFlyer.getConversionData(this.gameObject.name);
        //******************************//
        AppsFlyer.startSDK();
    }

    void Update()
    {

    }

    // Mark AppsFlyer CallBacks
    public void onConversionDataSuccess(string conversionData)
    {
        AppsFlyer.AFLog("didReceiveConversionData", conversionData);
        Dictionary<string, object> conversionDataDictionary = AppsFlyer.CallbackStringToDictionary(conversionData);
        // add deferred deeplink logic here
        if (_testViewer.text != "")
            _testViewer.text = "(second)" + conversionData;
    }

    public void onConversionDataFail(string error)
    {
        AppsFlyer.AFLog("didReceiveConversionDataWithError", error);
    }

    public void onAppOpenAttribution(string attributionData)
    {
        AppsFlyer.AFLog("onAppOpenAttribution", attributionData);
        Dictionary<string, object> attributionDataDictionary = AppsFlyer.CallbackStringToDictionary(attributionData);
        _testViewer.text = attributionData;
        // add direct deeplink logic here
    }

    public void onAppOpenAttributionFailure(string error)
    {
        AppsFlyer.AFLog("onAppOpenAttributionFailure", error);
    }
}
