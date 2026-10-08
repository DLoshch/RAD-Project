using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputs playerInputs;
    public CharacterController characterController;
    public GameObject playerCamera;
    public float moveSpeed = 5f;

    public float lookSensitivity = 10f;
    public float yRotation = 0;
    public float xRotation = 0;

    [SerializeField]
    private Vector2 moveInput;
    [SerializeField]
    private Vector2 lookInput;

    public GameObject fireEffect;
    public GameObject firePoint;
    public Animator animator;

    void Start()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Enable();
        playerInputs.Player.Enable();
        


        playerInputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        playerInputs.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerInputs.Player.Look.canceled += ctx => lookInput = Vector2.zero;
        playerInputs.Player.LMB.performed += ctx => Click();
    }

    void Update()
    {
        Look();
        Move();
<<<<<<< HEAD
=======
        InteractRaycast();
>>>>>>> 2a5537990277d9f569a2c2d07bc614b8ad3215ac
    }

    public void Move()
    {
        float gravity = -9.81f;
        Vector3 move = new Vector3(moveInput.x * Time.deltaTime * moveSpeed, gravity * Time.deltaTime, moveInput.y * Time.deltaTime * moveSpeed);
        move = transform.TransformDirection(move);
        characterController.Move(move);
    }

    public void Look()
    {
        yRotation += lookInput.x * Time.deltaTime * lookSensitivity;
        Quaternion playerRotationY = Quaternion.Euler(0, yRotation, 0);
        transform.rotation = playerRotationY;

        xRotation += -lookInput.y * Time.deltaTime * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        Quaternion cameraRotationX = Quaternion.Euler(xRotation, transform.eulerAngles.y, 0);
        playerCamera.transform.rotation = cameraRotationX;
    }

    public void InteractRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCamera.transform.position, playerCamera.transform.forward, 100f);
        if (hit.collider != null)
        {
<<<<<<< HEAD
            if (hit.collider.gameObject.GetComponent<EnemyMovement>())
            {
                Destroy(hit.collider.gameObject);
            }
=======
            Debug.Log(hit.collider.gameObject.name);
>>>>>>> 2a5537990277d9f569a2c2d07bc614b8ad3215ac
        }
    }

    public void Click()
    {
<<<<<<< HEAD
        InteractRaycast();
=======
>>>>>>> 2a5537990277d9f569a2c2d07bc614b8ad3215ac
        fireEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("Fire");
    }
}
