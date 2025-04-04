using System;

namespace ObjectRef;

public class Topic
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Session> Sessions { get; set; }
}
