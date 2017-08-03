// -------------------------------------------------------------------------------------------------------------------
// <copyright file="StartViewModel.cs" company="CodigoEdulis">
//    Código Edulis 2017
//    http://www.codigoedulis.es
//  </copyright>
//  <summary>
//     This implementation is a group of the offers of several persons along the network;
//     because of this, it is under Creative Common By License:
//     
//     You are free to:
// 
//     Share — copy and redistribute the material in any medium or format
//     Adapt — remix, transform, and build upon the material for any purpose, even commercially.
//     
//     The licensor cannot revoke these freedoms as long as you follow the license terms.
//     
//     Under the following terms:
//     
//     Attribution — You must give appropriate credit, provide a link to the license, and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
//     No additional restrictions — You may not apply legal terms or technological measures that legally restrict others from doing anything the license permits.
//  
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace QuotesXamarinForms.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;
    using Interfaces;
    using Model;

    public class StartViewModelObservable : ViewModelBase, IStartViewModel
    {
        private readonly IDialogService dialogService;
        private readonly IWebApiProviderObservable webApiProvider;
        private bool _isBusy;
        private ICommand getQuotesCommand;

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public StartViewModelObservable(IDialogService dialogService, IWebApiProviderObservable webApi)
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
                var list = this.webApiProvider.GetQuotesAsync();
                list.Subscribe(this.UpdateQuotesList);
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
            this.QuotesList.Clear();
            foreach (var quote in list)
            {
                this.QuotesList.Add(quote);
            }
        }
    }
}