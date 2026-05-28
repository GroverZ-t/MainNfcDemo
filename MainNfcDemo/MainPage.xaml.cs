using NfcReaderLib;
using NfcReaderLib.Interfaces;

namespace MainNfcDemo
{
    public partial class MainPage : ContentPage
    {
        private readonly INfcReader _reader;

        public MainPage()
        {
            InitializeComponent();

            _reader = NfcReaderFactory.Create();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!_reader.IsAvailable)
            {
                UidLabel.Text = "NFC недоступен";
                return;
            }

            _reader.OnTagRead += ReaderOnTagRead;

            _reader.StartReading();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _reader.OnTagRead -= ReaderOnTagRead;

            _reader.StopReading();
        }

        private void ReaderOnTagRead(String uid)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                UidLabel.Text = $"UID: {uid}";
            });
        }
    }
}