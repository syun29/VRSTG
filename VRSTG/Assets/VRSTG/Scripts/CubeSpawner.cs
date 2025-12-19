using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private float m_spawnInterval = 0.01f;
    [SerializeField] private Cube m_cubePrefab;
    [SerializeField] private float m_randScaleMin;
    [SerializeField] private float m_randScaleMax;

    private List<Cube> m_cubes = new List<Cube>();
    private float m_elapsedTime;

    private void Update()
    {
        //if (m_elapsedTime >= m_spawnInterval)
        if (m_spawnInterval < 0)
        {
            Cube cube = Instantiate(m_cubePrefab, transform);
            m_cubes.Add(cube);

            Vector3 spawnPos = Vector3.zero;
            spawnPos.x = Random.Range(-49f, 98f);
            spawnPos.y = 1.0f;
            spawnPos.z = Random.Range(-67f, 79f);
            cube.transform.localPosition = spawnPos;

            cube.transform.localRotation = Quaternion.Euler
                (
                    Random.Range(0f, 360f),
                    Random.Range(0f, 360f),
                    Random.Range(0f, 360f)
                );

            float spawnScale = Random.Range(m_randScaleMin, m_randScaleMax);
            cube.transform.localScale = Vector3.one * spawnScale;


            //m_elapsedTime -= m_spawnInterval;
            m_spawnInterval = 0.01f;
        }
        else
        {
            m_spawnInterval -= Time.deltaTime;
        }
        //m_elapsedTime += Time.deltaTime;
        
    }
}
