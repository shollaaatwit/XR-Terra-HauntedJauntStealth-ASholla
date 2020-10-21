using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteOrKnob : MonoBehaviour {

    public GameObject DoorObject;
	
	void Click () {
      DoorObject.GetComponent<Collider>().SendMessage("RemoteClick", SendMessageOptions.RequireReceiver);
	}
}
