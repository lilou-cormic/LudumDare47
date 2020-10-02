// From http://www.huwyss.com/level-it/localization-textmanager-for-unity3d

using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;


public class TextManager : MonoBehaviour
{
    private static readonly IDictionary<string, string> TextTable = new Dictionary<string, string>();
    private static TextAsset _language;

    private static TextManager _instance;

    void Awake()
    {
        if (_instance == null)
        {
            // If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
            SetDefaultLanguage();
        }
        else
        {
            // If a Singleton already exists and you find
            // another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    public static void SetDefaultLanguage()
    {
        string language = PlayerPrefs.GetString("Language");

        if (string.IsNullOrWhiteSpace(language))
        {
            var osLanguage = Application.systemLanguage;
            language = osLanguage.ToString();

            PlayerPrefs.SetString("Language", language);
        }

        SetLanguage(language);
    }

    public static void SetLanguage(string langFilename)
    {
        //HACK
        langFilename = "English";

        // Load a asset by its AssetName.
        // The text file must be located within 'Resources' subfolder in Unity ('Assets/Resources' in Visual Studio).
        TextAsset newLanguage = Resources.Load(@"Languages/" + langFilename) as TextAsset;
        if (newLanguage == null)
            newLanguage = Resources.Load<TextAsset>(@"Languages/English");

        LoadLanguage(newLanguage);
        _language = newLanguage;
    }

    private static void LoadLanguage(TextAsset asset)
    {
        TextTable.Clear();

        var lineNumber = 1;
        using (var reader = new StringReader(asset.text))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Ignore comments and empty lines
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("'"))
                    continue;

                int firstSpaceIndex = line.IndexOf('=');
                if (firstSpaceIndex > 0)
                {
                    var key = line.Substring(0, firstSpaceIndex);
                    var val = line.Substring(firstSpaceIndex + 1);

                    if (key != null && val != null)
                    {
                        var loweredKey = key.ToLower();
                        if (TextTable.ContainsKey(loweredKey))
                        {
                            Debug.LogWarning(String.Format("Duplicate key '{1}' in language file '{0}' detected.", asset.text, key));
                        }
                        TextTable.Add(loweredKey, val);
                    }
                }
                else
                {
                    Debug.LogWarning(String.Format("Format error in language file '{0}' on line {1}. Each line must be of format: key = value", asset.text, lineNumber));
                }

                lineNumber++;
            }
        }
    }

    public static string GetText(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return key;

        string result = "";
        var loweredKey = key.ToLower();

        if (TextTable.ContainsKey(loweredKey))
        {
            result = TextTable[loweredKey];
        }
        else
        {
            Debug.LogWarning(String.Format("Couldn't find key '{0}' in dictionary.", key));
            result = key;
        }

        return result;
    }
}
