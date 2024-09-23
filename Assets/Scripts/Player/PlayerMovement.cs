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

    private PlayerInput playerInput;
    private InputAction moveController;
    private CharacterController cc;
    private Vector3 MoveVector;
    private Vector2 testVector;

    void OnEnable()
    {
        cc = GetComponent<CharacterController>();

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
        MoveVector.y = 0;
    }

    void UpdateRotation()
    {
        if (MoveVector.x != 0 || MoveVector.z != 0)
        {
            transform.LookAt(transform.position + MoveVector);
        }
    }

}