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
        
    }

    void UpdateMovement()
    {
        Vector3 playerInputVector = moveController.ReadValue<Vector2>();
        print(playerInputVector);
        MoveVector.x = playerInputVector.x;
        MoveVector.z = playerInputVector.y;
        if (!cc.isGrounded)
        {
            MoveVector.y -= .98f;
        }
    }

    void UpdateRotation()
    {
        if (MoveVector.x != 0 || MoveVector.z != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(MoveVector, Vector3.up);

            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, RotationSpeed * 100);
            this.transform.rotation = Quaternion.Euler(initialRotation.eulerAngles.x, transform.rotation.eulerAngles.y, initialRotation.eulerAngles.z);

        }
    }
}