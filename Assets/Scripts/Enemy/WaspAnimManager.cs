using Mahou.Combat;
using UnityEngine;

namespace Mahou
{
    public class WaspAnimManager : MonoBehaviour
    {
        private StatManager _statManager;
        public Animator animator;

        void Start()
        {
            _statManager = GetComponent<StatManager>();
        }

        void Update()
        {
            animator.SetBool("Dead", _statManager.hp <= 0);
        }
    }
}