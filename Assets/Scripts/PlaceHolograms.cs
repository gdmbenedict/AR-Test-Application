using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaceHolograms : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager; //reference to tracked image manager to get target surfaces
    [SerializeField] private GameObject[] holograms; //hologram prefabs to be spawned in
    private Dictionary<string, GameObject> instantiatedPrefabs = new Dictionary<string, GameObject>();

    //called when script is enabled or added
    private void OnEnable()
    {
        //subscribe to event
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    //called when script is disabled or removed
    private void OnDisable()
    {
        //un-subscribe from event
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    //Function handling changes in tracked images
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //if there are new tracked objects go through spawn logic
        if (eventArgs.added.Count > 0)
        {
            AddHologram(eventArgs.added);
        }

        //go through each image and update if the image is still in view
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            //set active to tracking state (disabled if not tracking, enabled if on screen)
            instantiatedPrefabs[trackedImage.referenceImage.name].SetActive(trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            //destroy corresponding hologram and remove from list of tracked images
            Destroy(instantiatedPrefabs[trackedImage.referenceImage.name]);
            instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
        }
    }

    //Function that adds prefab on to tracked images
    private void AddHologram(List<ARTrackedImage> trackedImages)
    {
        //loop through all tracked images just added
        foreach (ARTrackedImage trackedImage in trackedImages)
        {
            string imageName = trackedImage.referenceImage.name; //getting image name

            //try to find coresponding prefab
            foreach (GameObject hologram in holograms)
            {
                //check if image name is same as a stored hologram and is not already spawned
                if (imageName == hologram.name && !instantiatedPrefabs.ContainsKey(imageName))
                {
                    //instantiate prefab at tracked image location
                    instantiatedPrefabs[imageName] = Instantiate(hologram, trackedImage.transform);
                }
            }
        }
    }

    
}
