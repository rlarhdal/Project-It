using UnityEngine;

public class EquipAxe : MonoBehaviour // ����
{
    private void Start()
    {
        GameManager.Instance.canDestroy = true; //��� ������ �ڹ��� �ı� ���� ����
    }
}