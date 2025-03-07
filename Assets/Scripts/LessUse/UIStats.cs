using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStats : MonoBehaviour
{
    public static UIStats Instance { get; private set; }

    public EntityStats player_stats;

    int numHearts;
    public Image[] hearts;
    public Sprite FullHeart, EmptyHeart;
    public GameObject canvas;
    public Slider exp_bar;
    public Text number_coins;

    float waitTime=3;

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
        StartCoroutine(Active());
    }
    IEnumerator Active()
    {
        yield return new WaitForSeconds(waitTime);
        canvas.SetActive(true);
        SaveManager.Instance.LoadGame();
        player_stats.RenewHp();
        CoinsScript();        
    }

    // Update is called once per frame
    void Update()
    {
        if (player_stats.hp > numHearts)
        {
            numHearts = player_stats.hp;
        }
        //if(player_stats == null)
        //{
        //    canvas.SetActive(false);
        //}
        //else { canvas.SetActive(true); }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player_stats.hp)
            {
                hearts[i].sprite = FullHeart;
            }
            else { hearts[i].sprite = EmptyHeart; }

            if (i < numHearts)
            {
                hearts[i].enabled = true;
            }
            else { hearts[i].enabled = false; }
        }
        ExpBar();
    }

    void ExpBar()
    {
        exp_bar.value = player_stats.exp;
        exp_bar.maxValue = player_stats.level *50;
    }

    public void CoinsScript()
    {
        number_coins.text = player_stats.coin_qtd.ToString();
    }
}
