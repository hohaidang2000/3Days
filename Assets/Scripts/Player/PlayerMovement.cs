using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    #region Variables

    private PlayerControls controls;
    
    private Vector2 _move;
    private Vector2 _rotate;
    
    private float speed;
    private float jumpStarted;
    
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 10f;

    [SerializeField] private Camera playerCamera;
    
    private Vector3 cameraRotation;

    #endregion


    #region MonoBehaviour

    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }
    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Jump.performed += ctx => Jump();

        controls.Player.Run.performed += ctx => speed = runSpeed;
        controls.Player.Run.canceled += ctx => speed = walkSpeed;

        controls.Player.Move.performed += ctx => _move = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => _move = Vector2.zero;

        controls.Player.Look.performed += ctx => _rotate = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => _rotate = Vector2.zero;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        speed = walkSpeed;
        
        cameraRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        
        jumpStarted = -1f;
        
    }
    private void Update()
    {
        cameraRotation = new Vector3(cameraRotation.x + _rotate.y, cameraRotation.y + _rotate.x, cameraRotation.z);
        playerCamera.transform.eulerAngles = cameraRotation;

        transform.eulerAngles = new Vector3(transform.rotation.x, cameraRotation.y, transform.rotation.z);
        transform.Translate(Vector3.right * Time.deltaTime * _move.x * speed, Space.Self);
        transform.Translate(Vector3.forward * Time.deltaTime * _move.y * speed, Space.Self);

        if (jumpStarted + 0.5f > Time.time)
        {
            transform.Translate((Vector3.up * 8f * Time.deltaTime), Space.Self);
        }
        else if (jumpStarted + 1f > Time.time)
        {
            transform.Translate((Vector3.up * -8f * Time.deltaTime), Space.Self);
        }
    }

    private void FixedUpdate()
    {
        
    }

    #endregion


    #region Methods
    private void Jump()
    {
        if (jumpStarted + 1f < Time.time)
            jumpStarted = Time.time;
    }
    #endregion


}
