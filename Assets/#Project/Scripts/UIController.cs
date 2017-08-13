﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		CoinController.OnCoinTotalUpdate += OnTotalCoinsChanged;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTotalCoinsChanged(int totalCoins){
		Debug.Log(totalCoins);
	}
}
