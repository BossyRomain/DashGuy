using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    private string nextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(GoNextLevel());
        }
    }

    IEnumerator GoNextLevel()
    {
        yield return new WaitForSeconds(1.0f);

        if (nextLevel.Equals(""))
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }

        yield return null;
    }
}
