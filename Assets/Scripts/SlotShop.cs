using UnityEngine;
using UnityEngine.UI;

public class SlotShop : MonoBehaviour
{
    
    public ItemShop itemToDisplay;
    public Image icon;
    public new Text name;
    public Text level;
    public Text cost;
    public Button buttonToUpgrade;
    public bool alwaysVisible;

    private void Start()
    {
        icon.sprite = itemToDisplay.icon;
        name.text = itemToDisplay.name;
        RefreshInfo();
        if(itemToDisplay.IsBlocked() && !alwaysVisible){
            Object.Destroy(base.gameObject);
        }
    }

    public void UpgradeItem(){
        if(ApplicationController.GetAmountGold() >= itemToDisplay.GetCost() && itemToDisplay.CanUpgrade()){
            ApplicationController.RemoveGold(itemToDisplay.GetCost());
            itemToDisplay.UpgradeItem();
            RefreshInfo();
        }
    }

    private void RefreshInfo(){
        cost.text = "(" + itemToDisplay.GetCost() + ")";
        level.text = "Level " + itemToDisplay.GetLevel();
        if(!itemToDisplay.CanUpgrade()){
            buttonToUpgrade.gameObject.SetActive(false);
        }
    }
}
