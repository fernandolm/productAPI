namespace ProductAPI.Jobs.Worker;

public static class WorkItemWorker
{
    public static async Task ProcessWorkItemAsync(string data, CancellationToken cancellationToken)
    {
        try
        {
            Console.WriteLine($"Processing data: {data}");
            await Task.Delay(10000, cancellationToken).ConfigureAwait(false);
            Console.WriteLine($"Successfully processed data: {data}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"Processing was canceled for data: {data}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing data: {data}. Exception: {ex.Message}");
        }
    }
}
