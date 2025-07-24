using UnityEngine;
using System.Collections.Generic;

public class SpikeTrapActivator : MonoBehaviour
{
    public List<GameObject> spikeObjects;  // Các object chông (Spike1, Spike2)
    public Transform player;               // Gán Player vào đây trong Inspector
    public float activationDistance = 0.2f;  // Khoảng cách để hiện trap

    private bool spikesShown = false;
    private AudioManagerTan audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerTan>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.dieClip, 0.1f);
        }
    }
    void Update()
    {
        if (!spikesShown && player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= activationDistance)
            {
                ShowSpikes();
                spikesShown = true;
            }
        }
    }

    void ShowSpikes()
    {
        foreach (var spike in spikeObjects)
        {
            var sprite = spike.GetComponent<SpriteRenderer>();
            if (sprite != null)
                sprite.enabled = true;

            var col = spike.GetComponent<Collider2D>();
            if (col != null)
                col.enabled = true;
        }
    }
}
