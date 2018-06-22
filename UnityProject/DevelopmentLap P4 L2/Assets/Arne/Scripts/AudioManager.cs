using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	//soundtracks
	public List<AudioClip> audioclip = new List<AudioClip>();
	//volume
	public float vol;
	public bool muteBool;

	//audio source
	public AudioSource thisAudio;

	void Start () 
	{
		thisAudio = GetComponentInChildren<AudioSource>();
		//thisAudio.audio = audioclip[0];
		thisAudio.clip = audioclip[0];
	}

	public void ListConverter (int number) 
	{

	}
	public void PlayMusic (AudioClip music)
	{

	}
	public void Mute (bool active) 
	{
		muteBool = !active;
		thisAudio.mute = muteBool;
	}
}
