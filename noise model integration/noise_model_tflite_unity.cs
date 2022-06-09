using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TensorFlowLite;
using UnityEngine;
using UnityEngine.UI;

public class noise : MonoBehaviour
{
    private Interpreter interpreter;
    private float[] outputs;

    void Awake()
}
    void Start ()
    {
        var options = new Interpreter.Options()
        {
            threads = 2,
        };
        interpreter = new Interpreter(model.bytes, options);
        int inputCount = interpreter.GetInputTensorCount();
    int outputCount = interpreter.GetOutputTensorCount();
    for (int i = 0; i < inputCount; i++)
    {
      Debug.LogFormat("Input {0}: {1}", i, interpreter.GetInputTensorInfo(i));
    }
    for (int i = 0; i < inputCount; i++)
    {
      Debug.LogFormat("Output {0}: {1}", i, interpreter.GetOutputTensorInfo(i));
    }
    }

  void Update ()
  {
    if (inputs == null)
    {
      return;
    }

    if (outputs == null || outputs.Length != inputs.Length)
    {
      interpreter.ResizeInputTensor(0, new int[]{inputs.Length});
      interpreter.AllocateTensors();
      outputs = new float[inputs.Length];
    }

    float startTimeSeconds = Time.realtimeSinceStartup;
    interpreter.SetInputTensorData(0, inputs);
    interpreter.Invoke();
    interpreter.GetOutputTensorData(0, outputs);
    float inferenceTimeSeconds = Time.realtimeSinceStartup - startTimeSeconds;
  }

  void OnDestroy()
  {
    interpreter.Dispose();
  }

   private static string ArrayToString(float[] values)
   {
    return string.Join(",", values.Select(x => x.ToString()).ToArray());
  }