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
	GoAway,
	Max,
};

public class HornetAI01 : MonoBehaviour {
	public Transform LineTarget = null;
	private Vector3 LineTargetPos;
	public GameObject Player = null;
	public GameObject FaceForward = null;
	
	public float LifeMax = 0.3f;

	private HornetAIState nowState;
	private int nowLine;
	private float IntervalTime;
	private float Timer;
	private float LMMV;

	private Vector3 randPos;

	private ObjectBurning burningS;

	private ModelAnimation modelAnim;

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
		LMMV = 0.0f;

		randPos = new Vector3(
			(Random.value - 0.5f) * 1.7f, 
			(Random.value - 0.2f) * 0.8f, 
			(Random.value - 0.5f) * 1.3f
			);
#if false
		LineTarget.position = LineTarget.position 
			+ LineTarget.right   * randPos.x
			+ LineTarget.up      * randPos.y
			+ LineTarget.forward * randPos.z;
#else
		LineTargetPos = LineTarget.position 
			+ LineTarget.right   * randPos.x
				+ LineTarget.up      * randPos.y
				+ LineTarget.forward * randPos.z;
#endif

		burningS = this.transform.GetComponentInChildren<ObjectBurning>();

		AudioSource AS = GetComponent<AudioSource>();
		if (AS != null) AS.pitch = (Random.value - 0.5f) - 0.5f;

		modelAnim = this.transform.GetComponentInChildren<ModelAnimation>();
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
			if (LineMove(0.02f)) { 
				nowState = HornetAIState.Aproach; 
				NextLineSet();
				//animation.CrossFade("BeeAnimIdle00");
			}
			//AproachPlayer();
			break;
		case HornetAIState.Aproach:
			if (burningS.burning) nowState = HornetAIState.Burn;
			if (Time.time > IntervalTime) nowState = HornetAIState.Move;
			AproachPlayer();
			if (Distans3(this.transform.position, Player.transform.position, 1.0f)) { 
				nowState = HornetAIState.FollowForward; 
				animation.CrossFade("BeeAnimIdle00");
				Player = DataBase.CameraLookAt;
			}
			break;
		case HornetAIState.Move:
			if (burningS.burning) nowState = HornetAIState.Burn;
			if (LineMove(0.02f)) { 
				nowState = HornetAIState.Aproach; 
				NextLineSet();
			}
			break;
		case HornetAIState.FollowForward:
			if (FaceFollowing()) { 
				nowState = HornetAIState.Speer;
				if (modelAnim != null) modelAnim.SetAnimSpeer();
				Timer = Time.time + 0.55f;
				IntervalTime = Time.time + 1.3f;
			}
			break;
		case HornetAIState.Burn:
			Timer = Time.time + 15.0f;
			IntervalTime = Time.time + 6.0f;
			animation.CrossFade("BeeAnimIdle00");
			//transform.GetChild(0).rigidbody.useGravity = true;
			transform.rigidbody.useGravity = true;
			transform.rigidbody.constraints = new RigidbodyConstraints();
			//transform.GetChild(0).rigidbody.isKinematic = false;
			//transform.GetChild(0).rigidbody.AddTorque(-0.1f,0.0f,0.0f,ForceMode.VelocityChange);
			nowState = HornetAIState.Dead;
			break;
		case HornetAIState.Dead:
			if (Time.time > IntervalTime) {
				burningS.RemoveBurn();
#if false
				MeshRenderer[] MR = GetComponentsInChildren<MeshRenderer>();
				foreach (MeshRenderer mr in MR) {
					mr.enabled = false;
				}
#else
				SkinnedMeshRenderer[] SMR = 
					GetComponentsInChildren<SkinnedMeshRenderer>();
				foreach (SkinnedMeshRenderer smr in SMR) {
					smr.enabled = false;
				}
#endif
			}
			if (Time.time > Timer) {
				Destroy(this.gameObject);
			}
			break;
		case HornetAIState.Speer:
			if (Time.time > Timer) {
				DamageEffect.getInstance.SetEffect();
				Timer = Time.time + 30.0f;
			}
			if (Time.time > IntervalTime) nowState = HornetAIState.GoAway;
			break;
		case HornetAIState.GoAway:
			GoAway();
			break;
		default:
			break;
		}
