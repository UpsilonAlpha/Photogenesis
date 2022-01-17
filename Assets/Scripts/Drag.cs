using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    /*
    public bool mousePressed;

    public Vector3 mouseStartPosition;
    public Vector3 mouseEndPosition;

    public Vector3 heading;
    public float distance;
    public Vector3 direction;

    Vector2 offset;

    void OnMouseDown()
    {
        offset = (Vector2)gameObject.transform.position - GetMouseWorldPos();
        GetComponent<Collider2D>().isTrigger = true;
    }

    Vector2 GetMouseWorldPos()
    {
        Vector2 mousePoint = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        GetComponent<Collider2D>().isTrigger = false;
    }*/

    private float fingerStartTime;
    private Vector2 fingerStartPos;
    public static bool isSwipe = false;
    private float maxSwipeTime = 10f;
    private float minSwipeDist = 5f;
    public LineRenderer line;
    public Photon photon = Photon.instance;
    public Vector3 fireDirection;
    public AudioManager speaker;
    public Animator anim;
    private Ray2D ray;
    private RaycastHit2D hit;
    public float maxLength = 100f;
    public bool paused = false;

    void Update () 
    {

        /*ray = new Ray2D(transform.position, fireDirection);
        line.positionCount = 1;
        line.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            hit = Physics2D.Raycast(ray.origin+(Vector2)fireDirection, ray.direction, remainingLength);

            if (hit != null)
            {
                line.positionCount += 1;
                line.SetPosition(line.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray2D(hit.point, Vector3.Reflect(ray.direction, hit.normal));
            }
            else
            {
                line.positionCount += 1;
                line.SetPosition(line.positionCount - 1, ray.origin + ray.direction*remainingLength);
            }
        }*/

//This is all broken
        if (InputManager.paused != true)
        {
            if (photon.fireTime > 0.75f)
            {
                anim.Play("Flash", 0);
                photon.timeBar.GetComponent<SpriteRenderer>().color = Color.yellow;
                foreach (Touch touch in Input.touches)
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            isSwipe = true;
                            fingerStartTime = Time.time;
                            fingerStartPos = touch.position;
                            break;
                        case TouchPhase.Canceled:
                            isSwipe = false;
                            break;
                        default:
                            float gestureTime = Time.time - fingerStartTime;
                            float gestureDist = (touch.position - fingerStartPos).magnitude;
                            if(isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                            {
                                Vector2 direction = touch.position - fingerStartPos;
                                Vector2 swipeType = Vector2.zero;
                                fireDirection = -direction;
                            }
                            break;
                    }
                }

                line.SetPosition(0, transform.position);
                line.SetPosition(1, transform.position+fireDirection/220);

                if (Input.touchCount == 0 )
                {
                    line.SetPosition(1, transform.position);
                    if (fireDirection != Vector3.zero)
                    {
                        photon.rb.velocity = fireDirection.normalized;
                        fireDirection = Vector3.zero;
                        photon.photonReleased = true;
                        photon.fireTime = 0;
                        photon.recharging = false;
                        photon.speaker.lowPass.enabled = false;
                        photon.speaker.Play("Whoosh");
                        PostManager.instance.ChromaticAbberation(0);
                    }
                    Time.timeScale = 1;
                    //else
                        //if (photon.photonReleased)
                            //photon.fireTime += Time.deltaTime;
                }
                else
                {
                    Time.timeScale = 0.2f;
                    photon.speaker.lowPass.enabled = true;
                    PostManager.instance.ChromaticAbberation(0.5f);
                    /*if (photon.fireTime != 0)
                        photon.fireTime -= Time.deltaTime;
                    else{
                        line.SetPosition(1, transform.position);
                        photon.rb.velocity = fireDirection.normalized;
                        fireDirection = Vector3.zero;
                        photon.photonReleased = true;
                        photon.fireTime = 0;
                        photon.recharging = false;
                        Time.timeScale = 1;
                    }*/

                    //if (photon.photonReleased)
                        //photon.fireTime += Time.deltaTime*5;
                }
            }
            else if (fireDirection != Vector3.zero)
            {
                line.SetPosition(1, transform.position);
                photon.rb.velocity = fireDirection.normalized;
                fireDirection = Vector3.zero;
                photon.photonReleased = true;
                photon.fireTime = 0;
                photon.recharging = false;
                Time.timeScale = 1;
                photon.speaker.lowPass.enabled = false;
                photon.speaker.Play("Whoosh");
                PostManager.instance.ChromaticAbberation(0);
            }
            else //if (Input.touchCount == 0)
            {
                Time.timeScale = 1f;
                photon.timeBar.GetComponent<SpriteRenderer>().color = Color.white;
                if (photon.recharging && photon.fireTime <= 1f)
                {
                    photon.fireTime += Time.deltaTime;
                }
            }
        }
    }

}
