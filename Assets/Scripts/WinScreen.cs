using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{

    private enum WIN_STEPS
    {
        NONE = 0,
        SHOW_GOLD = 1,
        SHOW_NEXT_BUTTON = 2,
        SHOW_RETRY_AND_EXIT_BUTTON = 3,
        SHOW_MESSAGE_NEW_MAGIC_AVAILABLE = 4
    }
    public Text amountGold;
    public Level levelController;
    public float timeToLoadGold;
    private float currentTimeToLoadGold;
    private int goldToAdd;
    public Transform buttonExit;
    public Transform buttonNext;
    public Transform buttonRetry;
    public Transform buttonNewMagic;
    private WIN_STEPS currentStep = WIN_STEPS.SHOW_GOLD;
    
    private void Start()
    {
        levelController = Object.FindObjectOfType(typeof(Level)) as Level;
        buttonExit.gameObject.SetActive(false);
        buttonNext.gameObject.SetActive(false);
        buttonRetry.gameObject.SetActive(false);
        buttonNewMagic.gameObject.SetActive(false);
        amountGold.text = ApplicationController.GetAmountGold().ToString();
        goldToAdd = levelController.goldAfterWin;
        ApplicationController.AddMaxLevelCompleted();
        if(levelController.magicToUnlock != null){
            levelController.magicToUnlock.itemShop.UnblockItem();
        }
        UIController.instance.gameplayUI.SetActive(false);
        UIController.instance.HideMobileJoystick();
    }


    private void Update()
    {
        switch(currentStep){
            case WIN_STEPS.SHOW_GOLD:
                currentTimeToLoadGold += Time.deltaTime;
                if(currentTimeToLoadGold > timeToLoadGold){
                    int num = ((goldToAdd < 5) ? goldToAdd : 5);
                    goldToAdd -= num;
                    ApplicationController.AddGold(num);
                    amountGold.text = ApplicationController.GetAmountGold().ToString();
                    currentTimeToLoadGold = 0f;
                    GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
                    if(goldToAdd == 0){
                        currentStep = WIN_STEPS.SHOW_NEXT_BUTTON;
                    }
                }
                break;
            case WIN_STEPS.SHOW_NEXT_BUTTON:
                buttonNext.gameObject.SetActive(true);
                currentStep = WIN_STEPS.SHOW_RETRY_AND_EXIT_BUTTON;
                break;
            case WIN_STEPS.SHOW_RETRY_AND_EXIT_BUTTON:
                if(WhereAmI.instance.levelName == "Level4"){
                    buttonRetry.gameObject.SetActive(false);
                    buttonExit.gameObject.SetActive(false);
                }
                else if(WhereAmI.instance.levelName == "Level5"){
                    buttonRetry.gameObject.SetActive(false);
                    buttonNext.gameObject.SetActive(false);
                    buttonExit.gameObject.SetActive(true);
                }
                else
                {
                    buttonRetry.gameObject.SetActive(true);
                    buttonExit.gameObject.SetActive(true);
                }
                currentStep = WIN_STEPS.SHOW_MESSAGE_NEW_MAGIC_AVAILABLE;
                break;
            case WIN_STEPS.SHOW_MESSAGE_NEW_MAGIC_AVAILABLE:
                if(levelController.magicToUnlock != null){
                    buttonNewMagic.gameObject.SetActive(true);
                }
                if(WhereAmI.instance.levelName == "Level4"){
                    buttonNewMagic.gameObject.SetActive(false);
                }
                currentStep = WIN_STEPS.NONE;
                Time.timeScale = 1f;
                break;
            }
    }

    public void ExitLevel(){
        SceneManager.LoadScene("LevelSelect");
    }

    public void GoToShop(){
        SceneManager.LoadScene("Shop");
    }

    public void ReloadLevel(){
        SceneManager.LoadScene("Level" + GameController.instance.levelIndex);
    }

    public void NextLevel(){
        if(ApplicationController.CanAccessLevel(ApplicationController.currentLevel + 1)){
            SceneManager.LoadScene("Level" + (ApplicationController.currentLevel + 1));
        }
    }
}
