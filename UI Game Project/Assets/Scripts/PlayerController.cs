using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum ActionType
    {
        MOVE_FORWARD,
        ROTATE_LEFT,
        ROTATE_RIGHT,
        TOGGLE_LIGHT,
        JUMP,
        FUNC1,
        FUNC2
    }

    public float lerpSpeed = 1.0f;
    Vector3 currentPosition;
    Vector3 targetPosition;
    float currentYRotation;
    float targetYRotation;
    float lerpTime = 0.0f;
    public List<ActionType> actions = new List<ActionType>();
    public int currentActionIndex = 0;
    GameObject currentPlatform;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
        targetPosition = transform.position;
        currentYRotation = transform.rotation.eulerAngles.y;
        targetYRotation = transform.rotation.eulerAngles.y;
        SnapToPlatform();
    }

    // Update is called once per frame
    void Update()
    {
        ActionController();

        TempKeyboardController();
        lerpTime += Time.deltaTime * lerpSpeed;
        if (lerpTime > 1.1f)
        {
            lerpTime = 1.1f;
            currentPosition = targetPosition;
            currentYRotation = targetYRotation;
            SnapToPlatform();
        }
        transform.position = Vector3.Lerp(currentPosition, targetPosition, lerpTime);
        transform.rotation = Quaternion.Euler(0, Mathf.Lerp(currentYRotation, targetYRotation, lerpTime), 0);
    }

    public void MoveForward()
    {
        targetPosition = currentPosition + transform.forward;
        lerpTime = 0.0f;
    }

    public void RotateCW()
    {
        targetYRotation += 90.0f;
        lerpTime = 0.0f;
    }

    public void RotateCCW()
    {
        targetYRotation -= 90.0f;
        lerpTime = 0.0f;
    }

    public void Jump()
    {
        targetPosition = currentPosition + transform.forward + transform.up;
        lerpTime = 0.0f;
    }

    void TempKeyboardController()
    {
        if (CanPreformAction() == false)
            return;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveForward();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RotateCCW();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RotateCW();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ToggleLight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void ActionController()
    {
        if (CanPreformAction() == false)
            return;
        if (currentActionIndex >= actions.Count)
            return;
        var action = actions[currentActionIndex];
        currentActionIndex += 1;
        switch (action)
        {
            case ActionType.MOVE_FORWARD: MoveForward(); break;
            case ActionType.ROTATE_LEFT: RotateCCW(); break;
            case ActionType.ROTATE_RIGHT: RotateCW(); break;
            case ActionType.TOGGLE_LIGHT: ToggleLight(); break;
            case ActionType.JUMP: Jump(); break;
        }
    }

    bool CanPreformAction()
    {
        return lerpTime == 1.1f;
    }

    void SnapToPlatform()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            var hitPlatform = hit.collider.gameObject;
            if (hitPlatform != currentPlatform)
            {
                currentPlatform = hitPlatform;
                targetPosition = hit.point + (Vector3.up * 0.01f);
            }
        }
    }

    void ToggleLight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            var platform = hit.collider.gameObject.GetComponentInParent<PlatformController>();
            if (platform)
            {
                platform.ToggleLight();
                lerpTime = 0;
            }
        }
    }
}