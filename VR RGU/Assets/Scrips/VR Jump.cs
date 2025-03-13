using UnityEngine;
using UnityEngine.XR;

public class VRJump : MonoBehaviour
{
    public float jumpHeight = 3f; // Height of the jump
    public LayerMask groundLayer; // This makes the ground layer visible in the Inspector
    private bool isGrounded;
    private Rigidbody rb;

    private InputDevice rightController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Get the right controller for input detection
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        // Check if the player is grounded (on the floor)
        isGrounded = IsGrounded();

        // Detect if the jump button (e.g., the A button) is pressed
        if (isGrounded && IsJumpButtonPressed())
        {
            Jump();
        }
    }

    // Function to check if the player is grounded
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1f, groundLayer);
    }

    // Function to check if the jump button (primary button on the right controller) is pressed
    private bool IsJumpButtonPressed()
    {
        bool isPressed = false;
        rightController.TryGetFeatureValue(CommonUsages.primaryButton, out isPressed);
        return isPressed;
    }

    // Function to apply the jump force
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), rb.velocity.z);
    }
}
