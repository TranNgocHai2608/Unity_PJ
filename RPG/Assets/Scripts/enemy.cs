using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : mover
{
    //exp
    public int expValue = 1;

    //logic
    public float triggerLenght = 1;
    public float chaseLenght = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosiion;

    //hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = gameManager.instance.player.transform;
        startingPosiion = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    protected override void UpdateMotor(Vector3 input)
    {
        xSpeed = 0.5f;
        ySpeed = 0.5f;
        base.UpdateMotor(input);

        if (moveDelta.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void FixedUpdate()
    {
        //is the player in range?
        if (Vector3.Distance(playerTransform.position, startingPosiion) < chaseLenght)
        {
            if(Vector3.Distance(playerTransform.position, startingPosiion) < triggerLenght)
                chasing = true;

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosiion - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosiion - transform.position);
            chasing = false;
        }

        //check for overlaps
        collidingWithPlayer = false;
        //collision work
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            //the array not cleared up, so we do it ourself
            hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        gameManager.instance.GrantXp(expValue);
        gameManager.instance.showText("+ " + expValue +" xp", 25, Color.magenta, transform.position, Vector3.up * 20, 1.5f);
    }

}
