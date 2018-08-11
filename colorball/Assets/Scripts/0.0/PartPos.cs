using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartPos : MonoBehaviour {

	ParticleSystem partsys;
	public  float r =3;
	Vector3 pos3;
	void Start () {
		partsys = this.GetComponent<ParticleSystem> ();

		if (this.tag == "red") {
			partsys.startColor = HSV.GetColor (0, 360,80,80);
			pos3.x = r * Mathf.Cos (0*Mathf.Deg2Rad);
			pos3.y = r * Mathf.Sin (0*Mathf.Deg2Rad);
			pos3.z = -1;
			this.transform.position = pos3;

		}
		if (this.tag == "green") {
			partsys.startColor = HSV.GetColor (120, 360,80,80);
			pos3.x = r * Mathf.Cos (120*Mathf.Deg2Rad);
			pos3.y = r * Mathf.Sin (120*Mathf.Deg2Rad);
			pos3.z = -1;
			this.transform.position = pos3;
		}
		if (this.tag == "blue") {
			partsys.startColor = HSV.GetColor (240, 360,80,80);
			pos3.x = r * Mathf.Cos (240*Mathf.Deg2Rad);
			pos3.y = r * Mathf.Sin (240*Mathf.Deg2Rad);
			pos3.z = -1;
			this.transform.position = pos3;
		}

	}
	
	void Update () {

	}
}
