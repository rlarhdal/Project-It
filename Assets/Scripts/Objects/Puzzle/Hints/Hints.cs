using UnityEngine;

public class Hints : MonoBehaviour
{
    public void SetHints(int num)
    {
        switch (num)
        {
            case 1:
                SetHint1();
                break;
            case 2:
                SetHint2();
                break;
            default:
                break;
        }
    }

    private void SetHint1()
    {
        string str1;
        string str2;

        if (GameManager.Instance.randam == 1)
        {
            str1 = "2�� �ްԽ�";
            str2 = "���� �� ��";
        }
        else
        {
            str1 = "���� �� ��";
            str2 = "2�� �ްԽ�";
        }

        GameManager.Instance.hintMassage.text = "�߰� ���� �������\n" +
            "\n" +
            "����ϱ� ���� 1�� ���� ���� �����ǿ� �ִ� ���ܱ⸦ ������ �� ������.\n" +
            "�׸��� 1��, 2��, ���ϼ����� �ѷ����ְ�, ���ܱⰡ �� �۵��ϴ����� Ȯ�����ְ�\n" +
            $"{str1}�� �ִ� ���ܱ⵵ Ȯ���� ������\n" +
            $"��, �׸��� {str2}�� �ִ� ���ܱ�� �ظ��ϸ� �ǵ帮����\n" +
            "�������� ���� ����, �׳� ���� �������� �׷�����\n" +
            "���� ���� �־ ����� �ǵ帮�� ����� �׷�����\n" +
            "�׷��Ÿ� �׳� ���ִ°� ���� �ʳ� �ͱ⵵ �ѵ�...";
    }
    private void SetHint2()
    {
        GameManager.Instance.hintMassage.text = $"X X X {GameManager.Instance.passward4}";
    }
}