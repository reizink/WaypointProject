using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowPath : MonoBehaviour
{
    enum PathOptions{
        Circle, Square, Triangle, Irregular
    }; 
    
    [SerializeField] PathOptions choosePath = new PathOptions();
    [SerializeField] public List<GameObject> waypoints; //
    private GameObject WaypointParent; //
    public float totalTime = 6;
    public bool isLoop = true;
    public InputField myTime;
    public Dropdown Paths;
    //public Toggle

    private Vector3 target;
    private int i = 0; //index
    private float timePerPoint;
    private Vector3 lastTarget;
    private bool running = false;


    void Start(){

        //WaypointParent = GameObject.Find("Waypoints");
        //waypoint path
        sortPathPoints();
        Debug.Log(WaypointParent.name);

        target = waypoints[i].transform.position;
        timePerPoint = totalTime / waypoints.Count;
        lastTarget = transform.position;
    }

    private void Update(){

        target = waypoints[i].transform.position;
        float distance = Vector3.Distance(transform.position, target);

        if(distance > 0.05 && running == false){
            StartCoroutine(MoveInTime(lastTarget, target, timePerPoint));
            running = true;
        }
        else if (distance <= 0.1){
            lastTarget = target;
            running = false;
            //StopCoroutine("MoveInTime");
            Debug.Log(target);
            if(i < waypoints.Count - 1){
                i++;
                
            }
            else{
                if(isLoop){
                    i = 0;
                }
            }

            sortPathPoints();
        }

    }

    IEnumerator MoveInTime(Vector3 beginPos, Vector3 targetPos, float time){
        //Debug.Log("started Coroutine");
        for(float j = 0; j < 1; j += Time.deltaTime / time){
            transform.position = Vector3.Lerp(beginPos, targetPos, j);
            yield return null;
        }
    }

    void sortPathPoints(){
        switch(choosePath){
            case PathOptions.Circle:
                WaypointParent = GameObject.Find("CircleWaypoints");
                break;
            case PathOptions.Triangle:
                WaypointParent = GameObject.Find("TriangleWaypoints");
                break;
            case PathOptions.Square:
                WaypointParent = GameObject.Find("SquareWaypoints");
                break;
            case PathOptions.Irregular:
                WaypointParent = GameObject.Find("Waypoints");
                break;
            default:
                Debug.Log("PathOptions Default");
                break;
        }

        waypoints.Clear();
        Transform[] childTransforms = WaypointParent.GetComponentsInChildren<Transform>();
        foreach (Transform child in childTransforms)
        {
            waypoints.Add(child.gameObject);
            Debug.Log(child.gameObject);
        }

    }
}
