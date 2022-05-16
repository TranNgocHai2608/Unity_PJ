using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fighter : MonoBehaviour
{
    //public field
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    //Push
    protected Vector3 pushDirection;

    //all fighter can receiveDamage / Die
    protected virtual void ReceiveDamage(damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPoint -= dmg.dmgAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
        }

        gameManager.instance.showText(dmg.dmgAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);

        if (hitPoint <= 0)
        {
            hitPoint = 0;
            Death();
        }
    }

    protected virtual void Death()
    {
        Debug.Log("immunity");
    }
}
