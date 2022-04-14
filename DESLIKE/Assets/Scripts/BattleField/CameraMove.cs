using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Camera mainCamera;
    Transform mainCameraTransform;
    GameObject hero;

    float zoomSpeed = 20.0f;
    float cameraMaxSize = 25.0f;
    float cameraMinSize = 5.0f;

    bool isFollowHero;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        mainCameraTransform = GetComponent<Transform>();
        hero = GameObject.Find(SaveManager.Instance.heroPrefab.name + "(Clone)");
    }

    void Update()
    {
        Zoom();
        CameraMoving();
        FollowingHero();
    }

    void Zoom()
    {
        float zoomValue = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        if(zoomValue != 0)
        {
            if (zoomValue < 0 && mainCamera.orthographicSize > cameraMinSize)
            {
                mainCamera.orthographicSize += zoomValue;
            }
            else if(zoomValue > 0 && mainCamera.orthographicSize < cameraMaxSize)
            {
                mainCamera.orthographicSize += zoomValue;
            }
        }
    }

    void CameraMoving()
    {
        if(isFollowHero == false)//히어로에 카메라 고정이 아니라면
        {
            //방향키로 카메라 이동
            if (Input.GetKey(KeyCode.UpArrow)) CameraUp();
            else if (Input.GetKey(KeyCode.DownArrow)) CameraDown();
            else if (Input.GetKey(KeyCode.LeftArrow)) CameraLeft();
            else if (Input.GetKey(KeyCode.RightArrow)) CameraRight();
        }
    }
    //카메라 위치 이동 함수
    void CameraUp()
    {
        mainCameraTransform.position += new Vector3(0, 0.5f + 0.005f * mainCamera.orthographicSize, 0);
    }
    void CameraDown()
    {
        mainCameraTransform.position += new Vector3(0, -0.5f - 0.005f * mainCamera.orthographicSize, 0);
    }
    void CameraLeft()
    {
        mainCameraTransform.position += new Vector3(-1.5f - 0.005f * mainCamera.orthographicSize, 0, 0);
    }
    void CameraRight()
    {
        mainCameraTransform.position += new Vector3(1.5f + 0.005f * mainCamera.orthographicSize, 0, 0);
    }
    //영웅에 카메라 고정함수
    void FollowingHero()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isFollowHero == false)
            {
                isFollowHero = true;
                mainCamera.transform.SetParent(hero.transform);
                mainCamera.transform.position = new Vector3(hero.transform.position.x, hero.transform.position.y, -10);
            }
            else if (isFollowHero == true)
            {
                isFollowHero = false;
                mainCamera.transform.SetParent(null);
            }
        }
    }
}
