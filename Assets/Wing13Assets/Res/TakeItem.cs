using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : MonoBehaviour {

	public TakeM InteractorMode;
	public enum TakeM{PlayerLookAndClick, PlayerComeClose};

	public int ItemID = 0;

	private float distance = 100500.0f;
	private Transform ObjectT;

	void Start () {
		ObjectT = this.gameObject.transform;
		if (W13Core.ItemTaken[ItemID]==true) {this.gameObject.SetActive (false);}
	}

	void Update () {
		if (InteractorMode == TakeM.PlayerComeClose && W13Core.ItemTaken[ItemID]==false) {
			if (W13Core.Player != null) {
				distance = Vector3.Distance (W13Core.PlayerT.position, ObjectT.position);
				if (distance<1) {W13Core.ItemTaken [ItemID] = true; Destroy (this.gameObject);}
			} else {W13Core.FindPlayer ();}
		}
	}

	void Click () {
		if (InteractorMode == TakeM.PlayerLookAndClick) {
		W13Core.ItemTaken [ItemID] = true;
		Destroy (this.gameObject);
		}
	}
}
