using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Image back;
    public bool vis = true;

    private bool isVisible = false; // Add a boolean flag to track the visibility

    void Start()
    {
        if (vis)
        {
            SetVisibility(false); // Set initial visibility to false
        }
        else
        {
            SetVisibility(true); 
        }
        
    }

    public void SetHealth(float health)
    {
        SetVisibility(true); // Make the health bar visible if it's not already
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(float max)
    {
        slider.maxValue = max;
        slider.value = max;
        fill.color = gradient.Evaluate(1f);
    }

    private void SetVisibility(bool visible)
    {
        StopAllCoroutines(); // Stop any previous fade coroutine
        isVisible = visible;
        foreach (var graphic in GetComponentsInChildren<Graphic>())
        {
            graphic.enabled = visible; // Enable or disable all child graphics based on the visibility flag
        }

        if (visible && vis)
        {
            // If the health bar is becoming invisible, start a coroutine to fade it out
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        float duration = 2f; // Fade out duration in seconds
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            // Fade out the fill image alpha over time
            fill.color = new Color(fill.color.r, fill.color.g, fill.color.b, 1f - elapsedTime / duration);
            back.color = new Color(back.color.r, back.color.g, back.color.b, 1f - elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the fill image alpha to 0 and disable the health bar graphics
        fill.color = new Color(fill.color.r, fill.color.g, fill.color.b, 0f);
        back.color = new Color(back.color.r, back.color.g, back.color.b, 0f);
        foreach (var graphic in GetComponentsInChildren<Graphic>())
        {
            graphic.enabled = false;
        }
        isVisible = false;
    }

}