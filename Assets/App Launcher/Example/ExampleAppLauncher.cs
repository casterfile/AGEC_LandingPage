using UnityEngine;
using System.Collections;

using AppLauncherPlugin;

public class ExampleAppLauncher : MonoBehaviour {

#if UNITY_ANDROID
	string log = "Log:";

	// ********** called from native code ****************//
	void OnSuccess(string message)
	{
		log += "\n" + message;
	}
	
	void OnError(string message)
	{
		log += "\n" + message;
	}
	//****************************************************//

	void OnGUI()
	{
		GUILayout.Label (log);

		float x = 10;
		float y = 10;
		float width = 170, height = 70;

		if(GUI.Button(new Rect(x,y,width,height),"Facebook"))
		{
			log += "\n" + AppLauncher.LaunchFacebookApp("1300757898",FacebookProfileType.PROFILE,gameObject.name);
		}
		x+=width + 20;

		if(GUI.Button(new Rect(x,y,width,height),"Launch WhatsApp"))
		{
			log += "\n" +AppLauncher.LaunchWhatsApp("",gameObject.name);
		}

		x = 10;
		y+=height+20;

		if(GUI.Button(new Rect(x,y,width,height),"LinkedIn Profile"))
		{
			log += "\n" +AppLauncher.LaunchLinkedInApp("devesh-pandey-b2089738",LinkedInProfileType.INDIVIDUAL,gameObject.name);
		}

		x+=width + 20;

		if(GUI.Button(new Rect(x,y,width,height),"LinkedIn Company"))
		{
			log += "\n" +AppLauncher.LaunchLinkedInApp("versatile-techno",LinkedInProfileType.COMPANY,gameObject.name);
		}

		x = 10;
		y+=height+20;

		if(GUI.Button(new Rect(x,y,width,height),"Launch Twitter"))
		{
			log += "\n" +AppLauncher.LaunchTwitter("devesh_pandey19",gameObject.name);
		}

						
		x+=width + 20;

		if(GUI.Button(new Rect(x,y,width,height),"Launch SMS"))
		{
			log += "\n" +AppLauncher.LaunchSMS("9898989898", "Hello World!",gameObject.name);
		}


		x = 10;
		y+=height+20;

		if(GUI.Button(new Rect(x,y,width,height),"Email"))
		{
			log += "\n" +AppLauncher.LaunchEmailApp("example@gmail.com","Demo Email","This is message body.",gameObject.name);
		}


		x+=width + 20;

		
		if(GUI.Button(new Rect(x,y,width,height),"Launch App (Skype)"))
		{
			if(Application.platform == RuntimePlatform.Android)
				log += "\n" +AppLauncher.LaunchApp("com.skype.raider",gameObject.name);
			else if(Application.platform == RuntimePlatform.IPhonePlayer)
				log += "\n" +AppLauncher.LaunchApp("skype://",gameObject.name);
		}

		x = 10;
		y+=height+20;

		if(GUI.Button(new Rect(x,y,width,height),"Settings"))
		{
			log += "\n" +AppLauncher.LaunchSettings(SettingType.DATE_SETTING,gameObject.name);
		}	

		x+=width + 20;
		if(GUI.Button(new Rect(x,y,width,height),"Gallery"))
		{
			log += "\n" +AppLauncher.LaunchGallery(AppLauncher.GalleryContent.DEFAULT,gameObject.name);
		}

	}
    #endif
}
