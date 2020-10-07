using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // variables
    public GameObject camera;
    public GameObject bulletSpawnPoint;
    public float waitTime = 0.0f;
    public GameObject playerObj;
    public GameObject bullet;
    public float points = 0;
    public float gravity = 20.0F;

    //movement
    private Rigidbody rb;
    private CharacterController controller;
    public float curSpeed = 5f;
    public float movementSpeed = 5f;

    // joysticks
    public Joystick moveJoystick;
    public Joystick lookJoystick;

    // tests
    private Vector3 moveDirection = Vector3.zero;
    private bool moveForward = false;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool moveBack = false;


    // methods

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.freezeRotation = true;

        controller = GetComponent<CharacterController>();
    }

    void UpdateLookJoystick()
    {
        float hoz = lookJoystick.Horizontal;
        float ver = lookJoystick.Vertical;
        Vector3 direction = new Vector3(lookJoystick.Horizontal, 0, lookJoystick.Vertical).normalized;
        Vector3 lookAtPosition = transform.position + direction;
        transform.LookAt(lookAtPosition);


    }

    private void Update()
    {
        // player facing mouse
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        /* more face mouse
        if (playerPlane.Raycast(ray, out hitDist)) {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);
        }
        */

        Vector3 direction = new Vector3(lookJoystick.Horizontal, 0, lookJoystick.Vertical).normalized;
        Vector3 lookAtPosition = transform.position + direction;
        Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
        transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);

        // move forwards
        if (Input.GetKey(KeyCode.W)) {
            moveForward = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveLeft = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveRight = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveBack = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            moveForward = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            moveLeft = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            moveRight = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            moveBack = false;
        }

        // standard assets input manager
        //Vector3 move = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));
        Vector3 move = new Vector3(moveJoystick.Horizontal, 0, moveJoystick.Vertical).normalized;
        controller.Move(move * Time.deltaTime * movementSpeed);


        // shoot
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log("Shoot!");
        Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
    }
}
