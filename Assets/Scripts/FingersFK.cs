using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

[ExecuteInEditMode]
public class FingersFK : MonoBehaviour
{

    [System.Serializable]
    public class FingerFKJoints
    {
        [HideInInspector]
        public string name = "";
        public bool enable = false;

        // [Header("Angles")]
        [Range(0, 90)]
        public float angleJoint = 0;
        [Range(0, 90)]
        public float angleBase = 0;
        public bool baseCopyJoints = false;
        [Range(1, 100)]
        public float influenceBase = 0;

        [Header("Base")]
        public bool doBase = true;
        public Transform proximal;
        public Vector3 rotationAxisBase;
        public Vector3 rotationOffsetBase;

        [Header("Phalanges")]
        public Transform midal;
        public Transform distal;
        public Vector3 rotationAxis;
        public Vector3 rotationOffset;
        public Vector3 midalRotationOffset;


        [Header("Mapping")]
        public int inFrom = 355;
        public int inTo = 411;
        public int outFrom = 0;
        public int outTo = 90;
        public float target = 0;
        public float smoothTarget = 0;

        public void inputTarget(int value)
        {

            float v = Remap(value, inFrom, inTo, outFrom, outTo);


           smoothTarget = Mathf.Lerp(smoothTarget, v, Time.deltaTime * 50);
            angleJoint = smoothTarget;
        }

        public float Remap(float value, float from1, float to1, float from2, float to2)
        {
            if (value < from1) value = from1;
            if (value > to1) value = to1;

            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }


    };

    public float smoothSpeed = 3.0F;

    public FingerFKJoints[] fingerJoints= new FingerFKJoints[5];
    // Start is called before the first frame update

    [Header("Debug")]
    public bool copyAll = false;
    [Range(0, 90)]
    public float angleJoint = 0;
    [Range(0, 90)]
    public float angleBase = 0;

    public FingerFKJoints GetFingerByName(string n)
    {
        FingerFKJoints f = null;

        foreach (FingerFKJoints fkj in fingerJoints)
        {
            if (fkj.name == n)
                f = fkj; 
        }
        return f;
    }

        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FingerFKJoints fkj in fingerJoints)
        {
            if (copyAll)
            {
                fkj.angleBase = angleBase;
                fkj.angleJoint = angleJoint;
            }

            UpdateFinger(fkj);
        }
        
    }

    public void Receive(string d, UduinoDevice D)
    {
        Debug.Log(d);
        string[] p = d.Split(' ');
        if(p.Length == 2)
        {
            int a = -1;
            int.TryParse(p[1], out a);
            if( a != -1 )
            {
                if (p[0] == "a") GetFingerByName("Index").inputTarget(a);
                if (p[0] == "b") GetFingerByName("Middle").inputTarget(a);
            }
        }
    }


    void UpdateFinger(FingerFKJoints joint)
    {
        if (!joint.enable) return;
        if (joint.doBase)
        {
            joint.proximal.localEulerAngles = ((joint.baseCopyJoints ? (joint.angleJoint / joint.influenceBase) : (joint.angleBase)) * joint.rotationAxisBase );
            joint.proximal.Rotate(joint.rotationOffsetBase);
        }

        joint.midal.localEulerAngles = (joint.angleJoint * joint.rotationAxis);
        joint.midal.Rotate(joint.rotationOffset + joint.midalRotationOffset);



        joint.distal.localEulerAngles = (joint.angleJoint * joint.rotationAxis);
        joint.distal.Rotate(joint.rotationOffset);
    }
}
