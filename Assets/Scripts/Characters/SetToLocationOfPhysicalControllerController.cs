using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

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
        controllerRotation.Brush.RightHandRotation.performed += ctx => HandleRotation(ctx.ReadValue<quaternion>());

    }

    private void HandleLocation(Vector3 Location)
    {
        this.transform.position = Location + xrRig.transform.position;
    }
    private void HandleRotation(Quaternion rotation)
    {
        this.transform.rotation = rotation * xrRig.transform.rotation;
    }
}
