using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{

    [SerializeField]
    private GameObject[] hitboxes;

    [SerializeField]
    private GameObject hbCenter;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ActivateHitbox(int num)
    {
        hitboxes[num].SetActive(true);
    }

    void DeactivateHitbox(int num)
    {
        hitboxes[num].SetActive(false);
        hitboxes[num].GetComponent<BasicHitbox>().resetHit();
    }
}
