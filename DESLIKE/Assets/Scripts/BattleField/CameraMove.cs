using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Camera mainCamera;
    Transform mainCameraTransform;
    GameObject hero;

    Vector3 cameraPos = new Vector3(0,0,-10);

    float zoomSpeed = 20.0f;//마우스 휠에 따라 줌 되는 정도
    float cameraMaxSize = 25.0f;//카메라의 최대 크기
    float cameraMinSize = 5.0f;//카메라의 최소 크기

    float cameraXSize, cameraYSize;//현재 카메라의 가로 세로 크기
    [SerializeField] float mapXSize, mapYSize;//현재 맵의 크기

    bool isFollowHero;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        mainCameraTransform = GetComponent<Transform>();
        hero = GameObject.Find(SaveManager.Instance.heroPrefab.name + "(Clone)");
        cameraYSize = mainCamera.orthographicSize;
        cameraXSize = cameraYSize * Screen.width / Screen.height;
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
            if (zoomValue < 0 && mainCamera.orthographicSize > cameraMinSize)//카메라가 최소 크기보다 크고 마우스 휠을 위로 올렸다면
            {
                mainCamera.orthographicSize += zoomValue;//카메라 사이즈 변경
                cameraYSize = mainCamera.orthographicSize;//카메라 사이즈 갱신
                cameraXSize = cameraYSize * Screen.width / Screen.height;//카메라 사이즈 갱신
            }
            else if(zoomValue > 0 && mainCamera.orthographicSize < cameraMaxSize)
            {
                mainCamera.orthographicSize += zoomValue;//카메라 사이즈 변경
                cameraYSize = mainCamera.orthographicSize;//카메라 사이즈 갱신
                cameraXSize = cameraYSize * Screen.width / Screen.height;//카메라 사이즈 갱신
            }
        }
    }

    void CameraMoving()
    {
        float xConstraint, yConstraint;
        xConstraint = mapXSize - cameraXSize;
        yConstraint = mapYSize - cameraYSize;

        if (isFollowHero == true)//히어로에 카메라가 고정이라면
        {
            mainCamera.transform.position = hero.transform.position + cameraPos;
        }
        if(isFollowHero == false)//히어로에 카메라 고정이 아니라면
        {
            //방향키로 카메라 이동
            if (Input.GetKey(KeyCode.UpArrow)) CameraUp();
            else if (Input.GetKey(KeyCode.DownArrow)) CameraDown();
            else if (Input.GetKey(KeyCode.LeftArrow)) CameraLeft();
            else if (Input.GetKey(KeyCode.RightArrow)) CameraRight();
        }
        //mainCamera.transform.position = new Vector3(Mathf.Clamp(mainCamera.transform.position.x, -(mapXSize - cameraXSize), (mapXSize - cameraXSize)), Mathf.Clamp(mainCamera.transform.position.y, -(mapYSize - cameraYSize), (mapYSize - cameraYSize)), -10);
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
            }
            else if (isFollowHero == true)
            {
                isFollowHero = false;
            }
        }
    }
}
