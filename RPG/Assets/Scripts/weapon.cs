using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : colliable
{
    //damage struct
    public int[] dmgPoint = { 1, 2, 3, 4, 5, 6 };
    public float[] pushForce = { 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 5.0f };

    //swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;


    //upgrade
    public int weaponlvl = 0;
    public SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void onCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
                return;

            //Create a new damage object, then we will send it to the fighter we've hit
            damage dmg = new damage
            {
                dmgAmount = dmgPoint[weaponlvl],
                origin = transform.position,
                pushForce = pushForce[weaponlvl]
            };
            coll.SendMessage("ReceiveDamage", dmg);

            Debug.Log(coll.name);
        }
        
    }

    private void Swing()
    {
        anim.SetTrigger("swing"); //"swing" is parameters in animator
    }

    public void UpgradeWeapon()
    {
        weaponlvl++;
        spriteRenderer.sprite = gameManager.instance.weaponSprites[weaponlvl];

    }

    public void SetWeaponLevel(int level)
    {
        weaponlvl = level;
        spriteRenderer.sprite = gameManager.instance.weaponSprites[weaponlvl];
    }
}
