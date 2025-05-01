using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VRCaptionController : MonoBehaviour
{
    [SerializeField] private Image captionBackground;
    [SerializeField] private TextMeshProUGUI captionText;
    [SerializeField] private float typingSpeed = 30f; // characters per second

    private Coroutine typeCoroutine;

    void Start()
    {
        // Hide caption panel at start
        captionBackground.gameObject.SetActive(false);
        captionText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger (player must be tagged "Player")
        if (other.CompareTag("NPC"))
        {
            Debug.Log("trying to show");
            ShowCaption("Hello, welcome to the VR world!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When player leaves, hide the caption
        if (other.CompareTag("Player"))
        {
            HideCaption();
        }
    }

    // Call this to show a new caption with typewriter effect
    public void ShowCaption(string fullText)
    {
        // Stop any ongoing typing
        if (typeCoroutine != null)
        {
            Debug.Log("not null");
            StopCoroutine(typeCoroutine);
        }
        Debug.Log("ShowCaption Ran");
        // Activate UI and reset text
        captionBackground.gameObject.SetActive(true);
        captionText.text = "";
        // Start typing the new caption
        typeCoroutine = StartCoroutine(TypeText(fullText));
    }

    // Hide the caption panel and stop typing
    public void HideCaption()
    {
        if (typeCoroutine != null)
        {
            StopCoroutine(typeCoroutine);
            typeCoroutine = null;
        }
        captionBackground.gameObject.SetActive(false);
    }

    // Coroutine for the typewriter effect
    private IEnumerator TypeText(string fullText)
    {
        string currentText = "";
        foreach (char c in fullText)
        {
            currentText += c;
            captionText.text = currentText;
            // Wait a fraction of a second based on typing speed
            yield return new WaitForSeconds(1f / typingSpeed);
        }
    }
}