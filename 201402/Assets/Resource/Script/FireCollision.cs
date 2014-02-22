using UnityEngine;
using System.Collections;

public class FireCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider col) {
#if false
		col.transform.rigidbody.AddForce(this.transform.up * 10, ForceMode.Acceleration);
#else
		ObjectBurning scr = (ObjectBurning)col.GetComponent<ObjectBurning>();
		if (scr == null) return;
		scr.BurnObject();
#endif
	}
	
	public void RefireEffect() {
		collider.enabled = true;
		this.transform.GetChild(0).FindChild("InnerCore").particleEmitter.emit = true;
		this.transform.GetChild(0).GetChild(0).FindChild("InnerCore2").particleEmitter.emit = true;
		this.transform.GetChild(0).GetChild(0).FindChild("OuterCore").particleEmitter.emit = true;
		this.transform.GetChild(0).GetChild(0).FindChild("smoke").particleEmitter.emit = true;
	}
}
