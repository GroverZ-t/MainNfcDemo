using NfcReaderLib.Interfaces;
using NfcReaderLib.Services;

namespace NfcReaderLib;

public static class NfcReaderFactory
{
    public static INfcReader Create()
    {
#if ANDROID
        return new AndroidNfcReader();
#else
        return new DummyNfcReader();
#endif
    }
}