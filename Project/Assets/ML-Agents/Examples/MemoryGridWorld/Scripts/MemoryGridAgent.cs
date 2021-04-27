using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MemoryGridAgent : Agent
{
    [SerializeField]
    private float _walkDistance = 1.3f;
    private Vector3 _walkDirection = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector2[] _directions = new Vector2[] {Vector2.right, Vector2.down, Vector2.left, Vector2.up};
    private int _currentDir = 0;
    [SerializeField]
    public MemoryGridArea _gridArea;
    [SerializeField]
    public Transform[] _spawnPositions;

    public override void OnEpisodeBegin()
    {
        // Reset grid
        _gridArea.Reset();
        // Random spawn position
        transform.position = _spawnPositions[Random.Range(0, _spawnPositions.Length)].position;
        // Random spawn rotation
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _currentDir = 0;
        for (int i = 0; i < Random.Range(0, 4); i++)
        {
            _currentDir++;
            RotateRight();
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Execute action
        int action = actions.DiscreteActions[0];
        switch (action)
        {
            case 0:
                // Only move forward if there is no door up front
                //Debug.DrawRay(transform.position, _directions[_currentDir], Color.red, 10.0f);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, _directions[_currentDir], 1.0f);
                if (!hit)
                {
                    transform.Translate(_walkDirection * _walkDistance);
                }
                else
                {
                    if(hit.transform.tag.Equals("Finish"))
                    {
                        EndEpisode();
                    }
                    if(hit.transform.tag.Equals("goal"))
                    {
                        AddReward(ComputeSuccessReward());
                        EndEpisode();
                    }
                }
                break;
            case 1:
                RotateLeft();
                if (_currentDir == 0)
                    _currentDir = 3;
                else
                    _currentDir--;
                break;
            case 2:
                RotateRight();
                if (_currentDir == 3)
                    _currentDir = 0;
                else
                    _currentDir++;
                break;
            case 3:
                // do nothing
                break;
        }

        // Test game over

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = 3; // Do nothing
        if (Input.GetKeyDown(KeyCode.W))
        {
            discreteActionsOut[0] = 0;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            discreteActionsOut[0] = 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            discreteActionsOut[0] = 2;
        }
    }

    private float ComputeSuccessReward()
    {
        return 1.0f - (0.9f * ((float)StepCount / (float)MaxStep));
    }

    public void RotateRight()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, -90.0f));
    }

    public void RotateLeft()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f));
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
