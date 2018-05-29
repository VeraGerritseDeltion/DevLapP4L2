using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActivations : MonoBehaviour {


	//FOR TESTING PURPOSES
	
	//singleton
	public static UIActivations instance;

	//UIState
	public enum UIState {MainMenu, Ingame};
	public UIState _UIState;

	//ui elements lists
	public List <RectTransform> allMenuItems = new List<RectTransform>(); 
	public RectTransform mainMenu, ingame, settings, pauseMenu, buildingsbar, eventlog, statistics;

	public KeyCode test;

	public KeyCode esc;


	void Awake () 
	{
		if(instance == null)
		{
			instance = this;
		}
	}
	// Use this for initialization
	void Start () {
		
		CheckState();
	}
	void Update () 
	{
		if(Input.GetKeyDown(test))
		{
			//stuff
		}
	}
	void CheckState ()
	{
		switch(_UIState)
		{
			case UIState.MainMenu:

				List<RectTransform> mainMenuList = new List<RectTransform>() {mainMenu};
			    //EnableMenuItems(mainMenuList); 

				break;

			case UIState.Ingame:

				List<RectTransform> ingameList = new List<RectTransform>() {ingame};
			    //EnableMenuItems(ingameList); 

				break;
		}
	}
	
	void SetState (UIState state)
	{
		_UIState = state;
		CheckState();
	}	
	void Sorter (bool deactActivate, bool activate, RectTransform element, List<RectTransform> thislist)
	{
		if(deactActivate)
		{
			
		}
		if(activate)
		{
			Activate(thislist);
		}
		if(!activate)
		{
			//Deactivate(false, thislist);
		}
	}
	void Deactivate (bool activate, RectTransform element, List<RectTransform> thislist)
	{
		foreach (RectTransform rT in thislist)
		{
			rT.gameObject.SetActive(false);
		}
		if(activate)
		{
			Activate(thislist);
		}

	}
	void Activate (List<RectTransform> thislist)
	{
		foreach (RectTransform rT in thislist)
		{
			rT.gameObject.SetActive(true);
		}
	}

	/* 
	//makes you pause ingame or unpause
	private void PressEscape () {
		
		if(Input.GetKeyDown(esc) && _UIState == UIState.Ingame && paused == false) {

			paused = true;
			Time.timeScale = 0;
			pauseMenu.gameObject.SetActive(true);

			SwitchCursorState(false);
		}		
		else if(Input.GetKeyDown(esc) && settingsActive == true) {

			SettingMenu();
		}
		else if(Input.GetKeyDown(esc) && _UIState == UIState.Ingame && paused == true) {

			Resume();
		}		
	}
	*/
}
