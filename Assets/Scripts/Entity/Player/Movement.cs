using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� ������ ������
/// <author>�����</author>
/// </summary>
public class Movement : MonoBehaviour
{
    [Header("# Movement")]
    public float speed;
    public float moveSpeed;
    public float runSpeed;
    public float useStamina;

    [Header("# Look")]
    public Transform cameraContainer;
    public Camera cam;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot; // ī�޶� ȸ��
    [HideInInspector]public float lookSensitivity; // ī�޶� ���� // ����, ���� �� ������ ���̴� ���� ����.
    private Vector2 mouseDelta; // input ������ �޾ƿ��� ���콺 delta ��

    [HideInInspector]
    public bool isRun = false;
    [HideInInspector]
    public bool canLook = true;

    private Rigidbody movementRigidbody;
    private FootStepsSound footStepsSound;

    public GameObject flashlight = null;
    public GameObject menuObject;
    public GameObject optionObject;

    Vector2 movementDir = Vector2.zero;

    private void Awake()
    {
        speed = moveSpeed;
        cam = UnityEngine.Camera.main;
        movementRigidbody = GetComponent<Rigidbody>();
        footStepsSound = GetComponent<FootStepsSound>();
    }

    private void Start()
    {
        CharacterManager.Instance.Player.controller.OnMoveEvent += Move;
        CharacterManager.Instance.Player.controller.OnInventoryEvent += UseInventory;
        CharacterManager.Instance.Player.controller.OnRunEvent += Run;
        CharacterManager.Instance.Player.controller.OnLookEvent += Camera;

        CharacterManager.Instance.Player.controller.OnMenuEvent += OpneMenu; // ���� �޴� ����
        CharacterManager.Instance.Player.controller.OnMenuEvent += ToggleCursor; // ���� �޴� ���½� ���콺 �¿���

    }

    private void FixedUpdate()
    {
        if (isRun)
        {
            if (CharacterManager.Instance.Player.condition.UseStamina(useStamina))
            {
                footStepsSound.SetRunning(true);
                speed = runSpeed;                
            }
            else
            {
                footStepsSound.SetRunning(false);
                speed = moveSpeed;                
            }
        }
        else
        {
            footStepsSound.SetRunning(false);
            speed = moveSpeed;            
        }

        ApplyMovement(movementDir);
    }

    private void LateUpdate()
    {
        if(canLook)
        {
            CameraLook();
        }
    }

    private void Move(Vector2 direction)
    {
        movementDir = direction;
    }

    private void ApplyMovement(Vector2 direction)
    {
        Vector3 dir = transform.forward * direction.y + transform.right * direction.x;
        dir *= speed;
        dir.y = movementRigidbody.velocity.y;

        movementRigidbody.velocity = dir;
    }

    private void Run(float value)
    {
        if(isRun == false)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }
    }

    private void UseInventory()
    {
        ToggleCursor();
    }

    public void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    private void Camera(Vector2 vector)
    {
        mouseDelta = vector;
    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * GameManager.Instance.GamelookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cam.transform.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        if(flashlight != null)
        {
            flashlight.transform.localEulerAngles = new Vector3(camCurXRot - 90, 180, 0);
        }

        transform.eulerAngles += new Vector3(0, mouseDelta.x * GameManager.Instance.GamelookSensitivity, 0);
    }

    private void OpneMenu()
    {
        optionObject.SetActive(false); // �޴� ų �� Ȱ��ȭ �� �ִ� �ɼ�â ���α�

        menuObject.SetActive(!menuObject.activeSelf); // ESC ���� �� �¿��� 
    }
}
