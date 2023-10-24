using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Pelki.Gameplay
{
    public class TriggerDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;

        public event Action<GameObject> Detected;

        public void Start()
        {
            Debug.Log("trigger detector start", this);
        }

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            Debug.Log("OnTriggerEnter", collider2d.gameObject);
            if ((layerMask.value & collider2d.gameObject.layer) == collider2d.gameObject.layer)
            {
                Detected?.Invoke(collider2d.GameObject());
            }
        }
    }
}