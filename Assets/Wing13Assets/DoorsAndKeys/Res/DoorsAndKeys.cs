using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class DoorsAndKeys : MonoBehaviour {

	public DoorM DoorMode;
	public enum DoorM{Door, DoorToAnotherLocation, Teleport};

	public bool LockedWithKey = false;
	public int DoorID = 0;
	public bool AutoClose = false;
    public bool AutoOpen = false;
    public float AutoDistance = 2.0f;
	public float CloseAfter = 2.0f; private float CAV = 0.0f;
	public int Angle = -90;
	public int Speed = 4;
	public int KeyItemID = 0;
	public string LoadLevel = "";
	public Transform ExitHere;
    public bool RemoteOrKnob = false;

	public AudioClip LockedSound; private AudioSource ASourse;
	public AudioClip CloseSound;
    public AudioClip OpenSound;
	public AudioClip OpendWithKeySound;

	private bool DoorOpened = false;
	private bool DoorClosing = false;
	private float ClickDelay = 0.0f;

	private Transform DoorT;
	private Vector3 StartXYZ;
	private Vector2 FromTo;
    private float distance = 100500.0f;

	private void PlayLockedSound() {ASourse.clip = LockedSound; ASourse.Play();}
	private void PlayCloseSound()  {ASourse.clip = CloseSound;  ASourse.Play();}
    private void PlayOpenSound()   {ASourse.clip = OpenSound;  ASourse.Play();}
	private void PlayOpenWithKey() {ASourse.clip = OpendWithKeySound;  ASourse.Play();}

	public void Click () {
if (RemoteOrKnob==false) {
                                 if (ClickDelay<=0) {ClickDelay = 0.5f; if (LockedWithKey==false) {if (DoorOpened==false) {PlayOpenSound();}OpenClose ();}
	                                           else {if (W13Core.ItemTaken [KeyItemID] == true) {
                                                     if (DoorOpened==false) {PlayOpenWithKey ();}
					                                 OpenClose (); 
				                                     } else {PlayLockedSound ();}}
		                   }
      }
	}

   public void RemoteClick () {
                                if (ClickDelay<=0) {ClickDelay = 0.5f; if (LockedWithKey==false) {if (DoorOpened==false) {PlayOpenSound();} OpenClose ();}
                                              else {if (W13Core.ItemTaken [KeyItemID] == true) {
                                                    if (DoorOpened==false) {PlayOpenWithKey ();}
                                                    OpenClose (); 
                                                    } else {PlayLockedSound ();}}
                          }
  }

	void OpenClose () {
		if (DoorMode==DoorM.DoorToAnotherLocation) {
			W13Core.LastEntredDoor = DoorID;
			UnityEngine.SceneManagement.SceneManager.LoadScene(LoadLevel);}
		
		if (DoorMode==DoorM.Door) {
			if (DoorOpened==true) {FromTo.x = StartXYZ.y+Angle; FromTo.y = StartXYZ.y; DoorClosing = true;}
			else {FromTo.y = StartXYZ.y+Angle; DoorOpened = true; CAV = CloseAfter;}}

		if (DoorMode == DoorM.Teleport) {
			if (W13Core.Player!=null && ExitHere!=null) {W13Core.Player.transform.position = ExitHere.position;}
		}
	}

	void Update () {
		if (ClickDelay>0) {ClickDelay = ClickDelay - Time.deltaTime;}

		if (DoorMode == DoorM.Door) {FromTo.x = Mathf.LerpAngle(FromTo.x, FromTo.y, Time.deltaTime*Speed);
			                         DoorT.rotation = Quaternion.Euler(StartXYZ.x, FromTo.x, StartXYZ.z);

			                         if (DoorClosing==true) {
				                                             if (FromTo.x < FromTo.y + 2 && FromTo.x > FromTo.y - 2) {DoorOpened = false; DoorClosing = false; PlayCloseSound ();}
			                                                }

                                    if (AutoOpen==true || AutoClose==true) {
                                                                           if (W13Core.Player != null) {
                                                                                                        distance = Vector3.Distance (W13Core.PlayerT.position, DoorT.position);
                                                                                                       } else {W13Core.FindPlayer ();}
                                                                           }

                                     if (AutoClose==true && DoorOpened==true && DoorClosing==false && distance>AutoDistance) {if (CAV > 0) {CAV = CAV - Time.deltaTime;} else {OpenClose ();}}
		                            
                                     if (AutoOpen==true) {if (DoorOpened==false && distance<AutoDistance) {Click();}}

                                    }
	}

	void Start () {
		ASourse = GetComponent<AudioSource>();

		DoorT = this.gameObject.transform;
		StartXYZ = DoorT.rotation.eulerAngles;
		FromTo.y = StartXYZ.y; FromTo.x = StartXYZ.y;

		if (DoorMode == DoorM.DoorToAnotherLocation) {
			if (W13Core.LastEntredDoor!=0) {
				if (W13Core.Player!=null && ExitHere!=null) {
					W13Core.Player.transform.position = ExitHere.position;
					W13Core.LastEntredDoor = 0; }
			}
		}
	}

}

