using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               //UI �� ����ϱ����� 

public class UISystemManager : MonoBehaviour
{
    [SerializeField] private List<UIPanel> panels;
    [SerializeField] private UIPopup popupPrefabs;

    [SerializeField]
    private GameObject Canvas;
    private int currentPanelIndex;
    private Stack<UIPopup> popupStack = new Stack<UIPopup>();

    public void ShowPanel(int index)
    {//�г� index�� �����ͼ� �ش� �гθ� ������
        for (int i = 0; i < panels.Count; i++)
        {
            if(i == index)
            {
                panels[i].Show();
            }
            else
            {
                panels[i].Hide();
            }
        }
    }

    public void NextPanel()     //���� �г��� ������
    {
        currentPanelIndex = (currentPanelIndex + 1) % panels.Count; //�г� ���ڸ�ŭ ������ ������ ����
        ShowPanel(currentPanelIndex);
    }

    public void PreviousPanel()
    {
        currentPanelIndex--;
        if(currentPanelIndex < 0)               //0 ���Ϸ� �������� �ȵǼ�
        {
            currentPanelIndex = panels.Count - 1;
        }
        ShowPanel(currentPanelIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(panels.Count > 0)
        {
            currentPanelIndex = 0;
            ShowPanel(currentPanelIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousPanel();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPanel();
        }
    }
}
