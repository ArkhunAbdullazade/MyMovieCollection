using MyMovieCollection.Attributes.Http.Base;

namespace MyMovieCollection.Attributes.Http;

public class HttpPutAttribute : HttpAttribute
{
    public HttpPutAttribute(string routing) : base(HttpMethod.Put, routing) { }

    public HttpPutAttribute() : base(HttpMethod.Put, null) { }
}
