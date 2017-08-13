using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

	public static Action<int> OnCoinTotalUpdate;

	private static int _totalCoinsCollected = 0;
	[SerializeField] AudioClip _audio;

	void OnTriggerEnter(Collider col){
		Destroy (gameObject);
		AudioSource.PlayClipAtPoint(_audio, transform.position);
		_totalCoinsCollected++;
		if (OnCoinTotalUpdate != null)
			OnCoinTotalUpdate (_totalCoinsCollected);
	}
}
