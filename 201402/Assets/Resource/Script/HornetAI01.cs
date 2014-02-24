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

	public float LifeMax = 0.3f;

	private HornetAIState nowState;
	private int nowLine;
	private float IntervalTime;
	private float Timer;

	private Vector3 randPos;

	private ObjectBurning burningS;

	// Use this for initialization
	void Start () {
		this.gameObject.animation.Play("BeeAnimRun00");

		nowState = HornetAIState.Aproach;
		if (Player == null) {
			nowLine = (int)(Random.value * (DataBase.Lines.Length - 0.01f));
			LineTarget = DataBase.Lines[nowLine];
			Player = DataBase.Player;
			FaceForward = DataBase.FaceForward;
			nowState = HornetAIState.Appear;
		}

		IntervalTime = Time.time + 0.5f + Random.value * 0.9f;
		Timer = 0.0f;

		randPos = new Vector3(
			(Random.value - 0.5f) * 1.7f, 
			(Random.value - 0.5f) * 1.1f, 
			(Random.value - 0.5f) * 1.3f
			);
		LineTarget.position = LineTarget.position + new Vector3();

		burningS = this.transform.GetComponentInChildren<ObjectBurning>();
	}
	
	// Update is called once per frame
	void Update () {
		PlayerLookAt();
#if false
		AproachPlayer();
		if (transform.position.y < -12.0f) Destroy(this.gameObject);
		//if (Time.time > 5.0f) if (burningS != null) burningS.BurnObject();
#else
		switch (nowState) {
		case HornetAIState.Appear:
			AproachPlayer();
			burningS.Count = LifeMax;
			if (Time.time > IntervalTime) nowState = HornetAIState.InitMove;
			break;
		case HornetAIState.InitMove:
			burningS.Count = LifeMax;
			if (LineMove(0.02f)) nowState = HornetAIState.Aproach;
			break;
		case HornetAIState.Aproach:
			AproachPlayer();
			break;
		case HornetAIState.Move:
			break;
		case HornetAIState.FollowForward:
			break;
		case HornetAIState.Burn:
			break;
		case HornetAIState.Dead:
			break;
		case HornetAIState.Speer:
			break;
		}
#endif
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

	bool LineMove (float Length) {
		Vector3 dpos = new Vector3(); // localPosition
		dpos.x = Dot(transform.position, LineTarget.position, transform.right);
		dpos.y = Dot(transform.position, LineTarget.position, transform.up);
		//dpos.z = Dot(transform.position, LineTarget.position, transform.forward);
		//dpos.z=0.0f
		transform.rigidbody.MovePosition(this.transform.position
		    + (transform.right * dpos.x * (1.0f / 32.0f))
		    + (transform.up * dpos.y * (1.0f / 32.0f))
		);
		return (dpos.x * dpos.x + dpos.y * dpos.y + dpos.z * dpos.z < Length * Length);
	}

	bool Distans3(Vector3 pos1, Vector3 pos2, float L) {
		Vector3 dpos = pos1 - pos2;
		return ((Mathf.Pow(dpos.x, 2) + Mathf.Pow(dpos.y, 2) + Mathf.Pow(dpos.z, 2)) < (L * L));
	}

	float Dot(Vector3 rpos, Vector3 tpos, Vector3 Base) {
		Vector3 dpos = tpos - rpos;
		return (dpos.x * Base.x + dpos.y * Base.y + dpos.z * Base.z);
	}
}
