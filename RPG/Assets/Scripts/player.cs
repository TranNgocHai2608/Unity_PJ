using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : mover
{
    //private BoxCollider2D boxCollider;
    //private Vector3 moveDelta;
    //private RaycastHit2D hit;

    //private void Start()
    //{
    //    boxCollider = GetComponent<BoxCollider2D>();
    //}

    //private void FixedUpdate()
    //{
    //    float x = Input.GetAxisRaw("Horizontal");
    //    float y = Input.GetAxisRaw("Vertical");

    //    //reset moveDelta
    //    moveDelta = new Vector3(x, y, 0);

    //    //swap sprite direction, wether you're move right or left
    //    if (moveDelta.x > 0)
    //        transform.localScale = new Vector3(1, 1, 1);
    //    else if (moveDelta.x < 0)
    //        transform.localScale = new Vector3(-1, 1, 1);

    //    //Make sure we can move in this dicection, by casting the box there first, if the box return null, we free to move 
    //    hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
    //    if(hit.collider == null)
    //    {
    //        //make this thing move
    //        transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
    //    }

    //    hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
    //    if (hit.collider == null)
    //    {
    //        //make this thing move
    //        transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
    //    }

    //}

    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int skinID)
    {
        spriteRenderer.sprite = gameManager.instance.playerSprites[skinID];
    }

    public void OnLevelUp()
    {
        maxHitPoint++;
        hitPoint = maxHitPoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }
}
