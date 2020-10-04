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
        look = (Input.GetAxisRaw("Mouse X") * rotSpeed);

        if (Input.GetKeyDown(KeyCode.LeftAlt)) ToggleCursorLock();

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
