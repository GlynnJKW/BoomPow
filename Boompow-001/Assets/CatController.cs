using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour {

    [SerializeField]
    private Renderer rend;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject catHead;
    [SerializeField]
    private Terrain terrain;
    [SerializeField]
    private float radius = 0;
    [SerializeField]
    private float radiusDrop = 0;
    [SerializeField]
    private float minRadius = 0;

    private bool seen;

    private Vector3 prevPos;
    private Vector3 prevDiff;


    [SerializeField]
    public Transform backLeft;
    [SerializeField]
    public Transform backRight;
    [SerializeField]
    public Transform frontLeft;
    [SerializeField]
    public Transform frontRight;

    private RaycastHit lr;
    private RaycastHit rr;
    private RaycastHit lf;
    private RaycastHit rf;
    private Vector3 upDir;

    // Use this for initialization
    void Start () {
        seen = false;
        bool hasPro = UnityEditorInternal.InternalEditorUtility.HasPro();
        print("Unity Version Pro: " + hasPro);
    }
	
	// Update is called once per frame
	void Update () {
        if (rend.isVisible)
        {
            seen = true;
        }
        else
        {
            //Debug.Log("Cat not being rendered");
            Vector3 pos = target.transform.position;
            if (seen)
            {
                prevPos = pos;
                float rotchange = Random.value * 180.0f - 90.0f;
                Vector3 diff = -target.transform.forward * radius;
                diff.y = 0;
                diff = Quaternion.AngleAxis(rotchange, Vector3.up) * diff;
                prevDiff = diff;
                pos = pos + diff;
                pos.y = terrain.SampleHeight(pos) + 0.1f;
                transform.position = pos;
                bool colliding = true;
                float currRadius = radius;
                while (colliding)
                {
                    Collider[] hitCol = Physics.OverlapSphere(transform.position + new Vector3(0, 2.5f, 0), 3f);
                    List<Collider> hitColliders = new List<Collider>();
                    foreach (Collider hit in hitCol)
                    {
                        //Debug.Log(gameObject + "    Hit    " + hit.gameObject);
                        if (hit.gameObject == gameObject)
                        {
                            //Debug.Log(gameObject + " hit itself");
                        }
                        else if (hit.gameObject.GetComponent<Terrain>() == terrain)
                        {
                            //Debug.Log(gameObject + " hit terrain");
                        }
                        else
                        {
                            //Debug.Log("Adding " + hit.gameObject + " to hit list");
                            hitColliders.Add(hit);
                            Debug.DrawLine(transform.position, hit.transform.position);
                        }
                    }

                    if (hitColliders.Count == 0)
                    {
                        colliding = false;
                    }
                    else
                    {

                        currRadius += 1;
                        rotchange = Random.value * 180.0f - 90.0f;
                        diff = -target.transform.forward * currRadius;
                        diff.y = 0;
                        diff = Quaternion.AngleAxis(rotchange, Vector3.up) * diff;
                        prevDiff = diff;
                        pos = pos + diff;
                        pos.y = terrain.SampleHeight(pos) + 0.1f;
                        transform.position = pos;
                        if (currRadius > 50)
                        {
                            foreach (Collider hit in hitColliders)
                            {
                                Debug.DrawLine(transform.position, hit.transform.position);
                            }
                            colliding = false;
                            Debug.Break();
                        }
                        transform.LookAt(target.transform);
                        Physics.Raycast(backLeft.position + Vector3.up, Vector3.down, out lr);
                        Physics.Raycast(backRight.position + Vector3.up, Vector3.down, out rr);
                        Physics.Raycast(frontLeft.position + Vector3.up, Vector3.down, out lf);
                        Physics.Raycast(frontRight.position + Vector3.up, Vector3.down, out rf);
                        upDir = (Vector3.Cross(rr.point - Vector3.up, lr.point - Vector3.up) +
                                 Vector3.Cross(lr.point - Vector3.up, lf.point - Vector3.up) +
                                 Vector3.Cross(lf.point - Vector3.up, rf.point - Vector3.up) +
                                 Vector3.Cross(rf.point - Vector3.up, rr.point - Vector3.up)
                                ).normalized;
                        Debug.DrawRay(rr.point, Vector3.up);
                        Debug.DrawRay(lr.point, Vector3.up);
                        Debug.DrawRay(lf.point, Vector3.up);
                        Debug.DrawRay(rf.point, Vector3.up);
                        transform.up = upDir;
                        Vector3 upNormal = upDir;
                        Vector3 normDiff = target.transform.position - transform.position;
                        Vector3.OrthoNormalize(ref upNormal, ref normDiff);
                        transform.forward = normDiff;
                    }
                }
                seen = false;
                radius -= radiusDrop;
                if (radius < minRadius) radius = minRadius;
            }
            else if (prevPos != null && prevPos != pos)
            {
                prevPos = pos;
                Vector3 diff = prevDiff;
                pos = pos + diff;
                pos.y = terrain.SampleHeight(pos);
                transform.position = pos;


                bool colliding = true;
                float currRadius = radius;
                while (colliding)
                {
                    Collider[] hitCol = Physics.OverlapSphere(transform.position + new Vector3(0, 2.5f, 0), 3f);
                    List<Collider> hitColliders = new List<Collider>();
                    foreach (Collider hit in hitCol)
                    {
                        //Debug.Log(gameObject + "    Hit    " + hit.gameObject);
                        if (hit.gameObject == gameObject)
                        {
                            //Debug.Log(gameObject + " hit itself");
                        }
                        else if (hit.gameObject.GetComponent<Terrain>() == terrain)
                        {
                            //Debug.Log(gameObject + " hit terrain");
                        }
                        else
                        {
                            //Debug.Log("Adding " + hit.gameObject + " to hit list");
                            hitColliders.Add(hit);
                            Debug.DrawLine(transform.position, hit.transform.position);
                        }
                    }

                    if (hitColliders.Count == 0)
                    {
                        colliding = false;
                    }
                    else
                    {

                        currRadius += 1;
                        float rotchange = Random.value * 180.0f - 90.0f;
                        diff = -target.transform.forward * currRadius;
                        diff.y = 0;
                        diff = Quaternion.AngleAxis(rotchange, Vector3.up) * diff;
                        prevDiff = diff;
                        pos = pos + diff;
                        pos.y = terrain.SampleHeight(pos) + 0.1f;
                        transform.position = pos;
                        if (currRadius > 50)
                        {
                            foreach (Collider hit in hitColliders)
                            {
                                Debug.DrawLine(transform.position, hit.transform.position);
                            }
                            colliding = false;
                            Debug.Break();
                        }
                        transform.LookAt(target.transform);
                        Physics.Raycast(backLeft.position + Vector3.up, Vector3.down, out lr);
                        Physics.Raycast(backRight.position + Vector3.up, Vector3.down, out rr);
                        Physics.Raycast(frontLeft.position + Vector3.up, Vector3.down, out lf);
                        Physics.Raycast(frontRight.position + Vector3.up, Vector3.down, out rf);
                        upDir = (Vector3.Cross(rr.point - Vector3.up, lr.point - Vector3.up) +
                                 Vector3.Cross(lr.point - Vector3.up, lf.point - Vector3.up) +
                                 Vector3.Cross(lf.point - Vector3.up, rf.point - Vector3.up) +
                                 Vector3.Cross(rf.point - Vector3.up, rr.point - Vector3.up)
                                ).normalized;
                        Debug.DrawRay(rr.point, Vector3.up);
                        Debug.DrawRay(lr.point, Vector3.up);
                        Debug.DrawRay(lf.point, Vector3.up);
                        Debug.DrawRay(rf.point, Vector3.up);
                        transform.up = upDir;
                        Vector3 upNormal = upDir;
                        Vector3 normDiff = target.transform.position - transform.position;
                        Vector3.OrthoNormalize(ref upNormal, ref normDiff);
                        transform.forward = normDiff;
                    }
                }


                seen = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 2.5f, 0), 3f);
    }

}
