using UnityEngine;

public class ButtonSelectLevel : MonoBehaviour
{

    public int levelIndex;
    public GameObject lockIcon;
    private LevelSelect levelSelect;

    private void Start()
    {
        levelSelect = Object.FindObjectOfType(typeof(LevelSelect)) as LevelSelect;
        if(ApplicationController.CanAccessLevel(levelIndex)){
            lockIcon.SetActive(false);
        }
    }

    public void GoToLevel(){
        levelSelect.GoToLevel(levelIndex);
    }
}
