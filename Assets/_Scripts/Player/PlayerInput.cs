using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class PlayerInput : MonoBehaviour, IDragHandler, IPointerClickHandler
{

    public int RainbowCost = 300;
    public int BombCost = 600;

    public TextMeshProUGUI text;
    [Inject]
    private Player player;
    [Inject(Id = "PlayerCubeAnchor")]
    private Transform playerCubeAnchor;
    [Inject]
    private GameSettings settings;

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = playerCubeAnchor.position;

        // Mouse.x = 1080 . Position.x => 1.55(board)
        // Mouse.x = 0 . Position.x => -1.55(board)
        // Mouse.x = 540 . Position.x => 0
        // Mouse.x = 800 . Position.x => ~1
        // Mouse.x = 200 . Position.x => ~ -1


        float halfOfScreen = Screen.width / 2;//540
        float rate = (Input.mousePosition.x - halfOfScreen) / Screen.width;// -1/2 <=> 1/2

        newPosition.x = rate * settings.Sensitivity;//sensitivity ~ 4

        newPosition.x = Mathf.Clamp(newPosition.x, -settings.CubeBoard, settings.CubeBoard);

        playerCubeAnchor.position = newPosition;
        player.SetCubePosition(newPosition);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        player.TryShoot();
    }

    public void ScoreSubtrackRainbow(int cube)
    {
        if (player.CurrentScore.Value >= RainbowCost) 
        { 
            player.CurrentScore.Value -= RainbowCost;
            player.SwitchCubeTo((ECube)cube);
        }else
        {
            text.gameObject.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, .5f, false);
            text.gameObject.SetActive(false);
        }
    }
    public void ScoreSubtrackBomb(int cube)
    {
        if (player.CurrentScore.Value >= BombCost)
        {
            player.CurrentScore.Value -= BombCost;
            player.SwitchCubeTo((ECube)cube);
        }
    }
}
