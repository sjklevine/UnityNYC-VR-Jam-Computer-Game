using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTimedToggleVisibilty : MonoBehaviour {
	public GameObject thingToToggle;
	public bool runOnStart;
	public bool stayHidden;
	public int numToggles = 3; // 0 = infinite
	public float timeToFirstShow = 1.0f;
	public float timeVisible = 0.5f;
	public float timeHidden = 0.5f;

	void Start () {
		if (runOnStart) {
			BeginToggling ();
		}
	}

	public void BeginToggling() {
		StartCoroutine (Run ());		
	}
	public void StopAndHide() {
		StopCoroutine ("Run");
		thingToToggle.SetActive (false);
	}
	private IEnumerator Run() {
		yield return new WaitForSeconds (timeToFirstShow);
		thingToToggle.SetActive (true);

		// Hmm... this feels like a bad idea... but it should work...
		if (numToggles == 0) {
			numToggles = int.MaxValue;
		}

		// Go through a loop, toggling visibility
		for (int i = 0; i < numToggles; i++) {
			thingToToggle.SetActive (true);
			yield return new WaitForSeconds (timeVisible);
			thingToToggle.SetActive (false);
			yield return new WaitForSeconds (timeHidden);
		}
		if (!stayHidden) {
			thingToToggle.SetActive (true);
		}
	}
}
