using System;
using System.Collections.Generic;
using UnityEngine;

namespace OnewaveGames.Scripts.System.State
{
    public class StateMachine
    {
        private Dictionary<int, StateBase> StateEntry = new Dictionary<int, StateBase>();
        private int CurrentStateKey = -1;
        
        public void Create() { }

        public void Destroy()
        {
            UnRegisterAllState();
        }

        public void Update()
        {
            StateBase currentState = GetCurrentState();
            currentState?.Update();
        }

        public StateBase RegisterState<T>(int index, string name)
            where T : StateBase
        {
            if (StateEntry.ContainsKey(index))
            {
                Debug.LogWarning($"Is Already Exist. [{name}]");
                return null;
            }

            StateBase newState = (T)Activator.CreateInstance(typeof(T));
            if (newState == null)
            {
                return null;
            }
            
            newState.Initialize(index, name);
            
            StateEntry[index] = newState;
            
            return newState;
        }

        public void UnRegisterAllState()
        {
            foreach (var State in StateEntry)
            {
                if (State.Value != null)
                {
                    State.Value.UnInitialize();
                }
            }
        }

        public StateBase GetCurrentState()
        {
            return StateEntry.GetValueOrDefault(CurrentStateKey);
        }

        public StateBase ChangeState(int index)
        {
            return SetState_Internal(index);
        }

        private StateBase GetState(int stateKey)
        {
            return StateEntry.GetValueOrDefault(stateKey);
        }

        private StateBase SetState_Internal(int index)
        {
            StateBase currentState = GetState(CurrentStateKey);
            if(currentState != null)
            {
                currentState.OnExitState();
            }

            CurrentStateKey = index;
            StateBase nextState = GetState(index);
            if(nextState != null)
            {
                nextState.OnBeginState();
            }

            return nextState;
        }
    }
}