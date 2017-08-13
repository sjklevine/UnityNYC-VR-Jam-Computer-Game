using UnityEngine;
using VRTK;
using System.Collections;

public class PlatformGrabHelper : MonoBehaviour {

	[SerializeField] GameObject helperArrows;
	void Start (){
		helperArrows.SetActive(true);
		if (GetComponent<VRTK_InteractableObject>() == null){
			Debug.LogError("Required to be attached to an Object that has the VRTK_InteractableObject script attached to it");
			return;
		}

		//GetComponent<VRTK_InteractableObject>().InteractableObjectTouched += new InteractableObjectEventHandler(ObjectGrabbed);
		//GetComponent<VRTK_InteractableObject>().InteractableObjectUntouched += new InteractableObjectEventHandler(ObjectUngrabbed);
	}

	private void ObjectGrabbed(object sender, InteractableObjectEventArgs e){
		helperArrows.SetActive(true);
	}

	private void ObjectUngrabbed(object sender, InteractableObjectEventArgs e){
		helperArrows.SetActive(false);
	}

}