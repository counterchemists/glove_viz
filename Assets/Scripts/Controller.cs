using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class Controller : MonoBehaviour
{

    public FingersFK fk;

    public int max = 1000;
    public int min = 0;

    public int from = 0;
    public int to = 90;

    public float target = 0;
    public float smoothTarget = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        smoothTarget = Mathf.Lerp(smoothTarget, target, Time.deltaTime * 2);
        Debug.Log(target);

        float m = Remap(smoothTarget, min, max, from, to);
        Debug.Log(m);
        fk.GetFingerByName("Index").angleJoint = m;
    }

    public void up(string data, UduinoDevice d)
    {
        int i = -1;

        int.TryParse(data, out i);
        if (i < 300) return;

        if (i != -1)
            target = (float)i;

    }

    public  float Remap(float value, float from1, float to1, float from2, float to2)
    {
        if (value < from1) value = from1;
        if (value > to1) value = to1;

        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
