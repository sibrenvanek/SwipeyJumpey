using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DarthFader : MonoBehaviour
{
    public static DarthFader Instance = null;

    private Image image = null;
    private Coroutine activeWaitTillFadedCoroutine = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        image = GetComponentInChildren<Image>();
    }

    public void FadeGameIn(float time = 1f)
    {
        if (activeWaitTillFadedCoroutine != null)
            StopCoroutine(activeWaitTillFadedCoroutine);

        image.DOKill();
        image.DOFade(0, time);
        activeWaitTillFadedCoroutine = StartCoroutine(WaitTillFaded(time));
    }

    public void FadeGameInInSeconds(float waitTime = 0f, float time = 1f)
    {
        StartCoroutine(WaitAndFadeIn(waitTime, time));
    }

    private IEnumerator WaitAndFadeIn(float waitTime = 0f, float time = 1f)
    {
        yield return new WaitForSeconds(waitTime);
        FadeGameIn(time);
    }

    public void FadeGameOut(float time = 1f)
    {
        image.DOKill();
        image.DOFade(1, time);
    }

    private IEnumerator WaitTillFaded(float waitTime = 0f)
    {
        yield return new WaitForSeconds(waitTime);
        activeWaitTillFadedCoroutine = null;
    }

    /*
    |:::::::::::::;;::::::::::::::::::|
    |:::::::::::'~||~~~``:::::::::::::|
    |::::::::'   .':     o`:::::::::::|
    |:::::::' oo | |o  o    ::::::::::|
    |::::::: 8  .'.'    8 o  :::::::::|
    |::::::: 8  | |     8    :::::::::|
    |::::::: _._| |_,...8    :::::::::|
    |::::::'~--.   .--. `.   `::::::::|
    |:::::'     =8     ~  \ o ::::::::|
    |::::'       8._ 88.   \ o::::::::|
    |:::'   __. ,.ooo~~.    \ o`::::::|
    |:::   . -. 88`78o/:     \  `:::::|
    |::'     /. o o \ ::      \88`::::|   "He will join us or die."
    |:;     o|| 8 8 |d.        `8 `:::|
    |:.       - ^ ^ -'           `-`::|
    |::.                          .:::|
    |:::::.....           ::'     ``::|
    |::::::::-'`-        88          `|
    |:::::-'.          -       ::     |
    |:-~. . .                   :     |
    | .. .   ..:   o:8      88o       |
    |. .     :::   8:P     d888. . .  |
    |.   .   :88   88      888'  . .  |
    |   o8  d88P . 88   ' d88P   ..   |
    |  88P  888   d8P   ' 888         |
    |   8  d88P.'d:8  .- dP~ o8       |   Darth Vader (1)
    |      888   888    d~ o888    LS |
    |_________________________________|
   */
}
