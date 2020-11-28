using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeTravel<T> where T : struct
{
    /// <summary>
    /// Defines how often a state is actually saved - "save a state every x frames"
    /// </summary>
    private const int StateCaptureInterval = 5;
    /// <summary>
    /// Defines how many frames can be travelled back at most and thus how many are saved in the buffer.
    /// </summary>
    private const int CapturedStateCountLimit = 20 * 60 / StateCaptureInterval;
    
    private int framesSinceLastCapture = 0;

    private readonly List<T?> stateBuffer = Enumerable.Repeat<T?>(null, CapturedStateCountLimit).ToList();
    private int stateBufferHeadIdx = -1;
    private int stateBufferBacklogSize = 0;

    public bool IsTravellingBack { private set; get; } = false;
    private int remainingFramesForTravellingBack = 0;
    

    public void CaptureState(T newState)
    {
        if (IsTravellingBack) {
            Debug.LogWarning("TimeTravel CaptureStateForFrame - trying to capture a frame but travelling in time");
            return;
        }

        if (framesSinceLastCapture < StateCaptureInterval) {
            framesSinceLastCapture++;
            // continue to wait...
            return;
        }
        
        framesSinceLastCapture = 0;

        stateBufferHeadIdx++;
        if (stateBufferHeadIdx >= CapturedStateCountLimit)
        {
            stateBufferHeadIdx = 0;
        }
        
        stateBuffer[stateBufferHeadIdx] = newState;

        if (stateBufferBacklogSize < CapturedStateCountLimit)
        {
            stateBufferBacklogSize++;
            // else stateBacklogSize does not get increased since we are over-writing data now
        }

        //PrintDebugInfo("CaptureStateForFrame");
    }

    public void StartToTravelBack(int framesDuration) {
        remainingFramesForTravellingBack = Math.Min(framesDuration, stateBufferBacklogSize);
        stateBufferBacklogSize = 0;
        IsTravellingBack = true;
    }

    public T? GetNextPastFrame() {
        if (!IsTravellingBack) {
            Debug.LogWarning("TimeTravel GetNextPastFrame - trying to get a frame but not travelling in time");
            return null;
        }

        PrintDebugInfo("GetNextPastFrame");

        var result = stateBuffer[stateBufferHeadIdx];
        stateBuffer[stateBufferHeadIdx] = null;
        
        stateBufferHeadIdx--;
        if (stateBufferHeadIdx < 0) {
            stateBufferHeadIdx = CapturedStateCountLimit - 1;
        }

        remainingFramesForTravellingBack--;
        if (remainingFramesForTravellingBack == 0) {
            IsTravellingBack = false;
        }

        return result;
    }

    private void PrintDebugInfo(string prefix)
    {
        var bufferAsStr = string.Join(" ; ",
            stateBuffer
                .Select(d => d.HasValue ? d.ToString() : "NULL")
                .ToArray()
        );
        var headValue = stateBuffer[stateBufferHeadIdx];
        var headValueStr = headValue.HasValue ? headValue.Value.ToString() : "NULL";
        Debug.Log(
            $"current time travel after {prefix}: " +
            $"head={headValueStr}@{stateBufferHeadIdx} backlogSize={stateBufferBacklogSize} " +
            $"isTravelling={IsTravellingBack} remaining={remainingFramesForTravellingBack} " +
            $"buffer={bufferAsStr}"
        );
    }
    
}
