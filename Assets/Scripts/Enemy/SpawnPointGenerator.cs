using UnityEngine;

public class SpawnPointGenerator
{
    private float _spawnDistance;

    public SpawnPointGenerator(float spawnDistance)
    {
        _spawnDistance = spawnDistance;
    }

    public Vector3 Generate(Vector3 playerPosition)
    {
        float randomPositionX = Random.Range(playerPosition.x - _spawnDistance, playerPosition.x + _spawnDistance);
        float randomPositionZ = Random.Range(playerPosition.z - _spawnDistance, playerPosition.z + _spawnDistance);

        Vector3 enemySpawnPoint = new Vector3(randomPositionX, playerPosition.y, randomPositionZ);
        
        return enemySpawnPoint;
    }
}
