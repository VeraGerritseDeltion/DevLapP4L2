using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	//soundtracks	BGM
	public List<AudioClip> audioclip = new List<AudioClip>();
	
	//volume
	public float vol;
	public bool muteBool;

	//audio source
	public AudioSource thisAudio;

	//test
	public int num;
	public bool go;

	void Start () 
	{
		thisAudio = GetComponentInChildren<AudioSource>();
	}
	void Update () 
	{
		if(go)
		{
			NewAudio();
			go = false;
		}
	}
	void NewAudio ()
	{
		thisAudio.clip = audioclip[num];
		PlayMusic(audioclip[num]);
	}
	public void ListConverter (int number) 
	{
		thisAudio.clip = audioclip[number];
		PlayMusic(audioclip[number]);
	}
	public void PlayMusic (AudioClip music)
	{
		thisAudio.Play();
	}
	public void Mute (bool active) 
	{
		muteBool = !active;
		thisAudio.mute = muteBool;
	}
}
