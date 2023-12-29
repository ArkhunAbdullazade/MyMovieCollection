using MyMovieCollection.Attributes.Http.Base;

namespace MyMovieCollection.Attributes.Http;

public class HttpPostAttribute : HttpAttribute
{
    public HttpPostAttribute(string routing) : base(HttpMethod.Post, routing) { }

    public HttpPostAttribute() : base(HttpMethod.Post, null) { }
}
