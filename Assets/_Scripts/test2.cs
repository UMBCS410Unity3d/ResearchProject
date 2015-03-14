using UnityEngine;
using System.Collections;

public class test2 : MonoBehaviour 
{
	public GameObject target;
	// Use this for initialization
	void Start () 
	{
		// create a new mesh
		GameObject go = Instantiate(target,Vector3.zero, Quaternion.identity) as GameObject;

		Mesh mesh = go.GetComponent<MeshFilter>().mesh;
		mesh.Clear();

		mesh.vertices = new Vector3[]{new Vector3(0,0,0),new Vector3(0,1,0),new Vector3(1,1,0),
			new Vector3(1,0,0)}; // need
		mesh.uv = new Vector2[]{new Vector2(UnityEngine.Random.Range(0,1f),
			                                UnityEngine.Random.Range(0,1f)),
								new Vector2(UnityEngine.Random.Range(0,1f),
			                                UnityEngine.Random.Range(0,1f)),
								new Vector2(UnityEngine.Random.Range(0,1f),
			                                UnityEngine.Random.Range(0,1f)),
								new Vector2(UnityEngine.Random.Range(0,1f),
			            					UnityEngine.Random.Range(0,1f))}; // re-cal base on the data
		// mesh.normals = new Vector3[]{Vector3.back, Vector3.back, Vector3.back}; // need
		mesh.triangles = new int[]{0,1,2,0,1,3}; // need 
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
