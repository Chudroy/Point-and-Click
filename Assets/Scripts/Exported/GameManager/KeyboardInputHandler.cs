using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputHandler : MonoBehaviour
{
    [SerializeField] bool isTurning;
    [SerializeField] int turnAngleDegree;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleTurnInput();
    }

    void HandleTurnInput()
    {
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
        var camRig = GameManager.Instance.cameraRig;

        isTurning = true;
        camRig.yAxisRotate(byAngleAmount);

        yield return new WaitForSeconds(camRig.getTweenDuration);

        isTurning = false;
    }
}
