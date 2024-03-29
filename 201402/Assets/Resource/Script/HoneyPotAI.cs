﻿using UnityEngine;
using System.Collections;

public class HoneyPotAI : MonoBehaviour {
	public GameObject HornetA = null;
	public Transform StartPoint = null;
	private float Timer;

	// Use this for initialization
	void Start () {
		//Timer = 12f;
		Timer = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (HornetA == null) return;
		if (Time.time > Timer) {
			Timer = Time.time + 5.2f + Random.value * 7.7f;
			//GameObject hornet = (GameObject)GameObject.Instantiate(HornetA, (DataBase.Lines[((int)(Random.value * (DataBase.Lines.Length - 0.02f)))].position), new Quaternion());
			GameObject hornet = (GameObject)GameObject.Instantiate(HornetA, StartPoint.position, new Quaternion());
		}
	}
}
