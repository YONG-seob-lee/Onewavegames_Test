using UnityEngine;

namespace OnewaveGames.Scripts.Skill.Indiacator
{
    public class SkillIndicator : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        public Material indicatorMaterial;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            if (_lineRenderer == null)
            {
                _lineRenderer = gameObject.AddComponent<LineRenderer>();
            }

            _lineRenderer.startWidth = 1f;
            _lineRenderer.endWidth = 1f;
            _lineRenderer.material = indicatorMaterial;
            _lineRenderer.enabled = false;
        }

        public void Show(Vector3 start, Vector3 end)
        {
            _lineRenderer.SetPosition(0, start);
            _lineRenderer.SetPosition(1, end);
            _lineRenderer.enabled = true;
        }

        public void Hide()
        {
            _lineRenderer.enabled = false;
        }
    }
}