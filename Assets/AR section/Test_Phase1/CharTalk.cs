using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Ensure you have the TextMesh Pro package installed
using UnityEngine.UI;

public class CharTalk : MonoBehaviour
{
    public GameObject objectToExclude; // Assign this in the Inspector
    public string tagToCheck = "MyTag"; // Assign or modify the tag you're interested in
    public TMP_Text text; // Drag your TMP_Text component here through the inspector
    public string[] messages; // Initialize this array through the inspector
    int x = -1; // Start at -1 so the first increment sets it to 0

    void Start()
    {
        if (messages == null || messages.Length == 0)
        {
            Debug.LogError("No messages set for CharTalk.");
            return;
        }
        ProcessAndDestroyTaggedObjects();

        // Find and check the chat button
        GameObject ob = GameObject.FindGameObjectWithTag("ChatBut");
        if (ob != null)
        {
            Image img = ob.GetComponent<Image>();
            Button btn = ob.GetComponent<Button>();
            if (img != null && btn != null)
            {
                img.enabled = true;
                btn.enabled = true;
            }
            else
            {
                Debug.LogError("Image or Button component not found on ChatBut GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameObject with tag 'ChatBut' not found.");
        }
    }
    void ProcessAndDestroyTaggedObjects()
    {
        // Find all game objects with the specified tag
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tagToCheck);
        List<GameObject> filteredObjects = new List<GameObject>();

        // Check each object to see if it should be excluded
        foreach (GameObject obj in taggedObjects)
        {
            if (obj != objectToExclude) // Exclude the specific object
            {
                filteredObjects.Add(obj);
            }
        }

        // Now destroy all objects in the filtered list
        foreach (GameObject obj in filteredObjects)
        {
            Destroy(obj);
        }

        // Optionally, you can also destroy the objectToExclude if needed
        // Destroy(objectToExclude);
    }
    public void TalkT()
    {
        if (x < messages.Length - 1) // Ensure we do not exceed the array's length
        {
            x++;
            Debug.Log($"Displaying message {x}: {messages[x]}");
            text.text = messages[x];
        }
        else
        {
            Debug.Log("No more messages to display.");
            // Optionally disable the button or reset the conversation
        }
    }
}
