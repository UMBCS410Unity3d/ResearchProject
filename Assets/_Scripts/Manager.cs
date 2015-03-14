// Haikun Huang
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour 
{
	// 0: Recorder 
	// 1: Replayer
	// 2: Visual Path
	// 3: Mesh Builder
	public static int player_mode = 0; 

	public static string filePath = "test1.txt";
	public static string pre_scene_filePath = "scene_";
	
	public Text fileText,defaultFileText;

	public Toggle visual_head_Toggle, viasual_line_Toggle;
	public static bool visual_head = false, viasual_line = false;

	public Toggle vr_oculus_Toggle;
	public static bool vr_oculus;

	// intensity
	public Text intensity_text;
	public Slider intensity_slider;
	public static float intensity = 0.02f;

	// intensity
	public Text interval_text;
	public Slider interval_slider;
	public static int interval = 5;

	public Text info;

	// Use this for initialization
	void Start () 
	{
		// cursor
		Cursor.visible = true;

		defaultFileText.text = filePath;

		visual_head_Toggle.isOn = visual_head;
		viasual_line_Toggle.isOn = viasual_line;
		vr_oculus_Toggle.isOn = vr_oculus;

		// init
		intensity_slider.value = intensity;
		OnIntensity();
		interval_slider.value = interval;
		OnInterval();

		info.text = "Data Path: " + Application.dataPath + "/";
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Go_Recorder()
	{
		player_mode = 0;
		if (fileText.text != "")
			filePath = fileText.text;

		LoadLevel("Level 02");
	}

	public void Go_Replayer()
	{
		player_mode = 1;
		if (fileText.text != "")
			filePath = fileText.text;

		LoadLevel("Level 02");
	}

	public void Go_VisualPath()
	{
		player_mode = 2;
		if (fileText.text != "")
			filePath = fileText.text;

		LoadLevel("Level 02");
	}

	public void Go_MeshBuilder()
	{
		player_mode = 3;
		if (fileText.text != "")
			filePath = fileText.text;
		
		LoadLevel("Level 02");
	}

	public void Go_HeatMap()
	{
		if (fileText.text != "")
			filePath = fileText.text;
		
		LoadLevel("heatMap");
	}

	public void LoadLevel(string level)
	{
		visual_head = visual_head_Toggle.isOn;
		viasual_line = viasual_line_Toggle.isOn;
		vr_oculus = vr_oculus_Toggle.isOn;
		Application.LoadLevel(level);
	}

	public void OnIntensity()
	{
		intensity_text.text = "Intensity: " + intensity_slider.value;
		intensity = intensity_slider.value;
	}

	public void OnInterval()
	{
		interval_text.text = "Interval: " + interval_slider.value;
		interval = (int)interval_slider.value;
	}
}
