using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemFocusedController : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public TextMeshProUGUI text;
    public float speed;
    Vector3 _defaultPosition;
    private Vector3 _newPosition;
    public HUDController hud;
    private ItemController _item;
    private State _state = State.Idle;

    private enum State
    {
        Raise,
        Lower,
        Idle
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_state == State.Raise) SetState(State.Lower);
    }

    // Start is called before the first frame update
    void Start()
    {
        _defaultPosition = transform.position;
        _newPosition = new Vector3(600, 350, 0);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.Raise:
            {
                if (transform.position != _newPosition)
                    transform.position = Vector3.MoveTowards(transform.position, _newPosition, speed);
                else SetState(State.Idle);
                break;
            }
            case State.Lower:
            {
                if (transform.position != _defaultPosition) transform.position = Vector3.MoveTowards(transform.position, _defaultPosition, speed);
                else SetState(State.Idle);
                break;
            }
        }
    }

    private void SetState(State state)
    {
        _state = state;
    }

    public void LoadItem(ItemController item)
    {
        _item = item;
        image.sprite = _item.imageFocused ? _item.imageFocused : _item.GetComponent<Image>().sprite;
        if (name == "Questlog")
        {
            text.text = WorldState.World.GetJournalEntries();
        }
        SetState(State.Raise);
    }

    public void Close()
    {
        SetState(State.Lower);
    }
}
