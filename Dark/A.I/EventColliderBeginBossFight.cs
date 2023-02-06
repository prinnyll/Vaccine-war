using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EventColliderBeginBossFight : MonoBehaviour
    {
        WorldEventManager worldEventManager;
        public AudioSource BOSS;
        public GameObject BGMUSIC;
        private void Awake()
        {
            worldEventManager = FindObjectOfType<WorldEventManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                worldEventManager.ActivateBossFight();
                BOSS.Play();
                Destroy(BGMUSIC);
            }
        }
    }
}
