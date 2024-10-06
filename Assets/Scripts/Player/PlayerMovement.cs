using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 2.0f;
    public float RotationSpeed = 7;

    private PlayerInput playerInput;
    private InputAction moveController;
    private CharacterController cc;
    private Vector3 MoveVector;
    private Vector2 testVector;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    void OnEnable()
    {
        cc = GetComponent<CharacterController>();
        initialRotation = this.gameObject.transform.rotation;
        playerInput = GetComponent<PlayerInput>();
        playerInput.ActivateInput();

        moveController = playerInput.actions["Move"];
        moveController.Enable();
    }

    private void Update()
    {
        UpdateMovement();
        UpdateRotation();
        cc.Move(MoveVector * MoveSpeed);
        // Draw ray in players current forward rotation
        Debug.DrawRay(this.transform.position, this.transform.forward * 3f,Color.green, 0);
    }

    void UpdateMovement()
    {
        Vector3 playerInputVector = moveController.ReadValue<Vector2>();
        MoveVector.x = playerInputVector.x;
        MoveVector.z = playerInputVector.y;
        // Apply Gravity only if player is not on ground
        if (!cc.isGrounded)
        {
            MoveVector.y += (Globals.Gravity * Time.deltaTime);
        }
    }

    void UpdateRotation()
    {
        if (MoveVector.x != 0 || MoveVector.z != 0)
        {
            this.targetRotation= Quaternion.LookRotation(MoveVector, Vector3.up);
            // Lerps towards target rotation so that the player rotates smoothly
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, this.targetRotation, RotationSpeed * 10);
            this.transform.rotation = Quaternion.Euler(initialRotation.eulerAngles.x, transform.rotation.eulerAngles.y, initialRotation.eulerAngles.z);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // Draws the target rotation as a point in space 
        Vector3 targetPosLocal = this.targetRotation * Vector3.forward;
        targetPosLocal.y = 0;
        Gizmos.DrawSphere(this.transform.position + (targetPosLocal * 30f), .5f);
    }
}