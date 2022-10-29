using System;
using System.Collections;
using System.Collections.Generic;
using HappyFlow.LonelyTraveler.Player;
using UnityEngine;

//CANNON TRAJECTORY PROJECTION METHOD

//Position(At Specified Time) = Initial Positions of X and Y multiplied by Speed, then subtract Gravity Factor from Y
//f(t) = (x0 + x*t, y0 + y*t - 9.81t²/2)
//In essence, move X and Y at a constant speed, subtract Gravity from Y

//Variables to decide:
    //Force, Mass, Origin & Direction in which to launch objects
    //Simulation Length, Step Measurement Interval
//Variables to calculate: 
    //Max Steps, Velocity, and Position at each Step :)

//Currently does not account for 3-D, Drag, Gravity Scale, Bounces, or Ground Velocity

public class TrajectoryPrediction : MonoBehaviour
{
    [SerializeField] private Slingshot m_Slingshot;
    
    private LineRenderer _lr; //Line to predict trajectory

    public float _force { get; set; } //Force, can be assigned in Unity Inspector
    public float _mass; //Automatic mass of an object is 1, can be reassigned
    private float _vel; //Initial Velocity, calculated via V = Force / Mass * fixedTime (0.02)
    private float _gravity;
    private float _collisionCheckRadius = 100f; //Collision radius of last point on SimulationArc, to communicate with it when to stop. Currently using IgnoreRaycast Layer on some objects, suboptimal
    private float maxDuration = 10f; //INPUT amount of total time for simulation
    private float timeStepInterval = 0.1f; //INPUT amount of time between each position check
    private int maxSteps;//Calculates amount of steps simulation will iterate for
    private List<Vector2> lineRendererPoints;

    private void Awake()
    {
        m_Slingshot.SubscribeOnTargetDraggingEvent(DrawTrajectory);
        m_Slingshot.SubscribeOnTargetReleasedEvent(CleanTrajectoryPrediction);
        maxSteps = (int)(maxDuration / timeStepInterval);
        lineRendererPoints = new List<Vector2>();
        _gravity = Physics2D.gravity.y;
    }

    private void OnDestroy()
    {
        m_Slingshot.UnsubscribeOnTargetDraggingEvent(DrawTrajectory);
        m_Slingshot.UnsubscribeOnTargetReleasedEvent(CleanTrajectoryPrediction);
    }

    void Start()
    {
        _lr = GetComponent<LineRenderer>();
    }
    
    void DrawTrajectory(Vector3 direction)
    {
        var a = SimulateArc(direction);
        _lr.positionCount = a.Count;
        
        for (var i = 0; i < _lr.positionCount; i++)
        {
            _lr.SetPosition(i, a[i]); //Add each Calculated Step to a LineRenderer to display a Trajectory. Look inside LineRenderer in Unity to see exact points and amount of them
        }
    }

    private void CleanTrajectoryPrediction(Vector3 direction)
    {
        _lr.positionCount = 0;
    }

    private List<Vector2> SimulateArc(Vector3 direction) //A method happening via this List
    {
        lineRendererPoints.Clear(); //Reset LineRenderer List for new calculation
        
        Vector3 launchPosition = transform.position + direction; //INPUT launch origin (Important to make sure RayCast is ignoring some layers (easiest to use default Layer 2))
        
        _vel = _force / _mass * Time.fixedDeltaTime; //Initial Velocity, or Velocity Modifier, with which to calculate Vector Velocity

        for (var i = 0; i < maxSteps; ++i)
        {
            Vector2 calculatedPosition = launchPosition + direction * _vel * i * timeStepInterval; //Move both X and Y at a constant speed per Interval
            calculatedPosition.y += _gravity / 2 * Mathf.Pow(i * timeStepInterval, 2); //Subtract Gravity from Y

            lineRendererPoints.Add(calculatedPosition); //Add this to the next entry on the list

            // if (CheckForCollision(calculatedPosition)) //if you hit something, stop adding positions
            // {
            //     break; //stop adding positions
            // }
        }
        return lineRendererPoints;
    }

    private bool CheckForCollision(Vector2 position)
    {   
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, _collisionCheckRadius); //Measure collision via a small circle at the latest position, dont continue simulating Arc if hit
        if (hits.Length > 0) //Return true if something is hit, stopping Arc simulation
        {   
            return true;
        }
        return false;
    }
}
