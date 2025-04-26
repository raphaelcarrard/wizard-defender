using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SecretArea : MonoBehaviour
{
    public static SecretArea instance;
    public Image keyRedImage, keyBlueImage, keyGreenImage, keyYellowImage;
    public Button keyRedButton, keyBlueButton, keyGreenButton, keyYellowButton;
    public GameObject panel, closeButton, goBackButton;
    public Image hotPic1, hotPic2, hotPic3, hotPic4;
    public Image keyRedLock, keyBlueLock, keyGreenLock, keyYellowLock;

    void Awake(){
        instance = this;
    }

    void Update(){
        if(PlayerPrefs.GetInt("keyRed") == 1){
             keyRedImage.enabled = true;
             keyRedButton.interactable = true;
             keyRedLock.enabled = false;
        }
        if(PlayerPrefs.GetInt("keyBlue") == 1){
             keyBlueImage.enabled = true;
             keyBlueButton.interactable = true;
             keyBlueLock.enabled = false;
        }
        if(PlayerPrefs.GetInt("keyGreen") == 1){
             keyGreenImage.enabled = true;
             keyGreenButton.interactable = true;
             keyGreenLock.enabled = false;
        }
        if(PlayerPrefs.GetInt("keyYellow") == 1){
             keyYellowImage.enabled = true;
             keyYellowButton.interactable = true;
             keyYellowLock.enabled = false;
        }
    }

    public void ButtonRed(){
        panel.SetActive(false);
        goBackButton.SetActive(false);
        hotPic1.enabled = true;
        closeButton.SetActive(true);
    }

    public void ButtonBlue(){
        panel.SetActive(false);
        goBackButton.SetActive(false);
        hotPic2.enabled = true;
        closeButton.SetActive(true);
    }

    public void ButtonGreen(){
        panel.SetActive(false);
        goBackButton.SetActive(false);
        hotPic3.enabled = true;
        closeButton.SetActive(true);
    }

    public void ButtonYellow(){
        panel.SetActive(false);
        goBackButton.SetActive(false);
        hotPic4.enabled = true;
        closeButton.SetActive(true);
    }

    public void ButtonCloseImage(){
        panel.SetActive(true);
        goBackButton.SetActive(true);
        hotPic1.enabled = false;
        hotPic2.enabled = false;
        hotPic3.enabled = false;
        hotPic4.enabled = false;
        closeButton.SetActive(false);
    }

    public void GoBackMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
