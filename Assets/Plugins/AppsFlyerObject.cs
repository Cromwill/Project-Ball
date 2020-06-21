using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AppsFlyerObject : MonoBehaviour
{
    void Start()
    {
        /* Mandatory - set your AppsFlyer’s Developer key. */
        AppsFlyer.setAppsFlyerKey("TcxKZZu9W8GxnxKSC7tFTE");

        /* For detailed logging */
        /* AppsFlyer.setIsDebug (true); */
#if UNITY_IOS
      /* Mandatory - set your apple app ID
      NOTE: You should enter the number only and not the "ID" prefix */
      AppsFlyer.setAppID ("YOUR_APP_ID_HERE");
      AppsFlyer.getConversionData();
      AppsFlyer.trackAppLaunch ();
#elif UNITY_ANDROID
        /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
        AppsFlyer.setAppID("ru.IJunior.ProjectBall");
        AppsFlyer.init("TcxKZZu9W8GxnxKSC7tFTE", "AppsFlyerTrackerCallbacks");
#endif


    }
}
