using System.Collections.Generic;
using OnewaveGames.Scripts.Ability;
using OnewaveGames.Scripts.System.Library;
using OnewaveGames.Scripts.System.Table.TableData;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OnewaveGames.Scripts.System.Controller
{
    public class SkillController : MonoBehaviour
    {
        private AbilitySystemComponent _asc;
        private AudioSource _audioSource;
        
        private List<Key> _skillKeys = new List<Key>();
        private void Awake()
        {
            _asc = GetComponent<AbilitySystemComponent>();
            _audioSource = GetComponent<AudioSource>();
            _skillKeys.Add(Key.Q);
            _skillKeys.Add(Key.E);
        }

        private void Update()
        {
            foreach (var key in _skillKeys)
            {
                if (Keyboard.current[key].wasPressedThisFrame)
                {
                    // Q키가 눌렸을 때
                    if (key == Key.Q)
                    {
                        Debug.Log("Q 키가 눌렸습니다. Grab 스킬 준비!");
                    }

                    if (_audioSource != null)
                    {
                        PrefabFile_DataTable prefabTable =
                            (PrefabFile_DataTable)SystemLibrary.GetTable(ETableType.PrefabFile);
                        if (!prefabTable)
                        {
                            Debug.LogError("[Prefab Data Table] is not exist!!");
                            return;
                        }
                        
                        string grabSoundPath = prefabTable.GetPath((int)EEffectSoundType.Grab);
                        AudioClip audioClip = Resources.Load<AudioClip>(grabSoundPath);
                        
                        if (_audioSource != null && audioClip != null)
                        {
                            _audioSource.PlayOneShot(audioClip);
                        }
                    }
                    _asc.StartActiveSkill(key.ToString());
                }

                if (Keyboard.current[key].wasReleasedThisFrame)
                {
                    // 키가 떼어졌을 때
                    _asc.EndActiveSkill(key.ToString());
                }
            }
        }
    }
}