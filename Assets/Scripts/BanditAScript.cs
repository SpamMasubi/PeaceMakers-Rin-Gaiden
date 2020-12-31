using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BanditAScript : MonoBehaviour
{

    bool isShaking = false;

    public AIPath aipath;

    [SerializeField]
    float shakeAmount = 0.05f;

    Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (isShaking)
        {
            transform.position = startPos + UnityEngine.Random.insideUnitCircle * shakeAmount;
        }

        if(aipath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }else if(aipath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "AttackHitbox")
        {
            //do damage
            isShaking = true;
            Invoke("StopShaking", 0.3f);
        }
    }

    void StopShaking()
    {
        isShaking = false;
        transform.position = startPos;
    }
}
