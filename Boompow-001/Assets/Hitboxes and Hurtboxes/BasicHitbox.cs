using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHitbox : Hitbox {

    [SerializeField]
    private float damage;
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
                    other.SendMessage("TakeDamage", damage);
                    hit = true;
                }
            }
        }
    }
}
