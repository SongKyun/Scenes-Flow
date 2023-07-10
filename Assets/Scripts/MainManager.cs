using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    // Start() 및 Update() 메서드 삭제 - 현재 필요하지 않음

    public static MainManager Instance;

    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    } // 새 변수 선언

        public void SaveColor()
    {
        SaveData data = new SaveData(); // 저장 데이터의 새 인스턴스를 만들고
        data.TeamColor = TeamColor; // 팀 색상 클래스 멤버를 MainManager에 저장된 TeamColor 변수로 채웠습니다.

        string json = JsonUtility.ToJson(data); // JsonUtility.ToJson을 사용하여 인스턴스를 JSON으로 변환했습니다. 

        // 특수한 메서드 File.WriteAllText로 파일에 문자열을 작성했습니다.
        // 첫 번째 파라미터는 파일에 대한 경로입니다. Application.persistentDataPath라는 Unity 메서드를 사용했는데,
        // 이 메서드는 애플리케이션을 재설치하거나 업데이트하는 동안에 유지되는 데이터를 저장할 수 있는 폴더를 제공하고 파일 이름 savefile.json을 추가합니다.
        // 두 번째 파라미터는 파일에 작성하려는 텍스트(이 경우 JSON)입니다.
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }

    public Color TeamColor; // 새 변수 선언

    private void Awake()
    {
        // 새 코드의 시작
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // 새 코드의 끝

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

}


