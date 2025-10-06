using UnityEngine;

namespace Code.Spawners.Enemy
{
    public class EnemyFabric
    {
        private readonly Player.Player _player;

        public EnemyFabric(Player.Player player)
        {
            _player = player;
        }
        
        public Code.Enemy.Enemy Create(Code.Enemy.Enemy enemy)
        {
            Code.Enemy.Enemy doneEnemy = Object.Instantiate(enemy);
            
            doneEnemy.Initialize(_player);

            return doneEnemy;
        }
    }
}