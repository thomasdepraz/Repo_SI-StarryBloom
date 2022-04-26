using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [Header("Data")]
    public ControllerData controllerData;

    public float playerHeight;
    private Rigidbody rb;
    private float speed;
    private float jumpForce;
    private float rotationSpeed;

    Gamepad gamepad = Gamepad.current;


    public Vector3 targetDirection;
    private Transform self;
    private Transform camTransform;

    //movement variables
    Vector3 characterForward;
    Vector3 rg;
    Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        if (gamepad == null)
            return;

        rb = GetComponent<Rigidbody>();

        //Set variable from scriptable
        speed = controllerData.speed;
        jumpForce = controllerData.jumpForce;
        rotationSpeed = controllerData.rotationSpeed;

        self = transform;
        targetDirection = Vector3.forward;

        camTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Get player forward based on camera;
        characterForward = camTransform.forward;
        characterForward.y = 0;
        characterForward.Normalize();
        rg = new Vector3(characterForward.z, 0, -characterForward.x);
        Debug.DrawLine(self.position, self.position + characterForward * 5, Color.red);
        Debug.DrawLine(self.position, self.position + rg * 5, Color.blue);
    }

    private void OrientPlayer()
    {
    }


    public void Move(InputAction.CallbackContext context)
    {
        Vector2 stick = context.ReadValue<Vector2>();
        direction = characterForward * stick.y + rg * stick.x;
        

        Vector3 velocity = new Vector3(direction.x, rb.velocity.y, direction.z);
        rb.velocity =  velocity * speed;
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        Vector2 stick = context.ReadValue<Vector2>();
        targetDirection = characterForward * stick.y + rg * stick.x;
        self.forward = Vector3.Slerp(self.forward,targetDirection,Time.deltaTime * rotationSpeed);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed)
        {
            if (isGrounded())
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool isGrounded()
    {
        return Physics.Raycast(self.position, -Vector3.up, playerHeight / 2 + 0.1f);
    }
    public void Throw(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Performed)
            Debug.Log("Throw");
    }
}
