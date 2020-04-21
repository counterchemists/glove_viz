using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LockX : MonoBehaviour {

    public float val;

	
	void LateUpdate ()
	{
        this.transform.position = new Vector3(val, transform.position.magnitude, transform.position.z);
		
	}
}
