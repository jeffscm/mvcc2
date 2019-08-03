using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PushManager : AppElement {

	// Use this for initialization
	void Start () {

        //IOSNativeUtility.SetApplicationBagesNumber(0);


		//OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.VERBOSE, OneSignal.LOG_LEVEL.NONE);

  //      // The only required method you need to call to setup OneSignal to receive push notifications.
  //      // Call before using any other methods on OneSignal.
  //      // Should only be called once when your app is loaded.
  //      // OneSignal.Init(OneSignal_AppId);
  //      OneSignal.StartInit("xxxxxx")
  //          .HandleNotificationReceived(HandleNotificationReceived)
  //          .HandleNotificationOpened(HandleNotificationOpened)
  //               .EndInit();

		//OneSignal.IdsAvailable(GetID);
       
		//#if UNITY_EDITOR
		//app.model.SetPush("TEST");
		//#endif

        //OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
	}

    public static Action<int, string> OnReceivePushText;
    public static Action<int> OnReceivePushChat;

	private void GetID(string userID, string pushToken) {
		//Debug.LogWarning("UserID:"  + userID);
		//Debug.LogWarning("PushToken:" + pushToken);

		//app.model.SetPush(userID);

	}
	
    public static void RemovePush ()
    {
        
    }

    public void SetSilentNotification(bool silent)
    {

        //if (!silent)
        //    OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
        //else
            //OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.None;
        
    }

	// Called when your app is in focus and a notificaiton is recieved.
	// The name of the method can be anything as long as the signature matches.
	// Method must be static or this object should be marked as DontDestroyOnLoad
	//private static void HandleNotificationReceived(OSNotification notification) {
		//OSNotificationPayload payload = notification.payload;

  //      Debug.Log("ReceivedPush");

		//string message = payload.body;

		//print("GameControllerExample:HandleNotificationReceived: " + message);
		//print("displayType: " + notification.displayType);
		//string extraMessage = "Notification received with text: " + message;

        ////TODO when app is open set this as last message from user...

        //Dictionary<string, object> additionalData = payload.additionalData;

       
        //if (additionalData != null && additionalData.ContainsKey("chatUser"))
        //{
        //    int temp = 0;

        //    if (additionalData["chatUser"].GetType() == typeof(string))
        //    {
        //        temp = int.Parse(additionalData["chatUser"].ToString());
        //    }
        //    else
        //    {
        //        temp = (int)(long)additionalData["chatUser"];
        //    }

        //    if (OnReceivePushText != null) OnReceivePushText(temp, message);
        //}

        //if (additionalData == null)
        //    Debug.Log("[HandleNotificationReceived] Additional Data == null");
        //else
            //Debug.Log("[HandleNotificationReceived] message " + message + ", additionalData: " + Newtonsoft.Json.JsonConvert.SerializeObject(additionalData));
    //}

	// Called when a notification is opened.
	// The name of the method can be anything as long as the signature matches.
	// Method must be static or this object should be marked as DontDestroyOnLoad
	//public static void HandleNotificationOpened(OSNotificationOpenedResult result) {

  //      Debug.Log("OpenedPush");
       
		//OSNotificationPayload payload = result.notification.payload;
		////string message = payload.body;
		////string actionID = result.action.actionID;
		////print("GameControllerExample:HandleNotificationOpened: " + message);
		////extraMessage = "Notification opened with text: " + message;

        //string message = payload.body;

        //print("GameControllerExample:HandleNotificationReceived: " + message);
        //string extraMessage = "Notification received with text: " + message;

        //Dictionary<string, object> additionalData = payload.additionalData;

        //if (additionalData != null && additionalData.ContainsKey("chatUser"))
        //{            
            

        //    int temp = 0;

        //    if (additionalData["chatUser"].GetType() == typeof(string))
        //    {
        //        temp = int.Parse(additionalData["chatUser"].ToString());
        //    }
        //    else
        //    {
        //        temp = (int)(long)additionalData["chatUser"];
        //    }
        //    if (OnReceivePushChat != null) OnReceivePushChat(temp);
        //    PlayerPrefs.SetInt("PUSHACTIVE", temp);

        //    if (App.instance != null)
        //    {
                
        //        if (App.instance.arViewState != NOTIFYEVENT.TOPMENUMSGS)
        //        {
        //            App.instance.Notify(NOTIFYEVENT.TOPMENUNEWMSG, null, null);
        //        }
        //    }

        //}

        //if (additionalData == null)
        //    Debug.Log("[HandleNotificationReceived] Additional Data == null");
        //else
            //Debug.Log("[HandleNotificationReceived] message " + message + ", additionalData: " + Newtonsoft.Json.JsonConvert.SerializeObject(additionalData));
    //}


    public static void PostNotification (List<string> users, string msg, int userId)
    {
        //var notification = new Dictionary<string, object>();
        //notification["contents"] = new Dictionary<string, string>() { { "en", msg } };

        //notification["include_player_ids"] = users;
        //notification["ios_badgeType"] = "Increase";
        //notification["ios_badgeCount"] = 1;

        //notification["data"] = new Dictionary<string, object>()
        //{

        //    {"chatUser", userId},

        //};

        //// Example of scheduling a notification in the future.
        //notification["send_after"] = System.DateTime.Now.ToUniversalTime().AddSeconds(0).ToString("U");

        //OneSignal.PostNotification(notification, (responseSuccess) => {

        //    Debug.Log("Notification posted successful! Delayed by about 30 secounds to give you time to press the home button to see a notification vs an in-app alert.\n");
        
        //}, (responseFailure) => {

        //    Debug.Log("Notification failed to post:\n");
        
        //});
    }
}
