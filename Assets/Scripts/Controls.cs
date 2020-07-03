using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    private bool touchStart = false;
    private Vector2 pointA;
    public Transform cursor;
    Vector3 velocity = Vector3.zero; 
    public float smoothTime = 5f;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start () {

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = player.GetComponent<RectTransform>().rect.width;
        objectHeight = player.GetComponent<RectTransform>().rect.height;

    }

    void LateUpdate(){

        Vector3 viewPos = player.position;

        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -3.25f + objectWidth, screenBounds.x * 3.25f - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1.5f + objectHeight, screenBounds.y * 1.5f - objectHeight);
        player.position = viewPos;

    }

    void Update(){

        
        if(Input.GetMouseButtonDown(0)){
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            cursor.transform.position = pointA;

            cursor.GetComponent<Image>().enabled = true;
        }

        if(Input.GetMouseButton(0)){
            touchStart = true;
        } else {
            touchStart = false;
        }

    }

    private void FixedUpdate(){

        Vector3 playerPos = player.position;

        if(touchStart){
            Vector2 direction = cursor.up;
            GetDirection();
            moveCharacter(direction);
            cursor.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
        } else {
            cursor.GetComponent<Image>().enabled = false;
        }

        transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref velocity, smoothTime);

     }

    void GetDirection(){
        
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - player.position.x, mousePosition.y - player.position.y);
        cursor.up = direction;

    }

    void moveCharacter(Vector2 direction){

        player.Translate(direction * speed * Time.deltaTime);

    }

}
