using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundEffects : MonoBehaviour
{


    [Header("ÄÄNI LÄHTEET")]
    public AudioSource tireScreeching;
    public AudioSource engine;
    public AudioSource carHit;

    float desiredEnginePitch = 0.5f;
    float tireScreechPitch = 0.5f;

    carcontroller CarController;

    // Start is called before the first frame update
    void Awake()
    {
        CarController= GetComponentInParent<carcontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSFX();
        UpdateTireScreeching();

    }

    

    void UpdateEngineSFX()
    {
        float velocityMagnitude = CarController.GetVelocityMagnitude();
        float desiredEngineVolume = velocityMagnitude * 0.05f;
        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume,0.2f, 1.0f);

        engine.volume = Mathf.Lerp(engine.volume, desiredEngineVolume, Time.deltaTime * 10);

        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2.0f);
        engine.pitch = Mathf.Lerp(engine.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);

    }
    void UpdateTireScreeching()
    {
        if (CarController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking)
            {
                tireScreeching.volume = Mathf.Lerp(tireScreeching.volume, 1.0f, Time.deltaTime * 10);
                tireScreechPitch = Mathf.Lerp(tireScreechPitch, 0.5f, Time.deltaTime * 10f);

            }
            else
            {
                tireScreeching.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                tireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }
        else tireScreeching.volume = Mathf.Lerp(tireScreeching.volume, 0, Time.deltaTime * 10);
    
    }

}
