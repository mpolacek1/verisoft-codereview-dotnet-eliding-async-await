using System.Diagnostics;

const string url = "https://www.verisoft.cz";
Uri uri = new(url);

var response = await GetResponseFromUrlAsync_WithAsyncAwait(uri);
WriteResponseDetailsToConsole(response);

try
{
    response = await GetResponseFromUrlAsync_WithoutAsyncAwait(uri);
    WriteResponseDetailsToConsole(response);
}
catch (Exception ex)
{
    WriteExceptionDetailsToConsole(ex);
}

uri = null;

try
{
    var responseTask = GetResponseFromUrlAsync_WithAsyncAwait(uri);
    response = await responseTask;
}
catch (Exception ex)
{
    WriteExceptionDetailsToConsole(ex);
}

try
{
    var responseTask = GetResponseFromUrlAsync_WithoutAsyncAwait(uri);
    response = await responseTask;
}
catch (Exception ex)
{
    WriteExceptionDetailsToConsole(ex);
}

await GetResponseFromUrlAsync_WithTryCatchAndAsyncAwait();

await GetResponseMessage_WithTryCatchAndWithoutAsyncAwait();

Task.Run(async () =>
{
    var result = await GetResponseFromUrlAsync_WithAsyncAwait(new Uri(url));
    WriteResponseDetailsToConsole(result);
});

Console.ReadKey();

await ValueTaskExample();

Console.ReadKey();

static async Task<HttpResponseMessage> GetResponseFromUrlAsync_WithTryCatchAndAsyncAwait()
{
    try
    {
        return await GetResponseFromUrlAsync_WithAsyncAwait(null);
    }
    catch (Exception ex)
    {
        WriteExceptionDetailsToConsole(ex);

        return null;
    }
}

static Task<HttpResponseMessage> GetResponseMessage_WithTryCatchAndWithoutAsyncAwait()
{
    try
    {
        return GetResponseFromUrlAsync_WithAsyncAwait(null);
    }
    catch (Exception ex)
    {
        WriteExceptionDetailsToConsole(ex);

        return Task.FromResult<HttpResponseMessage>(null);
    }
}

static async Task<HttpResponseMessage> GetResponseFromUrlAsync_WithAsyncAwait(Uri uri)
{
    ArgumentNullException.ThrowIfNull(uri);

    using HttpClient client = new();
    return await client.GetAsync(uri);
}

static Task<HttpResponseMessage> GetResponseFromUrlAsync_WithoutAsyncAwait(Uri uri)
{
    ArgumentNullException.ThrowIfNull(uri);

    using HttpClient client = new();
    return client.GetAsync(uri);
}

static void WriteResponseDetailsToConsole(HttpResponseMessage response)
{
    ArgumentNullException.ThrowIfNull(response);

    Console.WriteLine(response.StatusCode);
    Console.WriteLine(response.Content);
    Console.WriteLine();
}

static void WriteExceptionDetailsToConsole(Exception exception)
{
    ArgumentNullException.ThrowIfNull(exception);

    Console.WriteLine(exception.Message);
    Console.WriteLine(exception.StackTrace);
    Console.WriteLine();
}

static async Task ValueTaskExample()
{
    TimeSpan elapsedExecutionTime = new();
    string[] values = [];
    for (int i = 0; i < 10; i++)
    {
        var stopwatch = Stopwatch.StartNew();
        values = await GetValuesAsync(values);
        stopwatch.Stop();
        elapsedExecutionTime += stopwatch.Elapsed;
    }

    Console.WriteLine(elapsedExecutionTime);
}

static async ValueTask<string[]> GetValuesAsync(string[] values)
{
    ArgumentNullException.ThrowIfNull(values);

    if (values.Length > 0)
    {
        return values;
    }

    return await GetValuesFromSomewhereAsync();
}

static Task<string[]> GetValuesFromSomewhereAsync()
    => Task.FromResult<string[]>(["insert", "some", "dumb", "text", "here"]);