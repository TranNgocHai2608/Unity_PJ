using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterMenu : MonoBehaviour
{
    //test field
    public Text levelText, hitPointText, pesosText, upgradeCostText, xpText;

    //logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //character selection
    public void onArrowClick(bool up)
    {
        if (up)
        {
            currentCharacterSelection++;
            // if no more character in array
            if (currentCharacterSelection == gameManager.instance.playerSprites.Count)
            {
                currentCharacterSelection = 0;
            }
            onSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;
            // if no more character in array
            if (currentCharacterSelection < 0)
            {
                currentCharacterSelection = gameManager.instance.playerSprites.Count - 1;
            }
            onSelectionChanged();
        }
    }

    private void onSelectionChanged()
    {
        characterSelectionSprite.sprite = gameManager.instance.playerSprites[currentCharacterSelection];
        gameManager.instance.player.SwapSprite(currentCharacterSelection);

    }

    //weapon upgrade
    public void onUpgradeClick()
    {
        if (gameManager.instance.tryUpgradeWeapon())
        {
            updateMenu();
        }
    }

    //update character infomation
    public void updateMenu()
    {
        //weapon
        weaponSprite.sprite = gameManager.instance.weaponSprites[gameManager.instance.weapon.weaponlvl];
        if (gameManager.instance.weapon.weaponlvl == (gameManager.instance.weaponSprites.Count - 1))
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = gameManager.instance.weaponPrice[gameManager.instance.weapon.weaponlvl].ToString() + " G";

        //meta
        levelText.text = gameManager.instance.GetCurrentLevel().ToString();
        hitPointText.text = gameManager.instance.player.hitPoint.ToString();
        pesosText.text = gameManager.instance.pesos.ToString();

        // exp Bar
        int currLevel = gameManager.instance.GetCurrentLevel();
        if (gameManager.instance.GetCurrentLevel() == gameManager.instance.xpTable.Count) // max lv
        {
            xpText.text = gameManager.instance.exp.ToString() + " total exp point!"; // display total xp
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = gameManager.instance.GetXpLevel(currLevel - 1);
            int currLevelXp = gameManager.instance.GetXpLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = gameManager.instance.exp - prevLevelXp;

            float completionRaito = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRaito, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff;
        }
        
    }
}
