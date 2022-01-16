using Mahou.Combat;
using UnityEngine;

namespace Mahou
{
    public class PlayerManager : Singleton<PlayerManager>
    {

        private GameObject _player;
        private StatManager _playerStat;
        private PlayerInventoryController _playerInventoryController;


        private void EnsurePlayer()
        {
            if (_player == null)
            {
                _player = FindObjectOfType<PlayerMovementController>()?.gameObject;
                if (_player != null)
                {
                    _playerStat = _player.GetComponent<StatManager>();
                    _playerInventoryController = _player.GetComponent<PlayerInventoryController>();
                }
            }
        }

        public GameObject Player
        {
            get {
                EnsurePlayer();
                return _player;
            }
        }

        public StatManager PlayerStat
        {
            get
            {
                EnsurePlayer();
                return _playerStat;
            }
        }

        public PlayerInventoryController PlayerInventory
        {
            get
            {
                EnsurePlayer();
                return _playerInventoryController;
            }
        }


    }
}
