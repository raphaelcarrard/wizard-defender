using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public void GoToGameplay(){
        SceneManager.LoadScene("LevelSelect");
    }

    public void GoToEquipMagic(){
        SceneManager.LoadScene("EquipMagic");
    }

    public void GoToShop(){
        SceneManager.LoadScene("Shop");
    }

    public void GoToSecret(){
        SceneManager.LoadScene("Secret");
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
}
