﻿using UnityEngine;
using System.Collections;

public class WorldMovementFollow : MonoBehaviour 
{
	public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.

	public Transform world;  // Reference to the container of the entire world
	private Transform player;		// Reference to the player's transform.
	private Transform camera;

	void Awake ()
	{
		camera = this.transform;
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update() {
		Debug.DrawLine (camera.position + Vector3.right * xMargin + Vector3.up * 5f, camera.position + Vector3.right * xMargin + Vector3.down * 5f); 
		Debug.DrawLine (camera.position + Vector3.left * xMargin + Vector3.up * 5f, camera.position + Vector3.left * xMargin + Vector3.down * 5f); 
	}

	bool CheckXMargin()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		float playerDistanceFromCamera = Mathf.Abs(camera.position.x - player.position.x);
		//Debug.Log (playerDistanceFromCamera);
		return (playerDistanceFromCamera > xMargin);
	}

	void FixedUpdate ()
	{
		TrackPlayer();
	}

	void TrackPlayer ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = world.position.x;

		// If the player has moved beyond the x margin...
		if (CheckXMargin ()) {

			// ... the target x coordinate should be a Lerp between the world's current x position AND
			// the distance between the margin border and the player's current position.
			float difference = camera.position.x - player.position.x;
			if (difference > 0) {
				// Goin' right
				difference -= xMargin;
			} else {
				// Goin' left
				difference += xMargin;
			}
			targetX = Mathf.Lerp (world.position.x, world.position.x + difference, xSmooth * Time.deltaTime);
		}

		// The distance between world and player must be capped!
		//targetX = Mathf.Clamp(targetX, minX, maxX);

		// Set the WORLD's position to the target position with the same z component.
		world.position = new Vector3(targetX, world.position.y, world.position.z);
	}
}