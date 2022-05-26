using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputHandler : MonoBehaviour
{
    [SerializeField] bool isTurning;
    [SerializeField] int turnAngleDegree;
    GameObject core;
    CameraRig cameraRig;
    bool canTurn = true;

    private void Awake()
    {
        cameraRig = GameObject.FindGameObjectWithTag("Core").GetComponentInChildren<CameraRig>();
    }

    private void OnEnable()
    {
        Computer.SetPlayKeyboardInput += CanTurn;
    }

    private void OnDisable()
    {
        Computer.SetPlayKeyboardInput -= CanTurn;
    }

    // Update is called once per frame
    void Update()
    {
        HandleTurnInput();
    }

    void CanTurn(bool t)
    {
        canTurn = t;
    }

    void HandleTurnInput()
    {
        if (!canTurn) return;

        if (Input.GetKeyDown(KeyCode.Q) && !isTurning)
        {
            StartCoroutine(Turn(-turnAngleDegree));
        }
        else if (Input.GetKeyDown(KeyCode.E) && !isTurning)
        {
            StartCoroutine(Turn(turnAngleDegree));
        }
    }

    IEnumerator Turn(int byAngleAmount)
    {
        isTurning = true;
        cameraRig.yAxisRotate(byAngleAmount);

        yield return new WaitForSeconds(cameraRig.getTweenDuration);

        isTurning = false;
    }
}
