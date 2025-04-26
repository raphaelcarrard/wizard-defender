using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIController : MonoBehaviour
{

    public GameObject panelPause;
    public GameObject gameplayUI;
    public GameObject buttonPause;
    public Text amountGold;
    public GameObject goldIcon;
    public Image iconMagic;
    public static UIController instance;
    public GameObject mobileJoystick;
    private bool canShowSwitchButton;
    public Image switchButton;
    private void Awake()
    {
        panelPause.SetActive(false);
        instance = this;
    }


    private void Update()
    {
        amountGold.text = ApplicationController.GetAmountGold().ToString();
    }

    public void ShowPause(){
        panelPause.SetActive(true);
        buttonPause.SetActive(false);
        GameController.instance.Pause();
        HideMobileJoystick();
        goldIcon.SetActive(false);
        iconMagic.gameObject.SetActive(false);
    }

    public void ResumeGame(){
        panelPause.SetActive(false);
        buttonPause.SetActive(true);
        GameController.instance.Resume();
        ShowMobileJoystick();
        goldIcon.SetActive(true);
        iconMagic.gameObject.SetActive(true);
    }

    public void HideMobileJoystick(){
        Image[] componentsInChildren = mobileJoystick.GetComponentsInChildren<Image>();
        foreach(Image image in componentsInChildren){
            image.enabled = false;
        }
    }

    public void SetCanShowSwitchButton(bool value){
        canShowSwitchButton = value;
        switchButton.enabled = value;
    }

    public void ShowMobileJoystick(){
        Image[] componentsInChildren = mobileJoystick.GetComponentsInChildren<Image>();
        foreach(Image image in componentsInChildren){
            image.enabled = true;
        }
        if(!canShowSwitchButton){
            switchButton.enabled = false;
        }
    }

    public void ExitGame(){
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #elif UNITY_WEBGL
        SceneManager.LoadScene("ThanksForPlaying");
        #else
        Application.Quit();
        #endif
    }

    public void GoToSelectLevel(){
        ResumeGame();
        SceneManager.LoadScene("LevelSelect");
    }

    public void SetIconMagic(Sprite icon){
        iconMagic.sprite = icon;
    }
}
