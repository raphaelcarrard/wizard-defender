using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    
    public void GoToLevel(int levelID){
        if(ApplicationController.CanAccessLevel(levelID)){
            SceneManager.LoadScene("level" + levelID);
            ApplicationController.currentLevel = levelID;
        }
    }

    public void GoBackMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToShop(){
        SceneManager.LoadScene("Shop");
    }
}
