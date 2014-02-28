using UnityEngine;
using System.Collections;

public class TitleToStart : MonoBehaviour {
	public Animation StartAnim = null;
	private float Timer;
	public string NextScene = "mainGame";

	// Use this for initialization
	void Start () {
		if (StartAnim == null) StartAnim = GetComponent<Animation>();
		Timer = -1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Timer > 0.0f) {
			if (Time.time > Timer) {
				Application.LoadLevel(NextScene);
			}
		}
		else {
			if (ArduinoInput.InputData > 0) {
				if (StartAnim != null) StartAnim.Play(StartAnim.clip.name);
				Timer = Time.time + 16.0f;
			}
		}
	}
}
