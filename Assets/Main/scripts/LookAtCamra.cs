using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamra : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
    }
    [SerializeField] private Mode mode;
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.LookAtInverted:
               transform.forward=- Camera.main.transform.forward;
                break;

        }
    }
}
