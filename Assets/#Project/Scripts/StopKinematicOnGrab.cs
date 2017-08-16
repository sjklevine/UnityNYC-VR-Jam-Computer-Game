using UnityEngine;
using VRTK;
using System.Collections;

public class StopKinematicOnGrab : MonoBehaviour {

	void Start (){
		if (GetComponent<VRTK_InteractableObject>() == null){
			Debug.LogError("Required to be attached to an Object that has the VRTK_InteractableObject script attached to it");
			return;
		}

		GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(KillKinematic);
		//GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += new InteractableObjectEventHandler(KillKinematic);
	}

	private void KillKinematic(object sender, InteractableObjectEventArgs e){
		GetComponent<VRTK_InteractableObject> ().SetPreviousKinematicState (false);
	}
}