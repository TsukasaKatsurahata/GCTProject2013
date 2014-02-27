using UnityEngine;
using System.Collections;

public class DataBase : MonoBehaviour {
	public static Transform[] Lines;
	public static GameObject Player = null;
	public static GameObject FaceForward = null;
	public static GameObject CameraLookAt = null;

	public Transform[] line;
	public GameObject player;
	public GameObject face_forward;
	public GameObject camera_look_at;

	// Use this for initialization
	void Start () {
		Lines = new Transform[line.Length];
		if (Lines.Length < 1) {
			Lines = new Transform[1];
//			Lines[0] = new Transform();
			Lines[0].position = new Vector3(0, 0, 20);
		}
		else {
			for (int i = 0; i < Lines.Length; i++) {
				Lines[i] = line[i];
			}
		}
		Player = player;
		FaceForward = face_forward;
		CameraLookAt = camera_look_at;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
