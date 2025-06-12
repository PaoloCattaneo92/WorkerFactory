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
            //async work    
            Thread.Sleep(wait);
        })
        .With<IndeterminatePbarWorker.MessageBoxHandler>() //show exception messages in MessageBox
        .OnComplete((a, b) =>
        {
            //on complete (GUI) work
            MessageBox.Show("Done!");
        })
        .Start();
}
~~~
