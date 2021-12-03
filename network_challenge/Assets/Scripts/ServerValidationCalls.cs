using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ServerValidationCalls : MonoBehaviour {
    private List<ClientMessages> m_clientsInfos;
    private Queue m_queueCalls;

    private void Awake() {

        m_clientsInfos = new List<ClientMessages> {
            new ClientMessages("Bob", DateTime.Now.Millisecond, "Move"), 
            new ClientMessages("Bob",  DateTime.Now.Millisecond + 0.1f, "Move"),
            new ClientMessages("Luiz", DateTime.Now.Millisecond, "Move"),
            new ClientMessages("Fábio", DateTime.Now.Millisecond, "Move"),
            new ClientMessages("Pedro", DateTime.Now.Millisecond, "Move"),
            new ClientMessages("Maicon", DateTime.Now.Millisecond, "Move"),
            new ClientMessages("Leandro", DateTime.Now.Millisecond, "Move"),
        };
        
        
        var messagesValidated = ValidateMessagesDuplicated(m_clientsInfos);
        var queue = new Queue<ClientMessages>();
        foreach (var message in messagesValidated) {
            queue.Enqueue(message);
            Debug.Log("functions to process, origin: " + message.UserId + ", function: " + message.FunctionName);    
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
        
        // foreach (var message in clientMessage) {
        //     if (listId.Contains(message.UserId)) {
        //         var idx = GetAllIndexWithKey(listId, message.UserId);
        //         if (!IsDuplicate(listFunction, idx, message.FunctionName)) {
        //             continue;
        //         }
        //     }
        //
        //     validatedMessages.Add(message);
        //     listId.Add(message.UserId);
        //     listFunction.Add(message.FunctionName);
        // }
        //
        // var queue = new Queue<ClientMessages>();
        // foreach (var user in validatedMessages) {
        //     queue.Enqueue(user);
        // }
        // return queue;
    }

    private bool IsMessageDuplicated(ClientMessages message, ClientMessages enqueuedMessage)  {
        if (message.TimeStamp > enqueuedMessage.TimeStamp) return false;
        if (message.UserId != enqueuedMessage.UserId) return false;
        
        return message.FunctionName == enqueuedMessage.FunctionName;
    }
    
    private List<int> GetAllIndexWithKey(List<string> listOrigin, string key) {
        var allIdx = new List<int>();
        
        for (var i = 0; i < listOrigin.Count; i++) {
            if (listOrigin[i] == key) {
                allIdx.Add(i);
            }
        }

        return allIdx;
    }

    private bool IsDuplicate(List<string> listFunction, List<int> idx, string functionName)
    {
        foreach (var userFunction in listFunction)  {
            foreach (var t1 in idx)  {
                var function = listFunction[t1];
                if (function == functionName) {
                    return false;
                }
            }
        }

        return true;
    }
}
