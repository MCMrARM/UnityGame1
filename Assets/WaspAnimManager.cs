using Mahou.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
