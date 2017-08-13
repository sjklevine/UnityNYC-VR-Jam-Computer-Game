using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SidescrollerUICanvas : MonoBehaviour {
	public static SidescrollerUICanvas instance;
	public TextMeshProUGUI coinsText;
	public TextMeshProUGUI timeText;
	public int coins = 0;
	public float timeRemaining = 0f;

	private float startTime = 300f;

	void Start () {
		instance = this;
		timeRemaining = startTime;
	}

	void Update() {
		timeRemaining -= Time.deltaTime;

		// Currently, nothing actually happens if time hits zero...

		// ERRY FRAME
		UpdateText ();
	}

	public void CollectCoin() {
		coins++;
	}

	private void UpdateText() {
		coinsText.text = "x " + coins.ToString ("D2");
		timeText.text = Mathf.CeilToInt(timeRemaining).ToString ("D3");
	}
}