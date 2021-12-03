using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientMessages {
    public string UserId;
    public DateTime TimeStamp;
    public string FunctionName;

    public ClientMessages(string userId, DateTime timeStamp, string functionName) {
        UserId = userId;
        TimeStamp = timeStamp;
        FunctionName = functionName;
    }
}
