using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_asteroids;

public abstract class TextDrawer
{
    public static void DrawText(SpriteBatch spriteBatch, Text text, Vector2 position, Texture2D fontSpriteSheet, float scale=1)
    {
        List<TextItem> textItems = text.GetTextContent();

        int charactersPrinted = 0;
        foreach (TextItem textItem in textItems)
        {
            foreach (char character in textItem.text)
            {
                if (character != '\n')
                {
                    DrawCharacter(spriteBatch, character, textItem.color, fontSpriteSheet, new Vector2(position.X + charactersPrinted * scale * 9, position.Y), scale);
                    charactersPrinted++;
                }
                else
                {
                    position.Y = position.Y + 10 * scale;
                    charactersPrinted = 0;
                }
                
            }
        }

    }

    private static void DrawCharacter(SpriteBatch spriteBatch, char character, Color color, Texture2D fontSpriteSheet, Vector2 position, float scale)
    {
        Console.WriteLine("drawing chr");
        char[] availableCharacters = new char[] {
            '0','1','2','3','4','5','6','7','8','9',
            'a','b','c','d','e','f','g','h','i','j',
            'k','l','m','n','o','p','q','r','s','t',
            'u','v','w','x','y','z','!','?','.','"',
            ':','/','%','(',')','+','-','█'
        };

        if (!availableCharacters.Contains(character)) return;

        Vector2 spritePos = new Vector2(0, 0);
        int characterPos = Array.IndexOf(availableCharacters, character);

        spritePos.Y = characterPos * 9;

        Rectangle characterSprite = new Rectangle();
        characterSprite.X = (int)spritePos.X;
        characterSprite.Y = (int)spritePos.Y;

        characterSprite.Width = 8;
        characterSprite.Height = 10;

        spriteBatch.Draw(fontSpriteSheet, position, characterSprite, color, 0, new Vector2(0,0), scale, SpriteEffects.None, 0);


    }
}