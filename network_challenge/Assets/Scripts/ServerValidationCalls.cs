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
            new ClientMessages("Brazil", DateTime.Now, "Move"), 
            new ClientMessages("Brazil", DateTime.Now, "Move"),
            new ClientMessages("Brazil", DateTime.Now, "Move"),
            new ClientMessages("Brazil", DateTime.Now, "Move"),
            new ClientMessages("Brazil", DateTime.Now, "Move"),
            new ClientMessages("Brazil", DateTime.Now, "Move"),
            new ClientMessages("Brazil", DateTime.Now, "Move"),
        };
        
        
        var listToProcess = EnqueueFunctionCalls(m_clientsInfos);
        foreach (var user in listToProcess)
        {
            Debug.Log("functions to process, origin: " + user.UserId + ", function: " + user.FunctionName);    
        }
        
    }

    private Queue<ClientMessages> EnqueueFunctionCalls(List<ClientMessages> clientMessage) {
        var listId = new List<string>();
        var listFunction = new List<string>();
        var updatedCollection = new List<ClientMessages>();

        
        foreach (var message in clientMessage) {
            if (listId.Contains(message.UserId)) {
                var idx = GetAllIndexWithKey(listId, message.UserId);
                if (!IsDuplicate(listFunction, idx, message.FunctionName)) {
                    continue;
                }
            }
        
            updatedCollection.Add(message);
            listId.Add(message.UserId);
            listFunction.Add(message.FunctionName);
        }

        var queue = new Queue<ClientMessages>();
        foreach (var user in updatedCollection) {
            queue.Enqueue(user);
        }
        return queue;
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
