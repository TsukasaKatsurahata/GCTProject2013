using UnityEngine;
using System.Collections;

public class RemoveEffectFire : MonoBehaviour {
	public float RemovalTime = 1.0f;
	private float timer = 0.0f;
	private byte removal = 0x00;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		removal = 0x00;
	}
	
	// Update is called once per frame
	void Update () {
		if (removal > 0) {
			if ((removal | 0x02) > 0) removal = 0x00;
			else                      removal = 0x00;
		}
		else timer = 0.0f;
	}

	public void RemoveEffect() {
		removal = 0x03;
		timer += Time.deltaTime;
		if (timer > RemovalTime) collider.enabled = false;
		this.transform.GetChild(0).FindChild("InnerCore").particleEmitter.emit = false;
		this.transform.GetChild(0).GetChild(0).FindChild("InnerCore2").particleEmitter.emit = false;
		this.transform.GetChild(0).GetChild(0).FindChild("OuterCore").particleEmitter.emit = false;
		this.transform.GetChild(0).GetChild(0).FindChild("smoke").particleEmitter.emit = false;
	}
}
