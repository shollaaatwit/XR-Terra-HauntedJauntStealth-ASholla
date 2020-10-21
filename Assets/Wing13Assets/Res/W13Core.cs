using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W13Core : MonoBehaviour {

	public GameObject PlayerObject;
	public static GameObject Player; public static Transform PlayerT;
	 
	public static int LastEntredDoor = 0;
	public static bool [] ItemTaken = new bool[1000];

	void Awake () {

		if (PlayerObject!=null) {Player = PlayerObject;}
		FindPlayer ();   
	}

	static public void FindPlayer () {
		if (Player==null) {Player = GameObject.Find("FPSController");}
		if (Player==null) {Player = GameObject.FindGameObjectWithTag("Player");}
		if (Player==null) {Player = GameObject.Find("Player");}
		if (Player!=null) {PlayerT = Player.transform;}
	}

}
