using GAS;
using UnityEngine;

namespace OnewaveGames.Scripts.Effect
{
    [CreateAssetMenu(menuName = "GAS/Effect/Grab", fileName = "New GrabEffect")]
    public class GrabEffect : GameplayEffect
    {
        public float pullSpeed = 5.0f;
    }
}