using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Wave[] m_waves;

    private int m_waveIndex;
    [SerializeField]
    private List<Vector3> m_points;
    public List<Vector3> Points => m_points;

    
    // Start is called before the first frame update
    private void OnEnable() {
        if(m_points == null)
        {
            m_points = new List<Vector3>(32)
            {
                Vector3.forward,
                -Vector3.forward
            };
        }
    }
    
    public void Reset()
    {
        m_points = new List<Vector3>(32)
            {
                Vector3.forward,
                -Vector3.forward
            };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("SpawnWave")]
    public void SpawnWave()
    {
        StartCoroutine(SpawnWaveCoroutine());
    }
    public IEnumerator SpawnWaveCoroutine()
    {
        Wave waveEntry = m_waves[m_waveIndex];
        for(int i = 0; i < waveEntry.WaveEntries.Length; i++)
        {
            int count = waveEntry.WaveEntries[i].Count;
            string name = waveEntry.WaveEntries[i].Enemy.Name;
            for (int j = 0; j < count; j++)
            {
                yield return new WaitForSeconds(0.5f);
                GameObject gameObject = ObjectPooler.Instance.SpawnFromPool(name, m_points[0], Quaternion.identity);
                gameObject.GetComponent<Enemy>().Spawn(m_points);
            }
        }
        m_waveIndex = m_waveIndex % (m_waves.Length + 1);
    }

    public void Remove()
    {
        if(m_points.Count == 1)
        {
            m_points.RemoveAt(m_points.Count - 1);
        }
    }

    public void Add()
    {
        Vector3 nextPoint;
        int endIndex = m_points.Count - 1;
        Vector3 dir = m_points[endIndex] - m_points[endIndex - 1];
        nextPoint = m_points[endIndex] + dir.normalized;
        m_points.Add(nextPoint);
    }
}
