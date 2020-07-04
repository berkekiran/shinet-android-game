using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public Transform player;
    public GameObject playerObject;
    public float speed = 5.0f;
    private bool touchStart = false;
    private Vector2 pointA;
    public Transform cursor;
    Vector3 velocity = Vector3.zero; 
    public float smoothTime = 5f;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    public AudioSource audioSource;

    void Start () {

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = player.GetComponent<RectTransform>().rect.width;
        objectHeight = player.GetComponent<RectTransform>().rect.height;

    }

    void LateUpdate(){

        if(!GameMechanics.gameEnded){

            Vector3 viewPos = player.position;

            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -3.25f + objectWidth, screenBounds.x * 3.25f - objectWidth);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1.5f + objectHeight, screenBounds.y * 1.5f - objectHeight);
            player.position = viewPos;

        }

    }
    
    void Update(){

        if(!GameMechanics.gameEnded){
            
            if(Input.GetMouseButtonDown(0)){
                pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
                cursor.transform.position = pointA;

                cursor.GetComponent<Image>().enabled = true;
            }

            if(Input.GetMouseButton(0)){
                touchStart = true; 
                if(!audioSource.isPlaying)
                    audioSource.Play();
                playerObject.GetComponent<Animation>().Play("CharacterWalk");
            } else {
                touchStart = false;
                audioSource.Stop();
                playerObject.GetComponent<Animation>().Stop("CharacterWalk");
            }
        
        } else {
            audioSource.Stop();
            playerObject.GetComponent<Animation>().Stop("CharacterWalk");
        }

    }

    private void FixedUpdate(){

        if(!GameMechanics.gameEnded){

            Vector3 playerPos = player.position;

            if(touchStart){
                Vector2 direction = cursor.up;

                if(direction.x < 0)
                    player.localScale = new Vector3(-1493.333f, 1493.333f, 1493.333f);
                else
                    player.localScale = new Vector3(1493.333f, 1493.333f, 1493.333f);

                GetDirection();
                moveCharacter(direction);
                cursor.GetComponent<Image>().enabled = true;
                cursor.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);

            } else {
                cursor.GetComponent<Image>().enabled = false;
                
            }

            transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref velocity, smoothTime);

        } else {
            cursor.GetComponent<Image>().enabled = false;
        }
        
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
