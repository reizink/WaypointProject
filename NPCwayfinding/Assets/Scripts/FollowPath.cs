using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] public List<GameObject> waypoints;
    public GameObject WaypointParent;
    public float totalTime = 6;
    public bool isLoop = true;

    private Vector3 target;
    private int i = 0; //index
    private float timePerPoint;
    private Vector3 lastTarget;
    private bool running = false;

    void Start(){

        WaypointParent = GameObject.Find("Waypoints");
        Debug.Log(WaypointParent);

        Transform[] childTransforms = WaypointParent.GetComponentsInChildren<Transform>();
        foreach (Transform child in childTransforms)
        {
            waypoints.Add(child.gameObject);
        }

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
        else if (distance <= 0.05){
            lastTarget = target;
            running = false;
            //StopCoroutine("MoveInTime");
            //Debug.Log(target);
            if(i < waypoints.Count - 1){
                i++;
                
            }
            else{
                if(isLoop){
                    i = 0;
                }
            }
        }

    }

    IEnumerator MoveInTime(Vector3 beginPos, Vector3 targetPos, float time){
        //Debug.Log("started Coroutine");
        for(float j = 0; j < 1; j += Time.deltaTime / time){
            transform.position = Vector3.Lerp(beginPos, targetPos, j);
            yield return null;
        }
    }
}
