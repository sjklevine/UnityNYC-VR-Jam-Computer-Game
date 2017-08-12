using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTimedToggleVisibilty : MonoBehaviour {
	public GameObject thingToToggle;
	public bool runOnStart;
	public bool stayHidden;
	private float timeToFirstShow = 1.0f;
	private float timeVisible = 0.5f;
	private float timeHidden = 0.5f;
	private int numToggles = 3;

	void Start () {
		if (runOnStart) {
			BeginToggling ();
		}
	}

	public void BeginToggling() {
		StartCoroutine (Run ());		
	}

	private IEnumerator Run() {
		yield return new WaitForSeconds (timeToFirstShow);
		thingToToggle.SetActive (true);

		for (int i = 0; i < numToggles; i++) {
			thingToToggle.SetActive (true);
			yield return new WaitForSeconds (timeVisible);
			thingToToggle.SetActive (false);
			yield return new WaitForSeconds (timeHidden);
		}
		if (stayHidden) {
			GameObject.Destroy (this.gameObject);
		} else {
			thingToToggle.SetActive (true);
		}
	}
}
