using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialouge : MonoBehaviour
{
    Dictionary<string, string> chatData = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        TextAsset csvData = Resources.Load<TextAsset>("CSVData");
        var data = csvData.text.TrimEnd();

        Deserialization(data);

        Debug.Log(Show(2, 2));
    }

    public void Deserialization(string originData)
    {
        // ���Ͷ� , �� ����.
        // 1�� - ���ͷ� �ڸ���
        // 2�� - , �� �߶����.

        var rowData = originData.Split('\n');

        for (int i = 1; i < rowData.Length; i++)
        {
            var data = rowData[i].Split(',');

            chatData[data[0]] = data[1];
        }
    }

    public string Show(int chapter, int phase)
    {
        string t = $"{chapter}_{phase}";
        return chatData[t];
    }
}
