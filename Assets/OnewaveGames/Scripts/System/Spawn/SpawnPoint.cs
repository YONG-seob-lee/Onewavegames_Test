using UnityEngine;

namespace OnewaveGames.Scripts.System.Spawn
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private EUnitType _unitType;
        
        public EUnitType UnitType => _unitType;
        public Vector3 Position => transform.position;

        public void Start()
        {
            SpriteRenderer spriteRenderer = GetComponent <SpriteRenderer>();
            if (spriteRenderer)
            {
                spriteRenderer.enabled = false;
            }
        }
    }
}