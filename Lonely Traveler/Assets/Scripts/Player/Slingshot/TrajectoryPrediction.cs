using System.Collections.Generic;
using HappyFlow.LonelyTraveler.Player;
using UnityEngine;

/// <summary>
/// This class responsible for handling the trajectory prediction of the slingshot and the player
/// </summary>
public class TrajectoryPrediction : MonoBehaviour
{
    [SerializeField] private Slingshot m_Slingshot;
    [SerializeField] private float _collisionCheckRadius = 500f; 
    [SerializeField] private float m_MaxDuration = 50; //INPUT amount of total time for simulation
    [SerializeField] private Rigidbody2D m_Target;

    
    private LineRenderer m_LineRenderer; //Line to predict trajectory

    private float m_Force;

    private const float m_TimeStepInterval = 0.1f; //INPUT amount of time between each position check
    private int m_MaxSteps;//Calculates amount of steps simulation will iterate for
    private List<Vector2> lineRendererPoints;

    /// <summary>
    /// Initialize the <see cref="TrajectoryPrediction"/> component.
    /// </summary>
    /// <param name="force">The force with which the player is thrown</param>
    public void Initialize(float force)
    {
        m_Force = force;
    }
    
    private void Awake()
    {
        m_Slingshot.SubscribeOnTargetDraggingEvent(DrawTrajectory);
        m_Slingshot.SubscribeOnTargetReleasedEvent(CleanTrajectoryPrediction);
        m_MaxSteps = (int)(m_MaxDuration / m_TimeStepInterval);
    }
    
    private void OnDestroy()
    {
        m_Slingshot.UnsubscribeOnTargetDraggingEvent(DrawTrajectory);
        m_Slingshot.UnsubscribeOnTargetReleasedEvent(CleanTrajectoryPrediction);
    }

    private void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }
    
    private void DrawTrajectory(Vector3 direction)
    {
        var trajectory = SimulateArc(direction * m_Force, m_MaxSteps);
        m_LineRenderer.positionCount = trajectory.Length;
        
        for (var i = 0; i < m_LineRenderer.positionCount; i++)
        {
            m_LineRenderer.SetPosition(i, trajectory[i]); //Add each Calculated Step to a LineRenderer to display a Trajectory. Look inside LineRenderer in Unity to see exact points and amount of them
        }
    }

    private void CleanTrajectoryPrediction(Vector3 direction)
    {
        m_LineRenderer.positionCount = 0;
    }

    private Vector2[] SimulateArc(Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timeStep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * m_Target.gravityScale * timeStep * timeStep;

        float drag = 1f - timeStep * m_Target.drag;
        Vector2 moveStep = velocity * timeStep;
        Vector2 position = m_Target.transform.position;

        for (var i = 0; i < steps; i++)
        {
            moveStep = CalculateStep(moveStep, gravityAccel, drag, ref position);

            // if (CheckForCollision(moveStep))
            // {
            //     break;
            // }

            results[i] = position;
        }

        return results;
    }

    private Vector2 CalculateStep(Vector2 moveStep, Vector2 gravityAccel, float drag, ref Vector2 position)
    {
        moveStep += gravityAccel;
        moveStep *= drag;
        position += moveStep;
        return moveStep;
    }


    private bool CheckForCollision(Vector2 position)
    {   
        Collider2D hits = Physics2D.OverlapCircle(position, _collisionCheckRadius); //Measure collision via a small circle at the latest position, dont continue simulating Arc if hit
        
        return hits != null; //Return true if something is hit, stopping Arc simulation
    }
}
