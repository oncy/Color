using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public int index =2;

	public void _LoadScene(){
		
		SceneManager.LoadScene (index);

	}

}
