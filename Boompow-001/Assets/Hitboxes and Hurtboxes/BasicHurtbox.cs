using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHurtbox : MonoBehaviour {

    [SerializeField]
    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TakeDamage(float damage)
    {
        Debug.Log("Hurtbox took damage: " + damage);
        player.SendMessage("TakeDamage", damage);
    }
}
