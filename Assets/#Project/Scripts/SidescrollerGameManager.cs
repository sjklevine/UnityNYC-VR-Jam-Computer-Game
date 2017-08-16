using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SidescrollerGameManager : MonoBehaviour {
	public static SidescrollerGameManager instance;
	public enum GameState {Start, Running, Dead, Win};
	public bool startWithStartScreen;

    // Handy references
	public GameObject world;
	public GameObject player;
	public AudioSource soundtrack;

	// Start/transition stuff
	public GameState state {get; set;}
	public GameObject blackScreen;
	public SimpleTimedToggleVisibilty blackScreenTextObject;
	public SimpleTimedToggleVisibilty gameStartTextObject;
    public SimpleTimedToggleVisibilty youWinTextObject;
	public Transform currentLevelHolder;

    // Classic Mario UI stuff
    public TextMeshProUGUI coinsText;
	public TextMeshProUGUI timeText;
	private int coins = 0;
    private float timeRemaining = 0f;
	private float startTime = 300f;

	// Other privates
	private Vector3 cachedWorldPosition;
	private Vector3 cachedPlayerPosition;
	private int cachedCoinCount;

	void Awake () {
		instance = this;

		// Cache positions!
		cachedWorldPosition = world.transform.position;
		cachedPlayerPosition = player.transform.position;

        // Coins!
        CoinController.OnCoinTotalUpdate += CollectCoin;
		cachedCoinCount = GameObject.FindGameObjectsWithTag ("Coin").Length;

        // Start in the black!
		state = GameState.Start;
		blackScreen.SetActive (true);
		gameStartTextObject.gameObject.SetActive (false);

		// Unless you don't!
		if (!startWithStartScreen) {
			ResetLevel();
		}
	}

	void Update() {
		switch (state) {
		case GameState.Start:
			if (GotInputFromNonVRPlayer()) {
				// Start the game!
				ResetLevel();
			}
			break;
		case GameState.Running:
			timeRemaining -= Time.deltaTime;

			// Currently, nothing actually happens if time hits zero...
			// TODO: Player death at time zero

			// UPDATE GUI ERRY FRAME
			UpdateText ();
			break;
		case GameState.Dead:
			// Waiting for a reset key!
			if (GotInputFromNonVRPlayer()) {	
				//Start the game!
				ResetLevel();
			}
			break;
		}
	}

	public void CollectCoin(int newCoinCount) {
        coins = newCoinCount;
	}

	// For handling what happens when you fall and die!
	public void PlayerDeath() {
		// Audio!
		soundtrack.Stop();

		// Change state to avoid the timer code
		state = GameState.Dead;

		// Throw up the black screen!
		blackScreen.SetActive (true);

		// Edit the text to show you dead
		blackScreenTextObject.thingToToggle.GetComponent<TextMeshProUGUI>().text = "Oh no!\n\nPress any key to try again.";
		blackScreenTextObject.BeginToggling ();

		// Do nothing else... hopefully shenanigans don't occur
	}

	// For completing a level!
	public void LevelWin()
	{
		// Text toggle!
		youWinTextObject.BeginToggling();

		// Audio!
		youWinTextObject.GetComponent<AudioSource>().Play();
		soundtrack.Stop();

		// Immediately disable the player..
		state = GameState.Win;

		// Wait some time, then do a level reset with some nice text.
		StartCoroutine(LevelWinPart2());
	}

	private IEnumerator LevelWinPart2() {
		yield return new WaitForSeconds (5.0f);

		// Clean up
		youWinTextObject.StopAndHide();

		// Change state to start
		state = GameState.Start;

		// Throw up the black screen!
		blackScreen.SetActive (true);

		// Edit the text to show you won
		blackScreenTextObject.thingToToggle.GetComponent<TextMeshProUGUI>().text = "Thanks for playing!\n\nPress any key to play again\n\nCoins Collected: " + coins + " / " + cachedCoinCount;
		blackScreenTextObject.BeginToggling ();
	}

    // For resetting to start!
    public void ResetLevel()
    {
		// Stats!
		coins = 0;
		timeRemaining = startTime;

		//Soundtrack!
		soundtrack.Play();

		// Hide the black screen!
		blackScreen.SetActive (false);

		// Show the flashy text!
		gameStartTextObject.gameObject.SetActive(true);
		gameStartTextObject.BeginToggling ();

        // Player and world need to return to their start positions
		world.transform.position = cachedWorldPosition;
		player.transform.position = cachedPlayerPosition;

		// Reset player velocity
		player.GetComponent<Rigidbody>().velocity = Vector3.zero;

		// Is that really it?
		state = GameState.Running;
    }

    // Privates
	private void UpdateText() {
		coinsText.text = "x " + coins.ToString ("D2");
		timeText.text = Mathf.CeilToInt(timeRemaining).ToString ("D3");
	}
	private bool GotInputFromNonVRPlayer() {
		// Just a convenient abstraction.
		bool gotJump = Input.GetButtonDown ("PCPlayerJump");
		bool gotMovement = (Input.GetAxis ("PCPlayerHorizontal") != 0f);
		return (gotJump || gotMovement);
	}
}