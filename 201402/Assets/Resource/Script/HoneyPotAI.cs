using UnityEngine;
using System.Collections;

public class HoneyPotAI : MonoBehaviour {
	public GameObject HornetA = null;
	public Transform StartPoint = null;
	private float Timer;

	// Use this for initialization
	void Start () {
		Timer = 12f;
	}
	
	// Update is called once per frame
	void Update () {
		if (HornetA == null) return;
		if (Time.time > Timer) {
			Timer = Time.time + 2.3f;
			GameObject hornet = (GameObject)GameObject.Instantiate(HornetA, (DataBase.Lines[((int)(Random.value * (DataBase.Lines.Length - 0.02f)))].position), new Quaternion());
		}
	}
}
