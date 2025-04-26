using UnityEngine;
using UnityEngine.UI;

public class EquipMagicSlot : MonoBehaviour
{
    
    public Sprite iconLock;
    public Image iconSlot;
    public bool isLocked;
    public bool hasMagic;
    private ItemShop currentMagic;
    public EquipMagic controller;

    private void Start()
    {
        RefreshIcons();
        controller = Object.FindObjectOfType(typeof(EquipMagic)) as EquipMagic;
    }

    public void SetMagic(ItemShop magicToSet){
        iconSlot.sprite = magicToSet.icon;
        hasMagic = true;
        RefreshIcons();
        currentMagic = magicToSet;
        iconSlot.gameObject.SetActive(true);
    }

    public void SetLocked(){
        isLocked = true;
        RefreshIcons();
    }

    public void SetUnlocked(){
        isLocked = false;
        RefreshIcons();
    }

    public void RefreshIcons(){
        if(isLocked){
            iconSlot.sprite = iconLock;
            iconSlot.gameObject.SetActive(true);
        }
        if(!hasMagic && !isLocked){
            iconSlot.gameObject.SetActive(false);
        }
    }

    public void RemoveMagic(){
        if(hasMagic){
            hasMagic = false;
            RefreshIcons();
            currentMagic = null;
            controller.RefreshMagics();
        }
    }

    public bool IsEquipedMagic(ItemShop magic){
        return magic == currentMagic;
    }
}
