using UnityEngine;
using System.Collections;

public struct Math 
{
	/// <summary>
	/// 使浮点数f，使其保留n位小数，多余位数直接舍去，不进行四舍五入
	/// </summary>
	/// <param name="f">F.</param>
	/// <param name="n">N.</param>
	public static float mathfn( float f, int n){
		int tempint;
		float xiaoshu;
		int temp;

		temp = (int)f;
		f = f * Mathf.Pow (10, n);
		tempint = (int)f;
		xiaoshu = ((float)tempint % Mathf.Pow (10, n) )/(Mathf.Pow (10, n));
		f = temp + xiaoshu;
		return f;
	} 

	public static Vector2 GetMousedir(){
		Vector3 mousev3 = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 v2 =mousev3;
		Vector2 tempv2;
		tempv2.x = v2.x / Mathf.Sqrt (v2.x * v2.x + v2.y * v2.y);
		tempv2.y = v2.y / Mathf.Sqrt (v2.x * v2.x + v2.y * v2.y);

		return tempv2;
	}

	public static float GetLenghtFrom0toMousePos(){
		Vector3 mousev3 = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousev3.z = 0;
		float lenght = Vector3.Distance (Vector3.zero, mousev3);
		return lenght;
	}
}

