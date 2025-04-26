using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    
    public GAME_STATE currentState;
    private GAME_STATE nextState;
    public static GameController instance;
    public List<Castle> castleEnemy;
    public Castle castlePlayer;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject ui;
    public float timeScale;
    public int levelIndex;
    public GameObject youGotAKey;

    private void Awake(){
        instance = this;
        Time.timeScale = timeScale;
        ui.SetActive(true);
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
        ApplicationController.currentLevel = levelIndex;
    }
    private void Update()
    {
        currentState = nextState;
        switch(currentState){
            case GAME_STATE.START:
                ChangeState(GAME_STATE.INGAME);
                break;
            case GAME_STATE.INGAME:
                if(castleEnemy.Count == 0)
                {
                    SimpleEnemy[] array = Object.FindObjectsOfType(typeof(SimpleEnemy)) as SimpleEnemy[];
                    if(array.Length == 0)
                    {
                        ChangeState(GAME_STATE.WIN);
                    }
                }
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    UIController.instance.ShowPause();
                }
                break;
            case GAME_STATE.PAUSE:
                Time.timeScale = 0f;
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    UIController.instance.ResumeGame();
                }
                break;
            case GAME_STATE.WIN:
            {
                winScreen.SetActive(true);
                PlayerScript player = Object.FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
                if(WhereAmI.instance.levelName == "Level1" && PlayerPrefs.GetInt("keyRed") == 0){
                    PlayerPrefs.SetInt("keyRed", 1);
                    youGotAKey.SetActive(true);
                }
                if(WhereAmI.instance.levelName == "Level2" && PlayerPrefs.GetInt("keyBlue") == 0){
                    PlayerPrefs.SetInt("keyBlue", 1);
                    youGotAKey.SetActive(true);
                }
                if(WhereAmI.instance.levelName == "Level3" && PlayerPrefs.GetInt("keyGreen") == 0){
                    PlayerPrefs.SetInt("keyGreen", 1);
                    youGotAKey.SetActive(true);
                }
                if(WhereAmI.instance.levelName == "Level4" && PlayerPrefs.GetInt("keyYellow") == 0){
                    PlayerPrefs.SetInt("keyYellow", 1);
                    youGotAKey.SetActive(true);
                }
                break;
            }
            case GAME_STATE.LOSE:
                loseScreen.SetActive(true);
                break;
            }
    }
    
    public void ChangeState(GAME_STATE newState){
        nextState = newState;
    }

    public void FinishGame(){
        ChangeState(GAME_STATE.WIN);
    }

    public void Pause(){
        ChangeState(GAME_STATE.PAUSE);
    }

    public void Resume(){
        Time.timeScale = timeScale;
        ChangeState(GAME_STATE.INGAME);
    }
}
