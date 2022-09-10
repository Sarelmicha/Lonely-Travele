public class MovementSwing
{
    public float Burst { get; private set; }
    public float Duration { get; private set; }
    public MovementSwing(float burst, float duration)
    {
        Burst = burst;
        Duration = duration;
    }
}
