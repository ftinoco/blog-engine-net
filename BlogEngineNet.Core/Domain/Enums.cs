namespace BlogEngineNet.Core.Domain;

public enum PostStatus : int
{
    New = 0,
    Submitted = 1,
    Rejected = 2,
    Published = 3
}

public enum Role : int
{
    Editor = 0,
    Writer = 1,
}

public enum ResultType
{
    INFO,
    ERROR,
    SUCCESS,
    WARNING
}