using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHitbox : colliable
{
    //damage
    public int damn = 1;
    public float pushForce = 1.5f;

    protected override void onCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name == "Player")
        {
            //create a new dmgObject, before we sending it to the Player
            damage dmg = new damage
            {
                dmgAmount = damn,
                origin = transform.position,
                pushForce = pushForce
            };
            coll.SendMessage("ReceiveDamage", dmg);

            Debug.Log(coll.name);

        }
    }
}
