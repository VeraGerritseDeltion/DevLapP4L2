using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour {

	public CameraMovement camMove;			
	//public SoundThing Sound;			PSEUDO
	public TextMeshProUGUI soundVol;
	public TextMeshProUGUI scrollSpd;
	public GameObject camEffects;
	public Toggle camPan;
	public Toggle camEffect;
	public Toggle muteSound;

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
		//MuteSound.stop();
	}
	//decides how hard the sound is
	public void SoundVolume (float number)
	{
		float num = number * 100f;
		soundVol.text = ((int)num).ToString();
		//Sound.Volume = soundvol		PSEUDO
	}
	//how fast scroll speed is 
	public void ScrollSpeed (float number)
	{
		float num = number * 50f;
		scrollSpd.text = ((int)num).ToString();
		//camMove.zoomSpeed *= number;	//needs to be tested in maingame scene and tweaked
	}
}
