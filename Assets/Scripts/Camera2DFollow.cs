using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        //[SerializeField]
        //GameObject player;
        [SerializeField]
        float timeOffSet;
        [SerializeField]
        Vector2 posOffSet;

        [SerializeField]
        float leftLimit;
        [SerializeField]
        float rightLimit;
        [SerializeField]
        float bottomLimit;
        [SerializeField]
        float topLimit;

        private Vector3 velocity;
        public Transform target;
        float nextTimeToSearch = 0;
        Vector3 lastTargetPosition;

        void Start()
        {
            lastTargetPosition = target.position;
        }

        void Update()
        {
            if (target == null)
            {
                FindPlayer();
                return;


            }
            //camera current position
            Vector3 startPos = transform.position;

            //players current position
            Vector3 endPos = target.transform.position;

            endPos.x += posOffSet.x;
            endPos.y += posOffSet.y;
            endPos.z = -10;

            //smoothly move the camera towards the player position
            //transform.position = Vector3.Lerp(startPos, endPos, timeOffSet * Time.deltaTime);

            //move the camera towards the player position(SmoothDampening)
            transform.position = Vector3.SmoothDamp(startPos, endPos, ref velocity, timeOffSet);


            transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), Mathf.Clamp(transform.position.y, bottomLimit, topLimit), transform.position.z);
        }

        private void OnDrawGizmos()
        {
            //draw a box around our camera boundary
            Gizmos.color = Color.red;
            //top boundary line
            Gizmos.DrawLine(new Vector2 (leftLimit, topLimit), new Vector2 (rightLimit, topLimit));
            //right boundaryline
            Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));
            //bottom boundary line
            Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(leftLimit, bottomLimit));
            //left boundary line
            Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));
        }

        void FindPlayer()
        {
            if (nextTimeToSearch <= Time.time)
            {
                GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
                if (searchResult != null)
                    target = searchResult.transform;
                nextTimeToSearch = Time.time + 0.2f;
            }
        }
    }

}
/*
public class Camera2DFollow : MonoBehaviour
{
    public Transform target;
    public float dampaning = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;


    float offsetz;
    Vector3 lastTargetPosition;
    Vector3 currentVeclocity;
    Vector3 lookAheadPos;


    float nextTimeToSearch = 0;

    [SerializeField]
    float leftLimit;
    [SerializeField]
    float rightLimit;
    [SerializeField]
    float bottomLimit;
    [SerializeField]
    float topLimit;


    private void Start()
    {
        lastTargetPosition = target.position;
        offsetz = (transform.position - target.position).z;
        transform.parent = null;
    }


    void Update()
    {


        if (target == null)
        {
            FindPlayer();
            return;


        }


        float xMoveDelta = (target.position - lastTargetPosition).z;


        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) >
    lookAheadMoveThreshold;


        if (updateLookAheadTarget)
        {
            lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else
        {
            lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero,
                Time.deltaTime * lookAheadReturnSpeed);
        }


        Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward *
 offsetz;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref
 currentVeclocity, dampaning);


        transform.position = newPos;


        lastTargetPosition = target.position;

transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
        Mathf.Clamp(transform.position.x, bottomLimit, topLimit), transform.position.z);
    }




    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
                target = searchResult.transform;
            nextTimeToSearch = Time.time + 0.2f;
        }
    }
}*/

