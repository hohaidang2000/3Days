using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    #region Variables

    private PlayerControls controls;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    private Vector2 _move;
    private Vector2 _rotate;
    private Vector3 velocity;
    private bool isGround;
    private float speed;
    private float gravity = -20f;
    private float grounDistance = 0.1f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpHeight = 5f;
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
        
    }
    private void Update()
    {
        CameraRotate();
        Move();
        IsGround();
    }

    private void FixedUpdate()
    {
        
    }

    #endregion


    #region Methods
    private void Jump()
    {
        if (isGround){
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        isGround = false;
        }
    }
    private void CameraRotate(){
        cameraRotation = new Vector3(cameraRotation.x + _rotate.y, cameraRotation.y + _rotate.x, cameraRotation.z);
        if (cameraRotation.x >= 90)
            cameraRotation.x=90f;
        if (cameraRotation.x <= -90)
            cameraRotation.x=-90f;    
        playerCamera.transform.eulerAngles = cameraRotation;
        transform.eulerAngles = new Vector3(transform.rotation.x, cameraRotation.y, transform.rotation.z);
        
    }
    private void Move()
    {
        Vector3 _temp = transform.right * _move.x +transform.forward * _move.y;
        controller.Move(_temp * speed * Time.deltaTime);
    }
    void IsGround(){
        isGround = Physics.CheckSphere(groundCheck.position, grounDistance, groundMask);
        
        if (isGround && velocity.y < 0 ) 
        {
            velocity.y= -2f;
        }
        else 
        {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        }
    }
    #endregion


}
