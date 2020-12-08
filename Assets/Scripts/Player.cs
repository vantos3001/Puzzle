using UnityEngine;

public class Player : MonoBehaviour
{
    private const float CHANGE_POINT_DISTANCE = 0.001f;
    
    [SerializeField] private Path Path;

    [SerializeField] private float Speed = 1;

    private bool _isDead;

    public bool IsDead
    {
        get => _isDead;
        set
        {
            if (_isDead == value) return;

            _isDead = value;
            
            if (_isDead)
            {
                Debug.Log("IS DEAD NOW");
            }
        }
    }
    
    private void Update()
    {
        if (!_isDead)
        {
            Move();
        }
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
    
}
