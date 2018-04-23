using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveControll : MonoBehaviour {

	public Material WaveMateril;
	public float length=0.2f;
	public int Amplitudo=1;
	public Vector2  Direction;
	public float Speed=1;


	void Start(){
		if(WaveMateril!=null){
			WaveMateril.SetFloat("_Amplitude",Amplitudo);
			WaveMateril.SetFloat("_Length",length);
			WaveMateril.SetFloat("_DirctionX",Direction.x);
			WaveMateril.SetFloat("_DirctionY",Direction.y);
		}
	}
}
