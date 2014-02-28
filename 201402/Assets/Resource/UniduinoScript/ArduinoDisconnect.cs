using UnityEngine;
using System.Collections;
using Uniduino;

public class ArduinoDisconnect : MonoBehaviour {
	public Arduino arduino;

	// Use this for initialization
	void Start () {
		arduino = Arduino.global;
	}

	void OnDestroy() {
		arduino.Disconnect();
	}
}
