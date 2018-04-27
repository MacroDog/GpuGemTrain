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
		GerstnerWave();
	}

	public void GerstnerWave(){
		if(WaveMateril!=null){
			Matrix4x4 _waveParame=new Matrix4x4(new Vector4(1f,0.3f,2f,0),new Vector4(1f,0.2f,1f,1f),new Vector4(01f,0.5f,1,0),new Vector4(1f,0.4f,0.3f,1)){};
			Matrix4x4 _WaveDirection = new Matrix4x4(new Vector4(0.2f,0.3f,0,0),new Vector4(0.1f,1,0,0),new Vector4(2f,0.5f,0,0),new Vector4(1f,-0.4f,0,0) ){};
			WaveMateril.SetMatrix("_WaveMatrix",_waveParame);
			WaveMateril.SetMatrix("_WaveDirection",_WaveDirection);
		}
	}
}
