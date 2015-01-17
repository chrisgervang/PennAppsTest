using UnityEngine;
using System.Collections;

public class ShakeAnt : MonoBehaviour
{
	//public GameObject test;
	public GameObject Ant;
	public GameObject Ant1;
	public GameObject Ant2;

	// Use this for initialization
	void Start ()
	{
		//Debug.Log(test.transform.position);
	}

	// Update is called once per frame
	void Update ()
	{
		// Check accelerometer values (inputs)
		if(Input.acceleration.x==100 || Input.acceleration.y==100 || Input.acceleration.z==100)
		{
			//Shake off ant
			Destroy(GameObject.Find("Ant"));
		}
		if(Input.acceleration.x==200 || Input.acceleration.y==200 || Input.acceleration.z==200)
		{
			//Shake off ant2
			Destroy(GameObject.Find("Ant2"));
		}
		if(Input.acceleration.x==300 || Input.acceleration.y==300 || Input.acceleration.z==300)
		{
			//Shake off ant3
			Destroy(GameObject.Find("Ant3"));
		}
	}
}
