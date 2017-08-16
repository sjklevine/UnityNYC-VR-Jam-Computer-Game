using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamDisplay : MonoBehaviour {

	[SerializeField] private Renderer _renderer;

	void Start () {
		
		WebCamTexture _webcamTexture = new WebCamTexture();
		_renderer.material.mainTexture = _webcamTexture;
		if (_webcamTexture.requestedWidth != 0) {
			_webcamTexture.Play ();
		}

		// We can use this to debug camera issues.
		/*
		foreach (WebCamDevice device in WebCamTexture.devices) {
			Debug.Log ("device name " + device.name);
		}
		*/
	}

}
