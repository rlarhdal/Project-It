using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePosData : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("��ġ ���� ��...");
        DataManager.Instance.SavePosData(other.transform.position);
    }
}
