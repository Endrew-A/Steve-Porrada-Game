using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpScript : MonoBehaviour
{
    public static LevelUpScript Instance { get; private set; }

    public Button[] lvlup_button = new Button[4];

    public bool in_stats = false;

    public AudioSource upgrade;

    int coin;//PlayerPrefs.GetInt("coin_qtd");
    private void Awake()

    {

        if (Instance != null && Instance != this)

        {

            Destroy(this);

        }

        else

        {

            Instance = this;

        }


    }
    // Start is called before the first frame update
    void Start()
    {
        coin = PlayerPrefs.GetInt("coin_qtd");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (in_stats)
        {
            if (coin <= 4)
            {
                for(int i =0; i <= 3; i++)
                {
                    lvlup_button[i].interactable = false;
                }
            }
        }
    }

    public void LevelUp(int stat)
    {
        upgrade.Play();
        if(stat == 1) { int newhp = PlayerPrefs.GetInt("hp"); newhp += 1; PlayerPrefs.SetInt("hp", newhp); coin -= 5; PlayerPrefs.SetInt("coin_qtd", coin); }
        if(stat == 2) { int newatkdmg = PlayerPrefs.GetInt("attack_dmg"); newatkdmg += 1; PlayerPrefs.SetInt("attack_dmg", newatkdmg); coin -= 5; PlayerPrefs.SetInt("coin_qtd", coin); }
        if(stat == 3) { float newattackspeed = PlayerPrefs.GetFloat("attack_speed");  newattackspeed -= 0.5f; PlayerPrefs.SetFloat("attack_speed", newattackspeed); coin -= 5; PlayerPrefs.SetInt("coin_qtd", coin); }
        if(stat == 4) { float newrange = PlayerPrefs.GetFloat("attack_range"); newrange += 0.5f; PlayerPrefs.SetFloat("attack_range", newrange); coin -= 5; PlayerPrefs.SetInt("coin_qtd", coin); }
        UI.Instance.RenewStats();
    }
}
