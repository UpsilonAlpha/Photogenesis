using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Photon : MonoBehaviour
{
    public float c;
    Vector2[] dirs = {new Vector2(1,1), new Vector2(1,-1), new Vector2(0,-1), new Vector2(-1,-1), new Vector2(-1,1), new Vector2(0,1),};
    public Rigidbody2D rb;
    public bool photonReleased = false;
    public float fireTime = 3;
    public Scrollbar progress;
    public Text hint;
    public Text score;
    public Text wavelength;
    public Text boostText;
    public CircleCollider2D col;
    public static Photon instance;
    public AudioManager speaker;
    public SpriteRenderer background;
    public Transform timeBar;
    public CanvasGroup gameOver;
    public InputManager input;
    public float height;
    public bool boosting;
    public bool recharging = true;
    public int boosts;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Update()
    {
        if(!photonReleased)
        {
            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }
        height = transform.position.y;
        rb.velocity = rb.velocity.normalized*c;
        Color tmp = background.color;
        tmp = Color.HSVToRGB((1-(height/1000) -0.1f),1,1);
        tmp.a = 0.7f;
        background.color = tmp;
        progress.value = height/1000;
        timeBar.localScale = new Vector2(fireTime*3f, timeBar.localScale.y);

        if (!boosting && height < background.transform.position.y-5)
        {
            input.Enable(gameOver);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            photonReleased = false;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            fireTime = 0.76f;
        }

        float d = Mathf.Pow(10, height/100) * 1E-16f;
        wavelength.text = d.ToString("E1");

        if (height < 300f)
        {
            score.text = "You're a gamma ray! You had a wavelength of: " + d.ToString("E1") + "m\nVery dangerous ionizing radiation.";
            PlayerPrefs.SetString("g", "true");
        }
        else if (height < 600f)
        {
            score.text = "You're an X-ray! You had a wavelength of: " + d.ToString("E1") + "m\nHighly energetic and useful for imaging.";
            PlayerPrefs.SetString("x", "true");
        }
        else if (height < 900f)
        {
            score.text = "You're UV light! You had a wavelength of: " + d.ToString("E1") + "m\nCauses fluroescence and only harmful with prolonged exposure.";
            PlayerPrefs.SetString("u", "true");
        }
        else if (height < 1000f)
        {
            score.text = "You're visible light! You had a wavelength of: " + d.ToString("E1") + "m\nThis is part of the small spectrum that humans can see.";
            PlayerPrefs.SetString("v", "true");
        }
        else
        {
            if (photonReleased)
                input.Enable(gameOver);
            score.text = "You escaped the sun as infra-red light! You had a wavelength of: " + d.ToString("E1") + "m\nThis accounts for almost half of light emitted by the sun.";
            PlayerPrefs.SetString("i", "true");
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            photonReleased = false;
        }

        if (boosting)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, transform.position.y + 100), Time.deltaTime);
        }
    }

    public void Revive()
    {
        photonReleased = true;
    }

    public void releasePhoton()
    {
        transform.parent = null;
        photonReleased = true;
        rb.isKinematic = false;
        rb.velocity = Vector2.up * c;
        hint.enabled = false;
    }

    public void Boost()
    {
        boosts = PlayerPrefs.GetInt("boosts");
        if (boosts > 0)
        {
            boosting = true;
            PlayerPrefs.SetInt("boosts", boosts -= 1);
            boostText.text = boosts.ToString() + "x";
        }
    }

    public void Reward()
    {
        boosts = PlayerPrefs.GetInt("boosts");
        PlayerPrefs.SetInt("boosts", boosts += 1);
        boostText.text = PlayerPrefs.GetInt("boosts").ToString() + "x";
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // if (col.gameObject.name == "Platform")
        // {
        //     float x = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.x);
        //     Vector2 dir = new Vector2(x, 1).normalized;
        //     rb.velocity = dir * c;
        // }

        if (col.gameObject.tag == "Proton")
        {
            recharging = true;
            speaker.Pitch("Ding", Random.Range(1f,2f));
            speaker.Play("Ding");
            col.gameObject.GetComponent<Proton>().Break();
        }
    }

/*
    float hitFactor(Vector2 photonPos, Vector2 platformPos, float platformWidth)
    {
        return (photonPos.x - platformPos.x) / platformWidth;
    }
    */
    
}