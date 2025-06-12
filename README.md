# WorkerFactory
WPF Worker Factory

# Example
~~~csharp
private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            var wait = int.Parse(tbWait.Text);
            new IndeterminatePbarWorker(pbar, btnStart, tbWait)
                .Do((a, b) =>
                {
                    Thread.Sleep(wait);
                })
                .OnComplete((a, b) =>
                {
                    MessageBox.Show("Done!");
                })
                .Start();
        }
~~~
