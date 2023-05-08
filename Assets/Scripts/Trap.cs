using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Trap : MonoBehaviour
{
    [SerializeField]
    private Sprite on, off;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = off;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Un joueur!");
            StartCoroutine(Activation(collision.gameObject));
        }
    }

    IEnumerator Activation(GameObject player)
    {
        player.SendMessage("Death");
        GetComponent<SpriteRenderer>().sprite = on;

        yield return new WaitForSeconds(2.0f);

        GetComponent<SpriteRenderer>().sprite = off;

        yield return null;
    }
}
