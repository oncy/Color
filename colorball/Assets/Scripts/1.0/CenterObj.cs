using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CenterObj : MonoBehaviour {
	//色环变化属性
	public static float sum =5.0f;
	//纯度
	public static float s=80;
	//亮度
	public static float v=80;

	//实例化小球参数
	public GameObject PrefabBall;
	public GameObject Preparent;
	public int Foce =500;

	//颜色变化参数
	public static float speed ;

	public  SpriteRenderer render;
	private Light childlight;
	private Color SpriteColor;
	//记录时间,按住鼠标时间超过t则开始旋转
	float t;
	public static List<GameObject> BallLists;

	void Start () {
		BallLists = new List<GameObject> ();

		GameObjControl.InsBallEvents += InsBall;

		render = this.GetComponent<SpriteRenderer> ();
//		childlight =GetComponentInChildren<Light>();

		render.color = HSV.GetColor (0, sum, s, v);
//		childlight.color = render.color;
	}
	
	void Update () {
		//变色
//		childlight.color = render.color;
		//超过则重置
		if (Mathf.Abs (speed) >= sum) {
			speed = 0;
		}

		if(speed >0)
			render.color = HSV.GetColor (speed, sum, s, v);
		if(speed <0)
			render.color = HSV.GetColor (sum+speed, sum, s, v);
		
		if (Input.GetMouseButton (0) && !EventSystem.current.IsPointerOverGameObject()) {
			t += Time.deltaTime;
//			Debug.Log (t);
			if(t>0.2f)
			speed +=  Time.deltaTime;
		}
		if (Input.GetMouseButton (1)&& !EventSystem.current.IsPointerOverGameObject()) {
			t += Time.deltaTime;
			if(t>0.2f)
			speed -=  Time.deltaTime;
			}

		if ((Input.GetMouseButtonUp (0) || Input.GetMouseButtonUp (1))) {
			t = 0;
		}


	}

	public void InsBall(){
		//颜色方向
		Vector3 dir = HSV.GetColorDir (render.color);
		//实例化小球
		GameObject obj= Instantiate (PrefabBall, this.transform.localPosition, Quaternion.identity);
		obj.transform.localScale = Vector3.one;
		obj.transform.parent = Preparent.transform;

		obj.GetComponent<SpriteRenderer> ().color = render.color;
		//尾部渲染
		obj.GetComponent<TrailRenderer> ().startColor = render.color;
		obj.GetComponent<TrailRenderer> ().endColor = render.color;
		//给小球一个力
		Vector2 pos2;
		pos2.x = dir.x;
		pos2.y = dir.y;
		obj.GetComponent<Rigidbody2D> ().AddForce (pos2 *Foce);
//		Debug.Log(Vector3.Distance(Vector3.zero,obj.transform.localPosition));
		BallLists.Add(obj);
	}

	void OnDisable(){
		Debug.Log ("销毁1");
		GameObjControl.InsBallEvents -= InsBall;

	}

}
