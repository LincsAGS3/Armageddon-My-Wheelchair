﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;//added

public class MenuOptionsSelectionScript : MonoBehaviour
{
	

		static string[] AspectRatios = {"5:4","4:3","16:10","5:3","16:9",};
		static string[] Resolutions = {
				"1024x768",
				"1280x1024",
				"1680,1050",
				"1920x1080",
				"1920x1200"
		};
		static bool FullScreen;
		static string[] Blood = {"Off","Red","Green"};
		static GameObject AspectRatioObject;
		static GameObject ResolutionsObject;
		static GameObject FullScreenObject;
		static GameObject BloodObject;
		static int AspectRatioCounter;
		static int ResolutionsCounter;
		static int BloodCounter;
		static Camera camera;
		static Camera Backgroundcamera;
		public static int SubSelection;
		// Use this for initialization
		void Start ()
		{
				FullScreen = false;
				AspectRatioCounter = 0;
				ResolutionsCounter = 0;
				BloodCounter = 0;

				AspectRatioObject = GameObject.Find ("Aspect Ratio");
				ResolutionsObject = GameObject.Find ("Resolution");
				FullScreenObject = GameObject.Find ("Full Screen");
				BloodObject = GameObject.Find ("Blood");

				//defualt settings
				AspectRatioObject.GetComponent<Text> ().text = AspectRatios [AspectRatioCounter];
				ResolutionsObject.GetComponent<Text> ().text = Resolutions [ResolutionsCounter];
				FullScreenObject.GetComponent<Text> ().text = FullScreen.ToString ();
				BloodObject.GetComponent<Text> ().text = Blood [BloodCounter];

				camera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		}
	
		public static void ProcessSelection (int CurrentSelection)
		{
				switch (CurrentSelection) {
				case 0://aspect ratio
						if (SubSelection > 4)
								SubSelection = 0;
						if (SubSelection < 0)
								SubSelection = 4;
						AspectRatioCounter = SubSelection;
						Debug.Log ("Aspect Ratio set to : " + AspectRatioCounter);
						AspectRatioObject.GetComponent<Text> ().text = AspectRatios [AspectRatioCounter];
						break;
				case 1://resolution
						if (SubSelection > 3)
								SubSelection = 0;
						if (SubSelection < 0)
								SubSelection = 3;
						ResolutionsCounter = SubSelection;
						Debug.Log ("Resolution set to : " + ResolutionsCounter);
						ResolutionsObject.GetComponent<Text> ().text = Resolutions [ResolutionsCounter];
						break;
				case 2://full screen
						if (SubSelection > 1)
								SubSelection = 0;
						if (SubSelection < 0)
								SubSelection = 1;
						FullScreen = !FullScreen;
						FullScreenObject.GetComponent<Text> ().text = FullScreen.ToString ();
						break;
				case 3://Blood
						if (SubSelection > 3)
								SubSelection = 0;
						if (SubSelection < 0)
								SubSelection = 3;
						BloodCounter = SubSelection;
						BloodObject.GetComponent<Text> ().text = Blood [BloodCounter];
						break;
				case 4:
			//change aspect ratio resolution full screen and blood here

			//aspect ratio
						switch (AspectRatioCounter) {
						case 0://5:4
								Debug.Log ("Aspect Ratio 5:4 Selected");
								AspectRatioChange (1.25f);
								break;
						case 1://4:3
								Debug.Log ("Aspect Ratio 4:3 Selected");
								AspectRatioChange (1.3333f);
								break;	
						case 2://16:10
								Debug.Log ("Aspect Ratio 16:10 Selected");
								AspectRatioChange (1.6f);
								break;
						case 3://5:3
								Debug.Log ("Aspect Ratio 5:3 Selected");
								AspectRatioChange (1.66666666667f);
								break;
						case 4://16:9
								Debug.Log ("Aspect Ratio 16:9 Selected");
								AspectRatioChange (1.78f);
								break;
						}

			//resolution and full screen "Screen.fullScreen = FullScreen;" would be just full screen
						switch (ResolutionsCounter) {
						case 0: //1024x768
								Screen.SetResolution (1024, 768, FullScreen);
								break;
						case 1: //1280x1024
								Screen.SetResolution (1280, 1024, FullScreen);
								break;
						case 2://1680,1050
								Screen.SetResolution (1680, 1050, FullScreen);
								break;
						case 3://1920x1080
								Screen.SetResolution (1920, 1080, FullScreen);
								break;
						case 4://1920x1200
								Screen.SetResolution (1920, 1200, FullScreen);
								break;
						}

			//Blood
						switch (BloodCounter) {
						case 0://Red

								break;
						case 1://Green
				
								break;
						case 2://Off
				
								break;
						}
						MenuStateScript.MenuStateChange (0); // return to menu
						break;
				}		
		}

		public static void AspectRatioChange (float TargetAspectRatio)
		{
				float CurrentAspectRatio = (float)Screen.width / Screen.height;

				// use a full screen Rect; in case it was set to something else previously
				if ((int)(CurrentAspectRatio * 100) / 100.0f == (int)(TargetAspectRatio * 100) / 100.0f) {
						camera.rect = new Rect (0.0f, 0.0f, 1.0f, 1.0f);
				}
				// Pillarbox
				if (CurrentAspectRatio > TargetAspectRatio) {
						float inset = 1.0f - TargetAspectRatio / CurrentAspectRatio;
						camera.rect = new Rect (inset / 2, 0.0f, 1.0f - inset, 1.0f);
				}
		// Letterbox
		else {
						float inset = 1.0f - CurrentAspectRatio / TargetAspectRatio;
						camera.rect = new Rect (0.0f, inset / 2, 1.0f, 1.0f - inset);
				}
				if (!Backgroundcamera) { //if there is no background camera the following happens
						// Make a new camera behind the normal camera which displays black; otherwise the unused space is undefined
						Backgroundcamera = new GameObject ("BackgroundCamera", typeof(Camera)).camera; // creates a new background camera 
						Backgroundcamera.depth = int.MinValue;//the camera deph is set to the lowest possibly value
						Backgroundcamera.clearFlags = CameraClearFlags.SolidColor;//set the flag to solid colour
						Backgroundcamera.backgroundColor = Color.black;// set the colour to black
						Backgroundcamera.cullingMask = 0;// turn culling off
				}
		}
}
