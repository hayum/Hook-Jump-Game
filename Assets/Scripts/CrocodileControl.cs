using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileControl : MonoBehaviour {

	Animator animCro, animH;
	bool isEnd,timeOut;
	public int freq=1;
	GameObject Hook, empty;
	public GameObject cvs,cvs2;
	int translation=0, splitTime;
	// Use this for initialization
	void Start () {
		Random.InitState(22);
		animCro=GetComponent<Animator>();
		animH=GameObject.Find("Hook").GetComponent<Animator>();
		empty=GameObject.Find("emp");
		//cvs=GameObject.Find("Canvas");
	}

	// Update is called once per frame
	void Update () {
		if(Time.frameCount%50==0 && !isEnd){
			int randomNumber=Random.Range(0,50);
			//Debug.Log(randomNumber);
			if(randomNumber%freq==0)
			{
				animCro.SetBool("Open",true);
				timeOut=false;
				splitTime=0;
			}
			else {
				animCro.SetBool("Open",false);
			}
		}
		if(animCro.GetBool("Open") && !isEnd && !timeOut){
			//float translation = Time.deltaTime * 10;
			//empty.transform.Translate(0, 0, translation);

			translation++;
			if(translation<50){
				if(animH.GetBool("Jump")){
					timeOut=true;
					translation=0;
				}
			}
			else{
				isEnd=true;
				Debug.Log("fail");
				animCro.SetBool("Open",false);
				cvs.SetActive(true);
			}
		}
		if(!animCro.GetBool("Open") && !isEnd && animH.GetBool("Jump")){
			splitTime++;
			if(splitTime>60){
			isEnd=true;
			Debug.Log("fail2");
			animCro.SetBool("Open",false);
			cvs2.SetActive(true);
			}
		}

	}

}
