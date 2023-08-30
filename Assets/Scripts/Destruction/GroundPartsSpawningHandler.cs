using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Destruction
{
    [RequireComponent(typeof(BoxCollider))]
    public class GroundPartsSpawningHandler : MonoBehaviour
    {
        private const int HardCubeLayer = 15;

        private List<Rigidbody> _parts = new();
        private EnemyDestruction _enemyDestruction;
        private GroundPart _groundPart;

        public event Action PlatformDestroyed;

        private void Awake()
        {
            gameObject.layer = HardCubeLayer;
            GetComponent<Collider>().isTrigger = false;

            var groundPartPrefab = Resources.Load<GroundPart>("Prefabs/GroundPart");
             _groundPart = Instantiate(groundPartPrefab);

            _parts = _groundPart.Parts;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyDestruction enemyDestruction))
            {
                _enemyDestruction = enemyDestruction;
                _enemyDestruction.Destroyed += Demolish;
            }
        }

        private void Demolish(Transform position)
        {
            foreach (var part in _parts)
            {
                this.SetActive(part.gameObject, true, 0f);
                part.isKinematic = false;
                part.transform.position = _enemyDestruction.transform.position;
            }

            _enemyDestruction.Destroyed -= Demolish;
            _groundPart.InitDisable();
            PlatformDestroyed?.Invoke();
            transform.DOScale(0, 0);
            this.SetActive(gameObject, false, 1f);
        }
    }
}