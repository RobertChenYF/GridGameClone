using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Color[] colors;
    public SpriteRenderer SpriteRenderer;
    
    public float x;
    public float y;
    public  int colorcode;
    
    void Start()
    {
      //  x = GridManager.board

        SpriteRenderer.color = colors[colorcode];

        Destroy(gameObject, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(x,y);



    }

    



}
