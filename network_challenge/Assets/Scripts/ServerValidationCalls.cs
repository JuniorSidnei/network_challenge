using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ServerValidationCalls : MonoBehaviour {
    private List<ClientFunction> m_clientsInfos;
    private Queue m_queueCalls;

    private void Awake() {

        m_clientsInfos = new List<ClientFunction> {
            new ClientFunction("Brazil", DateTime.Now, "Move"), 
            new ClientFunction("Brazil", DateTime.Now, "Move"),
            new ClientFunction("Brazil", DateTime.Now, "Move"),
            new ClientFunction("Brazil", DateTime.Now, "Move"),
            new ClientFunction("Brazil", DateTime.Now, "Move"),
            new ClientFunction("Brazil", DateTime.Now, "Move"),
            new ClientFunction("Brazil", DateTime.Now, "Move"),
        };
        
        
        var listToProcess = EnqueueFunctionCalls(m_clientsInfos);
        foreach (var user in listToProcess)
        {
            Debug.Log("functions to process, origin: " + user.UserOrigin + ", function: " + user.FunctionName);    
        }
        
    }

    private Queue<ClientFunction> EnqueueFunctionCalls(List<ClientFunction> usersInfo) {
        var listOrigin = new List<string>();
        var listFunction = new List<string>();
        var updatedCollection = new List<ClientFunction>();

        
        foreach (var user in usersInfo) {
            if (listOrigin.Contains(user.UserOrigin)) {
                var idx = GetAllIndexWithKey(listOrigin, user.UserOrigin);
                if (user.UserOrigin == "Portugal") {
                    var bla = 0;
                }
                if (!IsFunctionValid(listFunction, idx, user.FunctionName)) {
                    continue;
                }
            }
        
            updatedCollection.Add(user);
            listOrigin.Add(user.UserOrigin);
            listFunction.Add(user.FunctionName);
        }

        var queue = new Queue<ClientFunction>();
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

    private bool IsFunctionValid(List<string> listFunction, List<int> idx, string functionName)
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
