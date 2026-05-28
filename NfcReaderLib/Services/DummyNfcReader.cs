using NfcReaderLib.Interfaces;

namespace NfcReaderLib.Services;

public class DummyNfcReader : INfcReader
{
    public event Action<String>? OnTagRead;

    public Boolean IsAvailable => false;

    public void StartReading()
    {
        Console.WriteLine("NFC unavailable");
    }

    public void StopReading() { }
}