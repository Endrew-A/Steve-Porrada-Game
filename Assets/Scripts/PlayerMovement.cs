using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float move_speed;
    public bool can_jump = true;
    Animator animator_;
    public EntityStats player_stats;

    public AudioSource footsteps;
    
    // Start is called before the first frame update
    void Start()
    {
        animator_ = gameObject.GetComponent<Animator>();
        if(animator_ == null) { Debug.Log("null"); }
        move_speed = player_stats.base_move;
        footsteps.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UI.Instance.Pause();
        }
    }

    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontal * move_speed * Time.deltaTime, 0));
        
        if (Input.GetKeyDown(KeyCode.W) && can_jump == true)
        {
            
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * move_speed);
            can_jump = false;
            animator_.SetBool("can_jump", can_jump);
            footsteps.volume = 0;
            AudioControl.Instance.audios[5].Play();
        }

        if((horizontal > 0 || horizontal < 0) && can_jump == true)
        {
            if (horizontal < 0) { gameObject.GetComponent<SpriteRenderer>().flipX = true; } else { gameObject.GetComponent<SpriteRenderer>().flipX = false; }
            animator_.Play("walk");
            float volume = PlayerPrefs.GetFloat("volume");
            footsteps.volume = 1 * volume;
        }
        else if(horizontal == 0 && can_jump ==true)
        {
            animator_.Play("idle");
            footsteps.volume = 0;
        }

        if(animator_.GetBool("can_jump") == false)
        {
            if (horizontal < 0) { gameObject.GetComponent<SpriteRenderer>().flipX = true; } else if(horizontal>0) { gameObject.GetComponent<SpriteRenderer>().flipX = false; }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            can_jump = true;
            animator_.SetBool("can_jump", can_jump);
        }
        if(collision.gameObject.tag == "House")
        {
            transform.position += new Vector3(0.5f,0,0);
        }
    }
}
