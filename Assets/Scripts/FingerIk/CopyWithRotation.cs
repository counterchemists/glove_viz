using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CopyWithRotation : MonoBehaviour {
    public bool isActive = true;

    public Transform target;
    public Vector3 offset;

	void Start ()
	{
		
	}
	
	void LateUpdate ()
	{
        if (isActive && target != null)
        {
            this.transform.rotation = target.rotation;
            this.transform.Rotate(offset);
        }
	}
}
