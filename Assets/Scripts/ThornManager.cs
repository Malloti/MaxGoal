using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornManager : MonoBehaviour
{
    private SliderJoint2D thorn;
    private JointMotor2D aux;

    // Start is called before the first frame update
    void Start()
    {
        thorn = GetComponent<SliderJoint2D>();
        aux = thorn.motor;
    }

    // Update is called once per frame
    void Update()
    {
        if (thorn.limitState == JointLimitState2D.UpperLimit) {
            aux.motorSpeed = Random.Range(-1, -5);
            thorn.motor = aux;
        }

        if (thorn.limitState == JointLimitState2D.LowerLimit) {
            aux.motorSpeed = Random.Range(1, 5);
            thorn.motor = aux;
        }
    }
}
