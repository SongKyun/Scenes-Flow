using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    // Start() �� Update() �޼��� ���� - ���� �ʿ����� ����

    public static MainManager Instance;

    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    } // �� ���� ����

        public void SaveColor()
    {
        SaveData data = new SaveData(); // ���� �������� �� �ν��Ͻ��� �����
        data.TeamColor = TeamColor; // �� ���� Ŭ���� ����� MainManager�� ����� TeamColor ������ ä�����ϴ�.

        string json = JsonUtility.ToJson(data); // JsonUtility.ToJson�� ����Ͽ� �ν��Ͻ��� JSON���� ��ȯ�߽��ϴ�. 

        // Ư���� �޼��� File.WriteAllText�� ���Ͽ� ���ڿ��� �ۼ��߽��ϴ�.
        // ù ��° �Ķ���ʹ� ���Ͽ� ���� ����Դϴ�. Application.persistentDataPath��� Unity �޼��带 ����ߴµ�,
        // �� �޼���� ���ø����̼��� �缳ġ�ϰų� ������Ʈ�ϴ� ���ȿ� �����Ǵ� �����͸� ������ �� �ִ� ������ �����ϰ� ���� �̸� savefile.json�� �߰��մϴ�.
        // �� ��° �Ķ���ʹ� ���Ͽ� �ۼ��Ϸ��� �ؽ�Ʈ(�� ��� JSON)�Դϴ�.
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

    public Color TeamColor; // �� ���� ����

    private void Awake()
    {
        // �� �ڵ��� ����
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // �� �ڵ��� ��

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

}


