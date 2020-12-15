using UnityEngine;

public class Player : MonoBehaviour
{
    private const float CHANGE_POINT_DISTANCE = 0.001f;

    private Path _path;
    
    [SerializeField] private float Speed = 1;

    private bool _isMove;

    private bool _isDead;

    private void Awake()
    {
        
    }

    public void InjectPath(Path path)
    {
        if (_path != null)
        {
            _path.OnPathEnded -= OnPathEnded;
        }
        
        _path = path;
        _path.OnPathEnded += OnPathEnded;
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
        var point = _path.CurrentPoint;
        var distance = (point - transform.position).magnitude;
        
        if (distance < CHANGE_POINT_DISTANCE)
        {
            _path.ChangePoint();
            point = _path.CurrentPoint;
            distance = (point - transform.position).magnitude;
        }
        
        var deltaDistance = Time.deltaTime * Speed;

        var t = deltaDistance / distance;

        transform.position = Vector3.Lerp(transform.position, point, t);
    }

    private void OnPathEnded()
    {
        Stop();
        EventManager.NotifyPlayerPathEnded();
    }

    private void OnDestroy()
    {
        _path.OnPathEnded -= OnPathEnded;
    }
}
