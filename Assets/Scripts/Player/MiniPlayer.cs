using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlayer : MonoBehaviour
{

    public GameObject PlayerBody;
    public GameObject ShaceShipBody;
    float PlayerStartPosition;
    float SpaceShipStartPosition;
    float positionDelta;
    float ratio;
    float whereTo;
    float startPosition;
    void Start()
    {
        startPosition = transform.localPosition.x;
        PlayerStartPosition = PlayerBody.transform.position.x;
        SpaceShipStartPosition = ShaceShipBody.transform.position.x;
        positionDelta = SpaceShipStartPosition - PlayerStartPosition;
        ratio = 250 / positionDelta;
    }


    void Update()
    {
        whereTo = (PlayerBody.transform.position.x - PlayerStartPosition) * ratio;
        transform.localPosition = new Vector2(startPosition + whereTo, transform.localPosition.y);
    }
}
