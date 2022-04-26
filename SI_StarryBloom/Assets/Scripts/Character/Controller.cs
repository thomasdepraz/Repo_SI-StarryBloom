using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    //[Header("References")]
    public KnightTower controlledTower;

    private Rigidbody _rb;
    public Rigidbody rb 
    {
        get => _rb;

        set 
        {
            _rb = value;
            self = _rb.gameObject.transform;
        } 
    }

    [Header("Data")]
    public ControllerData controllerData;

    //Data
    public float playerHeight;
    private float speed;
    private float jumpForce;
    private float rotationSpeed;


    //movement variables
    private Transform camTransform;
    private Transform self;
    Vector3 characterForward;
    Vector3 rg;
    Vector3 direction;
    Vector3 targetDirection;
    float verticalSpeed;


    // Start is called before the first frame update
    void Start()
    {
        //Set variable from scriptable
        speed = controllerData.speed;
        jumpForce = controllerData.jumpForce;
        rotationSpeed = controllerData.rotationSpeed;

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
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 stick = context.ReadValue<Vector2>();
        direction = characterForward * stick.y + rg * stick.x;
        

        Vector3 velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
        rb.velocity =  velocity;
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        Vector2 stick = context.ReadValue<Vector2>();
        targetDirection = characterForward * stick.y + rg * stick.x;
        self.forward = Vector3.Slerp(self.forward,targetDirection,Time.deltaTime * rotationSpeed);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (rb == null)
          return;

        if (context.action.phase == InputActionPhase.Performed)
        {
            if (isGrounded())
            {
                Debug.Log("Jump");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private bool isGrounded()
    {
        return Physics.Raycast(self.position + Vector3.up * 0.1f, -Vector3.up, 0.2f); ;
    }

    public bool IsFalling()
    {
        return !isGrounded() && rb.velocity.y < 0;
    }
    public void Throw(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Performed)
        {
            controlledTower.ThrowWeapon(targetDirection);
        }
    }
}
