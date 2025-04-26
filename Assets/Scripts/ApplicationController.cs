using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationController : MonoBehaviour
{
    
    public static ApplicationController instance;
    public static int currentLevel;
    public ItemShop initMagic;

    private void Start()
    {
        instance = this;
        Object.DontDestroyOnLoad(base.gameObject);
        SceneManager.LoadScene("MainMenu");
        if(PlayerPrefs.GetInt("itWasSetup") != 1){
            PlayerPrefs.SetInt("maxLevelCompleted", 0);
            initMagic.SetEquiped();
            PlayerPrefs.SetInt("itWasSetup", 1);
        }
    }

    public static bool CanAccessLevel(int levelID){
        return PlayerPrefs.GetInt("maxLevelCompleted") >= levelID - 1;
    }

    public static void AddMaxLevelCompleted(){
        if(currentLevel > PlayerPrefs.GetInt("maxLevelCompleted")){
            PlayerPrefs.SetInt("maxLevelCompleted", currentLevel);
        }
    }

    public static void AddGold(int amountGold = 1){
        int amountGold2 = GetAmountGold();
        PlayerPrefs.SetInt("Gold", amountGold2 + amountGold);
    }

    public static void RemoveGold(int amountGold){
        AddGold(amountGold * -1);
    }

    public static int GetAmountGold(){
        return PlayerPrefs.GetInt("Gold");
    }
}
