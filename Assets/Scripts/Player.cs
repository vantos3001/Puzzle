using UnityEngine;

public class Player : MonoBehaviour
{
    private const float CHANGE_POINT_DISTANCE = 0.001f;
    
    [SerializeField] private Path Path;

    [SerializeField] private float Speed = 1;

    private bool _isMove;

    private bool _isDead;

    private void Awake()
    {
        Path.OnPathEnded += OnPathEnded;
    }

    private void Update()
    {
        if (_isMove)
        {
            Move();
        }
    }

    public void Die()
    {
        if(_isDead) return;

        _isDead = true;
        Debug.Log("IS DEAD NOW");

        Stop();
        
        EventManager.NotifyPlayerDead();
    }

    public void Stop()
    {
        _isMove = false;
    }

    public void StartMove()
    {
        _isMove = true;
    }

    private void Move()
    {
        var point = Path.CurrentPoint;
        var distance = (point.position - transform.position).magnitude;
        
        if (distance < CHANGE_POINT_DISTANCE)
        {
            Path.ChangePoint();
            point = Path.CurrentPoint;
            distance = (point.position - transform.position).magnitude;
        }
        
        var deltaDistance = Time.deltaTime * Speed;

        var t = deltaDistance / distance;

        transform.position = Vector3.Lerp(transform.position, point.transform.position, t);
    }

    private void OnPathEnded()
    {
        Stop();
        EventManager.NotifyPlayerPathEnded();
    }

    private void OnDestroy()
    {
        Path.OnPathEnded -= OnPathEnded;
    }
}
