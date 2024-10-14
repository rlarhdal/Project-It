using System;
using UnityEngine;

/// <summary>
/// control 이벤트
/// <author>김수현</author>
/// </summary>
public class Controller : MonoBehaviour // WK interact �̺�Ʈ �Լ� ����
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<float> OnRunEvent;
    public event Action OnInventoryEvent;
    public event Action OnInteracEvent;
    public event Action OnMenuEvent; // ���� �޴�ȭ�� ������


    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    public void CallRunEvent(float value)
    {
        OnRunEvent?.Invoke(value);
    }

    public void CallInventoryEvent()
    {
        OnInventoryEvent?.Invoke();
    }

    public void CallInteracEvent()
    {
        OnInteracEvent?.Invoke();
    }

    public void CallMenuEvent() // ���� �޴�ȭ�� ������
    {
        OnMenuEvent?.Invoke();
    }
}
