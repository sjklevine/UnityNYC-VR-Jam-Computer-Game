using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollerEndingScript : MonoBehaviour {
	public SimpleTimedToggleVisibilty endgameText;
    public AudioSource Soundtrack;
 
	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("Player")) {
			endgameText.BeginToggling ();
            endgameText.GetComponent<AudioSource>().Play();
            Soundtrack.Stop();
		}
	}
}
