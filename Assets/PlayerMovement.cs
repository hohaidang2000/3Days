using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    MyInputActions inputActions;
    Vector2 move;
    Vector2 rotate;
    float run;
    float jumpStarted;
    public float walkSpeed = 3f;
    public float runSpeed = 10f;
    public Camera playerCamera;
    Vector3 cameraRotation;
    // Start is called before the first frame update
    private void OnEnable()
    {
        inputActions.Player.Enable();
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();
    }
    private void Awake()
    {
        inputActions = new MyInputActions();
        inputActions.Player.Jump.performed += ctx => Jump();

        inputActions.Player.Run.performed += ctx => run = runSpeed;
        inputActions.Player.Run.canceled += ctx => run = walkSpeed;

        inputActions.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => move = Vector2.zero;

        inputActions.Player.Look.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => rotate = Vector2.zero;
    }
    private void Jump()
    {
        if (jumpStarted + 1f < Time.time)
            jumpStarted = Time.time;
    }
    // Update is called once per frame
    void Start()
    {
        run = walkSpeed;
        cameraRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        jumpStarted = -1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        cameraRotation = new Vector3(cameraRotation.x + rotate.y, cameraRotation.y + rotate.x, cameraRotation.z);
        playerCamera.transform.eulerAngles = cameraRotation;
        transform.eulerAngles = new Vector3(transform.rotation.x, cameraRotation.y, transform.rotation.z);
        transform.Translate(Vector3.right * Time.deltaTime * move.x * run, Space.Self);
        transform.Translate(Vector3.forward * Time.deltaTime * move.y * run, Space.Self);
        if (jumpStarted + 0.5f > Time.time)
        {
            transform.Translate((Vector3.up * 8f * Time.deltaTime), Space.Self);
        }
        else if (jumpStarted + 1f > Time.time)
        {
            transform.Translate((Vector3.up * -8f * Time.deltaTime), Space.Self);
        }
    }
}
