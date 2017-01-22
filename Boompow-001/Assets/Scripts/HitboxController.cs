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
        hitboxes[num].GetComponent<Hitbox>().resetHit();
    }

    void DeactivateAllHitboxes()
    {
        for(int i = 0; i < hitboxes.Length; ++i)
        {
            hitboxes[i].SetActive(false);
            hitboxes[i].GetComponent<Hitbox>().resetHit();
        }
    }
}
