using System;
using UnityEngine;
/// <summary>Contains commonly used functions, methods, etc.
/// <para>This is a static class. </para>
/// <seealso cref="Settings"/>
/// </summary>
public static class Common
{
    public static int BoolToInt(bool boolean)
    {
        if(boolean) return 1;
        else return 0;
    }
    public static bool IntToBool(int integer)
    {
        if(integer == 0) return false;
        else return true;
    }
    /// <summary>
    /// Trims silence from both ends in an AudioClip.
    /// Makes mp3 files seamlessly loopable.
    /// Taken from https://answers.unity.com/questions/343057/how-do-i-make-unity-seamlessly-loop-my-background.html
    /// </summary>
    /// <param name="inputAudio"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public static AudioClip TrimSilence(AudioClip inputAudio, float threshold = 0.05f)
    {
        // Copy samples from input audio to an array. AudioClip uses interleaved format so the length in samples is multiplied by channel count
        float[] samplesOriginal = new float[inputAudio.samples * inputAudio.channels];
        inputAudio.GetData(samplesOriginal, 0);
        // Find first and last sample (from any channel) that exceed the threshold
        int audioStart = Array.FindIndex(samplesOriginal, sample => sample > threshold),
            audioEnd = Array.FindLastIndex(samplesOriginal, sample => sample > threshold);
        // Copy trimmed audio data into another array
        float[] samplesTrimmed = new float[audioEnd - audioStart];
        Array.Copy(samplesOriginal, audioStart, samplesTrimmed, 0, samplesTrimmed.Length);
        // Create new AudioClip for trimmed audio data
        AudioClip trimmedAudio = AudioClip.Create(inputAudio.name, samplesTrimmed.Length / inputAudio.channels, inputAudio.channels, inputAudio.frequency, false);
        trimmedAudio.SetData(samplesTrimmed, 0);
        return trimmedAudio;
    }
}
