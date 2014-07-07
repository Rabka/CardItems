using UnityEngine;
using System.Collections;

public interface ICard 
{
	CardType getType();
	Texture2D getTexture();
}
