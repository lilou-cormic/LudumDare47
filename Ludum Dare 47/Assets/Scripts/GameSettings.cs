using PurpleCable;
using System;
using UnityEngine;

public class GameSettings : Singleton<GameSettings>
{
    public static event Action PlayerCharacterChanged;

    public static string PlayerCharacter
    {
        get => PlayerPrefs.GetString("PlayerCharacter", "MaleAdventurer");

        set
        {
            PlayerPrefs.SetString("PlayerCharacter", value);
            PlayerCharacterChanged?.Invoke();
        }
    }
}
