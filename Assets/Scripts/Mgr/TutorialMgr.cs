using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI    ;

public class TutorialMgr : MonoBehaviour
{
    public GameObject portal;

    public RectTransform quadrangle;
    public RectTransform arrow;
    public Text Tip;

    public int TutorialStage;
    private void Awake()
    {
        if (Shared.TutorialMgr == null)
        {
            Shared.TutorialMgr = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Portal()
    {
        portal.SetActive(true);
    }

    public void Next(int Tutorial)
    {
        TutorialStage = Tutorial;

        switch (TutorialStage)
        {
            case 1:
                quadrangle.anchoredPosition = new Vector2(-720, -300);
                quadrangle.sizeDelta = new Vector2(600, 300);

                arrow.anchoredPosition = new Vector2(-300, -50);
                arrow.sizeDelta = new Vector2(240, 100);
                arrow.localRotation = Quaternion.Euler(0, 0, 20);
                Tip.text = "버튼을 눌러 \n 좌우로 이동하기";
                break;
            case 2:
                quadrangle.anchoredPosition = new Vector2(-400, -300);
                quadrangle.sizeDelta = new Vector2(250, 250);

                arrow.anchoredPosition = new Vector2(-300, -50);
                arrow.sizeDelta = new Vector2(240, 100);
                arrow.localRotation = Quaternion.Euler(0, 0, 70);
                Tip.text = "버튼을 눌러 \n 점프하기";
                break;
            case 3:
                quadrangle.anchoredPosition = new Vector2(700, -205);
                quadrangle.sizeDelta = new Vector2(300, 300);

                arrow.anchoredPosition = new Vector2(400, 50);
                arrow.sizeDelta = new Vector2(240, 100);
                arrow.localRotation = Quaternion.Euler(0, 0, 130);
                Tip.text = "버튼을 눌러 \n 공격하기";
                break;
            case 4:
                quadrangle.anchoredPosition = new Vector2(600, 0);
                quadrangle.sizeDelta = new Vector2(200, 200);

                arrow.anchoredPosition = new Vector2(350, 50);
                arrow.sizeDelta = new Vector2(240, 100);
                arrow.localRotation = Quaternion.Euler(0, 0, 160);
                Tip.text = "버튼을 눌러 \n 방어하기";
                break;
            case 5:
                quadrangle.anchoredPosition = new Vector2(450, -150);
                quadrangle.sizeDelta = new Vector2(200, 200);

                arrow.anchoredPosition = new Vector2(350, 50);
                arrow.sizeDelta = new Vector2(240, 100);
                arrow.localRotation = Quaternion.Euler(0, 0, 120);
                Tip.text = "포탈 앞으로 이동하고 \n 상호작용 버튼 누르기";
                break;

        }
    }
}
