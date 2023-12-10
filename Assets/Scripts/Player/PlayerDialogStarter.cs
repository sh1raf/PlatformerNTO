using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogStarter : ActionObject
{
    [SerializeField] private NPCConversation conversation;

    private PlayerPCInput _input;
    private void Awake()
    {
        _input = GetComponent<PlayerPCInput>();
    }

    public override void Action()
    {
        ConversationManager.Instance.StartConversation(conversation);
        _input.DisableInput();
    }
}
