using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour, IActionable
{
    public Action<string, bool> BigText;

    public static bool isDead;

    [SerializeField]
    private float time = 3;

    public void TriggerAction(Transform transformToFocus, bool value)
    {
        if(value && !isDead)
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        isDead = true;

        BigText?.Invoke("Game Over", true);
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("MainMenu");
    }
}
