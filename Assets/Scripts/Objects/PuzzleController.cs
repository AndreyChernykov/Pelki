using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float cameraOrthoSize;
    [SerializeField] float speedRotate;
    [SerializeField] List<PuzzleRotate> puzzleElements;
    [SerializeField] GameObject[] activateObjects;
    [SerializeField] GameObject[] deactivateObjects;

    float cameraOrthoSizeStart;
    bool puzzleActive = false;
    bool isSolved = false;

    private void Start()
    {
        cameraOrthoSizeStart = virtualCamera.m_Lens.OrthographicSize;

        SetReward(false, activateObjects);

        foreach (var puzzleElement in puzzleElements)
        {
            puzzleElement.speedRotate = speedRotate;
            puzzleElement.transform.Rotate(0, 0, Random.Range(0, 360));
        }
    }

    private void Update()
    {
        if(puzzleActive)CheckSolution();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            if(cameraBehavior != null)StopCoroutine(cameraBehavior);

            puzzleActive = true;
            if (!isSolved)
            {
                cameraBehavior = CameraBehavior(cameraOrthoSize);
                StartCoroutine(cameraBehavior);
            }

        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (cameraBehavior != null) StopCoroutine(cameraBehavior);

            puzzleActive = false;

            cameraBehavior = CameraBehavior(cameraOrthoSizeStart);
            StartCoroutine(cameraBehavior);

        }
    }

    IEnumerator cameraBehavior;

    IEnumerator CameraBehavior(float orthoSize)
    {
        float elapsed = 0;
        while (virtualCamera.m_Lens.OrthographicSize != orthoSize)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, orthoSize, elapsed/orthoSize);
            elapsed += Time.deltaTime;
            yield return null;
        }

        StopCoroutine(cameraBehavior);
    }

    private void CheckSolution()
    {
        int countSolution = 0;

        foreach(var puzzleElement in puzzleElements)
        {
            if (puzzleElement.solution == false)
            {
                countSolution = 0;
                break;
            }
            
            countSolution++;
        }

        if(countSolution >= puzzleElements.Count)
        {
            SetReward(false, deactivateObjects);
            SetReward(true, activateObjects);
            activateObjects = null;
            isSolved = true;
        }
    }

    private void SetReward(bool b, GameObject[] objects)
    {
        if(objects != null)
        {
            foreach (var obj in objects)
            {
                obj.SetActive(b);
            }
        }

    }
}

