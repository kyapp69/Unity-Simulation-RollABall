using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine.Diagnostics;
using UnityEngine;

// PickupStats keeps track of the Roll-A-Ball simulation state and
// determines when it should crash or quit.
public class PickupStats : MonoBehaviour {
    public static Text countText;
	public static Text winText;
    private static int count;
	private static int numToWin;
	public static bool timedSim;
	public static float forceCrashAfterSeconds;
	public static float quitAfterSeconds;
	private static float simElapsedSeconds;
    // Start is called before the first frame update
    void Start() {
		numToWin = GameObject.FindGameObjectsWithTag("Pickup Spawner")[0].GetComponent<PickupSpawner>().numPickups;

        // Set the count to zero
		count = 0;
		countText = GameObject.FindGameObjectsWithTag("Count Text")[0].GetComponent<Text>();
		winText = GameObject.FindGameObjectsWithTag("Win Text")[0].GetComponent<Text>();

		// Run the SetCountText function to update the UI (see below)
		SetCountText();

		// Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winText.text = "";
		if (forceCrashAfterSeconds > 0 || quitAfterSeconds > 0) {
			timedSim = true;
		}
    }

	public void Update() {
		simElapsedSeconds += Time.deltaTime;
		if (timedSim) {
			if (forceCrashAfterSeconds > 0 && simElapsedSeconds > forceCrashAfterSeconds) {
				forceCrashAfterSeconds = 0;
				Utils.ForceCrash(ForcedCrashCategory.Abort);
			}
			if (quitAfterSeconds > 0 && simElapsedSeconds > quitAfterSeconds) {
				Application.Quit();
			}
		}
	}

    public static void IncrementCount() {
        count += 1;
        SetCountText();
    }
	private static void SetCountText() {
		// Update the text field of our 'countText' variable
		countText.text = "Count: " + count.ToString ();
		Debug.Log("Count: " + count.ToString ());

		// Check if our 'count' is equal to or exceeded 12
		if (count >= numToWin && !timedSim){
			// Set the text value of our 'winText'
			winText.text = "You Win!";
			Application.Quit();
		}
	}
}
