using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    Rigidbody2D rb2d;
    float moveSpeed = 3;

    [SerializeField]
    Transform castPos;
    [SerializeField]
    float baseCastDist;
    [SerializeField]
    float baseCastDistForward;

    Enemy enemy;

    const string Left = "left";
    const string Right = "right";

    string faceDirection;
    Vector3 baseScale;

    void Start()
    {
        baseScale = transform.localScale;
        faceDirection = Right;
        rb2d = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        
        float vX = moveSpeed;

        if (faceDirection == Left)
        {
            vX = -moveSpeed;
        }
        //move GameObject
        if (Enemy.enemyDamage)
        {
            rb2d.velocity = Vector3.zero;
            rb2d.velocity = Vector3.zero;
            rb2d.velocity = Vector3.zero;
            rb2d.velocity = Vector3.zero;
        }
        else
        {
            rb2d.velocity = new Vector3(vX, rb2d.velocity.y);
        }

        if (isHittingWall() || isNearEdge())
        {
            if (faceDirection == Left)
            {
                ChangeFacingDirection(Right);
            }
            else if (faceDirection == Right)
            {
                ChangeFacingDirection(Left);
            }
        }

    }

    void ChangeFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;
        if (newDirection == Right)
        {
            newScale.x = baseScale.x;
        }
        else if (newDirection == Left)
        {
            newScale.x = -baseScale.x;
        }

        transform.localScale = newScale;

        faceDirection = newDirection;

    }

    bool isHittingWall()
    {
        bool val = false;

        float castDist = baseCastDistForward;
        //define the cast distance for left and right
        if (faceDirection == Right)
        {
            castDist = baseCastDistForward;
        }
        else
        {
            castDist = -baseCastDistForward;
        }

        //determine the target destination based on the cast distance
        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.red);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Terrain")) || Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Enemy")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    bool isNearEdge()
    {
        bool val = true;

        float castDist = baseCastDist;

        //determine the target destination based on the cast distance
        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.blue);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }
}
