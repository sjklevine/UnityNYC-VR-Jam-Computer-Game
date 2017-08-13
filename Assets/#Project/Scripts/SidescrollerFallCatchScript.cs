using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollerFallCatchScript : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player"))
        {
            //PlayerControl playerScript = other.GetComponent<PlayerControl>();
            //playerScript.TeleportToLastSafePosition();
            
			//Fall sound!
			GetComponent<AudioSource>().Play();

			// Tell game manger we dead
			SidescrollerGameManager.instance.PlayerDeath();
        }
    }
}
