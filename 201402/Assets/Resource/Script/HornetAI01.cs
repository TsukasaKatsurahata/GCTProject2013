using UnityEngine;
using System.Collections;

enum HornetAIState {
	Appear,
	InitMove,
	Aproach,
	Move,
	FollowForward,
	Speer,
	Burn,
	Dead,
	Max,
};

public class HornetAI01 : MonoBehaviour {
	public Transform LineTarget = null;
	public GameObject Player = null;
	public GameObject FaceForward = null;

	private HornetAIState nowState;
	private float IntervalTime;
	private float Timer;

	private ObjectBurning burningS;

	// Use this for initialization
	void Start () {
		this.gameObject.animation.Play("BeeAnimRun00");

		nowState = HornetAIState.Aproach;
		if (Player == null) {
			LineTarget = DataBase.Lines[(int)(Random.value * (DataBase.Lines.Length - 0.01f))];
			Player = DataBase.Player;
			FaceForward = DataBase.FaceForward;
			nowState = HornetAIState.Appear;
		}

		IntervalTime = Time.time + 0.5f + Random.value * 0.9f;
		Timer = 0.0f;

		burningS = this.transform.GetComponentInChildren<ObjectBurning>();
	}
	
	// Update is called once per frame
	void Update () {
		PlayerLookAt();
		AproachPlayer();
		if (transform.position.y < -12.0f) Destroy(this.gameObject);
		//if (Time.time > 5.0f) if (burningS != null) burningS.BurnObject();
	}

	void PlayerLookAt() {
		if (this.Player == null) return;
		transform.LookAt(this.Player.transform.position);
	}

	void AproachPlayer() {
		if (burningS.burning) { transform.rigidbody.MovePosition(this.transform.position + new Vector3(0, -0.03f, 0)); return; }
		if (Distans3(this.transform.position, Player.transform.position, 1.0f)) return;
		transform.rigidbody.MovePosition(this.transform.position + this.transform.forward * 0.02f);
		if (this.Player == null) return;
	}

	bool Distans3(Vector3 pos1, Vector3 pos2, float L) {
		Vector3 dpos = pos1 - pos2;
		return ((Mathf.Pow(dpos.x, 2) + Mathf.Pow(dpos.y, 2) + Mathf.Pow(dpos.z, 2)) < (L * L));
	}
}
