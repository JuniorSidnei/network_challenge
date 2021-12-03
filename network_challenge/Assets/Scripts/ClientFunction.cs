using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientFunction {
    public string UserOrigin;
    public DateTime TimeWasCalled;
    public string FunctionName;

    public ClientFunction(string userOrigin, DateTime timeWasCalled, string functionName) {
        UserOrigin = userOrigin;
        TimeWasCalled = timeWasCalled;
        FunctionName = functionName;
    }
}
