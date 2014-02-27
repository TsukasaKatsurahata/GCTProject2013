using UnityEngine;
using System.Collections;

public class ModelAnimation : MonoBehaviour {
	private Animation anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
		if (anim == null) return;
		if (!anim.isPlaying) anim.Play("Take 001");
	}

	public void SetAnimSpeer() {
		if (anim == null) return;
		anim.Play("Take 002");
	}

	public void SetAnimDead() {
		if (anim == null) return;
		anim.Play("Take 003");
	}
}
