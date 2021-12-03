using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EnqueueMessagesManager : MonoBehaviour {
    public TextMeshProUGUI enqueuedMessagesTxt;
    
    private List<ClientMessages> m_clientsMessages;
    private Queue<ClientMessages> m_enqueuedMessages;

    private void Awake() {

        m_clientsMessages = new List<ClientMessages> {
            new ClientMessages("Bob", DateTime.Now.Millisecond, "Move"), 
            new ClientMessages("Bob",  DateTime.Now.Millisecond, "Move"),
            new ClientMessages("Luiz", DateTime.Now.Millisecond, "Move"),
            new ClientMessages("Fábio", DateTime.Now.Millisecond, "Move"),
            new ClientMessages("Pedro", DateTime.Now.Millisecond, "Move"),
            new ClientMessages("Maicon", DateTime.Now.Millisecond, "Move"),
            new ClientMessages("Leandro", DateTime.Now.Millisecond, "Move"),
        };
        
        
        var messagesValidated = ValidateMessagesDuplicated(m_clientsMessages);
        m_enqueuedMessages = new Queue<ClientMessages>();
        foreach (var message in messagesValidated) {
            m_enqueuedMessages.Enqueue(message);
            enqueuedMessagesTxt.text += string.Format("queue message name: {0}, timeStamp: {1}, function: {2} \n\n",
                                                      message.UserId, message.TimeStamp, message.FunctionName);
                
        }
    }

    private List<ClientMessages> ValidateMessagesDuplicated(List<ClientMessages> clientMessages) {
        var enqueuedMessages = new List<ClientMessages>();

        foreach (var message in clientMessages) {
            var isDuplicate = false;
            
            foreach (var enqueuedMessage in enqueuedMessages) {
                if (IsMessageDuplicated(message, enqueuedMessage)) {
                    isDuplicate = true;
                    break;
                }
            }
            
            if (!isDuplicate) {
                enqueuedMessages.Add(message);
            }
        }
        return enqueuedMessages;
    }

    private bool IsMessageDuplicated(ClientMessages message, ClientMessages enqueuedMessage)  {
        if (message.TimeStamp > enqueuedMessage.TimeStamp) return false;
        if (message.UserId != enqueuedMessage.UserId) return false;
        
        return message.FunctionName == enqueuedMessage.FunctionName;
    }
}
