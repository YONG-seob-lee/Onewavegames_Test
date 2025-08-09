using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OnewaveGames.Scripts.System.Manager;
using OnewaveGames.Scripts.System.Table;
using UnityEngine;

namespace OnewaveGames.Scripts.System.Library
{
    public class SystemLibrary
    {
        public static TextAsset CreateTextAsset(string assetPath)
        {
            var textAsset = Resources.Load<TextAsset>(assetPath);
            if (textAsset == null)
            {
                Debug.LogError("UIDataTable CSV not found!!");
                return null;
            }

            return textAsset;
        }
        
        public static void CreateTableObject<TEntry>(TextAsset textAsset, Dictionary<int, TEntry> dataMap)
            where TEntry : Data, new()
        {
            dataMap.Clear();
            // so 파일 로드 또는 생성
            string[] lines = textAsset.text.Split('\n');

            string[] headers = lines[0].Trim().Split(',');
            for (int i = 1; i < lines.Length; ++i)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                string[] tokens = lines[i].Trim().Split(',');
                if (tokens.Length != headers.Length)
                {
                    Debug.LogWarning($"CSV 파싱 오류: {i}번째 줄의 열 개수가 헤더와 다릅니다");
                }
                
                TEntry entry = new TEntry();
                Type entryType = typeof(TEntry);

                for (int j = 0; j < headers.Length; ++j)
                {
                    string fieldName = headers[j].Trim();
                    string rawValue = tokens[j].Trim();
                    
                    FieldInfo field = entryType.GetField(fieldName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (field == null)
                    {
                        Debug.LogWarning($"필드 {fieldName} 을(를) {entryType.Name}에서 찾을 수 없습니다.");
                        continue;
                    }

                    object converted = ConvertValue(field.FieldType, rawValue);
                    field.SetValue(entry, converted);
                }

                FieldInfo keyField = entryType.GetField("Key", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (keyField == null)
                {
                    Debug.LogWarning($"{entryType.Name}에 Key 필드가 없습니다.");
                    continue;
                }

                int key = (int)keyField.GetValue(entry);
                
                if (dataMap.ContainsKey(key))
                {
                    Debug.LogWarning($"중복된 ID({key})가 발견되었습니다. 덮어씁니다.");
                }

                dataMap[key] = entry;
            }
        }
        
        private static object ConvertValue(Type type, string raw)
        {
            if (raw.Length <= 0) return default;
            
            if (type == typeof(int)) return int.Parse(raw);
            if (type == typeof(float)) return float.Parse(raw);
            if (type == typeof(string)) return raw;
            if (type == typeof(bool)) return bool.Parse(raw);
            if (type == typeof(GameObject)) return Resources.Load<GameObject>(raw);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                Type elementType = type.GetGenericArguments()[0];

                if (elementType == typeof(int))
                {
                    return raw.Split(';').Select(s => int.Parse(s.Trim())).ToList();
                }

                if (elementType == typeof(string))
                {
                    return raw.Split(';').Select(s => s.Trim()).ToList();
                }
            }
            
            throw new Exception($"지원되지 않는 타입 : {type.Name}");
        }

        public static IDataTable GetTable(ETableType tableType)
        {
            Table_Manager tableManager = (Table_Manager)GameManager.Instance.GetManager(EManager.Table);
            if (!tableManager)
            {
                Debug.LogError("[Table Manager] is not exist!!");
                return null;
            }

            return tableManager.GetTable(tableType);
        }
    }
}