using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjControl2 : MonoBehaviour
{
	//游戏对象
	public GameObject[] GamePrefabs;
	//游戏对象距离
	public float DistR =3;
	//游戏对象个数
	private int Gameobjssum =1;

	//UI设置
	public GameObject UIBallPrefab;
	//设置UI小球个数
	public static int UIballsum =9;
	public GameObject UIBallParent;
	public List<UIBallObj> UIBallList;
	//颜色数组储存颜色，随机取出给UIball
	List<Color> ColorList =new List<Color>(); 
	//发射的小球
	public GameObject BallPrefab;

	//UI界面的分数
	public  Text EgText;
	public  Text SurText;
	public  Text GetText;

	public  GameObject ReObj;

	public  static float Energy = 15;
	public  static int Score = 0;
	public static float Interval =40;


	Vector3 GameObjPosition;
	public static List<GameObject> BallLists =new List<GameObject>();


	//一次的生存时间
//	private float oncetime;
	//（参数）程序运行时间
//	private float lasttime;

	void Start () {

		for (int i = 0; i < UIballsum; i++) {
			InsUIBall ();
		}
		//------------------实例化新的List、设置信息-----------------
//        UIBallList = new List<UIBallObj> ();
		//List初始化属性、存储UI小球位置
//		for (int i = 0; i < UIBallParent.transform.childCount; i++) {
//			UIBallList.Add( UIBallParent.transform.GetChild (i).GetComponent<UIBallObj>());
//		}
		//分别设置9个ID
//		for (int i = 0; i < UIBallList.Count; i++) {
//			UIBallList [i].ID = i;
//		}
		//------------------ 添加实例事件--------------------
//		UIBallObj.InsUIEvents += InsUIBall;
		//实例化后重新排列
		UIBallObj.InsUIEvents += Rearrange;

		BallObj2.InsGameObjEvent += InsGameObjCircle;
		BallObj2.ChangeColorEvent += ChangeColorinUIBall;

		UIBallObj.InsBallEvents += InsBall;
		UIBallObj.InsBallEvents += InsUIBall;
		UIBallObj.RemoveEvents += RemoveUIBallinList;

		//-------------------实例化游戏对象，假如有子对象，销毁所有对象---------------------
		RePlay ();

	}

	void Update(){
		//----------------更新分数------------------------
//		Energy -= Time.deltaTime;
//		Energy = Math.mathfn (Energy, 3);
		EgText.text = "Energy : " + Energy;
		SurText.text = "Survival Time: " + Score ;

//		oncetime = Time.time - lasttime;
		//----------------检测GameOver------------------------
		if (Energy <= 0) {
			if (IshaveballinScreen (BallLists [BallLists.Count -1]))
				return;
			Time.timeScale = 0;
			Energy = 0;
				for (int i = 0; i < UIballsum; i++) {
					UIBallParent.transform.GetChild (i).GetComponent<Button> ().interactable = false;
				}
		
				ReObj.SetActive (true);
				GetText.text = "你的得分是 : " + Score;
				
			}
	}
	/*
	Color RandomGetColorinUIList(int listlength =9){
		Color color;
		int i = Random.Range (0, listlength);
		color = UIBallList [i].GetColor();
		return color;
	}
*/

	void  InsGameObjCircle ()
	{
		int i = Random.Range (0, 2);
		GameObjPosition = HSV.GetColorDirExceptRedandGreen () * DistR;

		GameObject obj = Instantiate (GamePrefabs [i], GameObjPosition, Quaternion.identity);
		obj.transform.parent = this.transform;
	
	}


	/*
	//控制实例化数目
	void  InsGameObjCircle (int sum)
	{
		for (int j = 0; j < sum; j++) {
			int i = Random.Range (0, 2);
			Vector3 TempRamdom = HSV.GetColorDirExceptRedandGreen () * DistR;
			int n=0;//记录次数
			while (Physics2D.OverlapCircleAll (TempRamdom, 0.5f).Length != 0) {
				TempRamdom = HSV.GetColorDir (RandomReturnColorinUIList ()) * DistR;
				n++;
				if (n > 100) {
					Debug.Log ("找不到点,随便找一点实例化对象");
					while (Physics2D.OverlapCircleAll (TempRamdom, 0.5f).Length != 0) 
						TempRamdom = HSV.GetColorDir () * DistR;
				}
			}
			GameObject obj = Instantiate (GamePrefabs [i], TempRamdom, Quaternion.identity);
			obj.transform.parent = this.transform;
		}
	}
	*/
	//实例化UI小球
	void InsUIBall(){
		GameObject obj = Instantiate (UIBallPrefab);
		obj.transform.parent = UIBallParent.transform;
		obj.transform.localScale = new Vector3 (0.7f, 0.7f, 1f);

		obj.GetComponent<UIBallObj> ().ID = UIBallParent.transform.childCount-1;
//		obj.GetComponent<UIBallObj> ().iccolor = color;
		UIBallList.Add (obj.GetComponent<UIBallObj> ());

	}
	void InsUIBall(Color color){
		GameObject obj = Instantiate (UIBallPrefab);
		obj.transform.parent = UIBallParent.transform;
		obj.transform.localScale = new Vector3 (0.7f, 0.7f, 1f);

		obj.GetComponent<UIBallObj> ().ID = UIBallParent.transform.childCount-1;
		obj.GetComponent<UIBallObj> ().iccolor = color;
		UIBallList.Add (obj.GetComponent<UIBallObj> ());

	}

	void ChangeColorinUIBall(float AngleInterval =40){
		ColorList.Clear ();
		//获取颜色
		Color GameObjColor = HSV.GetColorbyPosition (GameObjPosition);
		//获取颜色HSV模型下的H
		float G_H = HSV.ReturnHbyColor (GameObjColor);
		//添加LIst
		ColorList.Add(HSV.GetColor(G_H,360));
		float G_nextH;
		for (int i = 1; i <= 4; i++) {
			G_nextH = G_H + i * AngleInterval;
			if (G_nextH > 360)
				G_nextH -= 360;
			ColorList.Add(HSV.GetColor(G_nextH,360));

		}
		for (int i = 1; i <= 4; i++) {
			G_nextH = G_H - i * AngleInterval;
			if (G_nextH < 0)
				G_nextH += 360;
			ColorList.Add(HSV.GetColor(G_nextH,360));
		}

		//颜色随机顺序给UIball
		if (ColorList.Count == 9) {
			for (int i = 0; i < UIballsum; i++) {
				int index = Random.Range (0, ColorList.Count);
				UIBallList [i].iccolor = ColorList [index];
				ColorList.RemoveAt (index);
			}
		}

	}

	//实例化发射小球
	void InsBall(Color ballcolor){
		//颜色方向
		Vector3 dir = HSV.GetColorDir (ballcolor);
		//实例化小球
		GameObject obj= Instantiate (BallPrefab, Vector3.zero, Quaternion.identity);
		obj.transform.localScale = Vector3.one;
		obj.transform.parent = this.transform;

		obj.GetComponent<SpriteRenderer> ().color = ballcolor;
		//尾部渲染
		obj.GetComponent<TrailRenderer> ().startColor = ballcolor;
		obj.GetComponent<TrailRenderer> ().endColor = ballcolor;
		obj.transform.SetAsLastSibling ();
		//给小球一个力
		Vector2 pos2;
		pos2.x = dir.x;
		pos2.y = dir.y;
		obj.GetComponent<Rigidbody2D> ().AddForce (pos2 *500);

		BallLists.Add (obj);
	}

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

	public void RePlay ()
	{
		ReObj.gameObject.SetActive (false);
		Energy = 15;
		Score = 0;
		Interval = 40;
//		lasttime += oncetime;

		//销毁所有子对象
		for (int i = 0; i < this.transform.childCount; i++) {
			GameObject children = this.transform.GetChild (i).gameObject;
			Destroy (children);
		}
		//重新实例化对象
		InsGameObjCircle ();
		ChangeColorinUIBall ();


		for(int i=0 ;i< UIballsum;i++)
		{
			UIBallParent.transform.GetChild (i).GetComponent<Button> ().interactable = true;

		}
		if (BallLists != null) {
			BallLists.Clear ();
		}

		Time.timeScale = 1;
	}


	void RemoveUIBallinList(UIBallObj UIball){
		UIBallList.Remove (UIball);
	}

	void Rearrange(){
		if (UIBallList.Count == UIballsum) {
			for (int i = 0; i < UIballsum; i++) {
				UIBallList [i].ID = i;
			}
		} else {
			Debug.Log ("重新排列错误");
		}
	}

	void OnDisable(){
//		Debug.Log ("注销");
		UIBallObj.InsUIEvents -= Rearrange;

		BallObj2.InsGameObjEvent -= InsGameObjCircle;
		BallObj2.ChangeColorEvent -= ChangeColorinUIBall;


		UIBallObj.InsBallEvents -= InsBall;
		UIBallObj.InsBallEvents -= InsUIBall;
		UIBallObj.RemoveEvents -= RemoveUIBallinList;
	}
}