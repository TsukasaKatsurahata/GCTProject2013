using UnityEngine;
using System.Collections;

public class ObjectBurning : MonoBehaviour {
	public Object BurnEffect = null;
	public bool burning = false;
	public float Count = 0.5f;

	// Use this for initialization
	void Start () {
		burning = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void BurnObject() {
		if (burning) return;
		Count -= Time.deltaTime;
		if (Count > 0.0f) return;
		GameObject effect = (GameObject)GameObject.Instantiate (BurnEffect, this.transform.position, this.transform.rotation);
		effect.transform.parent = this.transform;
		BurnEffect = effect;
		burning = true;
	}
}
