using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem ParticleSystem;
    public static SpawnVFX Instance;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        ParticleSystem.gameObject.SetActive(false); 
    }

    public void ActiveParticle(Vector3 pos)
    {
        ParticleSystem.gameObject.SetActive(true);
        ParticleSystem.gameObject.transform.position=pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
