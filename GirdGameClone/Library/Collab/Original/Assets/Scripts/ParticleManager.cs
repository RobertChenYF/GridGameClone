using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Start is called before the first frame update

    public float ParticleLastTime;
    void Start()
    {
        Destroy(gameObject, ParticleLastTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
