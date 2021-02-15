
namespace AppLauncherPlugin
{
	using UnityEngine;
	using System.Collections.Generic;

	#if UNITY_IOS
	using System.Runtime.InteropServices;
	#endif

	public class AppLauncher
	{
		#if UNITY_ANDROID

		public enum GalleryContent
		{
			DEFAULT,
			IMAGE,
			VIDEO
		}


		static AndroidJavaClass _plugin;

		static AppLauncher()
		{
			if(Application.platform == RuntimePlatform.Android)
				_plugin = new AndroidJavaClass("com.astricstore.applauncher.AppLauncherActivity");
		}

		public static bool LaunchLinkedInApp(string profileId, LinkedInProfileType profileType, string callback)
		{
			if(Application.platform == RuntimePlatform.Android)
				return _plugin.CallStatic<bool>("LaunchLinkedInApp",profileId,(int)profileType,callback);

		return false;
		}
				

		public static bool LaunchFacebookApp(string id, FacebookProfileType pageType, string callback)
		{
			if(Application.platform == RuntimePlatform.Android)
				return _plugin.CallStatic<bool>("LaunchFacebookApp",id,(int)pageType,callback);

		return false;
		}

		public static bool LaunchTwitter(string screenName, string callback)
		{
			if(Application.platform == RuntimePlatform.Android)
				return _plugin.CallStatic<bool>("LaunchTwitter",screenName,callback);
			
			return false;
		}

		public static bool LaunchSMS(string phoneNo, string message, string callback)
		{
			if(Application.platform == RuntimePlatform.Android)
				return _plugin.CallStatic<bool>("LaunchSMS",phoneNo, message ,callback);
			
			return false;
		}

		public static bool LaunchEmailApp(string emailId, string subject, string message, string callback)
		{
			if(Application.platform == RuntimePlatform.Android)
				return _plugin.CallStatic<bool> ("LaunchEmailApp",emailId,subject,message,callback);

		return false;
		}


		public static bool LaunchSettings(SettingType type, string callback)
		{
			if(Application.platform == RuntimePlatform.Android)
				return _plugin.CallStatic<bool>("OpenSettings",(int)type,callback);

		return false;
		}


		public static bool LaunchApp(string packageName, string callback)
		{
			if(Application.platform == RuntimePlatform.Android)
				return _plugin.CallStatic<bool> ("LaunchApp",packageName,callback);

		return false;
		}

		public static bool LaunchWhatsApp(string msg, string callback)
		{
			if (Application.platform == RuntimePlatform.Android) {
				if (!string.IsNullOrEmpty (msg.Trim())) {
					return _plugin.CallStatic<bool> ("LaunchWhatsApp", msg, callback);				
				} else
					return _plugin.CallStatic<bool> ("LaunchWhatsApp", callback);
			}
			return false;
		}

		public static bool LaunchAppWithParameters(string packageName, Dictionary<string, string> parameters, string callback)
		{
			string strParams = "";
			if (parameters != null) {
				foreach (KeyValuePair<string,string> pair in parameters) {
					strParams += pair.Key + ":" + pair.Value + ";";
				}		
			}

			if (!string.IsNullOrEmpty (strParams.Trim ())) {
				return _plugin.CallStatic<bool> ("LaunchAppWithParams", packageName, strParams, callback);
			} else {
				return LaunchApp (packageName, callback);
			}
		return false;
		}

		public static bool LaunchGallery(GalleryContent contentType, string callback)
		{
			if(Application.platform == RuntimePlatform.Android)
				return _plugin.CallStatic<bool> ("LaunchGallery", (int) contentType, callback);

			return false;
		}

	
	#endif


	#if UNITY_IOS

		[DllImport ("__Internal")]
		private static extern bool _LaunchFacebook (string id, int pageType, string callback);

		[DllImport ("__Internal")]
		private static extern bool _LaunchLinkedIn (string id, int pageType, string callback);

		[DllImport ("__Internal")]
		private static extern bool _LaunchEmail (string emailId, string subject,string body, string callback);

		[DllImport ("__Internal")]
		private static extern bool _LaunchSettings (int type, string callback);

		[DllImport ("__Internal")]
		private static extern bool _LaunchApp (string urlScheme, string callback);

		[DllImport ("__Internal")]
		private static extern bool _LaunchWhatsApp (string urlScheme, string callback);

		[DllImport ("__Internal")]
		private static extern bool _LaunchTwitter (string screenName, string callback);

		[DllImport ("__Internal")]
		private static extern bool _LaunchSMS (string phoneNo, string message, string callback);

		public static bool LaunchFacebookApp(string id, FacebookProfileType pageType,string callback)
		{
			if(Application.platform == RuntimePlatform.IPhonePlayer)
					return _LaunchFacebook (id, (int)pageType, callback);

			return false;
		}

		public static bool LaunchTwitter(string screenName, string callback)
		{
			if(Application.platform == RuntimePlatform.IPhonePlayer)
				return _LaunchTwitter (screenName, callback);
			
			return false;
		}
		
		public static bool LaunchSMS(string phoneNo, string message, string callback)
		{
			if(Application.platform == RuntimePlatform.IPhonePlayer)
				return _LaunchSMS (phoneNo,message, callback);
			
			return false;
		}

		public static bool LaunchLinkedInApp(string id, LinkedInProfileType pageType,string callback)
		{
			if(Application.platform == RuntimePlatform.IPhonePlayer)
					return _LaunchLinkedIn (id, (int)pageType, callback);

			return false;
		}

		public static bool LaunchEmailApp(string emailId, string subject,string body, string callback)
		{
			if(Application.platform == RuntimePlatform.IPhonePlayer)
					return _LaunchEmail (emailId, subject, body, callback);

			return false;
		}

		public static bool LaunchSettings(SettingType type, string callback)
		{
			if(Application.platform == RuntimePlatform.IPhonePlayer)
				return _LaunchSettings ((int)type, callback);

			return false;
		}

		public static bool LaunchApp(string urlScheme, string callback)
		{
			if(Application.platform == RuntimePlatform.IPhonePlayer)
					return _LaunchApp (urlScheme, callback);

			return false;
		}

		public static bool LaunchWhatsApp(string msg, string callback)
		{
			if(Application.platform == RuntimePlatform.IPhonePlayer)
					return	_LaunchWhatsApp (msg, callback);

			return false;
		}
	#endif
	}
	public enum SettingType
	{
		DEFAULT,
		SOUND_SETTING,
		ACCESSBILITY_SETTING,
		AIRPLANE_MODE_SETTING,
		DATE_SETTING,
		BLUETOOTH_SETTING,
		WIFI,
		KEYBOARD,
	}

	public enum LinkedInProfileType
	{
			INDIVIDUAL,
			COMPANY
	}

	public enum FacebookProfileType
	{
			PROFILE,
			PAGE,
			GROUP
	}
}
