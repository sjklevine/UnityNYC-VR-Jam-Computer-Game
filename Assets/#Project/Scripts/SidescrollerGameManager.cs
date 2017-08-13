using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SidescrollerGameManager : MonoBehaviour {
	public static SidescrollerGameManager instance;
	public enum GameState {Start, Running};
	public bool startWithStartScreen;

	// Start/transition stuff
	public GameObject blackScreen;
	public SimpleTimedToggleVisibilty gameStartTextObject;

	// Classic Mario UI stuff
	public TextMeshProUGUI coinsText;
	public TextMeshProUGUI timeText;
	public int coins = 0;
	public float timeRemaining = 0f;

	private float startTime = 300f;
	private GameState state;

	void Start () {
		instance = this;
		timeRemaining = startTime;

		// Toggle stuff!
		if (startWithStartScreen) {
			state = GameState.Start;
			blackScreen.SetActive (true);
			gameStartTextObject.gameObject.SetActive (false);
		} else {
			state = GameState.Running;
			blackScreen.SetActive (false);
			gameStartTextObject.gameObject.SetActive (true);
		}
	}

	void Update() {
		switch (state) {

		case GameState.Start:
			Debug.Log ("waiting for key");
			if (Input.anyKeyDown) {	
				//Start the game!
				blackScreen.SetActive (false);
				gameStartTextObject.gameObject.SetActive(true);
				state = GameState.Running;
			}
			break;
		case GameState.Running:
			timeRemaining -= Time.deltaTime;

			// Currently, nothing actually happens if time hits zero...

			// ERRY FRAME
			UpdateText ();
			break;
		}
	}

	public void CollectCoin() {
		coins++;
	}

	private void UpdateText() {
		coinsText.text = "x " + coins.ToString ("D2");
		timeText.text = Mathf.CeilToInt(timeRemaining).ToString ("D3");
	}
}