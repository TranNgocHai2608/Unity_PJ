using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    private void Awake()
    {
        if (gameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll();

        instance = this;

        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrice;
    public List<int> xpTable;

    //References
    public player player;
    public weapon weapon;
    public floatingTextManager floatingTextManager;

    //Logic
    public int pesos;
    public int exp;

    //floating text
    public void showText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // upgrade weapon
    public bool tryUpgradeWeapon()
    {
        // is the weapon max level?
        if (weaponPrice.Count <=  weapon.weaponlvl)
        {
            return false;
        }

        if (pesos >= weaponPrice[weapon.weaponlvl])
        {
            pesos -= weaponPrice[weapon.weaponlvl];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    // exp system
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while(exp >= add)
        {
            add += xpTable[r];
            r++;
            if (r == xpTable.Count) // max lv
                return r;
        }

        return r;
    }
     public int GetXpLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        exp += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();
    }

    public void OnLevelUp()
    {
        Debug.Log("level up!!");
        player.OnLevelUp();
    }
    //save state
    /*
     * Int seferredSkin 
     * Int pesos
     * Int exp
     * Int weaponLevel
     */
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += exp.ToString() + "|";
        s += weapon.weaponlvl.ToString();

        PlayerPrefs.SetString("SaveState", s);
        Debug.Log("SAVE");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //change plyer skin
        pesos = int.Parse(data[1]);
        //exp
        exp = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());
        //chang weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));

        Debug.Log("LOAD");
    }
}
