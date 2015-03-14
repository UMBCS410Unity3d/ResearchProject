// haikun huang
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;


// *** NOTE: Not work for the object be marked as [static] *** //`

/*
 * the output file formation:

 *   // normals \n ; we do not need the normal, all the normal will recalculate later and point to camera, 
 * or using toon shader
 *  {
 *   { vertex \n }
 *   <end>
 *   { triangle \n }
 *   <end>
 * 	}
 * */

public class Heat_Map : MonoBehaviour 
{
	// public string levelFile = "Level 02.txt";

	// public string dataFile="test1.txt";

	public GameObject target;

	public GameObject red;

	KeyCode return_key = KeyCode.U;
	string return_to_menu = "Menu";

	// Use this for initialization
	void Start () 
	{
		// read the data
		Generator_HeatMap();

		Debug.Log("*** Done! ***");
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

	void Generator_HeatMap()
	{
		Queue<Vector3> quene;
		
		int interval = Manager.interval;


		
		// load the data from file
		StreamReader sr = new StreamReader(Application.dataPath + "/" + Manager.filePath);
		if (!sr.EndOfStream)
		{
			Generator_Level(sr.ReadLine()); // mesh file

		}

		while(!sr.EndOfStream)
		{
			// read line by line
			string line = sr.ReadLine();
			string[] info = line.Split(',');
			// the first 3 data are elements of position
		
			Vector3 pos =	new Vector3(float.Parse(info[0]),
			            float.Parse(info[1]),
			            float.Parse(info[2]));
			
			// the next 3 data are elements of direction

			Vector3 dir =	new Vector3(float.Parse(info[3]),
			            float.Parse(info[4]),
			            float.Parse(info[5]));

			// ray cast
			Ray ray = new Ray(pos,dir);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				GameObject go = Instantiate(red, hit.point, Quaternion.identity) as GameObject;
				ParticleSystem[] pses = go.GetComponentsInChildren<ParticleSystem>();
				foreach(ParticleSystem ps in pses )
				{
					ps.startColor = new Color(ps.startColor.r,
					                          ps.startColor.g,
					                          ps.startColor.b,
					                          Manager.intensity / 10.0f * 70.0f);
				}

			}

			// do interval
			for (int i=0; i<interval; i++)
			{
				if (!sr.EndOfStream)
				{
					sr.ReadLine();
				}
			}
		}
		sr.Close();

	
	}

	// 
	void Generator_Level(string filePath)
	{

		Debug.Log("File path: " + filePath);

		StreamReader sr = new StreamReader(filePath);

		while(!sr.EndOfStream)
		{
			// create the object
			// create a new mesh
			GameObject go = Instantiate(target,Vector3.zero, Quaternion.identity) as GameObject;

			//go.GetComponent<MeshFilter>().mesh = new Mesh();
			Mesh mesh = go.GetComponent<MeshFilter>().mesh;
			mesh.Clear();


			string line = "";

			// vertices
			mesh.vertices = ReadV3Array(sr);

			// normal
			mesh.normals = ReadV3Array(sr);

			
			// triangles
			mesh.triangles = ReadIntArray(sr);

			// collider
			go.GetComponent<MeshCollider>().sharedMesh = mesh;


		}

		sr.Close();

	}

	Vector3[] ReadV3Array(StreamReader sr)
	{
		List<Vector3> v3array = new List<Vector3>();
		 
		// read the data, until meet the <end>
		string line = sr.ReadLine();
		while (!line.Equals("<end>"))
		{
			string[] data = line.Split(',');
			Vector3 v = new Vector3 (
				float.Parse(data[0]),
				float.Parse(data[1]),
				float.Parse(data[2]));
			
			v3array.Add(v);
			line = sr.ReadLine();
		}

		return v3array.ToArray();
	}

	int[] ReadIntArray(StreamReader sr)
	{
		List<int> intarray = new List<int>();
		string line = sr.ReadLine();
		while (!line.Equals("<end>"))
		{
			string[] data = line.Split(',');
			intarray.Add(int.Parse(data[0]));
			intarray.Add(int.Parse(data[1]));
			intarray.Add(int.Parse(data[2]));

			line = sr.ReadLine();
		}

		return intarray.ToArray();
	}
}
