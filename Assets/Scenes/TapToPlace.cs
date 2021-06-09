using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TapToPlace : MonoBehaviour
{
    [SerializeField] private GameObject ObjectPrefabs;

    private ARRaycastManager aRRaycastManager;
    private Vector2 touchPosition;
    private GameObject spawnObject;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Start()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    private void Update()
    {
        Debug.Log(Input.touchCount);
        if (!TryGetTouchPosition(out touchPosition))
        {
            Debug.Log("no touch");
            return;
        }
        if (aRRaycastManager.Raycast(touchPosition, hits))
        {
            Debug.Log("Hit " + hits[0].pose.position);
            var hitPose = hits[0].pose;

            if (spawnObject)
            {
                Destroy(spawnObject);
               
            }
            spawnObject = Instantiate(ObjectPrefabs, hitPose.position, Quaternion.identity);
            Debug.Log(spawnObject.transform.position);
        }
    }
}
