using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] private float enemySpeed;
    [SerializeField] private bool positive;
    private int directionScale;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }

        if (positive)
            directionScale = 1;
        else
            directionScale = -1;
    }

    private void DirectionChange()
    {
        anim.SetBool("walking", false);
        movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        anim.SetBool("walking", true);

        // Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * directionScale * _direction, initScale.y, initScale.z);

        // Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * enemySpeed, enemy.position.y, enemy.position.z);
    }
}
