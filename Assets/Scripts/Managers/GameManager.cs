using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum QuestState
{
    // state : �ڹ��踦 �μ� ��
    First_Quest,
    // state : ���ܱ⸦ �� �� ���� ��
    FirstSwitch,
    // state : ���ܱ⸦ ��� ���� ��
    SwitchClear
}

[System.Serializable]
public class QuestLine
{
    public bool is1st;
    public bool is12st;
    public bool isClear;

    public void QuestData(QuestState questState)
    {
        QuestState state = questState;

        switch (state)
        {
            case QuestState.First_Quest:
                break;
            case QuestState.FirstSwitch:
                is1st = true;
                is12st = false;
                isClear = false;
                break;
            case QuestState.SwitchClear:
                is1st = true;
                is12st = true;
                isClear = true;
                break;
        }
    }
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private GameObject systemTextObj;
    private TextMeshProUGUI systemText;
    public GameObject hintMemo;
    [HideInInspector] public TextMeshProUGUI hintMassage;

    public bool is1stSwitch = false;
    public bool is2ndSwitch = false;
    public bool isClearSwitch = false;
    public bool isClearGame = false;
    public int randam = 0;
    public int? passward1;
    public int? passward2;
    public int? passward3;
    public int? passward4;
    public string passward = "1";

    [HideInInspector]public bool canDestroy = false; // ����, �ڹ��� �ı� ����
    [HideInInspector]public bool isOpening = false; // ����, ��door �� ����
    [HideInInspector]public bool isOpne = false; // ����, �� door �� ���� ���� 

    [HideInInspector]public float GamelookSensitivity = 0.1f; //����, �ɼ� ���� ���� �� ��
    [HideInInspector]public float audioValue = 0.1f; //����, �ɼ� ��ݼ��� ���� �� ��
    // �ڵ庸�� -> �ν����� ���߼���

    // ����, ���� ���� ��ġ
    public NPC npc;
    public GameObject Enemy;
    public Transform[] monsterSpawnPoints;
    public Transform monsterSpawnPosition;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        hintMassage = hintMemo.GetComponentInChildren<TextMeshProUGUI>();

        // TODO : randam�� ����� ���� �޾ƿ��� ��� �ʿ�
        if (randam == 0) // TODO : rarandam�� ����� ���� null�� ���� ������ ����
        {
            randam = Random.Range(1, 3);
        }
        else
        {
            // TODO : null�� �ƴ� ��� randam���� �ҷ�����
        }

        passward1 = Random.Range(0, 10);
        passward2 = Random.Range(0, 10);
        passward3 = Random.Range(0, 10);
        passward4 = Random.Range(0, 10);

        passward = $"{passward1}" + $"{passward2}" + $"{passward3}" + $"{passward4}";

        systemText = systemTextObj.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void MonsterData(GameObject enemy)
    {
        // ���� �罺�� ���� ��ġ ������
        Enemy = enemy;
        npc = enemy.GetComponent<NPC>();
        monsterSpawnPoints = npc.points;

        int i = Random.Range(0, npc.points.Length);
        monsterSpawnPosition = npc.points[i];
    }

    public void QuestInit()
    {
        is1stSwitch = CharacterManager.Instance.Player.questLine.is1st;
        is2ndSwitch = CharacterManager.Instance.Player.questLine.is12st;
        isClearSwitch = CharacterManager.Instance.Player.questLine.isClear;
    }

    public void ClearPuzzle()
    {
        if(is1stSwitch && is2ndSwitch)
        {
            StartCoroutine(OnSystemText("����, ���� ���� �� �ְڱ�"));
            isClearSwitch = true;
            Debug.Log("�Ϸ�!!!");
            // TODO : ������ �Ϸ�Ǹ� �۵��� ����� ����
            DataManager.Instance.SaveData(QuestState.SwitchClear);
        }
        else if (is1stSwitch && !is2ndSwitch)
        {
            StartCoroutine(OnSystemText("���𰡰� �� �� �� �� ����...", Color.red));
            Debug.Log("����!!!");
            // TODO : �� �ܿ� ������ Ʋ���� �� �۵��� ����� ����
        }
    }

    public void SpawnMonster()
    {
        Enemy.SetActive(true);
        Enemy.transform.position = monsterSpawnPosition.position;
    }

    public void GameOver()
    {
        // TODO : ���� �й� ����
        GameOverManager.instance.StartFadeIn();
    }

    public void GameEnd()
    {
        // TODO : ���� Ŭ���� ����
        Destroy(Enemy);
        UIManager.Instance.gameClear.SetActive(true);
    }

    public void GameReStart()
    {
        QuestInit();

        SpawnMonster();
        SpawnPlayer();

        // �÷��̾� ������ �ر�
        JumpscareManager.Instance.ChangePlayerMovementControll();
    }

    private void SpawnPlayer()
    {
        CharacterManager.Instance.Player.gameObject.transform.position = CharacterManager.Instance.Player.playerData.position;
        CharacterManager.Instance.Player.EquipItemLsit = CharacterManager.Instance.Player.playerData.equipItems;
        CharacterManager.Instance.Player.ItemLsit = CharacterManager.Instance.Player.playerData.itemList;
    }

    public IEnumerator OnSystemText(string massage)
    {
        systemText.text = massage;
        systemText.color = Color.white;
        if (GameObject.Find("SystemText(Clone)") != null)
        {
            Destroy(GameObject.Find("SystemText(Clone)"));
        }
        GameObject text = Instantiate(systemTextObj, GameObject.Find("Canvas").transform);
        yield return new WaitForSeconds(2);
        Destroy(text);
    }

    public IEnumerator OnSystemText(string massage ,Color color)
    {
        systemText.text = massage;
        systemText.color = color;
        if (GameObject.Find("SystemText(Clone)") != null)
        {
            Destroy(GameObject.Find("SystemText(Clone)"));
        }
        GameObject text = Instantiate(systemTextObj, GameObject.Find("Canvas").transform);
        yield return new WaitForSeconds(2);
        Destroy(text);
        systemText.color = Color.white;
    }
}
