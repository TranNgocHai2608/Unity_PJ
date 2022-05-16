using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliable : MonoBehaviour
{
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        //collision work
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            onCollide(hits[i]);

            //the array not cleared up, so we do it ourself
            hits[i] = null;
        }
    }

    protected virtual void onCollide(Collider2D coll)
    {
        Debug.Log("onCollide was not implemented in " + this.name);
    }
}
