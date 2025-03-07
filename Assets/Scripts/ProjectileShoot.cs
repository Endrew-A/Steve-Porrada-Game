using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{
    public GameObject projectile_;
    GameObject player;
    bool can_attack= true;
    float cooldown;
    float wait = 3.1f;
    float dist;
    EntityStats enemy_stats;
    public bool isboss = false;
    public Animator animator_;

    public AudioSource attack,hit;
    

    // Start is called before the first frame update
    void Start()
    {
        enemy_stats = this.gameObject.GetComponent<EntityStats>();
        StartCoroutine(WaitTime());
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(wait);
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) { dist = Vector3.Distance(transform.position, player.transform.position); }
        if(can_attack && player!=null && dist <=6f)
        {
            
            GameObject projectile_instance = Instantiate(projectile_, transform.position, Quaternion.identity);
            projectile_instance.GetComponent<ProjectileDamage>().projectile_dmg = enemy_stats.attack_dmg;
            projectile_instance.GetComponent<ProjectileDamage>().projectile_lifespan = enemy_stats.attack_lifespan;
            //if (transform.position.x <= player.transform.position.x) { projectile_.GetComponent<SpriteRenderer>().flipX = true; } else { projectile_.GetComponent<SpriteRenderer>().flipX = false; }
            Vector2 projectile_direction = player.transform.position - transform.position;
            projectile_direction.Normalize();
            if (isboss) {
                float rot_z = Mathf.Atan2(projectile_direction.y, projectile_direction.x) * Mathf.Rad2Deg;
                projectile_instance.transform.rotation = Quaternion.Euler(0f, 0f, rot_z-45);
            }
            projectile_instance.GetComponent<Rigidbody2D>().AddForce(projectile_direction*enemy_stats.attack_range, ForceMode2D.Impulse);
            can_attack = false;
            if (isboss) { animator_.Play("attack"); }
            cooldown = 0;
            attack.Play();
        }
        Cooldown();
    }

    void Cooldown()
    {
        if(cooldown >= enemy_stats.attack_speed)
        {
            can_attack = true;
        }
        else { cooldown += Time.deltaTime; }
    }

}
