using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyStats : CharacterStats
    {
        public GameObject ui;

        public GameObject hitvfx;
        public CapsuleCollider capsuleCollider;

        EnemyAnimatorManager enemyAnimatorManager;
        EnemyBossManager enemyBossManager;
        public UIEnemyHealthBar enemyHealthBar;

        public bool isBoss;

        public int soulsAwardedOnDeath = 50;



        private void Awake()
        {
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            enemyBossManager = GetComponent<EnemyBossManager>();
            maxHealth = SetMaxHealthFromHealthLevel();
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            if (!isBoss)
            {
                enemyHealthBar.SetMaxHealth(maxHealth);
            }
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamageNoAnimation(int damage)
        {
            currentHealth = currentHealth - damage;
            enemyHealthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public override void TakeDamage(int damage, string damageAnimation = "Damage_01")
        {

            if (isDead)
                return;
            base.TakeDamage(damage, damageAnimation = "Damage_01");



            if (!isBoss)
            {
                currentHealth = currentHealth - damage;
                enemyHealthBar.SetHealth(currentHealth);
                GameObject newhitvfx = Instantiate(hitvfx, capsuleCollider.transform);
            }
            else if (isBoss && enemyBossManager != null)
            {
                currentHealth = currentHealth - damage;
                enemyBossManager.UpdateBossHealthBar(currentHealth,  maxHealth);
                GameObject newhitvfx = Instantiate(hitvfx, capsuleCollider.transform);


            }

            enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);

            if (currentHealth <= 0 && !isBoss)
            {
                HandleDeath();
            }

            else if (currentHealth <= 0 && isBoss)
            {
                HandleDeath();
                ui.SetActive(true);
            }


        }

        private void HandleDeath()
        {
            currentHealth = 0;
            enemyAnimatorManager.PlayTargetAnimation("Dead_01", true);
            isDead = true;
        }
    }
}