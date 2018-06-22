using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour {

	//singleton
	public static UIManager instance;

	//UIState
	public enum UIState {MainMenu, LoadingScreen, Ingame};
	public UIState _UIState;

	//ui elements lists
	public List <RectTransform> allMenuItems = new List<RectTransform>(); 
	public List <RectTransform> subMenus = new List<RectTransform>();
	public RectTransform mainMenu, ingame, settings, pauseMenu, buildingsbar, eventlog, statistics, loadingScreen;
	public bool paused, settingsActive, creditsActive;
	public List <Image> time = new List<Image>();
	public List <Image> buildingCategory = new List<Image>();
	public Color pressedButton, normalButton;

	//cursor
	private bool cursorActive;

	//timescale
	private float currentTimeScale;

	//animation
	public List<Animator> animList = new List<Animator>(); 
	public List<bool> animationActive = new List<bool>();
	public List<RectTransform> buildingbars = new List<RectTransform>();

	public bool blockState; //for testing

	//eventlog
	public TextMeshProUGUI eventButtonText;
	//public List<TextMeshProUGUI> newsMessage = new List<TextMeshProUGUI>();

	public string texttest; //testing
	public bool go; //testing
	public Queue<string> queue = new Queue<string>();
	public TextMeshProUGUI eventText;
	public int textLimit;

	//statistics
	public TextMeshProUGUI woodText;
	public TextMeshProUGUI stoneText;
	public TextMeshProUGUI moneyText;
	public TextMeshProUGUI foodText;
	public TextMeshProUGUI yearText;
    public TextMeshProUGUI citizenText;


    public void TextUpdate()
	{
        woodText.text = StatisticManager.instance.wood + " / " + StatisticManager.instance.woodStorage;
		stoneText.text = StatisticManager.instance.stone + " / " + StatisticManager.instance.stoneStorage;
		moneyText.text = StatisticManager.instance.money + " / " + StatisticManager.instance.moneyStorage;
		foodText.text = StatisticManager.instance.food + " / " + StatisticManager.instance.foodStorage;
        yearText.text = "Year: " + StatisticManager.instance.age;
        citizenText.text = StatisticManager.instance.citizens + " / " + StatisticManager.instance.allCitizens;

    }
	void Awake () 
	{
		if(instance == null)
		{
			instance = this;
		}
	}
	// Use this for initialization
	public void MyStart () 
	{
		TextUpdate();
		currentTimeScale = 1f;
		SetTimeScale(1f);
		SelectedTimeColor(1);
		if(blockState)
		{
			return;
		}
		else
		{
			CheckState();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		PressEscape();

		HotKeys();
		if(go)	//TESTING 
		{
			MakeEvents();
			go = false;
			texttest = "";
		}
		
	}
	//Testing
	public void MakeEvents ()
	{
		EventLog(texttest);
	}
	//has a string queue which puts a string on one line and foreach string in the queue it remembers the newest input
	public void EventLog (string events)
	{
		eventButtonText.text = "Events: " + events;
		if (queue.Count >= textLimit)
        {
			queue.Dequeue();
		}	
     	queue.Enqueue(events);
     
     	eventText.text = "";

     	foreach(string st in queue)
        {
			eventText.text = eventText.text + st + "\n";
		} 	
	}
	void CheckState ()
	{
		switch(_UIState)
		{
			case UIState.MainMenu:

				//main scene
				//LoadScene("Main menu");
				List<RectTransform> mainMenuList = new List<RectTransform>() {mainMenu};
			    EnableMenuItems(mainMenuList); 

			break;

			case UIState.LoadingScreen:

				//loading screen
				//load one of the scenes
				List<RectTransform> loadingscreen = new List<RectTransform>() {loadingScreen};
				EnableMenuItems(loadingscreen);

			break;
			case UIState.Ingame:

				//ingame scene
				//LoadScene("Level");
				List<RectTransform> ingameList = new List<RectTransform>() {ingame};
			    EnableMenuItems(ingameList); 

			break;
		}
	}
	void LoadScene (string name)
	{
		if(SceneManager.GetActiveScene().name == name)
		{
			return;
		}
		else
		{
			SceneManager.LoadScene(name);
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
			//Debug.Log("deactivate");
		}
        foreach(RectTransform rT in items)
		{
            rT.gameObject.SetActive(true);
			//Debug.Log("true");
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
		//Debug.Log(number);
	}
	//Switches between the on and off of a RectTransform
	private void Submenus (RectTransform element)
	{
		bool active = element.gameObject.activeSelf;
		active = !active;
		element.gameObject.SetActive(active);
		//Debug.Log(element.gameObject.activeSelf);
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
	public void LoadingScreen ()
	{
		//do the loading stuff
	}
	//button function
	public void Resume () {

		paused = false;
		pauseMenu.gameObject.SetActive(false);
		SetTimeScale(currentTimeScale);
		//Debug.Log(currentTimeScale + "resume");
		//SwitchCursorState(true);
	}
	//button function
	public void QuitGame () {

		Application.Quit();
	}
	public void SetTimeScale (float scale)
	{
		//Debug.Log(scale);
		Time.timeScale = scale;
		if(!paused)
		{
			currentTimeScale = scale;
		}
		
	}
	#endregion
	/* 
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
	*/
	//hotkeys for time speed
	private void HotKeys () 
	{
		if(paused)
		{
			return;
		}
		if(Input.GetButtonDown("Pause"))
		{
			SelectedTimeColor(0);
			SetTimeScale(0f);
		}
		if(Input.GetButtonDown("Normal Speed"))
		{
			SelectedTimeColor(1);
			SetTimeScale(1f);
		}
		if(Input.GetButtonDown("Fast Speed"))
		{
			SelectedTimeColor(2);
			SetTimeScale(1.5f);
		}
		if(Input.GetButtonDown("Faster Speed"))
		{
			SelectedTimeColor(3);
			SetTimeScale(2f);
		}
	}
	
	//makes you pause ingame or unpause
	private void PressEscape () {
		
		//Debug.Log("escape");
		if(Input.GetButtonDown("Escape") && _UIState == UIState.Ingame && paused == false) 
		{
			paused = true;
			SetTimeScale(0f);
			pauseMenu.gameObject.SetActive(true);

			//SwitchCursorState(false);
		}		
		else if(Input.GetButtonDown("Escape") && settingsActive) 
		{
			SubMenuConverter(4);
		}
		else if(Input.GetButtonDown("Escape") && creditsActive)
		{
			SubMenuConverter(5);
		}
		else if(Input.GetButtonDown("Escape") && _UIState == UIState.Ingame && paused) 
		{
			Resume();
		}		
	}
	public void Buildingbar (int number) //when going down it resets  before the animation can play which causes it to disappear
	{
		//sets other bars false
		foreach (var item in buildingbars)
		{
			item.gameObject.SetActive(false);
		}
		buildingbars[number].gameObject.SetActive(true);

		//does the correct animation value
		for (int i = 0; i < animationActive.Count; i++)
		{
			if(i == number)
			{
				//Debug.Log("same as number");
				animationActive[number] = !animationActive[number];
			}
			else if(i != number)
			{
				//Debug.Log("false");
				animationActive[i] = false;	
			}
		}
		PlayAnimation(number);
	}
	void PlayAnimation (int number)
	{	
		Animator anim = animList[number];
		anim.speed = 1f;
		anim.speed = anim.speed / currentTimeScale;


		//set button color 		
		foreach (var item in buildingCategory)
		{
			var image = item.GetComponent<Image>();
			image.color = normalButton;
		}
		//sets right animation and button color
		if(animationActive[number] && !anim.GetCurrentAnimatorStateInfo(0).IsName("Up"))
        {
            anim.SetBool("Up", true);

			buildingCategory[number].GetComponent<Image>().color = pressedButton;

			return;
        }
        if(!animationActive[number] && anim.GetCurrentAnimatorStateInfo(0).IsName("Up"))
        {
            anim.SetBool("Up", false);

			buildingCategory[number].GetComponent<Image>().color = normalButton;
			
			return;
        }
	}
	//hotkeys and timescale feedback
	public void SelectedTimeColor (int number)
	{
		foreach (var item in time)
		{
			var image = item.GetComponent<Image>();
			image.color = normalButton;
		}
		time[number].color = pressedButton;
	}
}