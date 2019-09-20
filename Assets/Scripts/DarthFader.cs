using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DarthFader : MonoBehaviour
{
    private Image image = null;

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void FadeGameIn(float time = 1f)
    {
        image.DOFade(0, time);
    }

    public void FadeGameOut(float time = 1f)
    {
        image.DOFade(1, time);
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
