using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum Direction {
        Up,
        Left,
        Right,
        Down
    };

    private Dictionary<Direction, float> dirAngles = new Dictionary<Direction, float>()
    {
        { Direction.Up, 0f },
        { Direction.Left, 90f },
        { Direction.Right, 270f },
        { Direction.Down, 180f },
    };

    [SerializeField] private Direction direction = Direction.Up;
    [SerializeField] private float rotationSpeed;

    private bool _isNewDirection = false;
    private Transform _shieldPivotTransform;
    
    void Start()
    {
        _shieldPivotTransform = transform.GetChild(0);
    }

    void Update()
    {
        DirectionControl();
    }

    void DirectionControl()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && direction != Direction.Up)
        {
            direction = Direction.Up;
            _isNewDirection = true;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && direction != Direction.Left)
        {
            direction = Direction.Left;
            _isNewDirection = true;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && direction != Direction.Down)
        {
            direction = Direction.Down;
            _isNewDirection = true;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && direction != Direction.Right)
        {
            direction = Direction.Right;
            _isNewDirection = true;
        }

        if (UpdateAngle() && _isNewDirection)
        {
            _isNewDirection = false;
            // Debug.Log("Target Rotation reached!");
        }
    }

    bool UpdateAngle()
    {
        Vector3 currentAngles = _shieldPivotTransform.rotation.eulerAngles;
        float newAngle = Mathf.MoveTowardsAngle(currentAngles.z, dirAngles[direction], rotationSpeed*Time.deltaTime);
        
        if (Math.Abs(currentAngles.z - dirAngles[direction]) < 0.01f)
        {
            return true;
        }

        currentAngles.z = newAngle;
        _shieldPivotTransform.eulerAngles = currentAngles;
        return false;
    }
}
