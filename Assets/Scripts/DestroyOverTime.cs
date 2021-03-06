using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

    public float timeToDestroy;

	// Use this for initialization
	void Start () {

        StartCoroutine(destroyObject());
	}


    IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);

    }
}
