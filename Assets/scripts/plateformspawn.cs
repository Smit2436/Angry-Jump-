using UnityEngine;
using System.Collections.Generic;

public class SlidePoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] slidePrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float xRange = 2.5f;
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private int poolSize = 10;

    private float spawnTimer;
    private List<GameObject> slidePool = new List<GameObject>();
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        for (int i = 0; i < poolSize; i++)
        {
            int prefabIndex = Random.Range(0, slidePrefabs.Length);
            GameObject slide = Instantiate(slidePrefabs[prefabIndex]);
            slide.SetActive(false);
            slidePool.Add(slide);
        }
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnSlide();
            spawnTimer = 0f;
        }

       
        float screenBottomY = mainCamera.transform.position.y - mainCamera.orthographicSize - 2f;

        foreach (GameObject slide in slidePool)
        {
            if (slide.activeInHierarchy && slide.transform.position.y < screenBottomY)
            {
                slide.SetActive(false);
            }
        }
    }

    void SpawnSlide()
    {
        GameObject slide = GetInactiveSlide();
        if (slide != null)
        {
            float randomX = Random.Range(-xRange, xRange);
            Vector3 spawnPos = new Vector3(randomX, spawnPoint.position.y, spawnPoint.position.z);

            slide.transform.position = spawnPos;
            slide.SetActive(true);
        }
    }

    GameObject GetInactiveSlide()
    {
        foreach (GameObject slide in slidePool)
        {
            if (!slide.activeInHierarchy)
                return slide;
        }

        
        int prefabIndex = Random.Range(0, slidePrefabs.Length);
        GameObject newSlide = Instantiate(slidePrefabs[prefabIndex]);
        newSlide.SetActive(false);
        slidePool.Add(newSlide);
        return newSlide;
    }
}
