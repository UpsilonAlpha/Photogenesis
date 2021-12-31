using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostManager : MonoBehaviour
{
    ChromaticAberration ca;
    Bloom bloom;
    PostProcessVolume volume;
    public static PostManager instance;

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
    public void Start()
    {
        volume = GetComponent<PostProcessVolume>();
    }
    public void ChromaticAbberation(float intensity)
    {
        volume.profile.TryGetSettings(out ca);
        ca.intensity.value = intensity;
    }
    public void Bloom(float intensity)
    {
        volume.profile.TryGetSettings(out bloom);
        bloom.intensity.value = intensity;
    }
}
