using System;
using UnityEngine;
using UnityEngine.UI;

namespace InsaneSystems.HealthbarsKit.UI
{
    /// <summary> Healthbar class draws healthbar on UI above target object. Also controls state of healthbar like health value, color, etc. </summary>
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private float _offsetAbovePlayer = 2;
       [SerializeField] private Health _health;

        private float _maxHealthValue = 100;

        private Transform _hero;
        private RectTransform rectTransform;
        private UnityEngine.Transform target;
        private float _elapsedTime;
        private float targetHeight = 1f;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            // _maxHealthValue = _health.MaxValue;
        }

        private void OnEnable()
        {
            _health.ValueChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _health.ValueChanged -= OnHealthChanged;
        }

        public void SetHealthValue(float health)
        {
            fillImage.fillAmount = health / _maxHealthValue;
            //this.enabled = true;
        }

        private void LateUpdate()
        {
            transform.position = _health.transform.position + Vector3.up * _offsetAbovePlayer;
        }

        /// <summary> We using cusstom method for update, which called on all healthbars from HealthbarsController. It increases performance for a bit on big healthbars count. </summary>
        public void OnUpdate()
        {
            if (!target)
            {
                Destroy(gameObject);
                return;
            }

            var barWorldPos = target.position + Vector3.up * targetHeight;
            rectTransform.anchoredPosition = HealthbarsController.instance.cachedCamera.WorldToScreenPoint(barWorldPos);
        }

        public void SetImageFill(float fillAmount)
        {
            fillImage.fillAmount = fillAmount;
            SetColorByFillValue();
        }

        public void SetColorByFillValue()
        {
            fillImage.color = Color.Lerp(HealthbarsController.instance.MinHealthColor,
                HealthbarsController.instance.MaxHealthColor, fillImage.fillAmount);

            if (HealthbarsController.instance.UseHDRForBetterColoring)
                fillImage.color *= 2f;
        }

        public void SetupWithTarget(UnityEngine.Transform newTarget, float targetMaxHealth)
        {
            target = newTarget;
        }

        public void SetTargetHeight(float newHeight)
        {
            targetHeight = newHeight;
        }


        /// <summary> This method will be called by a event on your characters objects (dont forget to code it, see documentation) and update health to actual value. </summary>
        /// <param name="healthValue">Actual health of character.</param>
        public void OnHealthChanged(float healthValue)
        {
            fillImage.fillAmount = healthValue / _health.InitialValue;
            fillImage.fillAmount = Mathf.Clamp(fillImage.fillAmount, 0, _health.InitialValue);

            if (HealthbarsController.instance.SetColorByHealthPecents)
                SetColorByFillValue();

            if (fillImage.fillAmount <= 0)
            {
                gameObject.SetActive(false);
            }

            if (HealthbarsController.instance.HideHealthbarsIfHealthFull)
                gameObject.SetActive(fillImage.fillAmount < 1f);
        }
    }
}