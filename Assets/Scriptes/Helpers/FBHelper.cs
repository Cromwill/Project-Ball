using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine;

public class FBHelper : MonoBehaviour
{
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    public void LogCompletedTutorialEvent(string contentId, bool success)
    {
        var parameters = new Dictionary<string, object>();
        parameters[AppEventParameterName.ContentID] = contentId;
        parameters[AppEventParameterName.Success] = success ? 1 : 0;
        FB.LogAppEvent(
            AppEventName.CompletedTutorial,
            null,
            parameters
        );
    }

    public void AddClick(string adType)
    {
        var tutParams = new Dictionary<string, object>();
        tutParams["EVENT_PARAM_AD_TYPE"] = adType;

        //FB.LogAppEvent(
        //    AppEventName.ViewedContent,
        //    parameters: tutParams
        //);

        FB.LogAppEvent("EVENT_NAME_AD_CLICK", null, tutParams);
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                FB.Init(() => {
                    FB.ActivateApp();
                });
            }
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    private void Test()
    {
        
    }
}
