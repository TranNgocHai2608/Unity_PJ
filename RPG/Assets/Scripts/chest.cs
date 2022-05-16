using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;

    protected override void onCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            gameManager.instance.pesos += pesosAmount;
            gameManager.instance.showText("+" + pesosAmount + " Golds!", 25, Color.yellow, transform.position, Vector3.up * 50, 1.5f);

        }
    }

}
