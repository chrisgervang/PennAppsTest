using UnityEngine;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System;

public class ShakeAntsGizmo : MonoBehaviour {
	public GameObject AntPrefab;
	public List<Sprite> antSprites = new List<Sprite>();
	public int numOfAnts;

	//public GameObject test;
	private List<GameObject> Ant = new List<GameObject>();
	System.Random rnd = new System.Random();
	// Use this for initialization
	void Start () {
		CreateGameBehaviors.setResolutionToPortrait();
		//Debug.Log(test.transform.position);
		for (var i = 0; i < numOfAnts; i++) {
			Ant.Add(Instantiate(AntPrefab) as GameObject);
			//Debug.Log("I: " + i + "name: " + Ant[i].name);

			Vector2 antPos = new Vector2(UnityEngine.Random.Range(-1.4f,1.3f), UnityEngine.Random.Range(-2.0f, 3.95f));
			Ant[i].transform.position = antPos;
			Ant[i].transform.rotation = Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360f), Vector3.forward);
			Ant[i].GetComponent<SpriteRenderer>().sprite = antSprites[rnd.Next(0, antSprites.Count)];
		}
	}
	public float speed = 10.0F;
	float totalShake = 0;
	int direction = 0;
	float shakeThreshold = 0.1f;
	// Update is called once per frame
	void Update () {
		if(totalShake > shakeThreshold) {
			//Debug.Log("KILL ANT ONE");
			//Shake off ant
			if(Ant.Count > 0) {
				GameObject.Find("AudioManager").GetComponent<AudioManager>().playAudioClip("Oh No");
				Destroy(Ant[Ant.Count-1]);
				Ant.RemoveAt(Ant.Count-1);
				Debug.Log("Ants left: " + Ant.Count);
				shakeThreshold += 0.1f;
			}


		}

		Vector3 dir = Vector3.zero;
		//dir.y = Input.acceleration.y;
		dir.x = Input.acceleration.x;
		if (dir.sqrMagnitude > 1)
		dir.Normalize();

		dir *= Time.deltaTime;

		if(dir.x > 0 && direction == 0) {
			totalShake += Math.Abs(dir.x);
			direction = 1;
			Debug.Log(totalShake);
		} else if(dir.x < 0 && direction == 1) {
			totalShake += Math.Abs(dir.x);
			direction = 0;
			Debug.Log(totalShake);
		}

		// if (Ant[0].transform.position.x < 2.5f && Ant[0].transform.position.x > -2.5f && Ant[0].transform.position.y < 4.8f && Ant[0].transform.position.y > -4.0f) {
		//
		// } else {
		// 	Ant[0].transform.position = Vector3.zero;
		// }



	}

}
