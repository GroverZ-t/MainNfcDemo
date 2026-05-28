#if ANDROID

using Plugin.NFC;
using NfcReaderLib.Interfaces;

namespace NfcReaderLib.Services;

public class AndroidNfcReader : INfcReader
{
    public event Action<String>? OnTagRead;

    public Boolean IsAvailable =>
        CrossNFC.IsSupported &&
        CrossNFC.Current.IsAvailable;

    public async void StartReading()
    {
        CrossNFC.Current.OnTagDiscovered += CurrentOnTagDiscovered;

        CrossNFC.Current.OnMessageReceived += CurrentOnTagReceived;

        await Task.Delay(500);

        CrossNFC.Current.StartListening();
    }

    public void StopReading()
    {
        CrossNFC.Current.OnTagDiscovered -= CurrentOnTagDiscovered;

        CrossNFC.Current.OnMessageReceived -= CurrentOnTagReceived;

        CrossNFC.Current.StopListening();
    }

    private void CurrentOnTagDiscovered(ITagInfo tagInfo, Boolean format)
    {
        ProcessTag(tagInfo);
    }
    private void CurrentOnTagReceived(ITagInfo tagInfo)
    {
        ProcessTag(tagInfo);
    }

    private void ProcessTag(ITagInfo tagInfo)
    {
        if (tagInfo?.Identifier == null)
        {
            return;
        }

        String uid =
            BitConverter
            .ToString(tagInfo.Identifier)
            .Replace("-","");

        OnTagRead?.Invoke(uid);
    }
}

#endif