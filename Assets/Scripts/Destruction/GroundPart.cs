using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Destruction
{
    class GroundPart : MonoBehaviour
    {
        [field: SerializeField] public List<Rigidbody> Parts { get; private set; }

        public void InitDisable() => 
            DOTween.Sequence().AppendInterval(5f).OnComplete(() => gameObject.SetActive(false));
    }
}