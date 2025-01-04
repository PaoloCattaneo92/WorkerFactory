using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace WorkerFactory;

public class IndeterminatePbarWorker : BackgroundWorker
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }

    public class MessageBoxHandler : IErrorHandler
    {
        public void HandleError(Exception ex)
        {
            var message = ex.Message;
#if DEBUG
            message += ex.StackTrace;
#endif
            MessageBox.Show(message);
        }
    }

    private readonly ProgressBar pbar;
    private readonly UIElement[] disabledElements;
    private IErrorHandler? errorHandler;

    public IndeterminatePbarWorker(ProgressBar pbar, params UIElement[] disabledElements)
    {
        this.pbar = pbar;
        if (disabledElements == null || !disabledElements.Any())
        {
            this.disabledElements = Array.Empty<UIElement>();
        }
        else
        {
            this.disabledElements = disabledElements;
        }
        this.RunWorkerCompleted += (a, b) =>
        {
            if (b.Error != null)
            {
                errorHandler?.HandleError(b.Error);
            }

            pbar.Visibility = Visibility.Hidden;
            foreach (var element in this.disabledElements)
            {
                element.IsEnabled = true;
            }
        };
    }

    public IndeterminatePbarWorker With<E>() where E : IErrorHandler, new()
    {
        errorHandler = new E();
        return this;
    }

    public IndeterminatePbarWorker Do(DoWorkEventHandler doWork)
    {
        this.DoWork += doWork;
        return this;
    }

    public IndeterminatePbarWorker OnComplete(RunWorkerCompletedEventHandler onComplete)
    {
        this.RunWorkerCompleted += onComplete;
        return this;
    }

    public void Start()
    {
        foreach (var element in disabledElements)
        {
            element.IsEnabled = false;
        }

        pbar.Visibility = Visibility.Visible;
        this.RunWorkerAsync();
    }
}
