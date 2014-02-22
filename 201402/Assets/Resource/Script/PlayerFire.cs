using UnityEngine;
using System.Collections;

public class PlayerFire : MonoBehaviour {
	public GameObject[] FireInstance = new GameObject[1];
	private byte[] buttom = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, };
	private Vector3 localFirePosition;

	// Use this for initialization
	void Start () {
		localFirePosition = FireInstance[0].transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		//if (Time.time < 1.0f) return;
		//if (FireInstance[0] == null) return;
		byte id = (byte)ArduinoInput.InputData;
		for (int i = 0; i < FireInstance.Length && i < 7; i++) {
#if false
			FireInstance[i].SetActive((buttom[i] & id) > 0x00);
#else
			if ((buttom[i] & id) > 0x00) {
				//Destroy(FireInstance[i]);
				Transform obj = FireInstance[i].transform.FindChild("Fire00");
				if (obj == null) break;
				FireCollision fc = obj.GetComponent<FireCollision>();
				if (fc != null) fc.RefireEffect();
			}
			else {
				Transform obj = FireInstance[i].transform.FindChild("Fire00");
				if (obj == null) break;
				RemoveEffectFire fr = obj.GetComponent<RemoveEffectFire>();
				if (fr != null) fr.RemoveEffect();
			}
#endif
		}
	}
}