#endif
	}

	void PlayerLookAt() {
		if (this.Player == null) return;
		transform.LookAt(this.Player.transform.position);
	}

	void AproachPlayer() {
		//if (burningS.burning) { transform.rigidbody.MovePosition(this.transform.position + new Vector3(0, -0.03f, 0)); return; }
		//if (Distans3(this.transform.position, Player.transform.position, 1.0f)) return;
		transform.rigidbody.MovePosition(this.transform.position + this.transform.forward * 0.014f);
		if (this.Player == null) return;
	}

	bool LineMove (float Length) {
		bool result = false;
		LMMV += 98f * Time.deltaTime * Time.deltaTime;
		Vector3 dpos = new Vector3(); // localPosition
#if false
		dpos.x = Dot(transform.position, LineTarget.position, transform.right);
		dpos.y = Dot(transform.position, LineTarget.position, transform.up);
#else
		dpos.x = Dot(transform.position, LineTargetPos, transform.right);
		dpos.y = Dot(transform.position, LineTargetPos, transform.up);
#endif
		result = (dpos.x * dpos.x + dpos.y * dpos.y + dpos.z * dpos.z < Length * Length);
		//dpos.z = Dot(transform.position, LineTarget.position, transform.forward);
		//dpos.z=0.0f
		if (Mathf.Abs(dpos.x) > LMMV) dpos.x = ((dpos.x < 0.0f) ? -1.0f : 1.0f) * LMMV;
		if (Mathf.Abs(dpos.y) > LMMV) dpos.y = ((dpos.y < 0.0f) ? -1.0f : 1.0f) * LMMV;
		transform.rigidbody.MovePosition(this.transform.position
		    + (transform.right * dpos.x * (1.0f / 32.0f))
		    + (transform.up * dpos.y * (1.0f / 32.0f))
		    + (transform.forward * 0.01f)
		);
		return result;
	}

	void NextLineSet() {
		LMMV = 0.0f;
		IntervalTime = Time.time + 2.2f + Random.value * 8.0f;
		nowLine = nowLine + ((nowLine < 1) ? 1 : (nowLine > 5) ? -1 : (Random.value < 0.5f) ? 1 : -1);
		LineTarget = DataBase.Lines[nowLine];
#if false
		LineTarget.position = LineTarget.position 
			+ LineTarget.right   * randPos.x
			+ LineTarget.up      * randPos.y
			+ LineTarget.forward * randPos.z;
#else
		LineTargetPos = LineTarget.position 
			+ LineTarget.right   * randPos.x
			+ LineTarget.up      * randPos.y
			+ LineTarget.forward * randPos.z;
#endif
	}

	bool FaceFollowing() {
		if (FaceForward == null) return true;
		Vector3 dpos = FaceForward.transform.position - this.transform.position;
		transform.rigidbody.MovePosition(this.transform.position + dpos * (1.0f / 32.0f));
		return (dpos.x * dpos.x + dpos.y * dpos.y + dpos.z * dpos.z) < (0.08f * 0.08f);
	}

	void GoAway() {
		LMMV += 98f * Time.deltaTime * Time.deltaTime;
		rigidbody.MovePosition(transform.position + Vector3.up * LMMV + transform.forward * -0.01f);
		if (transform.position.y > 25.0f) Destroy(this.gameObject);
	}

	bool Distans3(Vector3 pos1, Vector3 pos2, float L) {
		Vector3 dpos = pos1 - pos2;
		return ((Mathf.Pow(dpos.x, 2) + Mathf.Pow(dpos.y, 2) + Mathf.Pow(dpos.z, 2)) < (L * L));
	}

	float Dot(Vector3 rpos, Vector3 tpos, Vector3 Base) {
		Vector3 dpos = tpos - rpos;
		return (dpos.x * Base.x + dpos.y * Base.y + dpos.z * Base.z);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Ground") {
			transform.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
	}
}
