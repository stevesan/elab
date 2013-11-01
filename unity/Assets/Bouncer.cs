using UnityEngine;
using System.Collections;
using SteveSharp;

public class Bouncer : MonoBehaviour {

    public float forceMag = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter( Collision col )
    {
        if( col.rigidbody )
        {
            // assume we're only hitting one collider for now..
            Vector3 toOther = col.collider.transform.position - transform.position;
            col.rigidbody.AddForce( forceMag * toOther.normalized );
        }
    }
}
