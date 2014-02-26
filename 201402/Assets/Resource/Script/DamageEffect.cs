using UnityEngine;
using System.Collections;

public class DamageEffect : MonoBehaviour {
	public GameObject Effect = null;
	public GameObject SetCamera = null;

	public static DamageEffect Instance = null;
	public static DamageEffect getInstance {
		get{
			return ((Instance != null) ? Instance : new DamageEffect());
		}
	}

	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown(KeyCode.Z)) SetEffect();
	}

	public void SetEffect() {
		if (Effect == null) return;
		if (SetCamera == null) return;
		GameObject effectSetter = 
			(GameObject)Instantiate(Effect, Vector3.zero, new Quaternion());
		effectSetter.transform.parent = SetCamera.transform;
		effectSetter.transform.localPosition =
			new Vector3(0, 0, 1);
	}
}
