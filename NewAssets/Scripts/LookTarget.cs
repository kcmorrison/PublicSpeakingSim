using UnityEngine;
using System.Collections;

public class LookTarget : MonoBehaviour {
    public HeadLookController headLook;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            headLook.target = hit.point;
        }
        
    }
}
