using UnityEngine;
using System.IO;


public class SaveManager : MonoBehaviour
{
    string saveFolder;


    void Awake()
    {
        saveFolder =
        Application.persistentDataPath + "/SaveData/";


        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
    }



    // 저장
    public void SaveGame(SaveData data)
    {
        string json =
        JsonUtility.ToJson(data, true);


        string path =
        saveFolder + data.playerName + ".json";


        File.WriteAllText(path, json);


        Debug.Log("저장 완료");
    }



    // 불러오기
    public SaveData LoadGame(string playerName)
    {
        string path =
        saveFolder + playerName + ".json";


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