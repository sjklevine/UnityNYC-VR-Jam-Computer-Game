using UnityEngine;
using VRTK;
using System.Collections;

public class StopKinematicOnGrab : MonoBehaviour {

	[SerializeField] GameObject helperArrows;
	void Start (){
		if (GetComponent<VRTK_InteractableObject>() == null){
			Debug.LogError("Required to be attached to an Object that has the VRTK_InteractableObject script attached to it");
			return;
		}
		if (GetComponent<Rigidbody>() == null){
			Debug.LogError("Required to be attached to an Object that has a Rigidbody component attached to it");
			return;
		}

		GetComponent<VRTK_InteractableObject>().InteractableObjectTouched += new InteractableObjectEventHandler(ObjectGrabbed);
		//GetComponent<VRTK_InteractableObject>().InteractableObjectUntouched += new InteractableObjectEventHandler(ObjectUngrabbed);
	}

	private void ObjectGrabbed(object sender, InteractableObjectEventArgs e){
		GetComponent<Rigidbody> ().isKinematic = false;
	}
}