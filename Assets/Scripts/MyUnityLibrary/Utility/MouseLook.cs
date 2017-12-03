using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    
    private float rotationY = 0f;
    private float rotationX = 0f;
    private Vector3 targetRot;

    private bool isActive = true;
    private bool isCursorLocked = true;

    public Transform cameraRigTransform;
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float lerpSpeed = 10f;
    public float sensitivityX = 10f;
    public float sensitivityY = 10f;
    public float minimumX = -360f;
    public float maximumX = 360f;
    public float minimumY = -60f;
    public float maximumY = 60f;
    public bool lockCursor;
    public KeyCode togleKey = KeyCode.Slash;

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(togleKey))
        {
            isActive = !isActive;
            cameraRigTransform.rotation = Quaternion.identity;
        }

        if (!isActive)
            return;

        switch(axes)
        {
            case RotationAxes.MouseXAndY:
                {
                    SetRotationX();
                    SetRotationY();
                }
                break;
            case RotationAxes.MouseX:
                {
                    SetRotationX();
                }
                break;
            case RotationAxes.MouseY:
                {
                    SetRotationY();
                }
                break;
        }

        UpdateRotation();
        UpdateCursorLock();
    }
#endif

    private void SetRotationX()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
    }

    private void SetRotationY()
    {
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
    }

    private void UpdateRotation()
    {
        targetRot = new Vector3(-rotationY, rotationX, 0f);
        cameraRigTransform.rotation = Quaternion.Slerp(cameraRigTransform.rotation, Quaternion.Euler(targetRot), Time.deltaTime * lerpSpeed);
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isCursorLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isCursorLocked = true;
        }

        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

