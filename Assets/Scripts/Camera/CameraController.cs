using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Row.Utils;

/// <summary>
/// Simulation / Startegy game camera that handles movement and rotation
/// </summary>
/// 
public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    //Standard movement speed in the xy plane
    public float normalMovementSpeed = 1;
    //Quickened movement speed in the xy plane
    public float fastMovementSpeed = 2f;
    //Evaluated movement speed value
    private float movementSpeed;
    //The lerp value to smooth all movement be it rotation or otherwise
    public float movementTime = 5;
    //Rotation speed
    public float rotationSpeed = 1;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    [Range(-90f, 90f)][SerializeField] float yRotationUpperLimit = 70f;
    [Range(-90f, 90f)][SerializeField] float yRotationLowerLimit = 10f;
    // Zoom speed / forward movements one and the same
    public float zoomSpeed = 3;

    //The Updated positions
    private Vector3 newPosition;
    private Quaternion newRotation;
    public Vector3 newZoom;

    //The input from the horizontal and vertical axis
    private Vector2 movementInput;

    //Positions from using the mouse to move
    private Vector3 rotateStartPosition;
    private Vector3 rotateCurrentPositon;
    private Vector3 startDragPosition;
    private Vector3 dragCurrentPosition;
    Vector2 rotationAxis;
    Vector2 rotation = Vector2.zero;

    Vector3 totalDifference = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
        rotation.y = 45;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
        HandleMovementInput();
        UpdateCamera();
    }

    void UpdateCamera()
    {
        //calculate new positions and rotations
        newPosition += (Utils.ProjectDirectionOnPlane(transform.forward, Vector3.up).normalized * movementSpeed * movementInput.y);
        newPosition += (Utils.ProjectDirectionOnPlane(transform.right, Vector3.up).normalized * movementSpeed * movementInput.x);
        newPosition = ValidatePosition(newPosition);
        rotation += rotationAxis * rotationSpeed;
        rotation.y = Mathf.Clamp(rotation.y, yRotationLowerLimit, yRotationUpperLimit);
        Quaternion xRotation = Quaternion.AngleAxis(rotation.x, Vector3.up);
        Quaternion yRotation = Quaternion.AngleAxis(rotation.y, Vector3.right);
        //newRotation *= Quaternion.AngleAxis(rotationAxis.x * rotationSpeed,Vector3.up);
        newRotation = xRotation * yRotation;

        //Update the relevant transforms
        transform.localRotation = newRotation;
        transform.position = Vector3.Lerp(transform.position, newPosition, movementTime * Time.deltaTime);

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }


    //Handle the input from the mouse
    void HandleMouseInput()
    {
        Cursor.visible = Input.GetMouseButton(1);
        //Scroll wheel is used to zoom
        if (Input.mouseScrollDelta.y != 0)
        {
            Vector3 dir = -cameraTransform.localPosition;
            newZoom += dir.normalized * Input.mouseScrollDelta.y * zoomSpeed;
            if (newZoom.z > -5)
            {
                newZoom.z = -5;
            }

            if (newZoom.z < -80)
            {
                newZoom.z = -80;
            }
        }

        //The right mouse button can be used to rotate the screen
        if (Input.GetMouseButtonDown(1))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            rotateCurrentPositon = Input.mousePosition;
            Vector3 difference = rotateStartPosition - rotateCurrentPositon;
            rotateStartPosition = rotateCurrentPositon;

            totalDifference += difference;

        }

        Vector3 curDifference = Vector3.Lerp(Vector3.zero, totalDifference, Time.deltaTime * movementTime); 

        rotationAxis.x = -curDifference.x / 5f;
        rotationAxis.y = curDifference.y / 5f;

        totalDifference -= curDifference;

        if (Input.GetMouseButtonUp(1))
        {
            rotationAxis = Vector3.zero;
        }
        //the Middle mouse button can be used to drag the camera 
        if (Input.GetMouseButtonDown(2))
        {
            startDragPosition = Utils.GetWorldPosAtY();
        }

        if (Input.GetMouseButton(2))
        {
            dragCurrentPosition = Utils.GetWorldPosAtY();

            newPosition = transform.position + startDragPosition - dragCurrentPosition;
        }
    }

    void HandleMovementInput()
    {
        //see if the user wants to move quickly
        movementSpeed = normalMovementSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastMovementSpeed;
        }
        //read in values from the input axis
        movementInput.y = Input.GetAxis("Vertical");
        movementInput.x = Input.GetAxis("Horizontal");

    }


    private Vector3 ValidatePosition(Vector3 position)
    {;
        return position;
    }
}