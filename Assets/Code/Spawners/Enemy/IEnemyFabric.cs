namespace Code.Spawners.Enemy
{
    public interface IEnemyFabric
    {
        Code.Enemy.Enemy Create(Code.Enemy.Enemy enemy);
    }
}