using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollerEndingScript : MonoBehaviour {
	public SimpleTimedToggleVisibilty endgameText;

	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("Player")) {
			endgameText.BeginToggling ();
		}
	}
}
