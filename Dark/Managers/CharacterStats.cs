using  System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public int staminaLevel = 10;
        public float maxStamina;
        public float currentStamina;

        public int focusLevel = 10;
        public float maxFocusPoints;
        public float currentFocusPoints;

        public bool isDead;

        public virtual void TakeDamage(int damage, string damageAnimation = "Damage_01")
        {

        }
    }
}