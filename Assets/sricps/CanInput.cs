using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanInput : MonoBehaviour
{

    carcontroller carContoller;
    // Start is called before the first frame update
    void Awake()
    {
        carContoller = GetComponent<carcontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        carContoller.SetInputVector(inputVector);
    }
}