#if UNITY_EDITOR
[CustomEditor(typeof(DoorsAndKeys))]
public class DoorsEditor : Editor {
	override public void OnInspectorGUI() {
		var DoorsAndKeys = target as DoorsAndKeys;

		DoorsAndKeys.DoorMode = (DoorsAndKeys.DoorM)EditorGUILayout.EnumPopup ("Door mode:", DoorsAndKeys.DoorMode);

		if (DoorsAndKeys.DoorMode == DoorsAndKeys.DoorM.DoorToAnotherLocation) {
			DoorsAndKeys.LoadLevel = EditorGUILayout.TextField ("Load level:", DoorsAndKeys.LoadLevel);
			DoorsAndKeys.DoorID = EditorGUILayout.IntField ("Door ID:", DoorsAndKeys.DoorID);
		}

		if (DoorsAndKeys.DoorMode == DoorsAndKeys.DoorM.DoorToAnotherLocation || DoorsAndKeys.DoorMode == DoorsAndKeys.DoorM.Teleport) {
			DoorsAndKeys.ExitHere = (Transform)EditorGUILayout.ObjectField ("Player appears here:", DoorsAndKeys.ExitHere, typeof(Transform), true);
		}

		if (DoorsAndKeys.DoorMode == DoorsAndKeys.DoorM.Door) {
			DoorsAndKeys.Angle = EditorGUILayout.IntSlider ("Angle:", DoorsAndKeys.Angle, -90, 90);
			DoorsAndKeys.Speed = EditorGUILayout.IntField ("Speed:", DoorsAndKeys.Speed);

			DoorsAndKeys.AutoClose = EditorGUILayout.Toggle ("Auto close:", DoorsAndKeys.AutoClose);
			if (DoorsAndKeys.AutoClose==true) {DoorsAndKeys.CloseAfter = EditorGUILayout.FloatField ("Close after:", DoorsAndKeys.CloseAfter);}

            DoorsAndKeys.AutoOpen = EditorGUILayout.Toggle ("Auto open:", DoorsAndKeys.AutoOpen);
            if (DoorsAndKeys.AutoOpen==true) {DoorsAndKeys.AutoDistance = EditorGUILayout.FloatField ("Opening distance:", DoorsAndKeys.AutoDistance);}

			DoorsAndKeys.CloseSound = (AudioClip)EditorGUILayout.ObjectField ("Sound - Close:", DoorsAndKeys.CloseSound, typeof(AudioClip), true);
            if (DoorsAndKeys.LockedWithKey == false) {DoorsAndKeys.OpenSound =  (AudioClip)EditorGUILayout.ObjectField ("Sound - Open:",  DoorsAndKeys.OpenSound, typeof(AudioClip),  true);}
		}

		DoorsAndKeys.LockedWithKey = EditorGUILayout.Toggle ("Locked with key:", DoorsAndKeys.LockedWithKey);
		if (DoorsAndKeys.LockedWithKey == true) {
			DoorsAndKeys.KeyItemID = EditorGUILayout.IntField ("Key item ID:", DoorsAndKeys.KeyItemID);
			DoorsAndKeys.LockedSound = (AudioClip)EditorGUILayout.ObjectField ("Sound - Locked:", DoorsAndKeys.LockedSound, typeof(AudioClip), true);
			DoorsAndKeys.OpendWithKeySound = (AudioClip)EditorGUILayout.ObjectField ("Sound - Opened with key:", DoorsAndKeys.OpendWithKeySound, typeof(AudioClip), true);
		}

if (DoorsAndKeys.AutoOpen==false) {DoorsAndKeys.RemoteOrKnob = EditorGUILayout.Toggle ("Remote opening or knob:", DoorsAndKeys.RemoteOrKnob);}

		if(GUI.changed){EditorUtility.SetDirty(DoorsAndKeys); EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());}
	}
	
}
#endif