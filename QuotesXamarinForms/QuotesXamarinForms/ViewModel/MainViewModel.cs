using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using QuotesXamarinForms.Interfaces;
using QuotesXamarinForms.Model;

namespace QuotesXamarinForms.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDialogService dialogService;
        private readonly IWebApiProvider webApiProvider;
        private bool _isBusy;
        private ICommand getQuotesCommand;

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDialogService dialogService, IWebApiProvider webApi)
        {
            this.dialogService = dialogService;
            this.webApiProvider = webApi;
            QuotesList = new ObservableCollection<Quote>();
            if (IsInDesignMode)
            {
                QuotesList.Add(new Quote
                {
                    Author = "Dummy Author",
                    QuoteType = "Dummy Type",
                    QuoteText = "Dummy QuoteText"
                });
            }
        }

        public ObservableCollection<Quote> QuotesList { get; set; }

        public ICommand GetQuotesCommand
            => getQuotesCommand ?? (getQuotesCommand = new RelayCommand(async () => await this.GetQuotesCommandExecute()));

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        private async Task GetQuotesCommandExecute()
        {
            try
            {
                IsBusy = true;
                var list = await this.webApiProvider.GetQuotesAsync();
                UpdateQuotesList(list);
            }
            catch (Exception e)
            {
                await dialogService.ShowMessage($"WebApiError: {e.Message}", "Error");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void UpdateQuotesList(IEnumerable<Quote> list)
        {
            foreach (var quote in list)
            {
                QuotesList.Add(quote);
            }
        }
    }
}