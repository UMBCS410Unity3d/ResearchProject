// haikun huang
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;


public class Replayer : MonoBehaviour 
{
	public Transform owner;

	public GameObject arrow;
	
	public int interval = 0;
	int current_intercal;

	// public string input_file  = "test1.txt";

	KeyCode return_key = KeyCode.U;
	string return_to_menu = "Menu";

	Queue<Vector3> quene;

	Vector3 nextPos, nextDir;
	float speed_pos, speed_dir;

	public bool isVR = false;

	void Awake()
	{


	}
	// Use this for initialization
	void Start () 
	{
		// cursor
		Cursor.visible = false;
		if (!owner)
			owner = transform;

		interval = Manager.interval;
		current_intercal = interval;

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

		// get the next set
		GetNext();
		owner.position = nextPos;
		owner.forward = nextDir;
	}

	void Update()
	{
		// return to menu
		if (Input.GetKeyDown(return_key))
		{
			Application.LoadLevel(return_to_menu);
		}
	}
	
	// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	void FixedUpdate () 
	{
		// get the next set
		GetNext();

	}

	// LateUpdate is called after all Update functions have been called. 
	// This is useful to order script execution. For example a follow camera should always 
	// be implemented in LateUpdate because it tracks objects that might have moved inside Update.
	void LateUpdate()
	{
		owner.position = Vector3.Lerp(owner.position,nextPos, speed_pos);
		owner.forward = Vector3.Lerp(owner.forward,nextDir, speed_dir);

	}

	void GetNext()
	{
		current_intercal --;

		if (quene.Count > 0)
		{
			// both position and direction
			nextPos = quene.Dequeue();
			nextDir = quene.Dequeue();

			// and calculate the speed, for smooth moving
			speed_pos = Math.Abs((nextPos - owner.position).magnitude) / Time.fixedDeltaTime;
			speed_dir = Vector3.Angle(owner.forward, nextDir) / Time.fixedDeltaTime;

			// visualize
			if (current_intercal <=0)
			{
				current_intercal = interval;
				Create_Arrow(nextPos,nextDir);
			}
		}

	}

	void Create_Arrow(Vector3 pos, Vector3 dir)
	{
		GameObject go = Instantiate(arrow) as GameObject;
		go.transform.position = pos;
		go.transform.forward = dir;

		go.GetComponent<Arrow>().head.SetActive(false);
		go.GetComponent<Arrow>().line.SetActive(false);
		
	}
}
