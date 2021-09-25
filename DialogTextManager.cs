using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class DialogTextManager : MonoBehaviour
{
    //1/16 �ǉ�:�o�^�֐��̑����쐬�i�{�^����OnClick�Ɠ����d�g�݁j
    [SerializeField] private UnityEvent onCompletedEvents = new UnityEngine.Events.UnityEvent();
    //1/16 �ǉ�:�e�L�X�g����I���ォ��֐������s����܂ł̎���
    [SerializeField] float eventDelayTime;
    //1/16 �I���������ǂ����̃t���O(���ꂪ�Ȃ��ƏI����J��Ԃ��֐������s���Ă��܂�)
    bool isEnd;

    public UnityAction onClickText;
    public string[] scenarios;
    [SerializeField] Text uiText;
    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;

    private string currentText = string.Empty;
    private float timeUntilDisplay = 0;
    private float timeElapsed = 1;
    private int currentLine = 0;
    private int lastUpdateCharacter = -1;

    public static DialogTextManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // �����̕\�����������Ă��邩�ǂ���
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    void Update()
    {
        // �����̕\�����������Ă�Ȃ�N���b�N���Ɏ��̍s��\������
        if (IsCompleteDisplayText)
        {
            if (currentLine < scenarios.Length && Input.GetMouseButtonDown(0))
            {
                SetNextLine();
            }
        }
        else
        {
            // �������ĂȂ��Ȃ當�������ׂĕ\������
            if (Input.GetMouseButtonDown(0))
            {
                timeUntilDisplay = 0;
            }
        }

        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
        if (displayCharacterCount != lastUpdateCharacter)
        {
            uiText.text = currentText.Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }
        CheckCompletedText(); // 1/16 �ǉ�:
    }

    // 1/16 �ǉ�:�I�����������ׂďI�����Ă���Γo�^�֐�����������
    void CheckCompletedText()
    {
        if (isEnd == false && IsCompleteDisplayText && scenarios.Length == currentLine)
        {
            isEnd = true;
            // �o�^�֐���eventDelayTime�b��Ɏ��s
            Invoke("EventFunction", eventDelayTime);
        }
    }
    // 1/16 �ǉ�:�o�^�֐��̎��s
    void EventFunction()
    {
        onCompletedEvents.Invoke();
    }


    public void SetNextLine()
    {
        isEnd = false;
        if (scenarios.Length - 1 < currentLine)
        {
            return;
        }
        currentText = scenarios[currentLine];
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        currentLine++;
        lastUpdateCharacter = -1;
    }
    // ��{�I�Ɏg���̂͂��ꂾ��
    public void SetScenarios(string[] sc)
    {
        scenarios = sc;
        currentLine = 0;
        SetNextLine();
    }
}