using UnityEngine;
using System.Collections;

public class Camear_Manager : MonoBehaviour {

	public GameObject 
		recorder,replayer,visual_path,
		recorder_VR,replayer_VR,visual_path_VR,
		mesh_builder;

	// get the player or camnera alternative
	public static GameObject owner;

	// Use this for initialization
	void Start () 
	{
		GetComponent<Camera>().enabled = false;

		if (Manager.player_mode == 0 )
		{
			GameObject go;
			if (!Manager.vr_oculus)
				go = Instantiate(recorder,transform.position,transform.rotation) as GameObject;
			else
				go = Instantiate(recorder_VR,transform.position,transform.rotation) as GameObject;

			owner = go.GetComponent<Recorder>().owner.gameObject;
		}

		else if (Manager.player_mode == 1 )
		{
			GameObject go;
			if (!Manager.vr_oculus)
				go = Instantiate(replayer,transform.position,transform.rotation) as GameObject;
			else
				go = Instantiate(replayer_VR,transform.position,transform.rotation) as GameObject;

			owner = go.GetComponent<Replayer>().owner.gameObject;
		}

		else if (Manager.player_mode == 2 )
		{
			GameObject go;
			if (!Manager.vr_oculus)
				go = Instantiate(visual_path,transform.position,transform.rotation) as GameObject;
			else
				go = Instantiate(visual_path_VR,transform.position,transform.rotation) as GameObject;

			owner = go.GetComponent<Visual_Path>().owner.gameObject;
		}

		else if (Manager.player_mode == 3 )
		{
			GameObject go;
			Instantiate(mesh_builder,transform.position,transform.rotation);
	
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
