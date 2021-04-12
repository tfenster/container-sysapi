using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

var app = WebApplication.Create(args);

app.MapGet("/", async http =>
{
    await http.Response.WriteAsync("Hello World, v2!");
});

app.MapGet("/headers", async http =>
{
    var sb = new StringBuilder("Headers: \r\n");
    foreach (var header in http.Request.Headers)
    {
        sb.Append($"{header.Key} = {header.Value}\r\n");
    }
    await http.Response.WriteAsync(sb.ToString());
});

app.MapGet("/url", async http =>
{
    await http.Response.WriteAsync(UriHelper.GetDisplayUrl(http.Request));
});

await app.RunAsync();