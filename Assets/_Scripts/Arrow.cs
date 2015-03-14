using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public GameObject head,line;
	public Light spot;

	float threshole = 60.0f; // if over angle, disenable the light.

	// also can set the view distance here 

	// Use this for initialization
	void Awake () 
	{
		head.SetActive(Manager.visual_head);
		line.SetActive(Manager.viasual_line);
		spot.intensity = Manager.intensity;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// if over angle, disenable the light.
		float angle = Vector3.Angle(Camear_Manager.owner.transform.forward, transform.forward);
		if (angle > threshole)
		{
			spot.enabled = false;
			if (Manager.visual_head)
			{
				head.SetActive(false);
				line.SetActive(false);
			}
		}
		else
		{
			spot.enabled = true;
			if (Manager.visual_head)
			{
				head.SetActive(true);
				line.SetActive(true);
			}
		}
	}
}
