using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform centerTransform;
    private Transform enemyTransform;
    private Vector3 TargetPosition;
    private Vector3 direction;
    private Quaternion finalDirection;
    private float enemyHeight;
    private bool targetChanged = true;
    public float radius = 5f;
    private Transform player;
    private float dist;
    public float close;
    public float moveSpeed;
    public float enemySpeed = 1f;
    public float enemyRotateSpeed = 3f;
    private Vector3 dir;
    public float waitTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        enemyTransform = gameObject.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemyHeight = enemyTransform.position.y;

        direction = TargetPosition - enemyTransform.position;
        finalDirection = Quaternion.LookRotation(direction);
        newTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyTransform.position == TargetPosition && targetChanged)
        {
            Invoke("newTarget", waitTime);
            targetChanged = false;
        }

        dist = Vector3.Distance(player.position, transform.position);
        dir = transform.position - player.position;
        
        if(dist <= close){
            // transform.LookAt(player);
            // GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
            player.position = player.position - (2 * dir);
            // Add melee combat lines and animation
        }

        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, TargetPosition, enemySpeed*Time.deltaTime);
        enemyTransform.rotation = Quaternion.RotateTowards(enemyTransform.rotation, finalDirection, enemyRotateSpeed * Time.deltaTime);
    }

    private void newTarget()
    {
        Vector2 vec2 = Random.insideUnitCircle.normalized * radius;
        TargetPosition = centerTransform.position + new Vector3(vec2.x, enemyHeight, vec2.y);

        direction = TargetPosition - enemyTransform.position;
        finalDirection = Quaternion.LookRotation(direction);
        targetChanged = true;
    }
}
