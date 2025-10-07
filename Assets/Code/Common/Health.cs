using System;
using Code.Stats;
using UnityEngine;

namespace Code.Common
{
    [Serializable]
    public class Health : BaseStat
    {
        public event Action LostHealth;

        public float MaxHealth => CalculateValue();

        public void Heal(float amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            CurrentValue = Mathf.Clamp(CurrentValue + amount, 0f, MaxHealth);
            OnAmountChanged(CurrentValue, MaxHealth);
        }

        public void TakeDamage(float amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            if (amount > 0)
                CurrentValue = Mathf.Clamp(CurrentValue - amount, 0f, MaxHealth);

            OnAmountChanged(CurrentValue, MaxHealth);

            if (CurrentValue <= 0)
                HandleDeath();
        }

        public void SetMaxHealth(float amount)
        {
            if (amount <= 0)
                return;

            OnAmountChanged(CurrentValue, MaxHealth);
        }

        private void HandleDeath()
        {
            LostHealth?.Invoke();
        }
    }
}