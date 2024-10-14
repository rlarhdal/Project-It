using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// input system���� �Էµ� �� �޾ƿ���
/// <author>�����</author>
/// </summary>
public class PlayerInputController : Controller // WK �߰� EŰ
{
    private Vector2 curMovementInput;
    private Vector2 mouseDelta;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //gamemanager�� �̵�
    }

    public void OnMove(InputValue value)
    {
        curMovementInput = value.Get<Vector2>();
        CallMoveEvent(curMovementInput);
    }

    public void OnLook(InputValue value)
    {
        mouseDelta = value.Get<Vector2>();
        CallLookEvent(mouseDelta);
    }

    public void OnRun(InputValue value)
    {
        float runValue = value.Get<float>();
        CallRunEvent(runValue);
    }

    public void OnInventory(InputValue value)
    {
        CallInventoryEvent();
    }

    public void OnInteract(InputValue vlaue)
    {
        CallInteracEvent();
    }

    public void OnMenu(InputValue vlaue) // ����, ESC ���� �� �޴� ȭ�� ������.
    {
        CallMenuEvent();
    }
}
