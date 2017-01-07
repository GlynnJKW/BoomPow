using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttacks : MonoBehaviour {

    public GameObject Sword;
    public GameObject Scabbard;
    public GameObject Rhand;
    public GameObject Lhand;


    // Use this for initialization
    void Start () {
		
	}
    
    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(0)) {
            Sword.gameObject.transform.parent = Rhand.transform;
            Sword.gameObject.transform.localRotation = Quaternion.identity;
            Sword.gameObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            Sword.gameObject.transform.parent = Scabbard.transform;
            Sword.gameObject.transform.localRotation = Quaternion.identity;
            Sword.gameObject.transform.localPosition = Vector3.zero;
        }
    }
}
