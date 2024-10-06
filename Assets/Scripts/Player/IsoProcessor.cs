using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class IsoProcessor : InputProcessor<Vector2>
{
    [Header("X Rotation of camera")]
    public float AngleX = 30f;
    [Header("X Rotation of camera")]
    public float AngleY = 45f;
    private Quaternion camRot;

#if UNITY_EDITOR
    static IsoProcessor()
    {
        Initialize();
    }
#endif

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        InputSystem.RegisterProcessor<IsoProcessor>();
    }


    public override Vector2 Process(Vector2 value, InputControl control)
    {
        if (camRot == null)
        {
            camRot = Quaternion.Euler(AngleX, AngleY, 0f);
        }

        Vector3 output3 = new Vector3(value.x, 0f, value.y);
        output3 = camRot * output3;
        Vector2 output = new Vector2(output3.x, output3.z);
        output.Normalize();
        return output;
    }
}

