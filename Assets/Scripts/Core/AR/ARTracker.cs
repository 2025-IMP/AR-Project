using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTracker : MonoBehaviour
{
    private ARTrackedImageManager m_ARTrackedImageManager;

    [SerializeField]
    private GameObject m_ObjectPrefab;
    private GameObject m_SpawnedObject;

    private void Awake()
    {
        m_ARTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        m_ARTrackedImageManager.trackablesChanged.AddListener(OnImageChanged);
    }

    private void OnDisable()
    {
        m_ARTrackedImageManager.trackablesChanged.RemoveListener(OnImageChanged);
    }

    private void OnImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (ARTrackedImage trackedImg in args.added)
        {
            Debug.Log($"Added: {trackedImg.referenceImage.name}");
            if (trackedImg.referenceImage.name == "ajou")
            {
                m_SpawnedObject ??= Instantiate(m_ObjectPrefab, trackedImg.transform.position, trackedImg.transform.rotation);
            }
        }

        foreach (ARTrackedImage trackedImg in args.updated)
        {
            Debug.Log($"Updated: {trackedImg.referenceImage.name}");
            UpdatePrefab(trackedImg);
        }

        foreach (KeyValuePair<TrackableId, ARTrackedImage> pair in args.removed)
        {
            Debug.Log($"Removed: {pair.Value.name}");
            m_SpawnedObject.SetActive(false);
        }
    }

    private void UpdatePrefab(ARTrackedImage trackedImg)
    {
        if (m_SpawnedObject == null) return;

        if (trackedImg.trackingState == TrackingState.Tracking)
        {
            if (trackedImg.referenceImage.name == "ajou")
            {
                m_SpawnedObject.transform.position = trackedImg.transform.position;
                m_SpawnedObject.transform.rotation = trackedImg.transform.rotation;
                m_SpawnedObject.SetActive(true);
            }
        } else if (trackedImg.trackingState == TrackingState.Limited)
        {
            Debug.Log("Limited tracking state!");
            m_SpawnedObject.SetActive(false);
        } else
        {
            Debug.Log("No tracking state!");
            m_SpawnedObject.SetActive(false);
        }
    }
}
