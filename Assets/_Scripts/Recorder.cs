// haikun huang
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Ovr;

/*
 * File format
 * 
 * Mesh data file path
 * {position, direction, hit(0,1) <, hitpoint>}
 * */

public class Recorder : MonoBehaviour 
{
	public Transform owner;
	// public string output_file  = "test1.txt";

	public bool file_append = false;

	public int buff_size = 2000;
	int buff_size_current = 0;

	KeyCode return_key = KeyCode.U;
	string return_to_menu = "Menu";

	StreamWriter sw;

	List<string> list;

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

		list = new List<string>();
		// new file if needed
		sw = new StreamWriter(Application.dataPath +"/" + Manager.filePath,file_append);

		// first is the mesh files.
		string meshFile = Application.dataPath +"/"+Manager.pre_scene_filePath 
			+ Application.loadedLevelName + ".txt";
		sw.WriteLine(meshFile);

		sw.Close();

	}
	void Update()
	{
		// return to menu
		if (Input.GetKeyDown(return_key))
		{
			Save();
			Application.LoadLevel(return_to_menu);
		}
	}
	
	//This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	void FixedUpdate () 
	{
		// record the position and direction
		string info = owner.position.x + "," + owner.position.y + "," + owner.position.z + ","
			+ owner.forward.x + "," + owner.forward.y + "," + owner.forward.z;

		// record the hitpoint
		Ray ray = new Ray(owner.position, owner.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			info += ",1";
			info += "," + hit.point.x + "," + hit.point.y + "," + hit.point.z;
		}
		else
		{
			info += ",0";
		}

		list.Add(info);
		buff_size_current ++;

		// if the buff full, flush to file first
		if (buff_size_current >= buff_size)
		{
			Save();
			buff_size_current = 0;
		}

	}

	void OnApplicationQuit()
	{
		Save();
	}

	void Save()
	{
		// output to the file
		Debug.Log("Save file " + Application.dataPath +"/" + Manager.filePath);
		sw = new StreamWriter(Application.dataPath +"/" + Manager.filePath,true);
		foreach(string info in list)
		{
			sw.WriteLine(info);
		}
		sw.Close();

		// clear the list
		list.Clear();
	}
}
