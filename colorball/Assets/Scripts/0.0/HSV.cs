using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct HSV  {
	public float h;
	public float s;
	public float v;
	public float a;

	public static Color red{ 
		get { 
			return HSV.GetColor (0, 360);
		}
	}

	public HSV(float H,float S ,float V){
		h = H;
		s = S;
		v = V;
		a = 1.0f;
	}

	public HSV(float H,float S ,float V,float A){
		h = H;
		s = S;
		v = V;
		a = A;
	}
	///<summary>
	/// 获取颜色色环sum中的i位置颜色
	/// </summary>

	public static Color GetColor(float i,float sum){
		Color rgb;
		rgb = Color.HSVToRGB ((float)i/sum,0.8f,0.8f);
		return rgb;
	} 
	///<summary>
	/// 获取颜色色环sum中的i位置颜色,并设置S,V（B)
	/// </summary>
	public static  Color GetColor(float i,float sum,float s,float v){
		Color rgb;
		rgb = Color.HSVToRGB ((float)i/sum,s/100.0f,v/100.0f);
		return rgb;
	} 
	///<summary>
	/// 获取某一颜色在色环中的方向
	/// </summary>
	public static Vector3 GetColorDir(Color color){
		HSV hsv;
		Color.RGBToHSV (color, out hsv.h, out hsv.s, out hsv.v);
		Vector3 v3;
		v3.x = Mathf.Cos (hsv.h * 360 * Mathf.Deg2Rad);
		v3.y = Mathf.Sin (hsv.h * 360 * Mathf.Deg2Rad);
		//Debug.Log ((int)(Mathf.Acos(v3.x)*Mathf.Rad2Deg) + " " +(int)(Mathf.Asin(v3.y)*Mathf.Rad2Deg));
		v3.z = 0;
		v3=Vector3.Normalize (v3);
		return v3;
	}
	///<summary>
	/// 随机获取色环中的某一方向
	/// </summary>
	public static Vector3 GetColorDir(){
		Vector3 v3;
		float temprandom = Random.Range (0, 360);
		v3.x = Mathf.Cos ( temprandom * Mathf.Deg2Rad);
		v3.y = Mathf.Sin (temprandom * Mathf.Deg2Rad);
		//Debug.Log ((int)(Mathf.Acos(v3.x)*Mathf.Rad2Deg) + " " +(int)(Mathf.Asin(v3.y)*Mathf.Rad2Deg));
		v3.z = 0;
		v3=Vector3.Normalize (v3);
		return v3;
	}
	public static Vector3 GetColorDirExceptRedandGreen2(){
		Vector3 v3;
		float temprandom = Random.Range (30, 31);
		v3.x = Mathf.Cos ( temprandom * Mathf.Deg2Rad);
		v3.y = Mathf.Sin (temprandom * Mathf.Deg2Rad);
		//Debug.Log ((int)(Mathf.Acos(v3.x)*Mathf.Rad2Deg) + " " +(int)(Mathf.Asin(v3.y)*Mathf.Rad2Deg));
		v3.z = 0;
		v3=Vector3.Normalize (v3);
		return v3;
	}

	public static Vector3 GetColorDirExceptRedandGreen(){
		Vector3 v3;
		float temprandom = Random.Range (15, 345);
		while((temprandom>105 && temprandom <135 )||(temprandom>225 && temprandom <255 ))
			temprandom = Random.Range (10, 350);


		v3.x = Mathf.Cos ( temprandom * Mathf.Deg2Rad);
		v3.y = Mathf.Sin (temprandom * Mathf.Deg2Rad);
		//Debug.Log ((int)(Mathf.Acos(v3.x)*Mathf.Rad2Deg) + " " +(int)(Mathf.Asin(v3.y)*Mathf.Rad2Deg));
		v3.z = 0;
		v3=Vector3.Normalize (v3);
		return v3;
	}
	///<summary>
	/// 获取某一位置在色环中的颜色
	/// </summary>
	public static Color GetColorbyPosition(Vector3 pos3){
		Color color;
		HSV hsv;
		pos3=Vector3.Normalize (pos3);
		hsv.s = 80/100.0f;
		hsv.v = 80/100.0f;
		if (pos3.y >= 0) {
			hsv.h =( Mathf.Acos (pos3.x) * Mathf.Rad2Deg)/360.0f;
			//Debug.Log ("h ==" + hsv.h);
		} else {
			hsv.h = (360 - Mathf.Acos (pos3.x) * Mathf.Rad2Deg)/360.0f;

		}
		color = Color.HSVToRGB (hsv.h, hsv.s, hsv.v);
		return color;
	}

	///<summary>
	/// 通过颜色返回角度0-360
	/// </summary>
	public static float ReturnHbyColor(Color color){
		HSV hsv;
		Color.RGBToHSV (color, out hsv.h, out hsv.s, out hsv.v);
		return hsv.h*360;
	}

}
