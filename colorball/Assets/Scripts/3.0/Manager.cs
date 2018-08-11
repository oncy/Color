using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Manager : MonoBehaviour {
	//提示TEXT
	public Text prompt;
	public GameObject ReObj;
	public Text Gettext;
	//画线
	LineRenderer line;
	float dist;
	//中心点颜色
	SpriteRenderer render;
	List<int> AnglesList;
//	List<int> AnglesList2;

	//总的偏差角度
	float angles;
	bool GameOver;
	bool isout;
	bool firstcolor =true;
	//随机list里的位置
	int i;

	//进度
	public Image lenght;
	//进度
	public Text perjindu;
	int n;

	bool istishi;

	// Use this for initialization
	void Start () {
		AnglesList =new List<int>();

		render = this.GetComponent<SpriteRenderer> ();
		line = this.GetComponent<LineRenderer> ();

		//中心点和线同色。开始默认蓝
		render.color = HSV.GetColor (240, 360, 80, 80);
		line.startColor = HSV.GetColor (240, 360, 80, 80);
		line.endColor   = HSV.GetColor (240, 360, 80, 80);
		line.SetPosition (0, new Vector3(0,0,this.transform.position.z));

		for (int i=0,j = 0; i < 15; i++,j++) {
			
			AnglesList.Add( 24 * i + j);
//			AnglesList2.Add( 24 * i + j);

			}
		ReObj.SetActive (false);

		//c初始化进度0
		lenght.fillAmount =0;
		perjindu.text = "完成进度:(" + n + "/16)";

	}
	
	// Update is called once per frame
	void Update () {
		if (!GameOver) {
//			if (Input.GetMouseButton (0)) {
			//line长度
			dist =Math.GetLenghtFrom0toMousePos();
			dist = Mathf.Clamp (dist, 0, 4);
			Vector3 mousev3 = Math.GetMousedir ()*dist;

			mousev3.z = this.transform.position.z;

			line.SetPosition (1, mousev3);

//			}
			if (Input.GetMouseButtonUp (0)) {
				//划线
//				line.SetPosition (1, Vector3.zero);
				//射线检测
				Vector3 v3 = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				Vector2 v2 = new Vector2 (v3.x, v3.y);
				RaycastHit2D hit = Physics2D.Raycast (v2, Vector2.zero);

				if (hit.collider != null) {
					n++;
					perjindu.text = "完成进度:(" + n + "/16)";

					//替换
					//假如没有提示开启
					if (!istishi) {
						istishi = true;
						prompt.text = "选择成功，请进行下一个颜色点的判断";
						prompt.gameObject.SetActive (true);
						StartCoroutine (wait1s ());

					}
					//计算偏移角度
					angles += CalculateAngle (v2);

					lenght.fillAmount += 1/16.0f;

					//更新颜色
					if (!firstcolor)
						AnglesList.RemoveAt (i);

					if (AnglesList.Count != 0) {
						i = Random.Range (0, AnglesList.Count);
						render.color = HSV.GetColor (AnglesList [i], 360, 80, 80);
						firstcolor = false;

						line.startColor = render.color;
						line.endColor = render.color;

					}

					if (AnglesList.Count == 0) {

						ReObj.SetActive (true);
						Gettext.text = "角度偏差: " + (int)angles + "度";
						GameOver = true;
						prompt.gameObject.SetActive (false);

					}
				}
			//如果检测不在
			else {
					if (!istishi) {
						istishi = true;

						prompt.text = "选择取消，请在白色圆环上抬起";
						prompt.gameObject.SetActive (true);
						StartCoroutine (wait1s ());
					}
				}

			}

		}
	}
		
	IEnumerator wait1s(){
		
		yield return new WaitForSeconds (1.8f);
		prompt.gameObject.SetActive (false);
		istishi = false;
		}
	IEnumerator waitdeltaTime(){
		yield return new WaitForSeconds (Time.deltaTime);
		GameOver = false;

	}

	float CalculateAngle(Vector3 mousev2){
		float angle;
		Vector2 thiscolorv2 = (Vector2)HSV.GetColorDir (render.color);
		angle = Vector2.Angle (mousev2, thiscolorv2);
		return angle;

	}

	public void replay(){
		n = 0;
		perjindu.text = "完成进度:(" + n + "/16)";

		firstcolor = true;
		render.color = HSV.GetColor (240, 360, 80, 80);
		line.startColor = HSV.GetColor (240, 360, 80, 80);
		line.endColor   = HSV.GetColor (240, 360, 80, 80);

		for (int i=0,j = 0; i < 15; i++,j++) {
			AnglesList.Add( 24 * i + j);

		}
		ReObj.SetActive (false);
		angles = 0;
		lenght.fillAmount =0;

		StartCoroutine (waitdeltaTime ());
	}


}
