// based on this: https://developer.vuforia.com/forum/faq/unity-how-can-i-popup-gui-button-when-target-detected
// aso this: https://developer.vuforia.com/forum/faq/unity-how-can-i-play-audio-when-targets-get-detected
using UnityEngine;
using System.Collections;
using Vuforia;

public class forTracker1 : MonoBehaviour, ITrackableEventHandler {
	private TrackableBehaviour mTrackableBehaviour; // trackers
	//float Player1LifePoints = 4000f;
	//float Player2LifePoints = 4000;
	//public GUIStyle MyGUIstyle;
	private bool mShowGUIButton  = false; // GUI 
	private Rect attackButton = new Rect(30,30,120,40); // GUI
	private Rect defenseButton = new Rect(30,80,120,40); // GUI

	private GameObject Spark, Cyclone; //effects, monsters

	void Start()
	{
		/*Spark = transform.Find("Spark").gameObject; // effects
		Spark.SetActive(false); 	
		// effects
		Cyclone = transform.Find("Cyclone").gameObject; // effects
		Cyclone.SetActive(false); */


		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

    void Update()
    {
        //float Dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        //Debug.Log("Position 1: " + Dist);
    }
	
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            // Play audio when target is found
            //Debug.Log("Play Sound");
            mShowGUIButton  = true;
			//audio.Play();
		}
		else
		{
			// Stop audio when target is lost
		//	Debug.Log("Stop Sound");
			mShowGUIButton  = false;
			//audio.Stop();
		}
	}  
	void OnGUI() {
	//	GUI.Label(new Rect (Screen.width - (Screen.width * 0.9f) - 50.0f, Screen.height - Screen.height, 100.0f, 30.0f), "Player 1 Score: " + Player1LifePoints, MyGUIstyle);
	//	GUI.Label(new Rect (Screen.width - (Screen.width * 0.3f) - 50.0f, Screen.height - Screen.height, 100.0f, 30.0f), "Player 1 Score: " + Player2LifePoints, MyGUIstyle);

		if (mShowGUIButton) {
			// draw the GUI button
			if (GUI.Button(attackButton, "ATK/3000")) {
				Debug.Log ("Attack!");
				Cyclone.SetActive(false);
				Spark.SetActive(true);
				StartCoroutine(StartWait());
				// do something on button click 
			}
			if (GUI.Button(defenseButton, "DEF/2500")) {
				Debug.Log ("Defense!");
				Cyclone.SetActive(true);
				//Spark.SetActive(false);
			}
		}
	}

	IEnumerator StartWait() 
	{
		yield return StartCoroutine(Wait(1.50F));
		Spark.SetActive(false);
		Cyclone.SetActive(false);
	}
	

	IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}
	
}


