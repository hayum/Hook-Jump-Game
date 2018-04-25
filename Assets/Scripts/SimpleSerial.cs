using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO.Ports;
using System.Threading;

public class SimpleSerial : MonoBehaviour {

	private SerialPort serialPort = null;
	private String portName = "/dev/cu.usbmodem1411";
	private int baudRate = 9600;
	private int readTimeOut = 100;

	private string serialInput;

	bool programActive = true;
	Thread thread;

	Animator anim;

	void Start()
	{   
		anim=GetComponent<Animator>();
		try
		{
			serialPort = new SerialPort();
			serialPort.PortName = portName;
			serialPort.BaudRate = baudRate;
			serialPort.ReadTimeout = readTimeOut;
			serialPort.Open();
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}
		thread = new Thread(new ThreadStart(ProcessData));
		thread.Start();
	}

	void ProcessData()
	{
		Debug.Log("Thread: Start");
		while (programActive)
		{
			try
			{
				serialInput = serialPort.ReadLine();
			}
			catch (TimeoutException)
			{

			}
		}
		Debug.Log("Thread: Stop");
	}

	void Update()
	{
		if (serialInput != null)
		{
			string[] strEul = serialInput.Split(',');
			if (strEul.Length > 0)
			{
				float potA = float.Parse(strEul[0]);
				Debug.Log(potA);
				//float potB = float.Parse(strEul[1]);
				//				potA = (potA - 512) / 512;
				//				potB = (potB - 512) / 512;
				//				this.transform.rotation = Quaternion.Euler(new Vector3(90.0f * potA, 90.0f * potB, 0f));
				//				if (int.Parse(strEul[2]) != 0)
				//				{
				//					this.GetComponent<MeshRenderer>().enabled = true;
				//				}
				//				else
				//				{
				//					this.GetComponent<MeshRenderer>().enabled = false; 
				//				}
				if(potA<120){
					anim.SetBool("Jump",true);
					//					if(potB==2){
					//						anim.speed = 2f;
					//					}
					//					else{
					//						anim.speed = 0.5f;
					//					}
					//anim.speed = potB;
				}
				else{
					anim.speed = 1f;
					anim.SetBool("Jump",false);
				}
				//Debug.Log(potA);
				//Debug.Log("--------");
				//Debug.Log(potB);

			}
		}
	}

	public void OnDisable()
	{
		programActive = false;
		if (serialPort != null && serialPort.IsOpen)
			serialPort.Close();
	}
}
