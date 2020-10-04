using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField]
    private float moveSpeed = 50;
    [SerializeField]
    private float sensitivity;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float rotSpeed = 5f;


    private float horizontal;
    private float forward;
    private float look;


    // shooting
    public GameObject projectile;
    public Transform staffTip;
    private bool tryFire1;
    public float projectileForce = 20f;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        GameManager.Instance.onResetPlayer += onResetPlayer;
    }
    void Update()
    {
        look = (Input.GetAxisRaw("Mouse X") * rotSpeed * sensitivity);

        if (Input.GetKeyDown(KeyCode.LeftShift)) ToggleCursorLock();
        if (Input.GetKeyDown(KeyCode.Alpha1)) sensitivity = 0.25f;
        if (Input.GetKeyDown(KeyCode.Alpha2)) sensitivity = 0.5f;
        if (Input.GetKeyDown(KeyCode.Alpha3)) sensitivity = 1f;
        if (Input.GetKeyDown(KeyCode.Alpha4)) sensitivity = 2f;
        if (Input.GetKeyDown(KeyCode.Alpha5)) sensitivity = 4f;

        if (Input.GetButtonDown("Fire1"))
        {
            tryFire1 = true;
        }

    }


    private void FixedUpdate()
    {
        player.Rotate(0, look * Time.fixedDeltaTime, 0, Space.Self);



        forward = Input.GetAxis("Vertical");
        if (forward != 0)
        {
            characterController.SimpleMove(moveSpeed * transform.forward * Time.fixedDeltaTime * forward);
        }
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal !=0)
        {
            characterController.SimpleMove(moveSpeed * transform.right * Time.fixedDeltaTime * horizontal);
        }

        // shooting
        if (tryFire1)
        {
            GameObject bullet = Instantiate(projectile, staffTip.position, staffTip.rotation);
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.AddForce(staffTip.forward * projectileForce, ForceMode.Impulse);
            tryFire1 = false;
        }
    }

    private void ToggleCursorLock()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void onResetPlayer()
    {
        characterController.enabled = false;
        characterController.transform.position = GameManager.Instance.playerRespawnLocation.position;
        characterController.transform.rotation = GameManager.Instance.playerRespawnLocation.rotation;

        characterController.enabled = true;

    }

    private void OnDisable()
    {
        GameManager.Instance.onResetPlayer -= onResetPlayer;
    }
}
