using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunHitbox : Hitbox {

    [SerializeField]
    private float stunTime;
    [SerializeField]
    private GameObject player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (!hit && !(other is CharacterController))
        {
            //Debug.Log("First Hit");
            if (other.CompareTag("Hurtbox"))
            {
                if(other.GetComponent<BasicHurtbox>().player != player)
                {
                    Debug.Log("Hit: " + other);
                    other.SendMessage("Stun", stunTime);
                    hit = true;
                }
            }
        }
    }
}
