using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public GameObject RoadPrefab;
    private List<GameObject> roads = new List<GameObject>();
    public float maxSpeed = 10;
    public float speed = 0;
    public int maxRoadCount = 5; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetLevel();
        //StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (speed == 0) return;
        else
        {
            foreach(GameObject road in roads)
            {
                road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
                if(road.transform.position.z < -10)
                {
                    Destroy(road);
                    roads.Remove(road);
                    CreateNextRoad();
                }
            }
        }
    }

    private void CreateNextRoad()
    {

        Vector3 pos = Vector3.zero;
        if(roads.Count > 0){ pos = roads[roads.Count-1].transform.position + new Vector3(0,0,10);}
        GameObject go = Instantiate(RoadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        roads.Add(go);
    }

    public void StartLevel()
    {
        speed = maxSpeed;
        SwipeManag.instance.enabled = true;
    }
    public void ResetLevel()
    {
        speed = 0;
        while (roads.Count > 0 )
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
        for (int i = 0; i<maxRoadCount; i++)
        {
            CreateNextRoad();
        }
         SwipeManag.instance.enabled = false;
    }
}
