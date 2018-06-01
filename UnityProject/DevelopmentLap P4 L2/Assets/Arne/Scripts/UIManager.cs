using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	//singleton
	public static UIManager instance;

	//UIState
	public enum UIState {MainMenu, Ingame};
	public UIState _UIState;

	//ui elements lists
	public List <RectTransform> allMenuItems = new List<RectTransform>(); 
	public List <RectTransform> subMenus = new List<RectTransform>();
	public RectTransform mainMenu, ingame, settings, pauseMenu, buildingsbar, eventlog, statistics;
	public bool paused, settingsActive, creditsActive;
	private bool cursorActive;

	private float currentTimeScale;



	public KeyCode esc; //NEEDS TO BE CHANGED



	void Awake () 
	{
		if(instance == null)
		{
			instance = this;
		}
	}
	// Use this for initialization
	void MyStart () 
	{
		CheckState();
	}
	
	// Update is called once per frame
	void Update () 
	{
		PressEscape();

		HotKeys();
	}
	void CheckState ()
	{
		switch(_UIState)
		{
			case UIState.MainMenu:

				List<RectTransform> mainMenuList = new List<RectTransform>() {mainMenu};
			    EnableMenuItems(mainMenuList); 

			break;

			case UIState.Ingame:

				List<RectTransform> ingameList = new List<RectTransform>() {ingame};
			    EnableMenuItems(ingameList); 

			break;
		}
	}
	void SetState (UIState state)
	{
		_UIState = state;
		CheckState();
	}	
	//receives items and will make a list of them that will get send to another function
	private void EnableMenuItems(RectTransform item) {

        List<RectTransform> items = new List<RectTransform>() {item};
        EnableMenuItems(items);
    }
    //gets a list that will in which the objects will get set true after everything is set false
    private void EnableMenuItems(List<RectTransform> items) {

        foreach(RectTransform rT in allMenuItems)
		{
            rT.gameObject.SetActive(false);
			Debug.Log("deactivate");
		}
        foreach(RectTransform rT in items)
		{
            rT.gameObject.SetActive(true);
			Debug.Log("true");
		}
    }
	#region ButtonFunctions
	//receives number from button, with correct rectTransform and switches between states of rectTransform
	public void SubMenuConverter (int number) 
	{
		if(number == 4)
		{
			settingsActive = !settingsActive;
		}
		if(number == 5)
		{
			creditsActive = !creditsActive;
		}
		Submenus(subMenus[number]);
		Debug.Log(number);
	}
	//Switches between the on and off of a RectTransform
	private void Submenus (RectTransform element)
	{
		bool active = element.gameObject.activeSelf;
		active = !active;
		element.gameObject.SetActive(active);
		Debug.Log(element.gameObject.activeSelf);
	}
	//button function
	public void MainMenu () {

		paused = false;
		SetState(UIState.MainMenu);
		SetTimeScale(1f);
	}
	//button function
	public void Ingame () {

		SetState(UIState.Ingame);
		SetTimeScale(1f);
	}
	//button function
	public void Resume () {

		paused = false;
		pauseMenu.gameObject.SetActive(false);
		SetTimeScale(currentTimeScale);
		Debug.Log(currentTimeScale + "resume");
		SwitchCursorState(true);
	}
	//button function
	public void QuitGame () {

		Application.Quit();
	}
	public void SetTimeScale (float scale)
	{
		Debug.Log(scale);
		Time.timeScale = scale;
		if(!paused)
		{
			currentTimeScale = scale;
		}
		
	}
	#endregion
	//sets the right cursor state
	private void SwitchCursorState (bool state) {

		cursorActive = state;
		if(!cursorActive) {

			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None; 
			
		}
		if(cursorActive) {

			Cursor.lockState = CursorLockMode.Locked; 
			Cursor.visible = false;
		}	
	}
	private void HotKeys () 
	{
		if(paused)
		{
			return;
		}
		if(Input.GetButtonDown("Pause"))
		{
			SetTimeScale(0f);
		}
		if(Input.GetButtonDown("Normal Speed"))
		{
			SetTimeScale(1f);
		}
		if(Input.GetButtonDown("Fast Speed"))
		{
			SetTimeScale(1.5f);
		}
		if(Input.GetButtonDown("Faster Speed"))
		{
			SetTimeScale(2f);
		}
	}
	//makes you pause ingame or unpause
	private void PressEscape () {
		
		Debug.Log("escape");
		if(Input.GetKeyDown(esc) && _UIState == UIState.Ingame && paused == false) 
		{
			paused = true;
			SetTimeScale(0f);
			pauseMenu.gameObject.SetActive(true);

			SwitchCursorState(false);
		}		
		else if(Input.GetKeyDown(esc) && settingsActive) 
		{
			SubMenuConverter(4);
		}
		else if(Input.GetKeyDown(esc) && creditsActive)
		{
			SubMenuConverter(5);
		}
		else if(Input.GetKeyDown(esc) && _UIState == UIState.Ingame && paused) 
		{
			Resume();
		}		
	}
}
