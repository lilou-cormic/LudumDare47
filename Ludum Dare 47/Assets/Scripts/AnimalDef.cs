using UnityEngine;

public class AnimalDef
{
    public string Name { get; }

    public Sprite Sprite { get; }

    public Season Season { get; set; }

    public AnimalDef(string name, Sprite sprite)
    {
        Name = name;
        Sprite = sprite;
    }
}
