using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager cameraManager;

    public GameObject[] cameraviews;
    public bool camFreeMode;

    public UnityEngine.UI.Text buttonText;

    [SerializeField] GameObject camButton;

    void Awake()
    {
        cameraManager = this;
        cameraviews = new GameObject[3];
    }


    void Start()
    {
        cameraviews[0] = GameObject.FindGameObjectWithTag("Standard Camera");
        cameraviews[1] = GameObject.FindGameObjectWithTag("Shoot Camera");
        cameraviews[2] = GameObject.FindGameObjectWithTag("Free Camera");

        cameraviews[0].SetActive(true);
        cameraviews[1].SetActive(false);
        cameraviews[2].SetActive(false);
        camFreeMode = false;

        camButton = GameObject.Find("Camera Button");
        buttonText = camButton.GetComponentInChildren<UnityEngine.UI.Text>();
    }


    void Update()
    {
        if (DragShoot.dragShoot.isShoot)
        {
            camButton.SetActive(false);
        }
        else
        {
            camButton.SetActive(true);
        }
    }


    public void OnFreeCamera()
    {
        if (!camFreeMode)
        {
            camFreeMode = true;

            cameraviews[0].SetActive(false);
            cameraviews[1].SetActive(false);
            cameraviews[2].SetActive(true);

            buttonText.text = "Back";
        }
        else if (camFreeMode)
        {
            camFreeMode = false;

            cameraviews[0].SetActive(true);
            cameraviews[1].SetActive(false);
            cameraviews[2].SetActive(false);

            buttonText.text = "Full View";
        }
    }
}
