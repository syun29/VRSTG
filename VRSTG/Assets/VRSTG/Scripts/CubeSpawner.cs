using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private float m_spawnInterval = 1f;
    [SerializeField] private Cube m_cubePrefab;

    private List<Cube> m_cubes = new List<Cube>();
    private float m_elapsedTime;

    private void Update()
    {
        if (m_elapsedTime >= m_spawnInterval)
        {
            Cube cube = Instantiate(m_cubePrefab, transform);
            m_cubes.Add(cube);

            Vector3 spawnPos = Vector3.zero;
            spawnPos.x = Random.Range(-1f, 1f);
            spawnPos.z = Random.Range(-1f, 1f);
            cube.transform.localPosition = spawnPos;

            cube.transform.localRotation = Quaternion.Euler
                (
                    Random.Range(0f, 360f),
                    Random.Range(0f, 360f),
                    Random.Range(0f, 360f)
                );

            float spawnScale = Random.Range(0.125f, 0.25f);
            cube.transform.localScale = Vector3.one * spawnScale;


            m_elapsedTime -= m_spawnInterval;
        }
        m_elapsedTime += Time.deltaTime;
    }
}
