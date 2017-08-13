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

		GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);
		GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += new InteractableObjectEventHandler(ObjectUngrabbed);
	}

	private void ObjectGrabbed(object sender, InteractableObjectEventArgs e){
		if (cachedAudio != null) {
			cachedAudio.Play ();
		}
	}

	private void ObjectUngrabbed(object sender, InteractableObjectEventArgs e){
		if (cachedAudio != null) {
			cachedAudio.Stop ();
		}
	}

}