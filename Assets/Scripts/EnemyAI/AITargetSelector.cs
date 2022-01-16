using Mahou.Combat;
using Mahou.Config;
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

        public float alertExpireTime = 1f;

        private GameObject _alertAttacker;
        private float _alertCurrentExpire;

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
                    if (_damageSum[e.Key] <= 0)
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
                int advanceCount = Mathf.CeilToInt((Time.time - _currentDamageFrameEnd) / damageFrameTime);
                int i = Math.Min(advanceCount, damageFrameCount);
                for (; i > 0; --i)
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
            prevTotalDmg = 0f;
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
            UpdateAlert();

            if (_topAttacker != null)
                return _topAttacker.gameObject;
            var closest = _foeLocator.GetClosestFoe();
            if (closest != null)
                return closest;
            return _alertAttacker;
        }

        private void UpdateAlert()
        {
            if (_alertCurrentExpire != 0f && Time.time > _alertCurrentExpire)
            {
                _alertAttacker = null;
                _alertCurrentExpire = 0f;
            }
        }

        public void OnAlert(GameObject attacker)
        {
            _alertAttacker = attacker;
            _alertCurrentExpire = Time.time + 1f;
        }

        // TODO: should this be here?
        public void AlertNearby(float range, GameObject attacker)
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, range, LayerMasks.MaskEnemy);
            foreach (Collider c in enemiesInRange)
            {
                AITargetSelector selector;
                if (c.TryGetComponent<AITargetSelector>(out selector))
                {
                    selector.OnAlert(attacker);
                }
            }
        }

    }
}
