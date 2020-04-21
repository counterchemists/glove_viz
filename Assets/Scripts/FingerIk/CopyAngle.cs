using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CopyAngle : MonoBehaviour {
    public bool isActive = true;

    public Transform t1;
    public Transform t2;
	
	void LateUpdate ()
	{
        if(isActive && t1 != null && t2 != null)
        {
            this.transform.localRotation = Quaternion.Euler(t2.localEulerAngles.x, t2.localEulerAngles.y, -t2.localEulerAngles.z);
            //this.transform.rotation = Quaternion.Inverse(t2.localRotation);
            //Quaternion q = Quaternion.FromToRotation(-LinkedPortal.transform.parent.forward, transform.parent.forward);
        }
    }
}
