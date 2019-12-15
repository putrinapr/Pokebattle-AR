// based on this: https://developer.vuforia.com/forum/faq/unity-how-can-i-popup-gui-button-when-target-detected
// aso this: https://developer.vuforia.com/forum/faq/unity-how-can-i-play-audio-when-targets-get-detected
using UnityEngine;
using System.Collections;
using Vuforia;

public class forTracker2 : MonoBehaviour, ITrackableEventHandler {
	private TrackableBehaviour mTrackableBehaviour; // trackers
	//float PlayerLifePoints = 4000f;
	//public GUIStyle MyGUIstyle;
	private bool mShowGUIButton  = false; // GUI 
	private Rect attackButton2 = new Rect(30, 30, 120, 40); // GUI
	private Rect defenseButton2 = new Rect(30, 80, 120, 40); // GUI

	private GameObject Spark, Cyclone; //effects, monsters

	void Start()
	{
		/*Spark = transform.Find("Spark").gameObject; // effects
		Spark.SetActive(false); 	

		Blast = transform.Find("Blast").gameObject; // effects
		Blast.SetActive(false); 
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
        //Debug.Log("Position 2: " + Dist);
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
            //	Debug.Log("Play Sound");
            mShowGUIButton  = true;
			//defenseButton = true;
			//audio.Play();
		}
		else
		{
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            // Stop audio when target is lost
            //	Debug.Log("Stop Sound");
            mShowGUIButton  = false;
			//defenseButton = false;
			//audio.Stop();
		}
	}  

	void OnGUI() {
		if (mShowGUIButton) {
			// draw the GUI button
			if (GUI.Button(attackButton2, "ATK/300")) {
				Debug.Log ("Attack!");
				Cyclone.SetActive(false);
				Spark.SetActive(true);
				StartCoroutine(StartWait());


				// do something on button click 
			}
			if (GUI.Button(defenseButton2, "DEF/200")) {
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


