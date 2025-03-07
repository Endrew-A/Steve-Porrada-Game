using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{

    public bool turn;
    public float move_speed;
    public EntityStats enemy_stats;
    // Start is called before the first frame update
    void Start()
    {
        move_speed = enemy_stats.base_move;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (turn)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            transform.position += new Vector3(move_speed*Time.deltaTime, 0, 0);
        }else if (!turn)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            transform.position += new Vector3(-move_speed*Time.deltaTime, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Turn")
        {
            turn = !turn;
            
        }
    }
}
