using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace alevel_asteroids;


public class Text
{
    private List<TextItem> textContent = new List<TextItem>();

    public Text(string contents)
    {
        TextItem textToAdd = new TextItem();
        textToAdd.color = Color.White;
        textToAdd.text = contents;
        textContent.Add(textToAdd);
    }

    public Text(string contents, Color stringColor)
    {
        TextItem textToAdd = new TextItem();
        textToAdd.color = stringColor;
        textToAdd.text = contents;
        textContent.Add(textToAdd);
    }

    public void Add(string contents)
    {
        TextItem textToAdd = new TextItem();
        textToAdd.color = Color.White;
        textToAdd.text = contents;
        textContent.Add(textToAdd);
    }

    public void Add(string contents, Color stringColor)
    {
        TextItem textToAdd = new TextItem();
        textToAdd.color = stringColor;
        textToAdd.text = contents;
        textContent.Add(textToAdd);
    }

    public List<TextItem> GetTextContent() => textContent;
}