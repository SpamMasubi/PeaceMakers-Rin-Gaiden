using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFGirlFighter : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Transform castPoint;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    Animator anim;

    Rigidbody2D rb2d;

    bool isFacingRight;
    private SFXManager sfxMan;

    private bool isAgros = false;
    private bool isSearching = false;
    private bool isAttacking = false;
    private bool canMove = true;
    float nextTimeToSearch = 0;
    Vector3 lastTargetPosition;

    public float waitBetweenAttacks;
    private float attackCounter;
    private float distToPlayer;

    [SerializeField]
    Transform castPos;
    [SerializeField]
    float baseCastDist;
    [SerializeField]
    float baseCastDistForward;

    FemaleEnemy enemy;

    const string Left = "left";
    const string Right = "right";

    string faceDirection;
    Vector3 baseScale;

    // Start is called before the first frame update
    void Start()
    {
        attackCounter = waitBetweenAttacks;
        rb2d = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        lastTargetPosition = player.position;
        sfxMan = FindObjectOfType<SFXManager>();

        baseScale = transform.localScale;
        faceDirection = Right;
        rb2d = GetComponent<Rigidbody2D>();
        enemy = GetComponent<FemaleEnemy>();

    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            distToPlayer = Vector2.Distance(transform.position, player.position);
            //print("player distance: " + distToPlayer);
        }
        if (player == null)
        {
            StopAttackingPlayer();
            FindPlayer();
            return;
        }

        if (CanSeePlayer(agroRange))
        {
            //agro player
            isAgros = true;

            if (distToPlayer <= 3)
            {
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
            }

        }
        else
        {
            if (isAgros)
            {
                if (!isSearching)
                {
                    isSearching = true;
                    Invoke("StopAttackingPlayer", 0);
                }
            }
        }

        if (isAgros && isAttacking)
        {

            attackPlayer();

        }


        /*
        //distance to player
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if(distToPlayer < agroRange)
        {
            //chase player
            ChasePlayer();
        }
        else
        {
            //stop chasing
            StopChasingPlayer();
        }*/
    }

    private void FixedUpdate()
    {

        float vX = -moveSpeed;

        if (faceDirection == Right)
        {
            vX = moveSpeed;
        }
        if (canMove)
        {
            //move GameObject
            if (FemaleEnemy.enemyDamage)
            {
                rb2d.velocity = Vector3.zero;
            }
            else
            {
                rb2d.velocity = new Vector3(-vX, rb2d.velocity.y);
            }
        }
        else if (!canMove)
        {
            rb2d.velocity = Vector3.zero;
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

    bool CanSeePlayer(float distance)
    {
        bool value = false;
        var castDist = distance;


        if (faceDirection == Right)
        {
            castDist = -distance;
        }
        else if (faceDirection == Left)
        {
            castDist = distance;
        }

        Vector2 endPoint = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPoint, 1 << LayerMask.NameToLayer("Player"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                value = true;
            }
            else
            {
                value = false;
            }
            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPoint, Color.blue);
        }

        return value;
    }

    void StopAttackingPlayer()
    {
        isAgros = false;
        isSearching = false;
        isAttacking = false;
        canMove = true;
        anim.Play("BFGirlFighterWalking");
    }

    void attackPlayer()
    {
        canMove = false;
        attackCounter -= Time.deltaTime;
        if (distToPlayer <= 4 && attackCounter < 0)
        {
            rb2d.velocity = Vector3.zero;
            anim.Play("BFGirlFighterAttack");
            sfxMan.GirlFighterAttack.Play();
            sfxMan.bossAttackSFX.Play();
            attackCounter = waitBetweenAttacks;
        }
        else
        {
            isAttacking = false;
        }
    }

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchPlayer = GameObject.FindGameObjectWithTag("Player");
            if (searchPlayer != null)
                player = searchPlayer.transform;
            nextTimeToSearch = Time.time + 0.2f;
        }
    }

    bool isHittingWall()
    {
        bool val = false;

        float castDist = baseCastDistForward;
        //define the cast distance for left and right
        if (faceDirection == Right)
        {
            castDist = -baseCastDistForward;
        }
        else
        {
            castDist = baseCastDistForward;
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
