using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBallObj : MonoBehaviour {

	public int ID;
	private int preID;

	private  Vector2 IDPos;

	private Vector2 StartPos;
	private Vector2 LerpPos;
  
	private Vector2[] IDPosArry;

	public  float t ;
	public Image image;
	public Color iccolor;

	public delegate void MyEventHandler();
	public static event MyEventHandler InsUIEvents;

	public delegate void MyEventHandler2(Color color);
	public static event MyEventHandler2 InsBallEvents;

	public delegate void MyEventHandler3(UIBallObj UIballobj);
	public static event MyEventHandler3 RemoveEvents;

	void Start () {

		//实例化后随机获得一个颜色
//		iccolor = HSV.GetColor (Random.Range (0, 361), 360, 80, 80);
		image = this.GetComponent<Image> ();
//		image.color = iccolor;

		//实例化初始位置
		StartPos = new Vector2 (620, 0);
		this.GetComponent<RectTransform> ().anchoredPosition = StartPos;

		//实例化9个固定位置
		IDPosArry =new Vector2[GameObjControl2.UIballsum] ;
		for (int i = 0; i < IDPosArry.Length; i++) {
			IDPosArry [i] = new Vector2 (60 * (i + 1), 0); 
		}

		//监听按钮点击
		Button butn = this.GetComponent<Button> ();
		butn.onClick.AddListener (OnClick);

		//pos =GetComponent<RectTransform> ().anchoredPosition;
		//初始化目标位置
		GetIDPos();


//		GameObjControl2.UIBallList.Add (this.GetComponent<UIBallObj> ());

	}

	// Update is called once per frame
	void Update () {
		image.color = iccolor;
		if (preID != ID) {
			//重新获取目标位置
			GetIDPos();
		}
		//从初始位置差值到目标位置
		LerpMove (0.1f);
		StartPos.x = LerpPos.x;
		preID = ID;


	}

	//行为1：根据ID获得该ID位置
	void GetIDPos(){
		
		switch (ID) {
		case 0:
			IDPos = IDPosArry [0];break;
		case 1:	
			IDPos = IDPosArry [1];break;
		case 2:	
			IDPos = IDPosArry [2];break;
		case 3:	
			IDPos = IDPosArry [3];break;
		case 4:	
			IDPos = IDPosArry [4];break;
		case 5:	
			IDPos = IDPosArry [5];break;
		case 6:	
			IDPos = IDPosArry [6];break;
		case 7:	
			IDPos = IDPosArry [7];break;
		case 8:	
			IDPos = IDPosArry [8];break;
		default:
			Debug.Log ("位置定义错误");
			break;
		}
	}

	//行为2：根据初始位置和目标位置差值移动；
	void LerpMove(float speed){
		t += Time.deltaTime/10*speed;
		t = Mathf.Clamp01 (t);
		LerpPos.x= Mathf.Lerp(StartPos.x,IDPos.x,t);
		this.GetComponent<RectTransform> ().anchoredPosition = LerpPos;
	}
	//点击事件
	void OnClick(){
		if (GameObjControl2.Energy >= 1) {
			//移除该组件
			if(RemoveEvents !=null)
				RemoveEvents.Invoke(this.GetComponent<UIBallObj>());
//         	GameObjControl2.UIBallList.Remove (this.gameObject.GetComponent<UIBallObj> ());
			//调用control中的实例化UI小球，并添加到list

			if (InsBallEvents != null)
				InsBallEvents.Invoke (GetColor ());

			if (InsUIEvents != null)
				InsUIEvents.Invoke ();
			
			GameObjControl2.Energy -= 1;

			Destroy (this.gameObject);
		}
	}

	public Color GetColor(){
		return this.GetComponent<Image> ().color;
	}
}
