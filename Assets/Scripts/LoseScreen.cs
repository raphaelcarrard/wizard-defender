using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    
    private enum LOSE_STEPS{
        NONE = 0,
        SHOW_RETRY_BUTTON = 1,
        SHOW_EXIT_BUTTON = 2
    }
    public Transform buttonExit;
    public Transform buttonRetry;
    public Transform buttonShop;
    private LOSE_STEPS currentStep = LOSE_STEPS.SHOW_RETRY_BUTTON;

    private void Start()
    {
        buttonExit.gameObject.SetActive(false);
        buttonShop.gameObject.SetActive(false);
        buttonRetry.gameObject.SetActive(false);
        UIController.instance.gameplayUI.SetActive(false);
        UIController.instance.HideMobileJoystick();
    }

    
    private void Update()
    {
        switch(currentStep){
            case LOSE_STEPS.SHOW_RETRY_BUTTON:
                buttonRetry.gameObject.SetActive(true);
                currentStep = LOSE_STEPS.SHOW_EXIT_BUTTON;
                break;
            case LOSE_STEPS.SHOW_EXIT_BUTTON:
                buttonExit.gameObject.SetActive(true);
                buttonShop.gameObject.SetActive(true);
                currentStep = LOSE_STEPS.NONE;
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
}
