using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrail : MonoBehaviour
{
    carcontroller carController;
    TrailRenderer trailrenderer;
    // Start is called before the first frame update
    void Awake()
    {
        carController = GetComponentInParent<carcontroller>();
        trailrenderer = GetComponent<TrailRenderer>();
        trailrenderer.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
            trailrenderer.emitting = true;
        else trailrenderer.emitting = false;      
                }
}
