using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

public class ResourceManager : SingleTon<ResourceManager>//ALL : (250707)�̱��� ����� �񵿱� �ε� ��ũ��Ʈ �����ϸ� ���� ĳ���صа� ����ϰ����� �˾Ƶθ� �۾��� ���� �� �� ���� �߰��մϴ�
{
    private bool isLoadAble<T>(T instance) { return instance != null; }
    //�޴��� �ν��Ͻ� ������ ����Ǵ� �Լ�
    protected override void Init()
    {
        base.Init();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">����Ÿ��</typeparam>
    /// <param name="key">Ÿ�� Ű��</param>
    /// <param name="callback">(obj)=>{targetInstance = obj}</param>
    public void LoadAsync<T>(string key, Action<T> callback, bool isCaching = false)
    {
        if (key.Contains(".sprite"))
        {
            key = $"{key}[{key.Replace(".sprite", "")}]";
        }
        AsyncOperationHandle<T> infoAsyncOP = Addressables.LoadAssetAsync<T>(key);
        infoAsyncOP.Completed += (op) =>
        {

            callback?.Invoke(infoAsyncOP.Result);
            if (isCaching) Addressables.Release(infoAsyncOP);
        };
    }

    private string GetSavePath() => Application.persistentDataPath;

    public bool SaveData<T>(T data, string fileName, bool isOverride)
    {
        string path = Path.Combine(GetSavePath(), fileName);
        Debug.Log(path);
        if (File.Exists(path))
        {
            if (isOverride == true)
            {
                
                File.WriteAllText(path, JsonUtility.ToJson(data));
                return true;
            }
        }
        else
        {
            if (File.Exists(GetSavePath()))
            {
                StreamWriter file = File.CreateText(path);
                file.WriteLine(JsonUtility.ToJson(data));
            }
            else
            {
                Directory.CreateDirectory(GetSavePath());
                StreamWriter file = File.CreateText(path);
                file.WriteLine(JsonUtility.ToJson(data));

            }
            return true;
        }
        return false;
    }
    public T LoadData<T>(string fileName) where T : new()
    {
        string path = Path.Combine(GetSavePath(), fileName);

        if (File.Exists(path))
        {
            T result;
            try
            {
                result = JsonUtility.FromJson<T>(File.ReadAllText(path));
            }
            catch
            {
                //�Ľ� ���н�
                result = default(T);
                Debug.LogError("���̽� �Ľ� ����");
            }
            return result;
        }
        else
        {
            Directory.CreateDirectory(GetSavePath());
            return default(T);
        }
    }
    public void XMLSave<T>(T obj, string dataName)
    {
        string resultPath = Path.Combine("./Assets/DataSheets/", dataName);
        XmlSerializer ser = new XmlSerializer(typeof(T));
        using (FileStream stream = new FileStream(resultPath, FileMode.Create))
        {
            ser.Serialize(stream, obj);
        }

        AssetDatabase.Refresh();
    }
    public T XMLLoad<T>(string dataName)
    {
        string resultPath = Path.Combine("./Assets/DataSheets/", dataName);
        T result = default(T);
        try
        {
            if (File.Exists(resultPath))
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                using (FileStream stream = new FileStream(resultPath, FileMode.Open))
                {
                    result = (T)ser.Deserialize(stream);
                }
                AssetDatabase.Refresh();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"XML Parser Load�����߻�{e.Message}");
        }
        return result;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">����Ÿ��</typeparam>
    /// <param name="label">Ÿ�� Ű��</param>
    /// <param name="callback">(obj)=>{targetInstance = obj}</param>
    public void LoadAsyncAll<T>(string label, Action<(string, T)[]> callback, bool isCaching = true)
    {
        var labelKeys = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        //label�� TŸ���� ������Ʈ���� Ű���� �����´�
        labelKeys.WaitForCompletion();
        //resource�� ���� load�Ҷ����� ���

        Debug.Log(labelKeys.Result);
        if (labelKeys.Result.Count == 0) { Debug.LogError($"{label}���� ����ֽ��ϴ�."); callback.Invoke(null); }//�ش��ϴ� Ű�� ������� null�� ����

        int doneCount = 0;

        (string, T)[] tempT = new (string, T)[labelKeys.Result.Count];
        for (int i = 0; i < tempT.Length; i++)
        {
            int curIndex = i;
            string curKey = labelKeys.Result[i].PrimaryKey; //�ݹ��� ���ÿ� ����� Ŭ���� �̽��� ���� �� �ֱ⿡ �и�(�ٸ� ������ ���ø޸𸮸� �����ϴ� ����)
            LoadAsync<T>(labelKeys.Result[i].PrimaryKey, (result) =>
            {
                tempT[curIndex].Item1 = curKey;
                tempT[curIndex].Item2 = result;
                doneCount++;
                if (doneCount == labelKeys.Result.Count)
                {
                    callback?.Invoke(tempT);
                    if (isCaching) Addressables.Release(labelKeys);
                }
            }, isCaching);
        }
    }
}
