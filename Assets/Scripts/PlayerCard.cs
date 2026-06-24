using TMPro;
using UnityEngine;

public class PlayerCard : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private CanvasGroup canvasGroup;

    public void SetPlayer(PlayerInfo playerInfo)
    {
        playerNameText.text = playerInfo.Name;
    }

    public void SetActive(bool active)
    {
        canvasGroup.alpha = active ? 1 : 0;
        canvasGroup.blocksRaycasts = active;
    }
}