using UnityEngine;
using UnityEngine.UI;

public class ListController : MonoBehaviour
{
    [SerializeField] private PlayerCard otherPlayerCardPrefab;
    [SerializeField] private PlayerCard thisPlayerCardPrefab;
    [SerializeField] private Transform root;
    [SerializeField] private VerticalLayoutGroup layoutGroup;
    
    [SerializeField] private PlayerCard thisPlayerCardTop;
    [SerializeField] private PlayerCard thisPlayerCardBottom;
    
    [SerializeField] private RectTransform contentRt;

    [SerializeField] private int playerCount = 200;
    [SerializeField] private int thisPlayerIndex = 100;
    
    private PlayerCard _thisPlayerCard;
    private RectTransform _thisPlayerCardRt;
    private RectTransform _thisPlayerCardTopRt;
    private RectTransform _thisPlayerCardBottomRt;

    private CardPlace? _cardPlace;

    private void Awake()
    {
        layoutGroup.enabled = false;
        
        _thisPlayerCardTopRt = thisPlayerCardTop.GetComponent<RectTransform>();
        _thisPlayerCardBottomRt = thisPlayerCardBottom.GetComponent<RectTransform>();
        
        thisPlayerCardTop.SetActive(false);
        thisPlayerCardBottom.SetActive(false);
        
        for (var i = 0; i < playerCount; i++)
        {
            var playerName = $"Player {i}";
            var playerInfo = new PlayerInfo(playerName);
            
            var prefab = i == thisPlayerIndex ? thisPlayerCardPrefab : otherPlayerCardPrefab;
            var playerCard = Instantiate(prefab, root).GetComponent<PlayerCard>();
            playerCard.name = $"{playerName} Card";
            playerCard.SetPlayer(playerInfo);

            if (i == thisPlayerIndex)
            {
                thisPlayerCardTop.SetPlayer(playerInfo);
                thisPlayerCardBottom.SetPlayer(playerInfo);

                _thisPlayerCard = playerCard;
                _thisPlayerCardRt = playerCard.GetComponent<RectTransform>();
            }
        }
        
        layoutGroup.enabled = true;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(root as RectTransform);
        Canvas.ForceUpdateCanvases();
    }

    private void Update()
    {
        var thisY = _thisPlayerCardRt.position.y;
        CardPlace cardPlace;

        if (thisY > _thisPlayerCardTopRt.position.y)
            cardPlace = CardPlace.Top;
        else if (thisY < _thisPlayerCardBottomRt.position.y)
            cardPlace = CardPlace.Bottom;
        else
            cardPlace = CardPlace.Middle;

        if (_cardPlace == null || cardPlace != _cardPlace)
        {
            _cardPlace = cardPlace;
            UpdatePlace(cardPlace);
        }
        UpdatePlace(cardPlace);
    }

    private void UpdatePlace(CardPlace place)
    {
        _thisPlayerCard.SetActive(place == CardPlace.Middle);
        thisPlayerCardTop.SetActive(place == CardPlace.Top);
        thisPlayerCardBottom.SetActive(place == CardPlace.Bottom);
    }
}