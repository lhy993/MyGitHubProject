using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMgr : MonoBehaviour
{
    public GameObject portal;

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
}
