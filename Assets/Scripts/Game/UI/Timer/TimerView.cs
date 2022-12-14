using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Game.UI.Timer.Interfaces;
using TMPro;
using UnityEngine;

namespace Game.UI.Timer
{
    public class TimerView : MonoBehaviour, ITimerView
    {
        public event Action OnTimerOver = () => { };
        
        [SerializeField] private TMP_Text text;

        private TweenerCore<float, float, FloatOptions> _tweener;
        
        public void SetValue(float value)
        {
            text.text = value.
                ToString("0.#")
                .Replace(',', '.');
        }

        public void StartTimer(float time)
        {
            text.gameObject.SetActive(true);
            
            float value = time;

            _tweener?.Kill();
            _tweener = DOTween
                .To(GetValue, SetValue, 0f, time)
                .SetEase(Ease.Linear)
                .OnComplete(Complete);

            float GetValue()
            {
                return value;
            }

            void SetValue(float newValue)
            {
                value = newValue;
                text.text = value
                    .ToString("0.#")
                    .Replace(',', '.');
            }

            void Complete()
            {
                OnTimerOver.Invoke();
                
                text.gameObject.SetActive(false);
            }
        }
    }
}