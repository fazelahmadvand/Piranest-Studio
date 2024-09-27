using UnityEngine;

public class ClickOrTouch : MonoBehaviour
{
    public CharTalk charTalk;
    // Function to be called when the GameObject is clicked or touched
    void PerformAction()
    {
        Debug.Log("GameObject was clicked or touched!");
        // Add more actions here as needed
    }

    // Detect mouse click
    void OnMouseDown()
    {
        charTalk.TalkT();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for touch events if running on a device that supports touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch
            if (touch.phase == TouchPhase.Began) // Check if the touch has just started
            {
                // Convert touch position to world space
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchPos.z = 0; // Ensure the touch position is at the correct depth

                // Perform a raycast to check if the touch is on the collider
                RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                {
                    charTalk.TalkT();
                }
            }
        }
    }
}
