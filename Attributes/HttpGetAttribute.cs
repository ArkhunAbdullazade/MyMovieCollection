using MyMovieCollection.Attributes.Http.Base;

namespace MyMovieCollection.Attributes.Http;

public class HttpGetAttribute : HttpAttribute
{
    public HttpGetAttribute(string routing) : base(HttpMethod.Get, routing) { }

    public HttpGetAttribute() : base(HttpMethod.Get, null) { }
}
