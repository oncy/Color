using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameObjControl : MonoBehaviour
{
	
	public CenterObj center;
	public GameObject[] GamePrefabs;

	//游戏对象数目
	public static int GameSum =1;
	//游戏对象距离
	public  float DistR = 3;

	public  static float Energy = 15;
	public  static float Score = 0;

	public Canvas canvas;
	public RectTransform UISpeed;
	private static float speedvalue=0.7f;

	public  Text EnergyText;
	public  Text SurText;
	public  Text GetText;

	//重新开始对象
	public GameObject REObj;

	//控制游戏对象位置
	private Vector2 pos2;
	private Vector3 pos3;

	//一次的生存时间
//	private float oncetime;
	//（参数）程序运行时间
//	private float lasttime;

	//游戏模式
//	public enum Mode
//	{
//		MixedMode,
//		ColorMode,
//		NoneColorMode
//	};
//	public Mode GameMode;
	//	public static IO ioobj;
	//	private string[] GameModes = { "MixedMode", "ColorMode", "NoneColorMode" };
//	private bool GameOver ;
	//小球发射事件
	public delegate void MyEventHandler();
	public static event MyEventHandler InsBallEvents;


	void Start ()
	{
		RePlay ();

		BallObj.InsGameObjEvent += InsGameObjCircle;

		//初始化速度
		UISpeed.sizeDelta = new Vector2 (speedvalue * 200.0f, 20);
        CenterObj.sum = 11 - 10 * speedvalue;
//		SurText.text = "Survival Time: " +Math.mathfn( oncetime,3) + "s";
	}

	void Update ()
	{

//		oncetime = Time.time - lasttime;
//		Energy -= Time.deltaTime;

		EnergyText.text = "Energy : " + Energy;
		SurText.text = "Score: " +Score;

		if ((Input.GetMouseButtonUp (0) ||Input.GetMouseButtonUp (1))
			&& !EventSystem.current.IsPointerOverGameObject()
		) {
			
			if (Energy >= 1f ) {
				if(InsBallEvents !=null)
				InsBallEvents.Invoke ();
				Energy--;

			} 
		}
		if (Energy <= 0.0f  ) {
			if (IshaveballinScreen (CenterObj.BallLists [CenterObj.BallLists.Count -1]))
				return;
			//时间暂停
			Time.timeScale = 0;
			Energy = 0;
//			GameOver = true;

			GetText.text = "你的得分是 : " + Score;
			REObj.gameObject.SetActive (true);

			}
			
	}

	/*
	private void  InsGameObjRandom ()
	{
		//实例对象的随机类型，一种有颜色、一种无
		int i = Random.Range (0, 2);

		//获得一个随机在屏幕中位置
		ReturnRandompos (out pos2, out pos3);
		//次数
		int n = 0;
		//检测这个地方是否有碰撞体，有则重新生成坐标
		while (Physics2D.OverlapCircleAll (pos2, 0.5f).Length != 0 || Vector2.Distance (pos2, Vector2.zero) < 1f) {
			ReturnRandompos (out pos2, out pos3);
			n++;
			if (n > 100) {
				Debug.Log ("找不到空的屏幕点");
				return;
			}
		}
		//实例化对象
		GameObject obj = Instantiate (GamePrefabs [i], pos3, Quaternion.identity);
		obj.transform.parent = this.transform;

		//假如有颜色
		if (GamePrefabs [i].tag == "CC") {
			obj.GetComponent<SpriteRenderer> ().color = HSV.GetColorbyPosition (pos3);
			Debug.Log ("已实例CC");

		}
		
	}
*/
	//实例化规则的圆上的对象
	private void  InsGameObjCircle ()
	{
		int i = Random.Range (0, 2);
		Vector3 TempRamdom = HSV.GetColorDirExceptRedandGreen () * DistR;
		GameObject obj = Instantiate (GamePrefabs [i], TempRamdom, Quaternion.identity);
		obj.transform.parent = this.transform;

	}
	/*
	//控制实例化数目
	private void  InsGameObjCircle (int sum)
	{
		for (int j = 0; j < sum; j++) {
			int i = Random.Range (0, 2);
			Vector3 TempRamdom = HSV.GetColorDir () * DistR;
			int n = 0;
			while (Physics2D.OverlapCircleAll (TempRamdom, 1.0f).Length != 0) {
				TempRamdom = HSV.GetColorDir () * DistR;
				n++;
				if (n > 100) {
					Debug.Log ("出错");
					return;
				}
			}
			GameObject obj = Instantiate (GamePrefabs [i], TempRamdom, Quaternion.identity);
			obj.transform.parent = this.transform;
		}
	}

	//返回屏幕中的一个位置,并转化为世界坐标
	void ReturnRandompos (out Vector2 p2, out Vector3 p3)
	{
		Vector3 tempp3;
		tempp3.x = Random.Range (0, Screen.width);
		tempp3.y = Random.Range (0, Screen.height);
		tempp3.z = 0;
		p3 = Camera.main.ScreenToWorldPoint (tempp3);
		//Debug.Log ("tempp3: "+tempp3.x + "  " + tempp3.y + " " + tempp3.z);
		//Debug.Log ("p3: "+ p3.x + "  " + p3.y + " " + p3.z);
		p3.z = 0;
		p2.x = p3.x;
		p2.y = p3.y;
	}
	*/

	bool IshaveballinScreen(GameObject ballobj){
		Vector3 xyz = ballobj.transform.position;
		Vector3 Screenxyz;
		Screenxyz = Camera.main.WorldToScreenPoint (xyz);
		if (Screenxyz.x <= 0 || Screenxyz.x >= Screen.width || Screenxyz.y <= 0 || Screenxyz.y > Screen.height) {
			Debug.Log (Screenxyz);
			return false;
		} else {
//			Debug.Log (Screenxyz + "2");

			return true;
		}
	}

	IEnumerator wait ()
	{
		yield return new WaitForSeconds (Time.deltaTime);
//		GameOver =false;
		Time.timeScale = 1;

		//CanIns = true;
		//tishiText.gameObject.SetActive (false);
	}
	IEnumerator wait1s ()
	{
		yield return new WaitForSeconds (1);

	}

	public void RePlay ()
	{
		REObj.gameObject.SetActive (false);
		Score = 0;
		Energy = 15;

//		lasttime += oncetime;


		//销毁所有子对象
		for (int i = 0; i < this.transform.childCount; i++) {
			GameObject children = this.transform.GetChild (i).gameObject;
			Destroy (children);
		}
		//重新实例化对象
		InsGameObjCircle ();

		CenterObj.speed = 0;

		if(center.render !=null)
		center.render.color = HSV.GetColor (0, 360);
		
		if (CenterObj.BallLists != null) {
			CenterObj.BallLists.Clear ();
		}
		
		Time.timeScale = 1;

//		StartCoroutine(wait());	

	}

	public void ControlUISpeed( ){
		Vector2 _pos = Vector2.one;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(UISpeed,
			Input.mousePosition, canvas.worldCamera, out _pos);
//		Debug.Log("UI:" + _pos);
		UISpeed.sizeDelta = new Vector2 (_pos.x, 20);
		speedvalue = _pos.x / 200.0f;
		speedvalue = Mathf.Clamp01 (speedvalue);
		Debug.Log (speedvalue);
		CenterObj.sum = 11 - 10 * speedvalue;

	}
		

	void OnDisable(){
		BallObj.InsGameObjEvent -= InsGameObjCircle;
	}
}
