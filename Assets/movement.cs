using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private Transform cameraTransform; // drag your child Camera here in the Inspector

    private Rigidbody rb;

    private float horizontalInput;
    private float verticalInput;

    // Tracks up/down look angle separately since it needs to be clamped
    // (you can't just read the camera's current rotation back out reliably).
    private float pitch = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //lock and hide the cursor so it doesn't drift off-screen while looking around
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        horizontalInput = 0;
        verticalInput = 0;
        
        if (Keyboard.current.aKey.isPressed)
        {
            horizontalInput = -1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            horizontalInput = 1;
        }
        if (Keyboard.current.wKey.isPressed)
        {
            verticalInput = 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            verticalInput = -1;
        }

        // --- Mouse look ---
        // TODO 1: read the mouse movement this frame into a Vector2
        Vector2 mousePos = Mouse.current.delta.ReadValue();

        // TODO 2: rotate the player body left/right (yaw) using the mouse's x delta * mouseSensitivity
        transform.Rotate(Vector3.up * mousePos.x * mouseSensitivity);

        // TODO 3: add the mouse's y delta * mouseSensitivity to `pitch`,
        // then clamp `pitch` between -80 and 80 degrees so you can't flip the camera upside down
        pitch += mousePos.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -80, 80);

        // TODO 4: apply `pitch` as the camera's local rotation around the X axis
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void FixedUpdate()
    {
        // Step 1: build the direction from input
    Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);

    // Step 2: grab the Rigidbody's current y velocity so we don't overwrite gravity/jumping
    float currentYVelocity = rb.linearVelocity.y;

    // Step 3: combine horizontal movement + preserved y velocity
    rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, currentYVelocity, moveDirection.z * moveSpeed);
    }
}
