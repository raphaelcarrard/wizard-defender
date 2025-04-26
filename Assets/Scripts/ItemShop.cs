using UnityEngine;

public class ItemShop : MonoBehaviour
{

    public new string name;
    public Sprite icon;
    public int initialCost;
    public float multiplierCostLevel;
    public float multiplierByLevel;
    public int initialLevel;
    public int maxUpgradeLevel;
    public bool alwaysUnblocked;

    public int GetCost(){
        if(GetLevel() == 1){
            return initialCost;
        }
        return (int)((float)GetLevel() * multiplierCostLevel * (float)initialCost);
    }

    public int GetLevel(){
        int @int = PlayerPrefs.GetInt("Level" + name);
        if(@int == 0 && initialLevel != 0){
            @int = initialLevel;
        }
        return @int;
    }

    public void UpgradeItem(){
        PlayerPrefs.SetInt("Level" + name, GetLevel() + 1);
    }

    public void SetEquiped(){
        PlayerPrefs.SetInt("MagicEquip" + name, 1);
    }

    public void SetUnequiped(){
        PlayerPrefs.SetInt("MagicEquip" + name, 0);
    }

    public bool IsEquiped(){
        return PlayerPrefs.GetInt("MagicEquip" + name) == 1;
    }

    public bool CanUpgrade(){
        return GetLevel() < maxUpgradeLevel;
    }

    public bool IsBlocked(){
        return PlayerPrefs.GetInt("IsUnblocked" + name) != 1 && !alwaysUnblocked;
    }

    public void UnblockItem(){
        PlayerPrefs.SetInt("IsUnblocked" + name, 1);
    }

    public float GetNewValueByLevel(float initValue){
        return initValue + multiplierByLevel * (float)GetLevel() * initValue;
    }

    public float GetNewValueByLevelReverse(float initValue){
        return initValue - (float)GetLevel() / multiplierByLevel / (initValue * 2f);
    }
}
