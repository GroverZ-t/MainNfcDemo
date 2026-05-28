namespace NfcReaderLib.Interfaces;

public interface INfcReader
{
    event Action<String> OnTagRead;

    void StartReading();

    void StopReading();

    Boolean IsAvailable { get; }
}