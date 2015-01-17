using UnityEngine;
using System.Collections;

//TODO: Figure out how we can generalize this so that we can spawn any number of these objects,
// and make the GameObjects aware of which unique one should move.

public class EnemyMovement : MonoBehaviour {

	public GameObject theVil; //specify in the script component which prefab GameObject to pay attention to.
	private int state = -1; 	//Simple way of storing the state of what the relation currently is between finger and GameObject.
														// -1 means nothing has initialized yet. Nothing should be moving.
														// 0 means a game object can move on the screen.

	private GameObject movingVil;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			Touch currentTouch = Input.GetTouch(0);
			//Used for collider. Mostlikely, Having two vectors is unnecesary nad they can be combined.
			//I'm interested to see what the difference is, if any between the two (besided the z axis).
			Vector2 v2 = new Vector2(Camera.main.ScreenToWorldPoint(currentTouch.position).x, Camera.main.ScreenToWorldPoint(currentTouch.position).y);
			Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(currentTouch.position.x, currentTouch.position.y, 10));


			//Debug.Log("Enemy Vector" + v2);

			//If player started tapping screen
			if (currentTouch.phase == TouchPhase.Began) {
				//Debug.Log("BEGAN");

				Collider2D c2d = Physics2D.OverlapPoint(v2); // THIS IS A GLOBAL THING.
																										 // So we need to dig into it to see if we collided with teh object we want.

				if (c2d != null && c2d.gameObject.name == "Enemy") {
					movingVil = c2d.gameObject;
					//Debug.Log("COLLIDED WITH: " + movingVil.name);
					//Debug.Log("Enemy Vector Moving" + v2);

					state = 0;

					// theVil.transform.position = v2;
					movingVil.transform.position = Vector3.Lerp(theVil.transform.position, touchPosition, Time.deltaTime);

				}
			//If finger is stationary or moving
			} else if (currentTouch.phase == TouchPhase.Stationary || currentTouch.phase == TouchPhase.Moved) {
				//Debug.Log("MOVED");

					if(state == 0) {
						//theVil.transform.position = v2;

						// If the finger is on the screen, move the object smoothly to the touch position
						movingVil.transform.position = Vector3.Lerp(theVil.transform.position, touchPosition, Time.deltaTime*100);
					}

			//If player lifts finger off screen
			} else if (currentTouch.phase == TouchPhase.Ended) {
				//Debug.Log("ENDED");
				state = -1;
			}

		}
	}
}
