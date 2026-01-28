using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card Data")]
public class CardData : ScriptableObject
{
    [Header("Identity")]
    public string cardName;

    [Header("Display")]
    [TextArea(2, 5)]
    public string cardText;
    public Sprite cardImage;
}
