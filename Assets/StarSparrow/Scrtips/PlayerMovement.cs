using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float forceMagnitude;
    [SerializeField] float maxVelocity;

    private Rigidbody rb;
    private Camera mainCamera;

    private Vector3 movementDirection;
    
    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        if(Touchscreen.current.primaryTouch.press.isPressed)
        {
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        movementDirection = transform.position - worldPosition;
        movementDirection.z = 0;
        movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }
    private void FixedUpdate()
    {
        if(movementDirection == Vector3.zero) { return; }
        rb.AddForce(movementDirection * forceMagnitude, ForceMode.Force);
    }
}
