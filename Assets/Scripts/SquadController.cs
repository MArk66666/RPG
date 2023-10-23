using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SquadController : MonoBehaviour
{
    private List<Entity> _squad = new List<Entity>();
    private Entity _selectedCharacter;

    private GameObject _interactionObject = null;
    private UnityAction _selectedAction;

    private CameraController _cameraController;
    private UserInterfaceManager _uiManager;

    public static SquadController Instance;

    private void Awake()
    {
        Instance = this;
        _cameraController = FindAnyObjectByType<CameraController>();
    }

    private void Start()
    {
        _uiManager = UserInterfaceManager.Instance;
    }

    private void Update()
    {
        CheckPlayerInput();
        TryInteractWithObject();
    }

    private void TryInteractWithObject()
    {
        if (_interactionObject != null && _selectedAction != null)
        {
            float distance = GetDistanceToObject(_selectedCharacter.transform, _interactionObject.transform);
            if (distance <= 2f)
            {
                InteractWithObject();
            }
            else
            {
                MoveCharacter(_selectedCharacter, _interactionObject.transform.position);
            }
        }
    }

    private void InteractWithObject()
    {
        if (_selectedAction != null && _interactionObject != null)
        {
            _selectedAction.Invoke();
            ResetInteractionValues();
            Debug.Log("Interaction completed!");
        }
    }

    private void CheckPlayerInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_selectedCharacter == null || _cameraController == null) return;

            if (InteractiveObjectDetected()) return;

            ResetInteractionValues();

            Vector3 clickPosition = _cameraController.GetMouseWorldPosition();
            MoveCharacter(_selectedCharacter, clickPosition);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_cameraController == null) return;

            GameObject hoveredObject = _cameraController.GetHoveredObject();
            if (hoveredObject != null && hoveredObject.GetComponent<Entity>())
            {
                Entity character = hoveredObject.GetComponent<Entity>();
                SelectCharacter(character);
            }
        }
    }

    private bool InteractiveObjectDetected()
    {
        GameObject hoveredObject = _cameraController.GetHoveredObject();

        IPickable pickable = hoveredObject.GetComponent<IPickable>();
        IInteractable interactable = hoveredObject.GetComponent<IInteractable>();
        IDamageable damageable = hoveredObject.GetComponent<IDamageable>();
        ITalkable talkable = hoveredObject.GetComponent<ITalkable>();

        if (hoveredObject == _selectedCharacter.gameObject) return false;

        ResetInteractionValues();

        bool interactive = pickable != null || interactable != null || damageable != null || talkable != null;

        if (interactive)
        {
            _interactionObject = hoveredObject;
            _uiManager.InitializeActionPanel(hoveredObject.gameObject);
            return true;
        }

        return false;
    }

    private void MoveCharacter(Entity character, Vector3 destination)
    {
        if (character is IMoveable moveableCharacter)
        {
            moveableCharacter.Move(destination);
        } 
        else
        {
            Debug.Log(character.name + " cant move beacuse of IMovable interface absence!");
        }
    }

    private void ResetInteractionValues()
    {
        if (_interactionObject != null)
        {
            _interactionObject = null;
        }

        if (_selectedAction != null)
        {
            _selectedAction = null;
        }

        _uiManager.DeactivateActionsPanel();
    }

    private float GetDistanceToObject(Transform origin, Transform target)
    {
        return Vector3.Distance(origin.position, target.position);
    }

    public void SelectCharacter(Entity entity)
    {
        if (entity is ISelectable selectableEntity)
        {
            _selectedCharacter = selectableEntity.GetCharacter();
        }
    }

    public EntityInventory ItemPresentInSquadInventory(Item item)
    {
        List<Entity> squad = GetSquadMembers();
        squad.Add(_selectedCharacter);

        foreach (Entity character in squad)
        {
            if (character.Inventory.HasItem(item))
            {
                return character.Inventory;
            }
        }
        return null;
    }

    public Entity GetCurrentSelectedCharacter()
    {
        return _selectedCharacter;
    }

    public List<Entity> GetSquadMembers()
    {
        return new List<Entity>(_squad);
    }

    public void RememberAction(UnityAction action)
    {
        _selectedAction = action;
        Debug.Log("Action memorized!");
    }
}