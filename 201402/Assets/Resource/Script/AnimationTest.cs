using UnityEngine;
using System.Collections;

public class AnimationTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		animation.Play("Take 002");
		//animation.Blend("Take 001");
	}
	
	// Update is called once per frame
	void Update () {
		if (!animation.isPlaying) animation.Blend("Take 001");
		if ((int)Time.time > (int)(Time.time - Time.deltaTime)
		    && (int)Time.time % 5 == 3)
			animation.Play("Take 002");
	}
}
