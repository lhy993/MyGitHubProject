using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMgr : MonoBehaviour
{
    private void Awake()
    {
        if (Shared.UserMgr == null)
        {
            Shared.UserMgr = this;

            DontDestroyOnLoad(this);
        }
    }
    public int gold = 0;
    public string UserName = "";
    public int Safe;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
