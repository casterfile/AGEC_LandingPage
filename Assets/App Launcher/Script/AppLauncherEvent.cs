namespace AppLauncherPlugin
{
	using UnityEngine;
	using System.Collections;
	public class AppLauncherEvent : MonoBehaviour {
		
		string log = "Log:";
		void OnSuccess(string message)
		{
			log += "\n" + message;
		}

		void OnError(string message)
		{
			log += "\n" + message;
		}

		void OnGUI()
		{
			GUILayout.Label (log);
		}

	}
}