using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PursueTargetState : State
    {
        public CombatStanceState combatStanceState;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            if (enemyManager.isPreformingAction)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                return this;
            }

            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float distanceFromTarget  = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

            if (distanceFromTarget > enemyManager.maximumAttackRange)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }

            HandleRotateTowardsTarget(enemyManager);
          

            if (distanceFromTarget <= enemyManager.maximumAttackRange)
            {
                return combatStanceState;
            }
            else
            {
                return this;
            }
        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
            //Rotate manually
            if (enemyManager.isPreformingAction)
            {
                Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
            //Rotate with pathfinding (navmesh)
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navmeshAgent.desiredVelocity);
                //Vector3 targetVelocity = enemyManager.enemyRigidBody.velocity;

                Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;

                direction.Normalize();
                direction.y = 0;

                float speed = 4;
                direction *= speed;

                Vector3 projectedVelocity = Vector3.ProjectOnPlane(direction, Vector3.up);
                Vector3 targetVelocity = projectedVelocity;

                enemyManager.navmeshAgent.enabled = true;
                enemyManager.navmeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.enemyRigidBody.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, enemyManager.navmeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
        }


        //private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        //{
        //    // Rotate manually
        //    if (enemyManager.isPreformingAction)
        //    {
        //        Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
        //        direction.y = 0;
        //        direction.Normalize();

        //        if (direction == Vector3.zero)
        //        {
        //            direction = transform.forward;
        //        }

        //        Quaternion targetRotation = Quaternion.LookRotation(direction);
        //        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
        //    }

        //    // Rotate with pathfinding (navmesh)
        //    else
        //    {
        //        Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navmeshAgent.desiredVelocity);
        //        if (enemyManager.distanceFromTarget > enemyManager.stoppingDistance)
        //        {
        //            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;

        //            direction.Normalize();
        //            direction.y = 0;

        //            float speed = 2;
        //            direction *= speed;

        //            Vector3 projectedVelocity = Vector3.ProjectOnPlane(direction, Vector3.up);
        //            Vector3 targetVelocity = projectedVelocity; // Everything in the IF statement from this line and above this line is new

        //            enemyManager.navmeshAgent.enabled = true;
        //            enemyManager.navmeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
        //            enemyManager.enemyRigidBody.velocity = targetVelocity;
        //            transform.rotation = Quaternion.Slerp(transform.rotation, enemyManager.navmeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
        //        }
        //    }
        //}
    }
}