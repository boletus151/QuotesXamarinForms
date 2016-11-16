using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace QuotesXamarinForms.Implementations
{
    public class DialogService : IDialogService
    {
        public DialogService()
        {
            this.dialogPage = new ContentPage();
        }

        private Page dialogPage;

        public async Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            await dialogPage.DisplayAlert(title, message, buttonText);

            afterHideCallback?.Invoke();
        }

        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            await dialogPage.DisplayAlert(title, error.Message, buttonText);

            afterHideCallback?.Invoke();
        }

        public async Task ShowMessage(string message, string title)
        {
            await dialogPage.DisplayAlert(title, message, "OK");
        }

        public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            await dialogPage.DisplayAlert(title, message, buttonText);

            afterHideCallback?.Invoke();
        }

        public async Task<bool> ShowMessage
            (string message, string title, string buttonConfirmText, string buttonCancelText,
                Action<bool> afterHideCallback)
        {
            var result = await dialogPage.DisplayAlert(title, message, buttonConfirmText, buttonCancelText);

            afterHideCallback?.Invoke(result);

            return result;
        }

        public async Task ShowMessageBox(string message, string title)
        {
            await dialogPage.DisplayAlert(title, message, "OK");
        }

        public void Initialize(Page dialog)
        {
            dialogPage = dialog;
        }
    }
}