using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {
    protected bool hit;

    // Use this for initialization
    void Start () {
        hit = false;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void resetHit()
    {
        hit = false;
    }
}
