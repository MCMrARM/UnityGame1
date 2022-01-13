using Mahou.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mahou.EnemyAI
{
    [RequireComponent(typeof(AIFoeLocator), typeof(EnemyDamageReceiver))]
    class AITargetSelector : MonoBehaviour
    {

        private AIFoeLocator _foeLocator;
        private EnemyDamageReceiver _damageReceiver;

        public int damageFrameCount = 5;
        public float damageFrameTime = 3f;

        private Dictionary<StatManager, float>[] _damageFrames;
        private Dictionary<StatManager, float> _damageSum = new Dictionary<StatManager, float>();
        private int _currentDamageFrame = 0;
        private float _currentDamageFrameEnd = 0;

        private StatManager _topAttacker;

        private void Start()
        {
            _foeLocator = GetComponent<AIFoeLocator>();
            _damageFrames = new Dictionary<StatManager, float>[damageFrameCount];
            _currentDamageFrameEnd = Time.time;
        }

        private void OnEnable()
        {
            _damageReceiver = GetComponent<EnemyDamageReceiver>();
            _damageReceiver.DamageReceived += OnDamageReceived;
        }

        private void OnDisable()
        {
            _damageReceiver.DamageReceived -= OnDamageReceived;
        }

        private void AdvanceDamageFrame()
        {
            _currentDamageFrame = (_currentDamageFrame + 1) % damageFrameCount;
            if (_damageFrames[_currentDamageFrame] != null)
            {
                foreach (var e in _damageFrames[_currentDamageFrame])
                {
                    _damageSum[e.Key] -= e.Value;
                    if (_damageSum[e.Key] == 0)
                        _damageSum.Remove(e.Key);
                }
                _damageFrames[_currentDamageFrame].Clear();
            }
            _topAttacker = null;
        }

        private void AdvanceDamageFrames()
        {
            if (Time.time >= _currentDamageFrameEnd)
            {
                int advanceCount = (int)((Time.time - _currentDamageFrameEnd) / damageFrameTime);
                advanceCount = Math.Min(advanceCount, damageFrameCount);
                for (; advanceCount > 0; --advanceCount)
                    AdvanceDamageFrame();
                _currentDamageFrameEnd += advanceCount * damageFrameTime;
            }
        }

        private void OnDamageReceived(DamageType type, float dmg, float finalDmg, StatManager attacker)
        {
            if (attacker == null)
                return;

            AdvanceDamageFrames();
            if (_damageFrames[_currentDamageFrame] == null)
                _damageFrames[_currentDamageFrame] = new Dictionary<StatManager, float>();
            float prevTotalDmg = 0f;
            _damageFrames[_currentDamageFrame].TryGetValue(attacker, out prevTotalDmg);
            _damageFrames[_currentDamageFrame][attacker] = prevTotalDmg + finalDmg;
            _damageSum.TryGetValue(attacker, out prevTotalDmg);
            _damageSum[attacker] = prevTotalDmg + finalDmg;
            if (_topAttacker != null && _topAttacker != attacker && _damageSum[attacker]  > _damageSum[_topAttacker])
                _topAttacker = null;
        }

        private void UpdateTopAttacker()
        {
            AdvanceDamageFrames();
            if (_topAttacker == null && _damageSum.Count > 0)
            {
                float maxVal = -1f;
                foreach (var e in _damageSum)
                {
                    if (e.Value > maxVal)
                    {
                        _topAttacker = e.Key;
                        maxVal = e.Value;
                    }
                }
            }
        }

        public GameObject GetTarget()
        {
            UpdateTopAttacker();
            if (_topAttacker != null)
                return _topAttacker.gameObject;
            return _foeLocator.GetClosestFoe();
        }

    }
}
