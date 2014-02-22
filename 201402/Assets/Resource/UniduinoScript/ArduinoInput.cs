using UnityEngine;
using System.Collections;
using Uniduino;

public class ArduinoInput : MonoBehaviour {
	public static ArduinoInput getInstance = null;
	public Arduino arduino;
	
	public static int readByte = 0x00;
	public static int InputData {
		get{
			byte id = 0x00;
			if(Input.GetKey(KeyCode.A)) id += 0x01;
			if(Input.GetKey(KeyCode.S)) id += 0x02;
			if(Input.GetKey(KeyCode.D)) id += 0x04;
			if(Input.GetKey(KeyCode.F)) id += 0x08;
			if(Input.GetKey(KeyCode.G)) id += 0x10;
			if(Input.GetKey(KeyCode.H)) id += 0x20;
			if(Input.GetKey(KeyCode.J)) id += 0x40;
			return (byte)(readByte | id);
		}
		set{}
	}

	void Awake () {
		getInstance = this;
	}
	
	void Start () {
		getInstance = this;
		readByte = 0x00;

		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);
	}	

	void ConfigurePins( ) {
		for (int pin = 2; pin < 9; pin++) {
			arduino.pinMode(pin, PinMode.INPUT);
			arduino.reportDigital((byte)(pin/8), 1);
		}
	}

	void Update() {
		readByte = 0x00;
		/*
		for (int i = 0; i < 7; i++) {
			readByte += arduino.digitalRead(i + 2);
		}
		*/
		if (!arduino.IsOpen) return;
		readByte += 0x01 * arduino.digitalRead(2);
		readByte += 0x02 * arduino.digitalRead(3);
		readByte += 0x04 * arduino.digitalRead(4);
		readByte += 0x08 * arduino.digitalRead(5);
		readByte += 0x10 * arduino.digitalRead(6);
		readByte += 0x20 * arduino.digitalRead(7);
		readByte += 0x40 * arduino.digitalRead(8);
	}
}
