using UnityEngine;
using System.Collections;

public class test : MonoBehaviour 
{
	public MeshFilter mf;
	public Material plain_matrial;
	Mesh mesh;

	public GameObject redDot;


	// Use this for initialization
	void Start () 
	{
		mesh = mf.mesh;

		Debug.Log("subMeshCount: " + mesh.subMeshCount);

		Debug.Log("vertics count: "+ mesh.vertexCount);

		Debug.Log("triangle count: "+ mesh.triangles.Length);

		Debug.Log("nv count: "+ mesh.uv.Length);

		Debug.Log("normal count: "+ mesh.normals.Length);

		// try to modify some
		Vector3[] vertics = mesh.vertices;
		for (int i =0; i<vertics.Length; i++)
		{
			// vertics[i] *= 1.4f;
		}
		mesh.vertices = vertics;

		// plain the main texture
		// color modify
		mf.gameObject.GetComponent<MeshRenderer>().material = plain_matrial;
		Color32[] colors = new Color32[mesh.vertexCount];
		for (int i=0; i<colors.Length; i++)
		{
			colors[i]= Color.blue;
		}
		mesh.colors32 = colors;
		Debug.Log("color count: "+ mesh.colors32.Length);


	}
	
	// Update is called once per frame
	void Update () 
	{
		// get the triangle we click
		Triangle_Clicked();
	}


	// get the triangle we click
	void Triangle_Clicked()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			// create the ray
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit info;
			if (Physics.Raycast(ray, out info))
			{
				Debug.Log(info.collider.gameObject.name);

				// traingle index
				Debug.Log("triangle index: " + info.triangleIndex);

				// red dot the intersction
				//Instantiate(redDot, info.point, Quaternion.identity);

				MeshCollider meshCollider = info.collider as MeshCollider;

				// Mesh mesh = meshCollider.sharedMesh;
				Vector3[] vertices = mesh.vertices;
				int[] triangles = mesh.triangles;
				Vector3 p0 = vertices[triangles[info.triangleIndex * 3 + 0]];
				Vector3 p1 = vertices[triangles[info.triangleIndex * 3 + 1]];
				Vector3 p2 = vertices[triangles[info.triangleIndex * 3 + 2]];
				Transform hitTransform = info.collider.transform;

				p0 = hitTransform.TransformPoint(p0);
				p1 = hitTransform.TransformPoint(p1);
				p2 = hitTransform.TransformPoint(p2);

				// only on the editor/scene view
				Debug.DrawLine(p0, p1, Color.red, 30.0f);
				Debug.DrawLine(p1, p2, Color.red, 30.0f);
				Debug.DrawLine(p2, p0, Color.red, 30.0f);

				// red dot
				Instantiate(redDot, p0, Quaternion.identity);
				Instantiate(redDot, p1, Quaternion.identity);
				Instantiate(redDot, p2, Quaternion.identity);

				//(Instantiate(redDot, info.point, Quaternion.identity) as GameObject).transform.localScale *= 1.5f;
				//Debug.DrawLine(info.point, p0, Color.white, 30.0f);
				//Debug.DrawLine(info.point, p1, Color.white, 30.0f);
				//Debug.DrawLine(info.point, p2, Color.white, 30.0f);

				// uv
				// Vector2[] uv = mesh.uv;
				// mesh.uv[triangles[info.triangleIndex * 3 + 0]] = new Vector2(1,1);
				// mesh.uv[triangles[info.triangleIndex * 3 + 1]] = new Vector2(1,1);
				// mesh.uv[triangles[info.triangleIndex * 3 + 2]] = new Vector2(1,1);

			}
		}
	}
	
}
