using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SidescrollerGameManager : MonoBehaviour {
	public static SidescrollerGameManager instance;
	public enum GameState {Start, Running, Dead1, Dead2, Win, PostWin};
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
	public GameObject titleObject;
	public GameObject creditsObject;

	// Important gameobjects for toggling
	public GameObject gameWorldObject;
	public GameObject gameCanvasesObject;

    // Classic Mario UI stuff
    public TextMeshProUGUI coinsText;
	public TextMeshProUGUI timeText;
	private int coins = 0;
    private float timeRemaining = 0f;
	private float startTime = 300f;
	private float preDead2Time = 1.0f;

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
		gameWorldObject.SetActive (false);
		gameCanvasesObject.SetActive (false);
		titleObject.SetActive (true);
		creditsObject.SetActive (false);

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
		case GameState.Dead1:
			// Lower the time...
			timeRemaining -= Time.deltaTime;
			if (timeRemaining <= 0) {
				state = GameState.Dead2;
			}
			break;
		case GameState.Dead2:
			// Waiting for a reset key!
			if (GotInputFromNonVRPlayer()) {	
				//Start the game!
				ResetLevel();
			}
			break;
		case GameState.PostWin:
			// Any player input RELOADS THE SCENE!
			if (GotInputFromNonVRPlayer()) {	
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
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
		// Do "predead" instead of "dead" to stop player from resetting too quickly.
		timeRemaining = preDead2Time;
		state = GameState.Dead1;

		// Throw up the black screen!
		blackScreen.SetActive (true);

		// Hide the world! Or don't... not sure which is best.
		gameWorldObject.SetActive (false);
		gameCanvasesObject.SetActive (false);

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
		yield return new WaitForSeconds (6.0f);

		// Clean up
		youWinTextObject.StopAndHide();

		// Change state to postwin
		state = GameState.PostWin;

		// Throw up the black screen, with proper objects
		blackScreen.SetActive (true);
		titleObject.SetActive (false);
		creditsObject.SetActive (true);
		gameWorldObject.SetActive (false);
		gameCanvasesObject.SetActive (false);

		// Edit the text to show you won
		blackScreenTextObject.thingToToggle.GetComponent<TextMeshProUGUI>().text = "Thanks for playing!\nCoins Collected: " + coins + " / " + cachedCoinCount + "\n\nPress any key to play again";
		blackScreenTextObject.BeginToggling ();
	}

    // For resetting to start!
    public void ResetLevel()
    {
		// Stats!
		coins = 0;
		timeRemaining = startTime;

		// Soundtrack!
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

		// Make sure game is visible...
		gameWorldObject.SetActive (true);
		gameCanvasesObject.SetActive (true);

		// Is that really it?
		state = GameState.Running;
    }

	// -------------- Privates --------------
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