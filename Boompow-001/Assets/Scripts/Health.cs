using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField]
    private float MaxHealth;
    private float CurrentHealth;


	// Use this for initialization
	void Start () {
        CurrentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TakeDamage(float damage)
    {
        Debug.Log("Health Script took damage: " + damage);
        CurrentHealth -= damage;
    }
}
