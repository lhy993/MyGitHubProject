using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public partial class SceneMgr : MonoBehaviour
{
    public SCENE Scene;

    public void ChangeScene(SCENE _e, bool _Loding = false)
    {
        if (Scene == _e)
            return;

        Scene = _e;

        SceneManager.LoadScene((int)Scene);
    }
}
