using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManagerScript : MonoBehaviour
{
    private enum Direction {
        Up,
        Left,
        Right,
        Down
    };
    
    private Dictionary<Direction, float> _dirAngles = new Dictionary<Direction, float>()
    {
        { Direction.Up, 0f },
        { Direction.Left, 90f },
        { Direction.Right, 270f },
        { Direction.Down, 180f },
    };
    
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float baseSpawnCooldown;
    [SerializeField] private float spawnCooldownOffset;
    [SerializeField] private float spawnXOffset;
    [SerializeField] private float spawnYOffset;
    [SerializeField] private float cooldownScaling;
    [SerializeField] private float speedScaling;

    private GameObject _arrowPrefab;
    private Coroutine _arrowSpawner;
    
    void Start()
    {
        _arrowPrefab = Resources.Load<GameObject>("Prefabs/ArrowPrefab");
        _arrowSpawner = StartCoroutine("ArrowSpawner");
    }

    IEnumerator ArrowSpawner()
    {
        while (true)
        {
            SpawnRandomArrow();
            UpdateScaling();
            float delay = baseSpawnCooldown + Random.Range(-spawnCooldownOffset, spawnCooldownOffset);
            yield return new WaitForSeconds(delay);
        }
    }

    void UpdateScaling()
    {
        baseSpawnCooldown *= Random.Range(cooldownScaling, 1);
        arrowSpeed *= Random.Range(1, speedScaling);
    }

    // void SpawnRandomArrowManager()
    // {
    //     SpawnRandomArrow();
    //     UpdateScaling();
    //     float delay = baseSpawnCooldown + Random.Range(-spawnCooldownOffset, spawnCooldownOffset);
    // }

    void SpawnRandomArrow()
    {
        Direction direction = (Direction)Random.Range(0, 4);
        SpawnArrow(direction);
    }

    void SpawnArrow(Direction direction)
    {
        Vector3 newPosition = (Vector3)GetNewArrowPos(direction);
        GameObject newArrow = Instantiate(_arrowPrefab, newPosition, Quaternion.Euler(0, 0, _dirAngles[direction]));
    }

    Vector2 GetNewArrowPos(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector2.down * spawnYOffset;
            case Direction.Left:
                return Vector2.right * spawnXOffset;
            case Direction.Right:
                return Vector2.left * spawnXOffset;
            case Direction.Down:
                return Vector2.up * spawnYOffset;
            default:
                // Debug.Log("Invalid Direction supplied to GetNewArrowPos");
                return Vector2.zero;
        }
    }
}
