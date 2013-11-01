using UnityEngine;
using System.Collections;
using SteveSharp;

public class Goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter( Collider col )
    {
        if( Utility.FindAncestor<TheBall>(col.gameObject) != null )
        {
            Utility.FindAncestor<MainController>(gameObject).OnGoalHit();
        }
    }
}
