using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Absorption")]
        public float physicalDamageAbsorption;


        [Header("Idle Animations")]
        public string right_hand_idle;
        public string left_hand_idle;
        public string th_idle;

        [Header("Attack Animations")]
        public string oh_light_attack_01;
        public string oh_light_attack_02;
        public string oh_light_attack_03;
        public string oH_Heavy_Attack_1;

        public string th_light_attack_01;
        public string th_light_attack_02;

        public string Unarmed_Attack_01;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;
        public float UnarmedAttackMultiplier;

        [Header("Weapon Type")]
        public bool isSpellCaster;
        public bool isFaithCaster;
        public bool isPyroCaster;
        public bool isMeleeWeapon;

    }
}
