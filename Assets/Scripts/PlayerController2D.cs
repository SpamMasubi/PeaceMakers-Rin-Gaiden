using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRender;

    bool isGrounded;

    private SFXManager sfxMan;

    [SerializeField]
    Transform attackEffect;
    [SerializeField]
    Transform yoyoAttackPoint;

    public float attackRate = 2f;
    private float nextTimeAttack = 0f;

    [SerializeField]
    int damage = 20;

    [SerializeField]
    int yoyodamage = 50;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Transform groundCheckR;

    [SerializeField]
    Transform groundCheckL;
    [SerializeField]
    private float walkSpeed = 1.5f;
    [SerializeField]
    private float jumpSpeed = 5f;
    private float newSpeed = 2f;
    private float runningSpeed = 0.0f;

    private bool isAttacking = false;
    private bool isCrouching = false;
    private bool running = false;

    public AudioSource playerAttackYell;
    public AudioClip[] randAttackYell;

    public float attackRange = 0.5f;
    public float YoyoattackRange = 1.0f;

    public LayerMask bossMask;
    public LayerMask enemyMask;
    public LayerMask bossMaskYoyo;
    public LayerMask enemyMaskYoyo;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        sfxMan = FindObjectOfType<SFXManager>();
        playerAttackYell = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!WinLevel.isWin && !BossDefeated.isWin)
        {
            if (Time.time >= nextTimeAttack)
            {
                if (Input.GetButtonDown("Fire1") && !isAttacking)
                {
                    isAttacking = true;
                    float delay = 0.4f;
                    if (!isGrounded)
                    {
                        animator.Play("PlayerJumpKick");
                        delay = 0.2f;
                        sfxMan.playerAttack.Play();
                        playerAttackYell.clip = randAttackYell[Random.Range(0, randAttackYell.Length)];
                        playerAttackYell.PlayOneShot(playerAttackYell.clip);
                        if (Boss.startBoss)
                        {
                            Collider2D[] bossArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, bossMask);
                            foreach (Collider2D boss in bossArray)
                            {

                                boss.GetComponent<BossHealth>().TakeDamage(damage);
                            }
                        }else if (Commander.startCommanderBoss)
                        {
                            Collider2D[] commanderArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, bossMask);
                            foreach (Collider2D commander in commanderArray)
                            {

                                commander.GetComponent<CommanderHealth>().TakeDamage(damage);
                            }
                        }
                        else if (ChapterIntroUI.level3)
                        {
                            Collider2D[] enemyArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, enemyMask);
                            foreach (Collider2D enemy in enemyArray)
                            {
                                enemy.GetComponent<FemaleEnemy>().takeDamage(damage);
                            }
                        }
                        else if (Tsukimi.startTsukimiBoss)
                        {
                            Collider2D[] tsukimiArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, bossMask);
                            foreach (Collider2D tsukimi in tsukimiArray)
                            {

                                tsukimi.GetComponent<TsukimiHealth>().TakeDamage(damage);
                            }
                        }
                        else if (Xelcior.startXelciorBoss)
                        {
                            Collider2D[] xelciorArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, bossMask);
                            foreach (Collider2D xelcior in xelciorArray)
                            {

                                xelcior.GetComponent<XelciorHealth>().TakeDamage(damage);
                            }
                        }
                        else if (Hanna.startHannaBoss)
                        {
                            Collider2D[] hannaArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, bossMask);
                            foreach (Collider2D hanna in hannaArray)
                            {

                                hanna.GetComponent<HannaHealth>().TakeDamage(damage);
                            }
                        }
                        else if (Mana.startManaBoss)
                        {
                            Collider2D[] manaArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, bossMask);
                            foreach (Collider2D mana in manaArray)
                            {

                                mana.GetComponent<ManaHealth>().TakeDamage(damage);
                            }
                        }
                        else
                        {
                            Collider2D[] enemyArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, enemyMask);
                            foreach (Collider2D enemy in enemyArray)
                            {
                                enemy.GetComponent<Enemy>().takeDamage(damage);
                            }
                        }
                        /*
                    }else if(isGrounded && isCrouching){

                        animator.Play("PlayerCrouchAttack");
                        sfxMan.playerAttack.Play();
                        playerAttackYell.clip = randAttackYell[Random.Range(0, randAttackYell.Length)];
                        playerAttackYell.PlayOneShot(playerAttackYell.clip);
                        Collider2D[] enemyArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, enemyLayers);
                        foreach (Collider2D enemy in enemyArray)
                        {
                            enemy.GetComponent<Enemy>().takeDamage(damage);
                            enemyDamage = true;
                        }*/
                    }
                    else
                    {
                        //choose a random attack to play
                        int index = UnityEngine.Random.Range(1, 5);
                        animator.Play("PlayerAttack" + index);
                        sfxMan.playerAttack.Play();
                        playerAttackYell.clip = randAttackYell[Random.Range(0, randAttackYell.Length)];
                        playerAttackYell.PlayOneShot(playerAttackYell.clip);
                        if (Boss.startBoss)
                        {
                            Collider2D[] bossArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, bossMask);
                            foreach (Collider2D boss in bossArray)
                            {

                                boss.GetComponent<BossHealth>().TakeDamage(damage);
                            }
                        }
                        else if (Commander.startCommanderBoss)
                        {
                            Collider2D[] commanderArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, bossMask);
                            foreach (Collider2D commander in commanderArray)
                            {

                                commander.GetComponent<CommanderHealth>().TakeDamage(damage);
                            }
                        }
                        else if (ChapterIntroUI.level3) {
                            Collider2D[] enemyArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, enemyMask);
                            foreach (Collider2D enemy in enemyArray)
                            {
                                enemy.GetComponent<FemaleEnemy>().takeDamage(damage);
                            }
                        }
                        else if (Tsukimi.startTsukimiBoss)
                        {
                            Collider2D[] tsukimiArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, bossMask);
                            foreach (Collider2D tsukimi in tsukimiArray)
                            {

                                tsukimi.GetComponent<TsukimiHealth>().TakeDamage(damage);
                            }
                        }
                        else if (Xelcior.startXelciorBoss)
                        {
                            Collider2D[] xelciorArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, bossMask);
                            foreach (Collider2D xelcior in xelciorArray)
                            {

                                xelcior.GetComponent<XelciorHealth>().TakeDamage(damage);
                            }
                        }
                        else if (Hanna.startHannaBoss)
                        {
                            Collider2D[] hannaArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, bossMask);
                            foreach (Collider2D hanna in hannaArray)
                            {

                                hanna.GetComponent<HannaHealth>().TakeDamage(damage);
                            }
                        }
                        else if (Mana.startManaBoss)
                        {
                            Collider2D[] manaArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, bossMask);
                            foreach (Collider2D mana in manaArray)
                            {

                                mana.GetComponent<ManaHealth>().TakeDamage(damage);
                            }
                        }
                        else
                        {
                            Collider2D[] enemyArray = Physics2D.OverlapCircleAll(attackEffect.position, attackRange, enemyMask);
                            foreach (Collider2D enemy in enemyArray)
                            {
                                enemy.GetComponent<Enemy>().takeDamage(damage);
                            }
                        }
                    }

                    nextTimeAttack = Time.time + 1f / attackRate;
                    StartCoroutine(DoAttack(delay));
                }

                if (Input.GetButtonDown("Fire2") && !isAttacking)
                {
                    isAttacking = true;
                    float delay = 0.4f;
                    if (!isGrounded)
                    {
                        animator.Play("PlayerJumpYoyoAttack");
                        delay = 0.3f;
                        sfxMan.playerYoyoAttack.Play();
                        sfxMan.playerAttack.Play();
                        if (Boss.startBoss)
                        {
                            Collider2D[] bossArrayYoyo = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, bossMaskYoyo);
                            foreach (Collider2D bossYoyoAttack in bossArrayYoyo)
                            {
                                bossYoyoAttack.GetComponent<BossHealth>().TakeDamage(yoyodamage);
                            }
                        }
                        else if (Commander.startCommanderBoss)
                        {
                            Collider2D[] commanderArrayYoyo = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, bossMaskYoyo);
                            foreach (Collider2D commander in commanderArrayYoyo)
                            {

                                commander.GetComponent<CommanderHealth>().TakeDamage(yoyodamage);
                            }
                        }
                        else if (ChapterIntroUI.level3)
                        {
                            Collider2D[] femaleEnemyArray = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, enemyMaskYoyo);
                            foreach (Collider2D femEnemy in femaleEnemyArray)
                            {
                                femEnemy.GetComponent<FemaleEnemy>().takeDamage(yoyodamage);
                            }
                        }
                        else if (Tsukimi.startTsukimiBoss)
                        {
                            Collider2D[] tsukimiArrayYoyo = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, bossMaskYoyo);
                            foreach (Collider2D tsukimi in tsukimiArrayYoyo)
                            {

                                tsukimi.GetComponent<TsukimiHealth>().TakeDamage(yoyodamage);
                            }
                        }
                        else if (Xelcior.startXelciorBoss)
                        {
                            Collider2D[] xelciorArrayYoyo = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, bossMaskYoyo);
                            foreach (Collider2D xelcior in xelciorArrayYoyo)
                            {

                                xelcior.GetComponent<XelciorHealth>().TakeDamage(yoyodamage);
                            }
                        }
                        else if (Hanna.startHannaBoss)
                        {
                            Collider2D[] hannaArrayYoyo = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, bossMaskYoyo);
                            foreach (Collider2D hanna in hannaArrayYoyo)
                            {

                                hanna.GetComponent<HannaHealth>().TakeDamage(yoyodamage);
                            }
                        }
                        else if (Mana.startManaBoss)
                        {
                            Collider2D[] manaArrayYoyo = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, bossMaskYoyo);
                            foreach (Collider2D mana in manaArrayYoyo)
                            {

                                mana.GetComponent<ManaHealth>().TakeDamage(yoyodamage);
                            }
                        }
                        else
                        {
                            Collider2D[] enemyArrayYoyo = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, enemyMaskYoyo);
                            foreach (Collider2D enemyYoyoAttack in enemyArrayYoyo)
                            {
                                enemyYoyoAttack.GetComponent<Enemy>().takeDamage(yoyodamage);
                            }
                        }

                    }
                    else
                    {
                        animator.Play("PlayerYoyoAttack");
                        sfxMan.playerYoyoAttack.Play();
                        sfxMan.playerAttack.Play();
                        if (Boss.startBoss) {
                            Collider2D[] bossArrayYoyo = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, bossMaskYoyo);
                            foreach (Collider2D bossYoyoAttack in bossArrayYoyo)
                            {
                                bossYoyoAttack.GetComponent<BossHealth>().TakeDamage(yoyodamage);
                            }
                        }
                        else if (Commander.startCommanderBoss)
                        {
                            Collider2D[] commanderArrayYoyo = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, bossMaskYoyo);
                            foreach (Collider2D commander in commanderArrayYoyo)
                            {

                                commander.GetComponent<CommanderHealth>().TakeDamage(yoyodamage);
                            }
                        }
                        else if (ChapterIntroUI.level3)
                        {
                            Collider2D[] femaleEnemyArray = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, enemyMaskYoyo);
                            foreach (Collider2D femEnemy in femaleEnemyArray)
                            {
                                femEnemy.GetComponent<FemaleEnemy>().takeDamage(yoyodamage);
                            }
                        }
                        else if (Tsukimi.startTsukimiBoss)
                        {
                            Collider2D[] tsukimiArrayYoyo = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, bossMaskYoyo);
                            foreach (Collider2D tsukimi in tsukimiArrayYoyo)
                            {

                                tsukimi.GetComponent<TsukimiHealth>().TakeDamage(yoyodamage);
                            }
                        }
                        else if (Xelcior.startXelciorBoss)
                        {
                            Collider2D[] xelciorArrayYoyo = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, bossMaskYoyo);
                            foreach (Collider2D xelcior in xelciorArrayYoyo)
                            {

                                xelcior.GetComponent<XelciorHealth>().TakeDamage(yoyodamage);
                            }
                        }
                        else if (Hanna.startHannaBoss)
                        {
                            Collider2D[] hannaArrayYoyo = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, bossMaskYoyo);
                            foreach (Collider2D hanna in hannaArrayYoyo)
                            {

                                hanna.GetComponent<HannaHealth>().TakeDamage(yoyodamage);
                            }
                        }
                        else if (Mana.startManaBoss)
                        {
                            Collider2D[] manaArrayYoyo = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, bossMaskYoyo);
                            foreach (Collider2D mana in manaArrayYoyo)
                            {

                                mana.GetComponent<ManaHealth>().TakeDamage(yoyodamage);
                            }
                        }
                        else
                        {
                            Collider2D[] enemyArrayYoyo = Physics2D.OverlapCircleAll(yoyoAttackPoint.position, YoyoattackRange, enemyMaskYoyo);
                            foreach (Collider2D enemyYoyoAttack in enemyArrayYoyo)
                            {
                                enemyYoyoAttack.GetComponent<Enemy>().takeDamage(yoyodamage);
                            }
                        }
                    }

                    nextTimeAttack = Time.time + 1f / attackRate;
                    StartCoroutine(DoAttack(delay));
                }
            }
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }
    IEnumerator DoAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
    }

    private void FixedUpdate()
    {
        if (!WinLevel.isWin && !BossDefeated.isWin)
        {
            if ((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
                    (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
                    (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))) ||
                    (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Terrain"))) ||
                    (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Terrain"))) ||
                    (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Terrain"))))

            {
                isGrounded = true;

            }
            else
            {
                isGrounded = false;

                if (!isAttacking)
                {
                    animator.Play("PlayerJump");
                }
            }
            if (!isAttacking)
            {
                if (Input.GetKey("d") || Input.GetKey("right"))
                {
                    rb2d.velocity = new Vector2(walkSpeed, rb2d.velocity.y);
                    if (isGrounded && !isAttacking && running)
                    {
                        runningSpeed = walkSpeed * newSpeed;
                        rb2d.velocity = new Vector2(runningSpeed, rb2d.velocity.y);
                        animator.Play("PlayerRun");
                        running = false;
                    }
                    else if(isGrounded && !isAttacking)
                    {
                        animator.Play("PlayerWalk");
                    }
                    if (Input.GetKey(KeyCode.LeftShift) && !isGrounded || Input.GetKey(KeyCode.RightShift) && !isGrounded)
                    {
                        runningSpeed = walkSpeed * newSpeed;
                        rb2d.velocity = new Vector2(runningSpeed, rb2d.velocity.y);

                    }

                    //spriteRender.flipX = false;
                    transform.localScale = new Vector3(1, 1, 1);

                }
                else if (Input.GetKey("a") || Input.GetKey("left"))
                {
                    rb2d.velocity = new Vector2(-walkSpeed, rb2d.velocity.y);
                    if (isGrounded && !isAttacking && running)
                    {
                        runningSpeed = walkSpeed * newSpeed;
                        rb2d.velocity = new Vector2(-runningSpeed, rb2d.velocity.y);
                        animator.Play("PlayerRun");
                        running = false;
                    
                    }
                    else if(isGrounded && !isAttacking)
                    {
                        animator.Play("PlayerWalk");
                    }    
                    if (Input.GetKey(KeyCode.LeftShift) && !isGrounded || Input.GetKey(KeyCode.RightShift) && !isGrounded)
                    {
                        runningSpeed = walkSpeed * newSpeed;
                        rb2d.velocity = new Vector2(-runningSpeed, rb2d.velocity.y);

                    }

                    //spriteRender.flipX = true;
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (isGrounded)
                {   
                    running = false;
                    if (!isAttacking)
                    {
                        animator.Play("PlayerIdle");
                        
                    }
                    rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                }

                if (Input.GetKey("up") && isGrounded || Input.GetKey("w") && isGrounded)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
                    animator.Play("PlayerJump");
                    sfxMan.playerJump.Play();
                }

                /*
                if (Input.GetKey("down") && isGrounded || Input.GetKey("s") && isGrounded)
                {
                    isCrouching = true;
                    rb2d.velocity = Vector2.zero;
                    animator.Play("PlayerCrouch");
                }*/
                if (Input.GetKey(KeyCode.LeftShift) && isGrounded || Input.GetKey(KeyCode.RightShift) && isGrounded)
                {
                    running = true;
                }
            }
            else
            {
                rb2d.velocity = Vector2.zero;
            }
        }
        else
        {
            animator.Play("PlayerWinPose");
            rb2d.velocity = Vector2.zero;
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackEffect == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackEffect.position, attackRange);

        if (yoyoAttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(yoyoAttackPoint.position, YoyoattackRange);
    }
    
}
