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
 *   { normal \n }
 *   <end>
 *   { triangle \n }
 *   <end>
 * 	}
 * */

public class Output_Scene : MonoBehaviour 
{
	KeyCode return_key = KeyCode.U;
	string return_to_menu = "Menu";

	// Use this for initialization
	void Start () 
	{
		File_Generator();

		// return to menu
		Application.LoadLevel(return_to_menu);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	void File_Generator()
	{
		// make file name 
		string filePath = Application.dataPath +"/"+Manager.pre_scene_filePath 
			+ Application.loadedLevelName + ".txt";

		// create the file
		StreamWriter sw = new StreamWriter(filePath,false);

		// collect all the mesh filter
		MeshFilter[] mfs = FindObjectsOfType<MeshFilter>();

		Debug.Log("MeshFilter count: " + mfs.Length);

		// get the mesh data and out put to the file
		for(int i=0; i< mfs.Length; i++)
		{
			Mesh mesh = mfs[i].mesh;

			Debug.Log("mesh: " + mesh.name);

			// vertices
			V3ArrayTostring(mesh.vertices, sw, mfs[i].transform);
			sw.WriteLine("<end>");
			//normals
			V3ArrayTostring(mesh.normals, sw);
			sw.WriteLine("<end>");
			// triangles
			IntArrayTostring(mesh.triangles, sw);
			sw.WriteLine("<end>");
		}

		// close the file
		sw.Close();

		Debug.Log("File output: "+ filePath);
	}

	void V3ArrayTostring(Vector3[] vs, StreamWriter sw, Transform trans)
	{

		for (int i=0;i<vs.Length; i++)
		{
			// local to world
			Vector3 v = trans.TransformPoint(vs[i]); 
			// Vector3 v = vs[i]; 
			sw.WriteLine(v.x + "," + v.y +"," + v.z);
		}

	}

	void V3ArrayTostring(Vector3[] vs, StreamWriter sw)
	{
		
		for (int i=0;i<vs.Length; i++)
		{
			// local to world
			Vector3 v = vs[i]; 
			sw.WriteLine(v.x + "," + v.y +"," + v.z);
		}
		
	}

	void V2ArrayTostring(Vector2[] vs, StreamWriter sw)
	{
		for (int i=0;i<vs.Length; i++)
		{
			Vector2 v = vs[i];
			sw.WriteLine(v.x + "," + v.y);
		}

	}

	void IntArrayTostring(int[] vs, StreamWriter sw)
	{
		for (int i=0;i<vs.Length; i+=3)
		{
			sw.WriteLine(vs[i]+","+vs[i+1] +"," + vs[i+2]);
		}
		
	}

}
