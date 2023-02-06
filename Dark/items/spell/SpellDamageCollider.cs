using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class SpellDamageCollider : DamageCollider
    {
        public GameObject impactParticles;
        public GameObject projectilParticles;
        public GameObject muzzleParticles;

        CharacterStats spellTarget;
        Rigidbody rigidbody;

        bool hasCollided = false;

        Vector3 impactNormal;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            projectilParticles = Instantiate(projectilParticles, transform.position, transform.rotation);
            projectilParticles.transform.parent = transform;

            if (muzzleParticles)
            {
                muzzleParticles = Instantiate(muzzleParticles, transform.position, transform.rotation);
                Destroy(muzzleParticles, 2f);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!hasCollided)
            {
                spellTarget = other.transform.GetComponent<CharacterStats>();

                if(spellTarget != null)
                {
                    spellTarget.TakeDamage(currentWeaponDamage);
                }

                hasCollided = true;
                impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

                Destroy(projectilParticles);
                Destroy(impactParticles, 5f);
                Destroy(gameObject, 2f);
            }
        }
    }
}
