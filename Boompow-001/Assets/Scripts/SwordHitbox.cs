using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        Debug.Log("Sword Hitbox up");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        GameObject otherplayer = other.gameObject;
        while(otherplayer != null)
        {
            if(otherplayer.tag != null && otherplayer.tag == "Player")
            {
                if(otherplayer != player)
                {
                    Debug.Log("Hit: " + otherplayer.gameObject.name);
                }
                break;
            }
            else
            {
                otherplayer = otherplayer.transform.parent.gameObject;
            }
        }
    }
}
