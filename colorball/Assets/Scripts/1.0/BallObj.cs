using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallObj : MonoBehaviour {
	
	//颜色渲染
	private SpriteRenderer render;
	private Light childlight;

	//判断小球的屏幕位置
	private Transform ballpos3;
	private Vector2 ballpos2;
	private float OutScreenDist = 100f;

	public delegate void MyEventHandler();
	public static event MyEventHandler InsGameObjEvent;
 
	void Start () {
		ballpos3 = this.GetComponent<Transform> ();
		render =this.GetComponent<SpriteRenderer>();
		childlight = this.GetComponentInChildren<Light> ();
	}
	  
	void Update () {
		//渲染灯光颜色
		childlight.color = render.color;
		//判断位置超出屏幕
		ballpos2 = Camera.main.WorldToScreenPoint (ballpos3.position);
		if (ballpos2.x> (Screen.width+OutScreenDist) || ballpos2.y >= (Screen.height+OutScreenDist)
			||ballpos2.x<=(0-OutScreenDist)||ballpos2.y<=(0-OutScreenDist)) {
			//Debug.Log (ballpos2.x + " " + ballpos2.y+"屏幕是"+Screen.height + " " + Screen.width);
			//判断已飞出
			//isout = true;
			//停留在原地
			GetComponent<Rigidbody2D>().velocity=Vector2.zero;
			Destroy (this.gameObject,1);
		}



	}

	void  OnCollisionEnter2D(Collision2D other){

	}
	//小球消灭对象
	void  OnTriggerEnter2D(Collider2D other){
		if (other.tag == "C" || other.tag == "CC") {
			Destroy (other.gameObject);
			GameObjControl.Energy += 1;
			GameObjControl.Score += 1;
			StartCoroutine (waittimetoInsGameObj ());

		}

	}

	IEnumerator waittimetoInsGameObj ()
	{
		yield return new WaitForSeconds (10*Time.deltaTime);
		if(InsGameObjEvent !=null)
			InsGameObjEvent ();
	}

}
