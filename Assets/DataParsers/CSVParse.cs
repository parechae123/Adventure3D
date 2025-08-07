using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Parsing
{

    public class CSVParse : MonoBehaviour
    {
        // Start is called before the first frame update
        Dictionary<string, string> parsedData = new Dictionary<string, string>();
        public TextAsset target;
        void Start()
        {
            var data = target.text.TrimEnd();
            DeSerialize(data);

            Show(2, 1);
        }

        public void DeSerialize(string originData)
        {
            var rowData = originData.Split('\n');

            for (int i = 1; i < rowData.Length; i++)
            {
                var data = rowData[i].Split(',');
                parsedData.Add(parsedData[data[0]], data[1]);
            }
        }
        public string Show(int chapter, int phase)
        {
            string t = $"{chapter}_{phase}";
            return parsedData[t];
        }
    }

}