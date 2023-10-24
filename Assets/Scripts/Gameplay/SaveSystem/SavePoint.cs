using System;
using UnityEngine;

namespace Pelki.Gameplay.SaveSystem
{
    public class SavePoint : MonoBehaviour
    {
        [SerializeField] private TriggerDetector triggerDetector;

        private bool isActivated = false;

        public event Action<SavePoint> Saved;

        public void Start()
        {
            Debug.Log("savepoint start");
            triggerDetector.Detected += OnDetected;
        }

        private void OnDestroy()
        {
            triggerDetector.Detected -= OnDetected;
        }

        public void OnDetected(GameObject gameObject)
        {
            Debug.Log("On Detected");
            if (!isActivated)
            {
                isActivated = true;
                Saved?.Invoke(gameObject);
            }
        }
    }
}