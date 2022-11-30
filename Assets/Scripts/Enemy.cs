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
    private GameObject player;
    private float dist;
    public float close;
    public float moveSpeed;
    public float enemySpeed = 1f;
    public float enemyRotateSpeed = 3f;
    private Vector3 dir;
    public float waitTime = 2f;
    private Animator  anim;
    // Start is called before the first frame update
    void Start()
    {
        enemyTransform = gameObject.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();

        enemyHeight = enemyTransform.position.y;

        direction = TargetPosition - enemyTransform.position;
        finalDirection = Quaternion.LookRotation(direction);
        newTarget();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(enemyTransform.position == TargetPosition && targetChanged)
        {
            Invoke("newTarget", waitTime);
            targetChanged = false;
            anim.SetBool("Walk_Anim", false);
            
        }

        dist = Vector3.Distance(player.transform.position, transform.position);
        dir = transform.position - player.transform.position;
        anim.SetBool("Walk_Anim", true);
        
        // if(dist <= close){
        //     player.GetComponent<CharacterController>().SimpleMove(-1*dir.normalized*moveSpeed);
            
        // }

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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Andar ghusa");
       if(other.CompareTag("Player"))
       {
        Debug.Log("Player hai");
        dir = transform.position - other.gameObject.transform.position;
        Debug.Log(dir);
        Debug.Log(other.gameObject.GetComponent<CharacterController>());
        other.gameObject.GetComponent<CharacterController>().SimpleMove(-1*dir.normalized*moveSpeed);
       }
    }

    
}


