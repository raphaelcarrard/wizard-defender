using UnityEngine;
using UnityEngine.SceneManagement;

public class EquipMagic : MonoBehaviour
{
    
    public ItemShop magicItemShop;
    public EquipMagicSlot[] listSlot;
    public MagicToEquipButton[] listMagics;
    public ItemShop defaultMagic;

    private void Start()
    {
        for(int i = 0; i < listSlot.Length; i++){
            if(magicItemShop.GetLevel() >= i + 1){
                listSlot[i].SetUnlocked();
            }
            else
            {
                listSlot[i].SetLocked();
            }
            MagicToEquipButton[] array = listMagics;
            foreach(MagicToEquipButton magicToEquipButton in array){
                if(!isMagicEquiped(magicToEquipButton.itemShop) && magicToEquipButton.itemShop.IsEquiped()){
                    listSlot[i].SetMagic(magicToEquipButton.itemShop);
                }
            }
        }
        RefreshMagics();
    }

    public void GoBackToShop(){
        SceneManager.LoadScene("Shop");
        if(!HasMagicEquiped()){
            defaultMagic.SetEquiped();
        }
    }

    public void SetNewMagic(ItemShop magic){
        if(isMagicEquiped(magic) || magic.IsBlocked()){
            return;
        }
        EquipMagicSlot[] array = listSlot;
        foreach(EquipMagicSlot equipMagicSlot in array){
            if(!equipMagicSlot.hasMagic && !equipMagicSlot.isLocked){
                equipMagicSlot.SetMagic(magic);
                RefreshMagics();
                break;
            }
        }
    }

    public bool HasMagicEquiped(){
        EquipMagicSlot[] array = listSlot;
        foreach(EquipMagicSlot equipMagicSlot in array){
            if(equipMagicSlot.hasMagic && !equipMagicSlot.isLocked){
                return true;
            }
        }
        return false;
    }

    public void RefreshMagics(){
        MagicToEquipButton[] array = listMagics;
        foreach(MagicToEquipButton magicToEquipButton in array){
                magicToEquipButton.SetUnequiped();
                if(isMagicEquiped(magicToEquipButton.itemShop))
                {
                    magicToEquipButton.SetEquiped();
                }
        }
    }

    public bool isMagicEquiped(ItemShop magic){
        bool result = false;
        EquipMagicSlot[] array = listSlot;
        foreach(EquipMagicSlot equipMagicSlot in array){
            if(equipMagicSlot.IsEquipedMagic(magic)){
                result = true;
                break;
            }
        }
        return result;
    }
}
