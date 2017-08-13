using UnityEngine;
using VRTK;
using System.Collections;

public class PlatformGrabHelper : MonoBehaviour {

	[SerializeField] GameObject helperArrows;
	private AudioSource cachedAudio;

	void Start (){
		helperArrows.SetActive(true);
		if (GetComponent<VRTK_InteractableObject>() == null){
			Debug.LogError("Required to be attached to an Object that has the VRTK_InteractableObject script attached to it");
			return;
		}

		cachedAudio = this.GetComponent<AudioSource> ();

		GetComponent<VRTK_InteractableObject>().InteractableObjectTouched += new InteractableObjectEventHandler(ObjectGrabbed);
		GetComponent<VRTK_InteractableObject>().InteractableObjectUntouched += new InteractableObjectEventHandler(ObjectUngrabbed);
	}

	private void ObjectGrabbed(object sender, InteractableObjectEventArgs e){
		helperArrows.SetActive(true);
		if (cachedAudio != null) {
			cachedAudio.Play ();
		}
	}

	private void ObjectUngrabbed(object sender, InteractableObjectEventArgs e){
		helperArrows.SetActive(false);
		if (cachedAudio != null) {
			cachedAudio.Stop ();
		}
	}

}