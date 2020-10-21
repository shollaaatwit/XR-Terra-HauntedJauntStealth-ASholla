using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W13Assets {
public class PlayerInteractor : MonoBehaviour {

	public int DistanceFromPlayer = 3;
    public IntM InteractorMode;
    public enum IntM{EButton, Mouse};

	static RaycastHit hit;
	static Vector3 RayFwd;
	static Transform RayObjT;

    public Texture2D Cross;
    private Rect CrossR;

	void Start () {RayObjT = this.gameObject.transform;}

	void Update () {
        CrossR = new Rect (Screen.width*0.475f,Screen.height*0.475f,Screen.width*0.05f,Screen.width*0.05f);

		if (InteractorMode == IntM.EButton && Input.GetKeyDown(KeyCode.E)) {Interact ();}
		if (InteractorMode == IntM.Mouse && Input.GetMouseButton(0)) {Interact ();}
	}

   void OnGUI () {
       if (Cross!=null) {GUI.DrawTexture (CrossR, Cross);}
   }

	void Interact () {
		RayFwd = RayObjT.TransformDirection(Vector3.forward);
		if (Physics.Raycast (RayObjT.position, RayFwd, out hit, DistanceFromPlayer)) {
			hit.collider.SendMessage("Click", SendMessageOptions.DontRequireReceiver);
		}
	}

 }
}