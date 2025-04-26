using UnityEngine;
using UnityEngine.UI;

public class MagicToEquipButton : MonoBehaviour
{
    
    public ItemShop itemShop;
    public Image iconImage;
    public Sprite lockIcon;

    private void Start()
    {
        if(!itemShop.IsBlocked()){
            iconImage.sprite = itemShop.icon;
        }
        else
        {
            iconImage.sprite = lockIcon;
        }
    }

    public void SetEquiped(){
        Color color = iconImage.color;
        color.a = 0.5f;
        iconImage.color = color;
        itemShop.SetEquiped();
    }

    public void SetUnequiped(){
        Color color = iconImage.color;
        color.a = 1f;
        iconImage.color = color;
        itemShop.SetUnequiped();
    }
}
