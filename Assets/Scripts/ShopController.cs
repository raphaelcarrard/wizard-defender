using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    
    public Text amountGold;
    
    private void Update()
    {
        amountGold.text = ApplicationController.GetAmountGold().ToString();
    }

    public void BackToMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToEquipMagic(){
        SceneManager.LoadScene("EquipMagic");
    }
}
