// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using AppKit;
using ReactiveUI;
using RxUISimpleTimer.Core.ViewModels;
using Splat;
using RxUISimpleTimer.Mac.Models;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Security.Cryptography.X509Certificates;

namespace RxUISimpleTimer.Mac.ViewControllers
{
    public partial class HistoryViewController : NSViewController, IViewFor<OperationViewModel>
    {
        public HistoryViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            HistoryTableView.Appearance = NSAppearance.GetAppearance(NSAppearance.NameAqua);

            VM = Locator.CurrentMutable.GetService<OperationViewModel>();
            var ds = new LapTimesDataSource(VM.LapTimes);
            HistoryTableView.DataSource = ds;
            HistoryTableView.Delegate = new LapTimesViewDelegate(ds, VM.GetFormattedElapsed);
            CountChangedSubscription = VM.LapTimes.CountChanged.Subscribe(_ => InvokeOnMainThread(HistoryTableView.ReloadData));
            ShowMsecSubscription = VM.WhenAnyValue(vm => vm.ShowMilliseconds).Subscribe(_ => InvokeOnMainThread(HistoryTableView.ReloadData));
        }

        private OperationViewModel VM;

        private IDisposable CountChangedSubscription;

        private IDisposable ShowMsecSubscription;

        protected override void Dispose(bool disposing)
        {
            CountChangedSubscription.Dispose();
            ShowMsecSubscription.Dispose();
            CountChangedSubscription = null;
            ShowMsecSubscription = null;
            base.Dispose(disposing);
        }

        #region IViewFor implementation

        OperationViewModel IViewFor<OperationViewModel>.ViewModel
        {
            get
            {
                return VM;
            }
            set
            {
                VM = value;
            }
        }

        #endregion

        #region IViewFor implementation

        object IViewFor.ViewModel
        {
            get
            {
                return VM;  
            }
            set
            {
                VM = (OperationViewModel)value;
            }
        }

        #endregion
    }
}
