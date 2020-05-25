using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Handles teleporting object when is touching screen edge 
public class EdgeTeleporter : MonoBehaviour
{
    LocalGameMaster lgm;

    void Start()
    {
        lgm = LocalGameMaster.LGM;
    }

    void Update()
    {
        if (transform.position.x > lgm.rightX)
        {
            transform.position = new Vector2(lgm.leftX, transform.position.y);
        }else if (transform.position.x < lgm.leftX)
        {
            transform.position = new Vector2(lgm.rightX, transform.position.y);
        }

        if (transform.position.y > lgm.topY)
        {
            transform.position = new Vector2(transform.position.x, lgm.bottomY);
        }
        else if (transform.position.y < lgm.bottomY)
        {
            transform.position = new Vector2(transform.position.x, lgm.topY);
        }
    }
}
