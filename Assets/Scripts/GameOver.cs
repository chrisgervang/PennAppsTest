using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using Prime31;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
public class GameOver : MonoBehaviour {
	public GameObject canvas1;
	// Use this for initialization
	//TODO: For consistancy, force PlayerID of 1 (the host) to send out its winning score calulation, and override the other players scores and winner.
	//This way, there is never an inconsistancy and there is an authoritative phone.

	void Start () {
		canvas1.GetComponentInChildren<Text>().text = "Game Over";
		updateWinner ();
	}
	void updateWinner(){
		int[] playerScores = new int[CreateGameBehaviors.sortedPlayers.Count];

		for (int i = 1; i <= 4; i++){
			for (int j = 1; j<=6; j++){
				if(GameObject.Find(i+","+j) != null) {
					int playId = GameObject.Find (i+","+j).GetComponent<ColorSquareMovement> ().playerId;
					if(playId !=-1){
						playerScores[playId]+=1;
					}
				} else {
					Debug.Log(i+","+j+" is null");
				}

			}
		}
		Dictionary<string, int> dict = (CreateGameBehaviors.sortedPlayers).ToDictionary (x => x, x => playerScores [(CreateGameBehaviors.sortedPlayers.IndexOf (x))]);
		SortedDictionary<string, int> sorted = new SortedDictionary<string, int> (dict);
		string scoresList = "";
		foreach (var pair in sorted) {
			scoresList = scoresList + pair.Key + ": " + pair.Value + "\n";
		}
		canvas1.GetComponentInChildren<Text> ().text = scoresList;
		canvas1.GetComponentInChildren<Text> ().fontSize = 60;

	}
	// Update is called once per frame
	void Update () {

	}
}
