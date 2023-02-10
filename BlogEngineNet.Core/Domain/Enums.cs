namespace BlogEngineNet.Core.Domain;

public enum PostStatus : int
{
    Published = 3,
    PendingApproval = 0,
    Approved = 1,
    Rejected = 2,
}

public enum Role : int
{
    Editor = 0,
    Writer = 1,
}
