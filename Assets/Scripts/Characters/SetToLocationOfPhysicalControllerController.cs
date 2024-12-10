using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

/****************************************************************
 * Attached to: hand 
 * Purpose: make the hand be where the controle is in real life
 * Author: Seamus 
 * Version: 1.0
 ****************************************************************/
public class SetToLocationOfPhysicalControllerController : MonoBehaviour
{
    private TusInputAction controllerLocation;
    private TusInputAction controllerRotation;
    [SerializeField] GameObject xrRig;

    private void Awake() 
    {
        controllerLocation = new TusInputAction();
        controllerRotation = new TusInputAction();
    }

    private void Start() 
    {
        controllerLocation.Enable();
        controllerRotation.Enable();

        controllerLocation.Brush.RightHandLocation.performed += ctx => HandleLocation(ctx.ReadValue<Vector3>());
        controllerRotation.Brush.RightHandRotation.performed += ctx => HandleRotation(ctx.ReadValue<Quaternion>());

    }

    private void HandleLocation(Vector3 location)
    {
        this.transform.position = location + xrRig.transform.position;
    }
   
    private void HandleRotation(quaternion rotation)
    {
        this.transform.rotation = rotation * xrRig.transform.rotation;
    }
}
