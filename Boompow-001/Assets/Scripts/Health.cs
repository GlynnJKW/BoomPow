using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField]
    private float MaxHealth;
    private float CurrentHealth;
    [SerializeField]
    private GameObject player;


	// Use this for initialization
	void Start () {
        CurrentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(CurrentHealth <= 0)
        {
            player.SetActive(false);
        }
	}

    void TakeDamage(float damage)
    {
        Debug.Log("Health Script took damage: " + damage);
        CurrentHealth -= damage;
    }

}
