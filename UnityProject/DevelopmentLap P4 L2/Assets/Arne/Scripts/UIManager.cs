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
	public RectTransform mainMenu, ingame, settings, pauseMenu, buildingsbar, eventlog, statistics;
	public bool paused, settingsActive;
	private bool cursorActive;



	public KeyCode esc; //NEEDS TO BE CHANGED



	void Awake () 
	{
		if(instance == null)
		{
			instance = this;
		}
	}
	// Use this for initialization
	void Start () 
	{
		CheckState();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
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

        foreach (RectTransform rT in allMenuItems)
		{
            rT.gameObject.SetActive(false);
		}
        foreach (RectTransform rT in items)
		{
            rT.gameObject.SetActive(true);
		}
    }
	private void Submenus (bool activate, RectTransform element)
	{
		//if filled in correctly settingsmenu function is not needed
	}
	//make you enable or disable the settings
	public void SettingMenu () {

		settingsActive = !settingsActive;
		settings.gameObject.SetActive(settingsActive);
		//enables settings rectTransform or disables it
	}
	
	//enables and disables the control info panel
	public void ControlInfo () {	//something else

		//controlInfoActive = !controlInfoActive;
		//controlInfo.gameObject.SetActive(controlInfoActive);
	}
	#region ButtonFunctions
	//button function
	public void MainMenu () {

		SetState(UIState.MainMenu);
	}
	//button function
	public void Ingame () {

		SetState(UIState.Ingame);
		SetTimeScale(1f);
	}
	//button function
	public void Resume () {

		paused = false;
		SetTimeScale(1f);
		pauseMenu.gameObject.SetActive(false);

		SwitchCursorState(true);
	}
	//button function
	public void QuitGame () {

		Application.Quit();
	}
	private void SetTimeScale (float scale)
	{
		Time.timeScale = scale;
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
	//makes you pause ingame or unpause
	private void PressEscape () {
		
		if(Input.GetKeyDown(esc) && _UIState == UIState.Ingame && paused == false) {

			paused = true;
			SetTimeScale(0f);
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
}
