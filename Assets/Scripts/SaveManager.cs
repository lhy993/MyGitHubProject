using UnityEngine;
using System.IO;


public class SaveManager : MonoBehaviour
{
    string path;


    void Awake()
    {
        path =
        Application.persistentDataPath
        + "/SaveData.json";
    }



    public void SaveGame(SaveData data)
    {
        string json =
        JsonUtility.ToJson(data, true);


        File.WriteAllText(path, json);


        Debug.Log("저장 완료");
    }



    public SaveData LoadGame()
    {
        if (File.Exists(path))
        {
            string json =
            File.ReadAllText(path);


            SaveData data =
            JsonUtility.FromJson<SaveData>(json);


            Debug.Log("불러오기 성공");


            return data;
        }


        return null;
    }
}