using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    EntityStats player_stats;
    public GameObject projectile, ultraprojectile;
    bool can_attack;
    public int unlock_ultra;
    float cooldown=3;
    GameObject target_enemy;

    public AudioSource attack;
    public AudioSource porrada;
    
    // Start is called before the first frame update
    void Start()
    {
        player_stats = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();
        unlock_ultra = PlayerPrefs.GetInt("unlock_ultra");
    }

    // Update is called once per frame
    void Update()
    {
        target_enemy = Nearest_enemy();
        if(Input.GetKeyDown(KeyCode.V) && can_attack && target_enemy != null)
        {
            GameObject projectile_instance = Instantiate(projectile, transform.position, Quaternion.identity);
            projectile_instance.GetComponent<ProjectileDamage>().projectile_dmg = player_stats.attack_dmg;
            projectile_instance.GetComponent<ProjectileDamage>().projectile_lifespan = player_stats.attack_lifespan;           

            Vector2 projectile_direction = target_enemy.transform.position - transform.position;
            projectile_direction.Normalize();

            float rot_z = Mathf.Atan2(projectile_direction.y, projectile_direction.x) * Mathf.Rad2Deg;
            projectile_instance.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

            projectile_instance.GetComponent<Rigidbody2D>().AddForce(projectile_direction * player_stats.attack_range, ForceMode2D.Impulse);

            attack.Play();

            can_attack = false;
            cooldown = 0;
        }
        else if(Input.GetKeyDown(KeyCode.C) && can_attack && target_enemy != null && unlock_ultra==1)
        {
            GameObject projectile_instance = Instantiate(ultraprojectile, new Vector2(transform.position.x,transform.position.y+1), Quaternion.identity);
            projectile_instance.GetComponent<UltraProjectileDamage>().projectile_dmg = player_stats.attack_dmg;
            projectile_instance.GetComponent<UltraProjectileDamage>().projectile_lifespan = player_stats.attack_lifespan;

            Vector2 projectile_direction = target_enemy.transform.position - transform.position;
            projectile_direction.Normalize();

            float rot_z = Mathf.Atan2(projectile_direction.y, projectile_direction.x) * Mathf.Rad2Deg;
            projectile_instance.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

            projectile_instance.GetComponent<Rigidbody2D>().AddForce(projectile_direction * player_stats.attack_range, ForceMode2D.Impulse);

            attack.Play();

            can_attack = false;
            cooldown = 0;
        }
        Cooldown();
    }

    void Cooldown()
    {
        if(cooldown >= player_stats.attack_speed)
        {
            can_attack = true;
        }
        else { cooldown += Time.deltaTime; }
    }

    GameObject Nearest_enemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
       
        float closest_distance = Mathf.Infinity;
        float distance;
        foreach(GameObject enemy in enemies)
        {
            distance = Vector3.Distance(this.gameObject.transform.position, enemy.transform.position);
            if (distance < closest_distance)
            {
                closest_distance = distance;
                target_enemy = enemy;
            }
            }
        if (target_enemy != null){
                if(target_enemy.transform.position.x <= gameObject.transform.position.x+15 && target_enemy.transform.position.x >= gameObject.transform.position.x - 15)
                {
                    return target_enemy;
                }else { return null; }
        }
            else { return null; }
    }
}
