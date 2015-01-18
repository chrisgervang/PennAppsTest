using UnityEngine;
using System.Collections;

//public static var bundleVersionCode: int;

public class ShakeAnt : MonoBehaviour
{
	//public GameObject test;
	private GameObject Ant;
	private GameObject Ant2;
	private GameObject Ant3;
	private GameObject Ant4;
	private GameObject Ant5;
	private GameObject Ant6;
	private GameObject Ant7;

	// Use this for initialization
	void Start ()
	{
		//Debug.Log(test.transform.position);
		Ant	= GameObject.Find("Ant");
		Ant2 = GameObject.Find("Ant2");
		Ant3 = GameObject.Find("Ant3");
		Ant4 = GameObject.Find("Ant4");
		Ant5 = GameObject.Find("Ant5");
		Ant6 = GameObject.Find("Ant6");
		Ant7 = GameObject.Find("Ant7");
	}

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyUp(KeyCode.A))
		{
			//Debug.Log("KILL ANT ONE");
			//Shake off ant
			Destroy(Ant);
		}
		if(Input.GetKeyUp(KeyCode.B))
		{
			//Shake off second ant
			Destroy(Ant2);
		}
		if(Input.GetKeyUp(KeyCode.C))
		{
			//Shake off third ant
			Destroy(Ant3);
		}
		if(Input.GetKeyUp(KeyCode.D))
		{
			Destroy(Ant4);
		}
		if(Input.GetKeyUp(KeyCode.E))
		{
			Destroy(Ant5);
		}
		if(Input.GetKeyUp(KeyCode.F))
		{
			Destroy(Ant6);
		}
		if(Input.GetKeyUp(KeyCode.G))
		{
			Destroy(Ant7);
		}
	}

}
