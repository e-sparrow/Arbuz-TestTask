using System;
using Game.Coins.Interfaces;
using Game.Player.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Coins
{
    public class CoinView : MonoBehaviour, ICoinView
    {
        private static readonly int TakeTrigger = Animator.StringToHash("Take");

        public event Action OnCoinTaken = () => { };
        
        [SerializeField] private Animator animator;

        [UsedImplicitly]
        public void OnTakeAnimationEnd()
        {
            OnCoinTaken.Invoke();
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IPlayerView>(out var _))
            {
                animator.SetTrigger(TakeTrigger);
            }
        }
    }
}