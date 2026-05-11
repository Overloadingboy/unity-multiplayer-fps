using Mirror;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float airDrag = 2f;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    [Header("References")]
    private Rigidbody rb;
    private Camera mainCamera;
    private Transform cameraHolder;

    [Header("Look")]
    [SerializeField] private float mouseSensitivity = 2f;
    private float xRotation = 0f;

    private Vector3 moveDirection;
    private float currentSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraHolder = transform.Find("Camera");

        if (!isLocalPlayer) return;
        mainCamera = cameraHolder.GetComponent<Camera>();
        if (mainCamera != null)
        {
            mainCamera.enabled = true;
        }
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        HandleInput();
        HandleLook();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        Move();
    }

    private void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (cameraHolder != null)
        {
            cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    private void Move()
    {
        rb.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > currentSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * currentSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        rb.drag = isGrounded ? groundDrag : airDrag;
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
