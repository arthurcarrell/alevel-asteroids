using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

    public int GetPixelLength(int scale=1) {
        int totalTextAmount = 0;
        foreach (TextItem textItem in textContent) {
            totalTextAmount += textItem.text.Count();
        }

        int textLength = (totalTextAmount*9*scale);

        return textLength;
    }

    public List<TextItem> GetTextContent() => textContent;
}