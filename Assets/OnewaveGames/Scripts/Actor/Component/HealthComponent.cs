using UnityEngine;

namespace OnewaveGames.Scripts.Actor.Component
{
    public class HealthComponent : MonoBehaviour
    {
        private int _maxHP = 100000;
        private int _currentHP = 1000;

        public void Init(int maxHp, int currentHp)
        {
            _maxHP = maxHp;
            _currentHP = currentHp;
        }

        public bool TakeDamage(int damage)
        {
            _currentHP = Mathf.Max(0, _currentHP - damage);
            return _currentHP <= 0;
        }
        
        public int CurrentHP => _currentHP;
    }
}