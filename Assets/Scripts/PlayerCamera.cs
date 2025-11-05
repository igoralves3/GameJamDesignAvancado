using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCamera : MonoBehaviour
{

    public Transform target;             // Jogador
    public float smoothSpeed = 0.125f;
    public Vector3 offset;               // Offset da câmera em relação ao jogador

    public Camera camera;

    // Limites da câmera
    public float minX, maxX, minY=-4, maxY=13;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = transform.position.z;

        // Suaviza o movimento
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Limita dentro das bordas
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minY, maxY);

        transform.position = smoothedPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();

        var currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Stage0" || currentSceneName == "Stage1")
        {
            camera.backgroundColor = new Color(0.1960784f, 0.8039216f, 0.1960784f, 1f);// Color.limeGreen;
        }else if (currentSceneName == "Stage2" || currentSceneName == "Stage4")
        {
            camera.backgroundColor = new Color(0.5294118f, 0.8078432f, 0.9215687f, 1f);// Color.skyBlue;
        }
        else if (currentSceneName == "Stage3")
        {
            camera.backgroundColor = new Color(0.1882353f, 0.682353f, 0.7490196f, 1f);// Color.softblue;
        }
        else if (currentSceneName == "Stage5" || currentSceneName == "Stage6")
        {
            camera.backgroundColor = new Color(0.9019608f, 0.9019608f, 0.9803922f, 1f);// Color.lavender;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
