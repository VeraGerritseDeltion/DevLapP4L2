using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour {

	//camera movement
	public CameraMovement camMove;

	//sound			
	public AudioManager sound;

	//text next to sliders
	public TextMeshProUGUI soundVol;
	public TextMeshProUGUI zoomSpd;

	//camera effects
	public GameObject camEffects;

	//toggles
	public Toggle camPan;
	public Toggle camEffect;
	public Toggle muteSound;

	//sliders
	public List<Slider> sliderlist = new List<Slider>();


	void Start () 
	{	
		ZoomSpeed(camMove.zoomSpeed);
		SoundVolume(100f);
	}

	//sets campanning to state of toggle it receives
	public void CameraPanning (Toggle tog)
	{
		bool active = tog.isOn;
		camMove.autoPanning = active;
	}
	//sets camEffects to the state of toggle it receives 
	public void CameraEffects (Toggle tog)
	{
		bool active = tog.isOn;
		camEffects.SetActive(active);
	}
	//mutes sound based on toggle state it receives
	public void MuteSound (Toggle tog)
	{
		bool active = tog.isOn;
		sound.Mute(active);
	}
	//decides how hard the sound is
	public void SoundVolume (float number)
	{
        if(sound != null)
        {
            float num = number * 1f;
            soundVol.text = ((int)num).ToString();
            sound.vol = num;
        }		
	}
	//how fast zoom speed is 
	public void ZoomSpeed (float number)
	{
		float num = number * 1f;
		zoomSpd.text = ((int)num).ToString();
		camMove.zoomSpeed = num;	//needs to be tested in maingame scene and tweaked
	}
}
