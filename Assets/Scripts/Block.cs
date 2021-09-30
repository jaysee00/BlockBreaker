using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Unity Editor Config
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private GameObject blockSparklesVFX;
    [SerializeField] private Sprite[] hitSprites;

    // Cached references
    private Level level;
    

    // State
    [SerializeField] private int hits = 0; // TODO: Only serialized for debugging purposes

    private void Start()
    {
        this.level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.OnCreateBlock();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            hits++;
            int maxHits = hitSprites.Length + 1;
            if (hits >= maxHits)
            {
                DestroyBlock();
            }
            else
            {
                DamageBlock();
            }
        }
    }

    private void DamageBlock()
    {
        int spriteIndex = hits - 1;
        if (spriteIndex > hitSprites.Length - 1)
        {
            spriteIndex = hitSprites.Length;
        }

        Sprite newSprite = hitSprites[spriteIndex];
        if (newSprite == null)
        {
            Debug.LogError(string.Format($"Missing Sprite at index {spriteIndex} for Game Object {this.name}"));
        }
        GetComponent<SpriteRenderer>().sprite = newSprite;
        
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position);
        Destroy(this.gameObject);
        TriggerSparklesVFX();
        level.OnBreakBlock();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = GameObject.Instantiate<GameObject>(blockSparklesVFX, this.transform.position, this.transform.rotation);
        Destroy(sparkles, 1f);
    }
}
