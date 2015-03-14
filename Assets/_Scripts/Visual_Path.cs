// haikun huang
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;


public class Visual_Path : MonoBehaviour 
{
	public Transform owner;

	public GameObject arrow;

	public int interval = 0;

	// public string input_file  = "test1.txt";
	
	KeyCode return_key = KeyCode.U;
	string return_to_menu = "Menu";

	Queue<Vector3> quene;

	public bool isVR = false;

	void Awake()
	{

	}
	// Use this for initialization
	void Start () 
	{
		// cursor
		Cursor.visible = false;

		interval = Manager.interval;

		quene = new Queue<Vector3>();
		
		// load the data from file
		StreamReader sr = new StreamReader(Application.dataPath + "/" + Manager.filePath);
		if (!sr.EndOfStream)
			sr.ReadLine(); // mesh file

		while(!sr.EndOfStream)
		{
			// read line by line
			string line = sr.ReadLine();
			string[] info = line.Split(',');
			// the first 3 data are elements of position
			quene.Enqueue(
				new Vector3(float.Parse(info[0]),
			            float.Parse(info[1]),
			            float.Parse(info[2])));
			
			// the next 3 data are elements of direction
			quene.Enqueue(
				new Vector3(float.Parse(info[3]),
			            float.Parse(info[4]),
			            float.Parse(info[5])));
		}
		sr.Close();

		// create arrows
		while(quene.Count>0)
		{
			// do interval
			for (int i=0; i<interval; i++)
			{
				if (quene.Count>0)
				{
					quene.Dequeue();
					quene.Dequeue();
				}
			}
			
			// create arrow
			if (quene.Count>0)
			{
				Create_Arrow(quene.Dequeue(),quene.Dequeue());
			}
		}

	}
	
	// Update is called once per frame
	void Update () 
	{

		// return to menu
		if (Input.GetKeyDown(return_key))
		{
			Application.LoadLevel(return_to_menu);
		}


	}

	void Create_Arrow(Vector3 pos, Vector3 dir)
	{
		GameObject go = Instantiate(arrow) as GameObject;
		go.transform.position = pos;
		go.transform.forward = dir;

	}
}
