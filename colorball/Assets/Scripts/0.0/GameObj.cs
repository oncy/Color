using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameObj : MonoBehaviour {
	//自带分数
	//private int Score;
	private SpriteRenderer render;
	private Light childlight;

	void Start () {
		render = GetComponent<SpriteRenderer> ();
		childlight =GetComponentInChildren<Light>(); 
		if (this.tag == "CC") {
			//render.color = HSV.GetColorbyPosition (this.transform.position);
		}
		//render.color = HSV.GetColorbyPosition (this.transform.position);

		//改变灯光颜色
		childlight.color = render.color;
		//Score =(int) Vector3.Distance(this.transform.position,Vector3.one);

		if (this.tag == "R") {
			
		}


	}
	

	void Update () {
		//childlight.color = render.color;

	}




}
