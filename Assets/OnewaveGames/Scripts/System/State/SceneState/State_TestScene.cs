using System.Collections.Generic;
using OnewaveGames.Scripts.System.Manager;
using OnewaveGames.Scripts.System.Spawn;
using UnityEngine;

namespace OnewaveGames.Scripts.System.State.SceneState
{
    public class State_TestScene : StateBase
    {
        protected override void Begin()
        {
            InitSpawn();
        }

        public void InitSpawn()
        {
            Unit_Manager unitManager = (Unit_Manager)GameManager.Instance.GetManager(EManager.Unit);
            if (!unitManager)
            {
                Debug.Assert(false, "[Unit Manager] is not Exist!!");
                return;
            }

            List<SpawnPoint> spawnPoints = unitManager.FindObjects<SpawnPoint>();

            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                if (spawnPoint.UnitType == EUnitType.Player)
                {
                    unitManager.CreateUnit(EUnitType.Player, 1, spawnPoint.Position);       
                }
                else if (spawnPoint.UnitType == EUnitType.Enemy)
                {
                    unitManager.CreateUnit(EUnitType.Enemy, 2, spawnPoint.Position);
                }
            }
        }
    }
}