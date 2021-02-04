using System;
using System.Collections.Generic;
using System.Text;

namespace CollabAssist.Incoming
{
    public class PullRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public PullRequestStatus Status { get; set; }
        public DateTime CreatedDate { get; set; } 
        


        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }

        public string ProjectName { get; set; }
        public string RepositoryName { get; set; }
        
        public List<Reviewer> Reviewers { get; set; } = new List<Reviewer>();
    }
}
